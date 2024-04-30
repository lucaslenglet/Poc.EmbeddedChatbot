using Microsoft.Extensions.Options;
using Poc.EmbeddedChatbot.BlazorBot.Services.Ai;

namespace Poc.EmbeddedChatbot.BlazorBot.Services;

public static class DependencyInjection
{
    public static IServiceCollection AddPocServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<PerplexityAiOptions>()
            .Bind(configuration.GetSection(PerplexityAiOptions.SectionName));

        services.AddHttpClient<PerplexityAiService>((sp, configure) =>
        {
            var options = sp.GetRequiredService<IOptions<PerplexityAiOptions>>().Value;

            configure.BaseAddress = new Uri(options.BaseAddress);
            configure.DefaultRequestHeaders.Add("Accept", "application/json");
            configure.DefaultRequestHeaders.Add("authorization", options.ApiKey);
        });

        return services;
    }
}