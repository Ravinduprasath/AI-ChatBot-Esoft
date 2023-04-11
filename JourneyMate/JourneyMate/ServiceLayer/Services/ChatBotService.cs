using JourneyMate.DbLayer.Domains;
using JourneyMate.DbLayer.Repositories;
using Microsoft.EntityFrameworkCore;

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
        /// Get all intents with keywords
        /// </summary>
        /// <returns></returns>
        public async Task<List<Intent>> IntentWithKeywords()
        {
            return await _chatBotRepo.IntentWithKeywords();
        }
    }
}
