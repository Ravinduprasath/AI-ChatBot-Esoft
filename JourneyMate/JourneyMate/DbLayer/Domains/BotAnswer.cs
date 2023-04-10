namespace JourneyMate.DbLayer.Domains
{
    public class BotAnswer
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int AnswerTypeId { get; set; }
        public string Text { get; set; }
        public byte[] Image { get; set; }
        public bool IsHtml { get; set; }
        public bool IsButton { get; set; }
        public Question Question { get; set; }
        public AnswerType AnswerType { get; set; }
    }
}
