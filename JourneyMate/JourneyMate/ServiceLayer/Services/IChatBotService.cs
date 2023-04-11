using JourneyMate.DbLayer.Domains;

namespace JourneyMate.ServiceLayer.Services
{
    public interface IChatBotService
    {
        /// <summary>
        /// Get user input and return a response
        /// </summary>
        /// <param name="userInput">Unser input in string</param>
        /// <returns>
        /// Success : Answer , Else : Error messege
        /// </returns>
        Task<List<BotAnswer>> GetResponse(string userInput);
    }
}