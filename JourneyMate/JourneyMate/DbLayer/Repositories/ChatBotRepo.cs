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
    }
}
