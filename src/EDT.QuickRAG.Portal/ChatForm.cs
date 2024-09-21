using EDT.Agent.Shared.Configurations;
using EDT.Agent.Shared.Handlers;
using EDT.Agent.Shared.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel.Connectors.Sqlite;
using Microsoft.SemanticKernel.Memory;
using Microsoft.SemanticKernel.Text;

namespace EDT.QuickRAG.Portal;

/// <summary>
/// Need to add the below code to disable warnings about SemanticKernel.Connectors.Sqlite
/// </summary>
#pragma warning disable SKEXP0050
#pragma warning disable SKEXP0001
#pragma warning disable SKEXP0020
#pragma warning disable SKEXP0010
public partial class ChatForm : Form
{
    private Kernel _kernel = null;
    private ChatHistory _chatHistory = null;
    private OpenAiConfiguration _embeddingApiConfiguration = null;
    private ISemanticTextMemory _textMemory = null;
    private int _textChunkerLinesToken;
    private int _textChunkerParagraphsToken;

    public ChatForm()
    {
        InitializeComponent();
    }

    private void AgentForm_Load(object sender, EventArgs e)
    {
        var configuration = new ConfigurationBuilder().AddJsonFile($"appsettings.Local.json");
        var config = configuration.Build();
        var chattingApiConfiguration = new OpenAiConfiguration(
            config.GetSection("Chatting:API_PROVIDER").Value,
            config.GetSection("Chatting:API_MODEL").Value,
            config.GetSection("Chatting:API_BASE_URL").Value,
            config.GetSection("Chatting:API_KEY").Value);
        var chattingApiHandler = new OpenAiHttpHandler(chattingApiConfiguration.Provider, chattingApiConfiguration.EndPoint);
        var openAiChattingClient = new HttpClient(chattingApiHandler);
        _kernel = Kernel.CreateBuilder()
            .AddOpenAIChatCompletion(chattingApiConfiguration.ModelId, chattingApiConfiguration.ApiKey, httpClient: openAiChattingClient)
            .Build();

        _embeddingApiConfiguration = new OpenAiConfiguration(
            config.GetSection("Embedding:API_PROVIDER").Value,
            config.GetSection("Embedding:API_MODEL").Value,
            config.GetSection("Embedding:API_BASE_URL").Value,
            config.GetSection("Embedding:API_KEY").Value);
        _textChunkerLinesToken = Convert.ToInt32(config["TextChunker:LinesToken"]);
        _textChunkerParagraphsToken = Convert.ToInt32(config["TextChunker:ParagraphsToken"]);

        _chatHistory = new ChatHistory();
    }

    /// <summary>
    /// Save the embedding content to remote embedding database
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void btnEmbedding_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(tbxPrompt.Text))
        {
            MessageBox.Show("Please input a text content before sending!", "Warning");
            return;
        }

        var query = new QueryModel(tbxIndex.Text, tbxPrompt.Text);
        _textMemory = this.GetTextMemory().GetAwaiter().GetResult();
        var lines = TextChunker.SplitPlainTextLines(query.Text, _textChunkerLinesToken);
        var paragraphs = TextChunker.SplitPlainTextParagraphs(lines, _textChunkerParagraphsToken);

        foreach (var para in paragraphs)
        {
            Task.Run(() =>
            {
                ShowProcessMessage("AI is embedding your content now...");
                _textMemory.SaveInformationAsync(
                    query.Index,
                    id: Guid.NewGuid().ToString(),
                    text: para)
                .GetAwaiter()
                .GetResult();
                ShowProcessMessage("Embedding success!");
                MessageBox.Show("Embedding success!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            });
        }
    }

    /// <summary>
    /// Send the prompt to LLM and try to get response from RAG firstly
    /// </summary>
    private void btnGetRagResponse_Click(object sender, EventArgs e)
    {
        if (_textMemory == null)
            _textMemory = this.GetTextMemory().GetAwaiter().GetResult();

        var query = new QueryModel(tbxIndex.Text, tbxPrompt.Text);
        var memoryResults = _textMemory.SearchAsync(query.Index, query.Text, limit: 3, minRelevanceScore: 0.3);

        Task.Run(() =>
        {
            var existingKnowledge = this.BuildPromptInformation(memoryResults).GetAwaiter().GetResult();
            var integratedPrompt = @"
                               获取到的相关信息：[{0}]。
                               根据获取到的信息回答问题：[{1}]。
                               如果没有获取到相关信息，请直接回答 Sorry不知道。
                            ";
            ShowProcessMessage("AI is handling your request now...");
            var response = _kernel.InvokePromptAsync(string.Format(integratedPrompt, existingKnowledge, query.Text))
                .GetAwaiter()
                .GetResult();
            UpdateResponseContent(response.ToString());
            ShowProcessMessage("AI Response:");
        });
    }

    #region UI Control Refresh Helper Methods
    private void UpdateResponseContent(string chatResponse)
    {
        if (tbxResponse.InvokeRequired)
        {
            tbxResponse.Invoke(() =>
            {
                tbxResponse.Clear();
                tbxResponse.Text = chatResponse;
                _chatHistory.AddAssistantMessage(chatResponse);
            });
        }
        else
        {
            tbxResponse.Clear();
            tbxResponse.Text = chatResponse;
            _chatHistory.AddAssistantMessage(chatResponse);
        }
    }

    private void ShowProcessMessage(string message)
    {
        if (lblResponse.InvokeRequired)
        {
            lblResponse.Invoke(() =>
            {
                lblResponse.Text = message;
            });
        }
        else
        {
            lblResponse.Text = message;
        }
    }
    #endregion

    #region Embedding Helper Function Methods
    private async Task<ISemanticTextMemory> GetTextMemory()
    {
        var memoryBuilder = new MemoryBuilder();
        var embeddingApiClient = new HttpClient(new OpenAiHttpHandler(_embeddingApiConfiguration.Provider, _embeddingApiConfiguration.EndPoint));
        memoryBuilder.WithOpenAITextEmbeddingGeneration(
            _embeddingApiConfiguration.ModelId,
            _embeddingApiConfiguration.ApiKey,
            httpClient: embeddingApiClient);
        var memoryStore = await SqliteMemoryStore.ConnectAsync("memstore.db");
        memoryBuilder.WithMemoryStore(memoryStore);
        var textMemory = memoryBuilder.Build();

        return textMemory;
    }

    private async Task<string> BuildPromptInformation(IAsyncEnumerable<MemoryQueryResult> memoryResults)
    {
        var information = string.Empty;
        await foreach (MemoryQueryResult memoryResult in memoryResults)
        {
            information += memoryResult.Metadata.Text;
        }

        return information;
    }
    #endregion
}
