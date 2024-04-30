using Poc.EmbeddedChatbot.BlazorBot.Components;
using Poc.EmbeddedChatbot.BlazorBot.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHttpClient<PerplexityAiService>(configure =>
{
    configure.BaseAddress = new Uri("https://api.perplexity.ai/");
    configure.DefaultRequestHeaders.Add("Accept", "application/json");
    configure.DefaultRequestHeaders.Add("authorization", builder.Configuration.GetValue<string>("PerplexityAiApiKey"));
});

builder.Services.AddMemoryCache();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
