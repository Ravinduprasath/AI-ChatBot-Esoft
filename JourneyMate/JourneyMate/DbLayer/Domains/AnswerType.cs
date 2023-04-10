namespace JourneyMate.DbLayer.Domains
{
    public class AnswerType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<BotAnswer> BotAnswers { get; set; }
    }
}
