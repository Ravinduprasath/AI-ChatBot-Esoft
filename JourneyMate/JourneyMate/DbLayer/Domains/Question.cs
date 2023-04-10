namespace JourneyMate.DbLayer.Domains
{
    public class Question
    {
        public int Id { get; set; }
        public int IntentId { get; set; }
        public string Text { get; set; }
        public List<BotAnswer> BotAnswers { get; set; }
        public Intent Intent { get; set; }
    }
}
