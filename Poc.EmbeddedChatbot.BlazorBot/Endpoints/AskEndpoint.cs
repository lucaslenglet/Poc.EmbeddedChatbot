using FastEndpoints;
using Poc.EmbeddedChatbot.BlazorBot.Services.Ai;
using Poc.EmbeddedChatbot.BlazorBot.Shared.Contracts;

namespace Poc.EmbeddedChatbot.BlazorBot.Endpoints;

public class AskEndpoint : Endpoint<AskRequest, AskResponse>
{
    public override void Configure()
    {
        AllowAnonymous();
        Post("/ask");
    }

    public override async Task HandleAsync(AskRequest req, CancellationToken ct)
    {
        var aiService = Resolve<PerplexityAiService>();

        var response = await aiService.AskAsync(req.Prompts, ct);

        await SendAsync(new AskResponse { Response = response }, cancellation: ct);
    }
}
