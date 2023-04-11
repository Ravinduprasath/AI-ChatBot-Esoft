using JourneyMate.Classes.Enum;
using JourneyMate.DbLayer.Domains;
using JourneyMate.DbLayer.Repositories;
using JourneyMate.Models.ChatBot;
using Microsoft.EntityFrameworkCore;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace JourneyMate.ServiceLayer.Services
{
    public class ChatBotService : IChatBotService
    {
        private readonly IChatBotRepo _chatBotRepo;

        public ChatBotService(IChatBotRepo chatBotRepo)
        {
            _chatBotRepo = chatBotRepo;
        }

        /// <summary>
        /// Get user input and return a response
        /// </summary>
        /// <param name="userInput">Unser input in string</param>
        /// <returns>
        /// Success : Answer , Else : Error messege
        /// </returns>
        public async Task<List<BotAnswer>> GetResponse(string userInput) 
        {
            var answers = new List<BotAnswer>();

            var answer = new BotAnswer
            {
                AnswerTypeId = (int)ChatResponseType.Text,
                Text = "Sorry I could't find an answer"
            };

            answers.Add(answer);

            // All intents keywords
            var userIntentKeywords  = await UserIntents();

            // Query ,Question we need to looking for
            var searchQuery = GetQuery(userInput, userIntentKeywords, out List<string> queryWords);

            // Get user real intent based on query words
            var intent = GetUserIntent(userIntentKeywords, queryWords, searchQuery);

            if (intent is null)
                return answers;

            // Get all questions in this intent type
            var questions = await Questions(intent.IntentId);

            if (questions is null)
                return answers;

            var closestMatchQuestion = questions.Where(x => x.Text.Contains(searchQuery)).FirstOrDefault();

            if (closestMatchQuestion is null)
                return answers;

            var answersFromDb = await Answers(closestMatchQuestion.Id);

            return answersFromDb;
        }

        /// <summary>
        /// Get all intents with keywords
        /// </summary>
        /// <returns></returns>
        private async Task<List<Intent>> IntentWithKeywords()
        {
            return await _chatBotRepo.IntentWithKeywords();
        }

        /// <summary>
        /// Get all intents keywords
        /// Decending by length
        /// </summary>
        /// <returns></returns>
        private async Task<List<UserIntent>> UserIntents()
        {
           return await _chatBotRepo.UserIntents();
        }

        /// <summary>
        /// Question for intent id
        /// </summary>
        /// <param name="intentId">Intent type id</param>
        /// <returns></returns>
        public async Task<List<Question>> Questions(long intentId)
        {
            return await _chatBotRepo.Questions(intentId);
        }

        /// <summary>
        /// Get answers for a question
        /// </summary>
        /// <param name="questionId">Unique question id</param>
        /// <returns></returns>
        public async Task<List<BotAnswer>> Answers(long questionId) 
        {
            return await _chatBotRepo.Answers(questionId);
        }

        /// <summary>
        /// What user looking for?
        /// Example : What is sigiriya
        ///           "What is" -> Intent of find something
        ///           find for -> "Sigiriya"
        ///           return "Sigiriya"
        ///           
        ///           "Hello" -> Intent of greeting
        ///           There is no question
        ///           Return "Hello"
        /// </summary>
        /// <param name="userInput">User insert string</param>
        /// <param name="userIntentKewords">List of intents</param>
        /// <returns></returns>
        private string GetQuery(string userInput, List<UserIntent> userIntentKewords, out List<string> intentWords) 
        {
            userInput = userInput.ToLower();

            // Intent words, Like = "Hello", "Search for"
            List<string> queryWords = new List<string>();

            //}
            foreach (var word in userIntentKewords)
            {
                if (userInput.StartsWith(word.Keyword))
                {
                    string result = userInput.Substring(word.Keyword.Length).Trim();

                    // Add keywords to list
                    queryWords.Add(word.Keyword);

                    // Return input without intent words
                    userInput = result;
                    break;
                }
            }

            // Set out pare list
            intentWords = queryWords;

            // No intent found? Return same user input
            return userInput;
        }

        /// <summary>
        /// Get user real intent
        /// </summary>
        /// <param name="userIntentKewords">Intent keywords</param>
        /// <param name="queryWords">Filtered intent words</param>
        /// <param name="searchQuery">What user looking for</param>
        /// <returns></returns>
        private UserIntent? GetUserIntent(List<UserIntent> userIntentKewords, List<string> queryWords, string searchQuery) 
        {
            #pragma warning disable CS8600

            UserIntent intent = new UserIntent();

            if (queryWords.Any())
                intent = userIntentKewords.Where(x => x.Keyword.Contains(queryWords.First())).FirstOrDefault();

            if (intent is null || !queryWords.Any())
                intent = userIntentKewords.Where(x => x.Keyword.Contains(searchQuery)).FirstOrDefault();

            #pragma warning restore CS8600

            return intent;
        }
    }
}
