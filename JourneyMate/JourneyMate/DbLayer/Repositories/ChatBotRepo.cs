using JourneyMate.DbLayer.Domains;
using JourneyMate.DbLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace JourneyMate.DbLayer.Repositories
{
    public class ChatBotRepo : IChatBotRepo
    {
        private readonly ChatBotContext _context;

        public ChatBotRepo(ChatBotContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all intents with keywords
        /// </summary>
        /// <returns></returns>
        public async Task<List<Intent>> IntentWithKeywords()
        {
            var list = await _context.Intents.Include(x => x.UserIntents).ToListAsync();

            if (list == null)
                return new List<Intent>();

            return list;
        }

        /// <summary>
        /// Get all intents keywords
        /// Decending by length
        /// </summary>
        /// <returns></returns>
        public async Task<List<UserIntent>> UserIntents()
        {
            var list = await _context.UserIntents.OrderByDescending(x => x.Keyword.Length).ToListAsync();

            if (list == null)
                return new List<UserIntent>();

            return list;
        }

        /// <summary>
        /// Question for intent id
        /// </summary>
        /// <param name="intentId">Intent type id</param>
        /// <returns></returns>
        public async Task<List<Question>> Questions(long intentId)
        {
            var list = await _context.Questions.Where(x => x.IntentId == intentId).ToListAsync();

            if (list == null)
                return new List<Question>();

            return list;
        }

        /// <summary>
        /// Get answers for a question
        /// </summary>
        /// <param name="questionId">Unique question id</param>
        /// <returns></returns>
        public async Task<List<BotAnswer>> Answers(long questionId)
        {
            var list = await _context.BotAnswers.Where(x => x.QuestionId == questionId).ToListAsync();

            if (list == null)
                return new List<BotAnswer>();

            return list;
        }

        /// <summary>
        /// Get answers for a keyword
        /// </summary>
        /// <param name="keyword">Some matching words</param>
        /// <returns></returns>
        public async Task<List<BotAnswer>> AnswersFromKeyword(string keyword)
        {
            var list = await _context.Questions.Where(x => x.Text.ToLower().Contains(keyword.ToLower()))
                                               .Include(x => x.BotAnswers)
                                               .Select(x => x.BotAnswers)
                                               .FirstOrDefaultAsync();

            if (list == null)
                return new List<BotAnswer>();

            return list;
        }
    }
}
