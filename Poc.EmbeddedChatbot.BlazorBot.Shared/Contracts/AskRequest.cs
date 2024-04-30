using Poc.EmbeddedChatbot.BlazorBot.Shared.Models;

namespace Poc.EmbeddedChatbot.BlazorBot.Shared.Contracts;

public class AskRequest
{
    public Prompt[] Prompts { get; set; } = [];
}