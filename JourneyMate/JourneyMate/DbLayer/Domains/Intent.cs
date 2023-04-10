namespace JourneyMate.DbLayer.Domains
{
    public class Intent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Question> Questions { get; set; }
        public List<UserIntent> UserIntents { get; set; }
    }
}
