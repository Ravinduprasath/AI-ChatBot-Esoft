using JourneyMate.Classes.Enum;
using JourneyMate.DbLayer.Domains;
using JourneyMate.Models.ChatBot;
using JourneyMate.ServiceLayer.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;


namespace JourneyMate.Controllers
{
    public class ChatBotController : Controller
    {
        private readonly IChatBotService _chatBotService;

        public ChatBotController(IChatBotService chatBotService)
        {
            _chatBotService = chatBotService;
        }

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
        public async Task<IActionResult> ChatMessege(string userInput)
        {         
            var res  = await _chatBotService.GetResponse(userInput);
            var data = GetResponseGetChatResponse(res);

            return Json(data);
        }

        /// <summary>
        /// Convert answer to ChatResponse
        /// </summary>
        /// <param name="botAnswers">List of answers</param>
        /// <returns></returns>
        private ChatResponse GetResponseGetChatResponse(List<BotAnswer> botAnswers) 
        {
            var data = new ChatResponse
            {
                Status = HttpStatusCode.OK,
                Type = ChatResponseType.Text
            };

            // Get intent type id
            long intentType = botAnswers.Select(x => x.Question.IntentId).FirstOrDefault();

            switch(intentType) 
            {
                case (int)IntentType.Greeting:
                    // If there is many answers pick one random
                    var randomAnswer = botAnswers[new Random().Next(botAnswers.Count)];

                    data.Type = (ChatResponseType)randomAnswer.AnswerTypeId;

                    if (data.Type == ChatResponseType.Text)
                        data.Messeges.Add(randomAnswer.Text);

                    break;
                case (int)IntentType.Farewell:

                    var randomAnswerFarewell = botAnswers[new Random().Next(botAnswers.Count)];

                    data.Type = (ChatResponseType)randomAnswerFarewell.AnswerTypeId;

                    if (data.Type == ChatResponseType.Text)
                        data.Messeges.Add(randomAnswerFarewell.Text);

                    break;
                case (int)IntentType.Find:

                    AddToMessege(botAnswers, data);

                    break;
                default:
                    data.Messeges.Add("Sorry I could't find an answer");
                    data.Type = ChatResponseType.Text;
                    break;
            }

            return data;
        }

        /// <summary>
        /// Add messege to chat response
        /// </summary>
        /// <param name="botAnswer"></param>
        /// <param name="data"></param>
        private void AddToMessege(List<BotAnswer> botAnswer, ChatResponse data) 
        {
            foreach (BotAnswer answer in botAnswer)
            {
                if (answer.AnswerTypeId == (int)ChatResponseType.Text) 
                {           
                    data.Messeges.Add(answer.Text);
                }
            }
        }

        //data.Buttons.Add(new ChatBotButton("Google", "btn btn-sm btn-primary", href: "https://www.google.lk"));
        //data.Buttons.Add(new ChatBotButton("C#", "btn btn-sm btn-primary"));

        //data.Messeges.Add("Hi");
    }
}
