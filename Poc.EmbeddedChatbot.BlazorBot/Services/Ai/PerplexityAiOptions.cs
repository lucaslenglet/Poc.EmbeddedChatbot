namespace Poc.EmbeddedChatbot.BlazorBot.Services.Ai;

public class PerplexityAiOptions
{
    public static string SectionName => "PerplexityAi";

    public string BaseAddress { get; set; } = default!;
    public string ApiKey { get; set; } = default!;
    public string Model { get; set; } = default!;
    public string[] SystemPrompts { get; set; } = [];
}
