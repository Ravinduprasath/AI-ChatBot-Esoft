using JourneyMate.DbLayer.Domains;
using Microsoft.EntityFrameworkCore;

namespace JourneyMate.DbLayer.Models
{
    public class ChatBotContext : DbContext
    {
        public ChatBotContext(DbContextOptions<ChatBotContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Question>()
                .HasOne(q => q.Intent)
                .WithMany(i => i.Questions)
                .HasForeignKey(q => q.IntentId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<BotAnswer>()
                .HasOne(a => a.Question)
                .WithMany(q => q.BotAnswers)
                .HasForeignKey(a => a.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BotAnswer>()
                .HasOne(a => a.AnswerType)
                .WithMany(t => t.BotAnswers)
                .HasForeignKey(a => a.AnswerTypeId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserIntent>()
                .HasOne(ui => ui.Intent)
                .WithMany(i => i.UserIntents)
                .HasForeignKey(ui => ui.IntentId)
                .OnDelete(DeleteBehavior.NoAction);
        }

        public DbSet<Intent> Intents { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<AnswerType> AnswerTypes { get; set; }
        public DbSet<BotAnswer> BotAnswers { get; set; }
        public DbSet<UserIntent> UserIntents { get; set; }
    }
}
