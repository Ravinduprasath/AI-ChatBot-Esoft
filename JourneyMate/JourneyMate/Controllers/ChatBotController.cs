using JourneyMate.Classes.Enum;
using JourneyMate.Models.ChatBot;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace JourneyMate.Controllers
{
    public class ChatBotController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Get user input and send a response
        /// </summary>
        /// <param name="userInput">User input as a text</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult ChatMessege(string userInput)
        {
            var data = new ChatResponse
            {
                Type   = ChatResponseType.Text,
                Status = HttpStatusCode.OK
            };

            data.Buttons.Add(new ChatBotButton("Google", "btn btn-sm btn-primary", href: "https://www.google.lk"));
            data.Buttons.Add(new ChatBotButton("C#", "btn btn-sm btn-primary"));

            data.Messeges.Add("Hi");

            return Json(data);
        }

    }
}
