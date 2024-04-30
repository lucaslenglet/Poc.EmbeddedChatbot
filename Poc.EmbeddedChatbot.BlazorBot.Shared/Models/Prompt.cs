namespace Poc.EmbeddedChatbot.BlazorBot.Shared.Models;

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