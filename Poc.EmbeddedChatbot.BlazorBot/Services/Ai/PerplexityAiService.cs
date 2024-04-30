using Microsoft.Extensions.Options;
using Poc.EmbeddedChatbot.BlazorBot.Shared.Models;

namespace Poc.EmbeddedChatbot.BlazorBot.Services.Ai;

public class PerplexityAiService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<PerplexityAiService> _logger;
    private readonly IOptionsSnapshot<PerplexityAiOptions> _options;

    public PerplexityAiService(
        HttpClient httpClient,
        ILogger<PerplexityAiService> logger,
        IOptionsSnapshot<PerplexityAiOptions> options)
    {
        _httpClient = httpClient;
        _logger = logger;
        _options = options;
    }

    public async Task<string> AskAsync(IEnumerable<Prompt> prompts, CancellationToken cancellationToken = default)
    {
        var options = _options.Value;

        var request = new ChatCompletionsRequest
        {
            model = options.Model,
            messages = [
                .. options.SystemPrompts.Select(sp => new ChatCompletionsMessageRequest() { role = "system", content = sp }),
                .. prompts.Select(p => new ChatCompletionsMessageRequest()
                {
                    role = p.Role.ToString().ToLower(),
                    content = p.Content
                }),
            ]
        };

        using var response = await _httpClient.PostAsJsonAsync(PerplexityAiRoutes.ChatCompletions, request, cancellationToken);

        try
        {
            response.EnsureSuccessStatusCode();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Une erreur est survenue.");
            var raw = await response.Content.ReadAsStringAsync(cancellationToken);
            _logger.LogError("Raison : {Raison}", raw);
            throw;
        }

        var body = await response.Content.ReadFromJsonAsync<BodyResponse>(cancellationToken);

        if (body?.usage is not null)
        {
            _logger.LogInformation("Usage: {Usage}", System.Text.Json.JsonSerializer.Serialize(body?.usage));
        }

        return body?.choices?.FirstOrDefault()?.message?.content ?? "ERROR";
    }

    private class ChatCompletionsRequest
    {
        public string model { get; set; }
        public ChatCompletionsMessageRequest[] messages { get; set; }
    }

    private class ChatCompletionsMessageRequest
    {
        public string? role { get; set; }
        public string? content { get; set; }
    }

    private class BodyResponse
    {
        public string? id { get; set; }
        public string? model { get; set; }
        public string? @object { get; set; }
        public int? created { get; set; }
        public BodyChoiseResponse[]? choices { get; set; }
        public BodyUsageResponse? usage { get; set; }
    }

    private class BodyChoiseResponse
    {
        public int? index { get; set; }
        public BodyChoiseMessageResponse? message { get; set; }
    }

    private class BodyChoiseMessageResponse
    {
        public string? content { get; set; }
        public string? role { get; set; }
    }

    private class BodyUsageResponse
    {
        public int? prompt_tokens { get; set; }
        public int? completion_tokens { get; set; }
        public int? total_tokens { get; set; }
    }
}