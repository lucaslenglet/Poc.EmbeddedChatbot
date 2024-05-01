namespace Poc.EmbeddedChatbot.BlazorBot.ChatBot;

public abstract record ChatMessageType(string Value);

public record BotMessageType(string Value) : ChatMessageType(Value);
public record UserMessageType(string Value) : ChatMessageType(Value);