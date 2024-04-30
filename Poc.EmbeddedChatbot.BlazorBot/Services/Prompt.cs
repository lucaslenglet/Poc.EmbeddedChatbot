namespace Poc.EmbeddedChatbot.BlazorBot.Services;

public class Prompt
{
    public EPromptRole Role { get; set; }
    public string Content { get; set; }
}

public enum EPromptRole
{
    User,
    Assistant
}
