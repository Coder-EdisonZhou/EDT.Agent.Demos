using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel;
using EDT.WorkOrderAgent.Shared.Configurations;
using EDT.WorkOrderAgent.Shared.Handlers;
using EDT.WorkOrderAgent.Service;

namespace EDT.WorkOrderAgent.Portal;

public partial class AgentForm : Form
{
    private Kernel _kernel = null;
    private OpenAIPromptExecutionSettings _settings = null;
    private IChatCompletionService _chatCompletion = null;
    private ChatHistory _chatHistory = null;

    public AgentForm()
    {
        InitializeComponent();
    }

    private void AgentForm_Load(object sender, EventArgs e)
    {
        var configuration = new ConfigurationBuilder().AddJsonFile($"appsettings.json");
        var config = configuration.Build();
        var openAiConfiguration = new OpenAiConfiguration(
            config.GetSection("LLM_API_PROVIDER").Value,
            config.GetSection("LLM_API_MODEL").Value,
            config.GetSection("LLM_API_BASE_URL").Value,
            config.GetSection("LLM_API_KEY").Value);
        var openAiClient = new HttpClient(new CustomLlmApiHandler(openAiConfiguration.Provider, openAiConfiguration.EndPoint));
        _kernel = Kernel.CreateBuilder()
            .AddOpenAIChatCompletion(openAiConfiguration.ModelId, openAiConfiguration.ApiKey, httpClient: openAiClient)
            .Build();

        _chatCompletion = _kernel.GetRequiredService<IChatCompletionService>();

        _chatHistory = new ChatHistory();
        _chatHistory.AddSystemMessage("You are one WorkOrder Assistant.");
    }

    /// <summary>
    /// Import Custom Plugin/Functions to Kernel
    /// </summary>
    private void cbxUseFunctionCalling_CheckedChanged(object sender, EventArgs e)
    {
        if (cbxUseFunctionCalling.Checked)
        {
            _kernel.Plugins.Add(KernelPluginFactory.CreateFromFunctions("WorkOrderHelperPlugin",
                new List<KernelFunction>
                {
                    _kernel.CreateFunctionFromMethod((string orderName) =>
                    {
                        var workOrderRepository = new WorkOrderService();
                        return workOrderRepository.GetWorkOrderInfo(orderName);
                    }, "GetWorkOrderInfo", "Get WorkOrder's Detail Information"),
                    _kernel.CreateFunctionFromMethod((string orderName, int newQuantity) =>
                    {
                        var workOrderRepository = new WorkOrderService();
                        return workOrderRepository.ReduceWorkOrderQuantity(orderName, newQuantity);
                    }, "ReduceWorkOrderQuantity", "Reduce WorkOrder's Quantity to new Quantity"),
                    _kernel.CreateFunctionFromMethod((string orderName, string newStatus) =>
                    {
                        var workOrderRepository = new WorkOrderService();
                        return workOrderRepository.UpdateWorkOrderStatus(orderName, newStatus);
                    }, "UpdateWorkOrderStatus", "Update WorkOrder's Status to new Status")
                }
            ));

            _settings = new OpenAIPromptExecutionSettings
            {
                ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
            };

            lblTitle.Text = "WorkOrder Agent";
        }
        else
        {
            _kernel.Plugins.Clear();
            _settings = null;

            lblTitle.Text = "AI Chatbot";
        }
    }

    /// <summary>
    /// Send the prompt to AI Model and get the response
    /// </summary>
    private void btnSendPrompt_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(tbxPrompt.Text))
        {
            MessageBox.Show("Please input a prompt before sending!", "Warning");
            return;
        }

        _chatHistory.AddUserMessage(tbxPrompt.Text);
        ChatMessageContent chatResponse = null;
        tbxResponse.Clear();

        if (cbxUseFunctionCalling.Checked)
        {
            Task.Run(() =>
            {
                ShowProcessMessage("AI is handling your request now...");
                chatResponse = _chatCompletion.GetChatMessageContentAsync(_chatHistory, _settings, _kernel)
                    .GetAwaiter()
                    .GetResult();
                UpdateResponseContent(chatResponse.ToString());
                ShowProcessMessage("AI Response:");
            });
        }
        else
        {
            Task.Run(() =>
            {
                ShowProcessMessage("AI is handling your request now...");
                chatResponse = _chatCompletion.GetChatMessageContentAsync(_chatHistory, null, _kernel)
                    .GetAwaiter()
                    .GetResult();
                UpdateResponseContent(chatResponse.ToString());
                ShowProcessMessage("AI Response:");
            });
        }
    }

    #region Refresh Control Value Methods
    private void UpdateResponseContent(string chatResponse)
    {
        if (tbxResponse.InvokeRequired)
        {
            tbxResponse.Invoke(() =>
            {
                tbxResponse.Clear();
                tbxResponse.Text = chatResponse;
                _chatHistory.AddAssistantMessage(chatResponse.ToString());
            });
        }
        else
        {
            tbxResponse.Clear();
            tbxResponse.Text = chatResponse;
            _chatHistory.AddAssistantMessage(chatResponse.ToString());
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
}
