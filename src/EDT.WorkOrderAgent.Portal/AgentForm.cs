using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel;
using EDT.Agent.Shared.Configurations;
using EDT.Agent.Shared.Handlers;
using EDT.WorkOrderAgent.Service;
using EDT.Agent.Shared.FunctionCallers;

namespace EDT.WorkOrderAgent.Portal;

public partial class AgentForm : Form
{
    private Kernel _kernel = null;
    private OpenAIPromptExecutionSettings _settings = null;
    private ChatHistory _chatHistory = null;
    private IWorkOrderService _workOrderService = null;

    public AgentForm()
    {
        InitializeComponent();
    }

    private void AgentForm_Load(object sender, EventArgs e)
    {
        var configuration = new ConfigurationBuilder().AddJsonFile($"appsettings.Qwen.json");
        var config = configuration.Build();
        var openAiConfiguration = new OpenAiConfiguration(
            config.GetSection("LLM_API_PROVIDER").Value,
            config.GetSection("LLM_API_MODEL").Value,
            config.GetSection("LLM_API_BASE_URL").Value,
            config.GetSection("LLM_API_KEY").Value);
        var openAiClient = new HttpClient(new OpenAiHttpHandler(openAiConfiguration.Provider, openAiConfiguration.EndPoint));
        _kernel = Kernel.CreateBuilder()
            .AddOpenAIChatCompletion(openAiConfiguration.ModelId, openAiConfiguration.ApiKey, httpClient: openAiClient)
            .Build();

        _chatHistory = new ChatHistory();
        _workOrderService = new WorkOrderService();
    }

    /// <summary>
    /// Import Custom Plugin/Functions to Kernel
    /// </summary>
    private void cbxUseFunctionCalling_CheckedChanged(object sender, EventArgs e)
    {
        if (cbxUseFunctionCalling.Checked)
        {
            _kernel.Plugins.Add(KernelPluginFactory.CreateFromFunctions("WorkOrderHelper",
                new List<KernelFunction>
                {
                    _kernel.CreateFunctionFromMethod((string orderName) =>
                    {
                        return _workOrderService.GetWorkOrderInfo(orderName);
                    }, "GetWorkOrderDetails", "获取某个工单的详细内容"),
                    _kernel.CreateFunctionFromMethod((string orderName, int newQuantity) =>
                    {
                        return _workOrderService.ReduceWorkOrderQuantity(orderName, newQuantity);
                    }, "ReduceWorkOrderQuantity", "减少某个工单的生产数量"),
                    _kernel.CreateFunctionFromMethod((string orderName, string newStatus) =>
                    {
                        return _workOrderService.UpdateWorkOrderStatus(orderName, newStatus);
                    }, "UpdateWorkOrderStatus", "更新某个工单的状态")
                }
            ));

            _settings = new OpenAIPromptExecutionSettings
            {
                ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
            };

            lblTitle.Text = "WorkOrder Agent";
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
        tbxResponse.Clear();

        if (cbxUseFunctionCalling.Checked && cbxUseFunctionPlanner.Checked)
        {
            Task.Run(() =>
            {
                ShowProcessMessage("AI is handling your request now...");
                var planner = new UniversalFunctionCaller(_kernel);
                var chatResponse = planner.RunAsync(tbxPrompt.Text)
                    .GetAwaiter()
                    .GetResult();
                UpdateResponseContent(chatResponse ?? string.Empty);
                ShowProcessMessage("AI Response:");
            });
        }
        else if (cbxUseFunctionCalling.Checked && !cbxUseFunctionPlanner.Checked)
        {
            Task.Run(() =>
            {
                ShowProcessMessage("AI is handling your request now...");
                var _chatCompletion = _kernel.GetRequiredService<IChatCompletionService>();
                var chatResponse = _chatCompletion.GetChatMessageContentAsync(_chatHistory, _settings, _kernel)
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
                var _chatCompletion = _kernel.GetRequiredService<IChatCompletionService>();
                var chatResponse = _chatCompletion.GetChatMessageContentAsync(_chatHistory, null, _kernel)
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
        chatResponse = chatResponse.Replace(@"\n", Environment.NewLine);

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
}
