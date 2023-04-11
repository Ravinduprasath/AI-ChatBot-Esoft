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

        /// <summary>
        /// Get all intents keywords
        /// Decending by length
        /// </summary>
        /// <returns></returns>
        Task<List<UserIntent>> UserIntents();

        /// <summary>
        /// Question for intent id
        /// </summary>
        /// <param name="intentId">Intent type id</param>
        /// <returns></returns>
        Task<List<Question>> Questions(long intentId);

        /// <summary>
        /// Get answers for a question
        /// </summary>
        /// <param name="questionId">Unique question id</param>
        /// <returns></returns>
        Task<List<BotAnswer>> Answers(long questionId);
    }
}