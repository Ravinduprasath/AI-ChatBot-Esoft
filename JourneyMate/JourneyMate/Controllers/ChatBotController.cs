using JourneyMate.Classes.Enum;
using JourneyMate.DbLayer.Domains;
using JourneyMate.DbLayer.Models;
using JourneyMate.Models.ChatBot;
using JourneyMate.ServiceLayer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;


namespace JourneyMate.Controllers
{
    public class ChatBotController : Controller
    {
        private readonly ChatBotContext _context;

        public ChatBotController(ChatBotContext context)
        {
            _context = context;
        }

        //private readonly IChatBotService _chatBotService;

        //public ChatBotController(IChatBotService chatBotService)
        //{
        //    _chatBotService = chatBotService;
        //}

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
            var data = new ChatResponse
            {
                Status = HttpStatusCode.OK,
                Type   = ChatResponseType.Text
            };

            userInput       = userInput.ToLower();
            var userIntents = await _context.UserIntents.ToListAsync();

            // Get specific intent
            var getIntent = userIntents.Where(x => x.Keyword.Contains(userInput)).FirstOrDefault();

            if(getIntent is null)
                data.Messeges.Add("Sorry I could't find an answer");
            else
            {
                // Get all questions in this intent type
                var questions = await _context.Questions.Where(x => getIntent.IntentId == x.IntentId).ToListAsync();

                if (questions is null)
                    data.Messeges.Add("Sorry I could't find an answer");
                else
                {
                    // I there any close matching questions?
                    var closestMatchQuestion = questions.OrderBy(x => LevenshteinDistance(x.Text, userInput)).FirstOrDefault();

                    if (closestMatchQuestion is null)
                        data.Messeges.Add("Sorry I could't find an answer");
                    else 
                    {
                        // Get answer for that qiestion
                        var answer = await _context.BotAnswers.Where(x => x.QuestionId == closestMatchQuestion.Id).ToListAsync();

                        if (getIntent.IntentId == (long)IntentType.Greeting)
                        {
                            // if there is many answers pick one random
                            var randomAnswer = answer[new Random().Next(answer.Count)];

                            data.Type = (ChatResponseType)randomAnswer.AnswerTypeId;

                            if (data.Type == ChatResponseType.Text)
                                data.Messeges.Add(randomAnswer.Text);
                        }                   
                    }
                }

            }

            return Json(data);
        }

        public int LevenshteinDistance(string s, string t)
        {
            if (string.IsNullOrEmpty(s))
            {
                if (string.IsNullOrEmpty(t))
                    return 0;
                return t.Length;
            }

            if (string.IsNullOrEmpty(t))
            {
                return s.Length;
            }

            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];

            for (int i = 0; i <= n; i++)
            {
                d[i, 0] = i;
            }

            for (int j = 0; j <= m; j++)
            {
                d[0, j] = j;
            }

            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;

                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }

            return d[n, m];
        }

        //data.Buttons.Add(new ChatBotButton("Google", "btn btn-sm btn-primary", href: "https://www.google.lk"));
        //data.Buttons.Add(new ChatBotButton("C#", "btn btn-sm btn-primary"));

        //data.Messeges.Add("Hi");
    }
}
