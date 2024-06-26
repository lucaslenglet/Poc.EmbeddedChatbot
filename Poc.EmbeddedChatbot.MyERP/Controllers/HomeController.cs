using Microsoft.AspNetCore.Mvc;
using Poc.EmbeddedChatbot.MyERP.Models;
using System.Diagnostics;
using System.Web;

namespace Poc.EmbeddedChatbot.MyERP.Controllers;

public class HomeController : Controller
{
    public HomeController()
    { }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult ChatBot([FromServices] IConfiguration configuration, string? convKey, string? userName)
    {
        var model = new ChatBotViewModel
        {
            Url = $"{configuration.GetValue<string>("ChatbotUrl")}?convkey={HttpUtility.UrlEncode(convKey?.ToLower())}&username={HttpUtility.UrlEncode(userName)}",
            ConvKey = convKey,
            UserName = userName
        };

        return View(model);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
