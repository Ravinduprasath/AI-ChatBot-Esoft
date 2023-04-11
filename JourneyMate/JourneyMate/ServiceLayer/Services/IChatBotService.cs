using JourneyMate.DbLayer.Domains;

namespace JourneyMate.ServiceLayer.Services
{
    public interface IChatBotService
    {
        /// <summary>
        /// Get all intents with keywords
        /// </summary>
        /// <returns></returns>
        Task<List<Intent>> IntentWithKeywords();
    }
}