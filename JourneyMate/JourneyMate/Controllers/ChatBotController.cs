using Microsoft.AspNetCore.Mvc;

namespace JourneyMate.Controllers
{
    public class ChatBotController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
