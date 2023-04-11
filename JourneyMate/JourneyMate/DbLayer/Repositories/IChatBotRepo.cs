using JourneyMate.DbLayer.Domains;

namespace JourneyMate.DbLayer.Repositories
{
    public interface IChatBotRepo
    {
        /// <summary>
        /// Get all intents with keywords
        /// </summary>
        /// <returns></returns>
        Task<List<Intent>> IntentWithKeywords();
    }
}