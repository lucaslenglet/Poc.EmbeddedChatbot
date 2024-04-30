namespace Poc.EmbeddedChatbot.BlazorBot.Services.Ai;

public class Prompt
{
    public required EPromptRole Role { get; init; }
    public required string Content { get; init; }
}

public enum EPromptRole
{
    User,
    Assistant
}
