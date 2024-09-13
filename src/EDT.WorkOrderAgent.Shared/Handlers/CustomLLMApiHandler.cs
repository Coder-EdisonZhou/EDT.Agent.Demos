using EDT.WorkOrderAgent.Shared.Constants;

namespace EDT.WorkOrderAgent.Shared.Handlers;

public class CustomLlmApiHandler : HttpClientHandler
{
    private readonly string _openAiProvider;
    private readonly string _openAiBaseAddress;

    public CustomLlmApiHandler(string openAiProvider, string openAiBaseAddress)
    {
        _openAiProvider = openAiProvider;
        _openAiBaseAddress = openAiBaseAddress;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        UriBuilder uriBuilder;
        var uri = new Uri(_openAiBaseAddress);
        switch (request.RequestUri?.LocalPath)
        {
            case "/v1/chat/completions":
                switch (_openAiProvider)
                {
                    case ConfigConstants.LLMApiProviders.ZhiPuAI:
                        uriBuilder = new UriBuilder(request.RequestUri)
                        {
                            Scheme = "https",
                            Host = uri.Host,
                            Path = ConfigConstants.LLMApiPaths.ZhiPuAIChatCompletions,
                        };
                        request.RequestUri = uriBuilder.Uri;
                        break;
                    default: // Default: OpenAI-Compatible API Providers
                        uriBuilder = new UriBuilder(request.RequestUri)
                        {
                            Scheme = "https",
                            Host = uri.Host,
                            Path = ConfigConstants.LLMApiPaths.OpenAIChatCompletions,
                        };
                        request.RequestUri = uriBuilder.Uri;
                        break;
                }
                break;
        }

        var response = await base.SendAsync(request, cancellationToken);
        return response;
    }
}
