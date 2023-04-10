namespace JourneyMate.DbLayer.Domains
{
    public class UserIntent
    {
        public int Id { get; set; }
        public int IntentId { get; set; }
        public string Keyword { get; set; }
        public Intent Intent { get; set; }
    }
}
