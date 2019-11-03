using Microsoft.EntityFrameworkCore;
using NovaBot.Models;

namespace NovaBot.Data
{
    public class ApplicationDbContext : DbContext
    {
        public virtual DbSet<QuoteModel> Quote { get; set; }
        public virtual DbSet<UserModel> User { get; set; }
        public virtual DbSet<ConfigurationModel> Configurations { get; set; }
        public virtual DbSet<VoteModel> VoteModels { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public ApplicationDbContext() : base() { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            configUserEntity(builder);
            configQuoteEntity(builder);
            configConfigurationEntity(builder);
            configVoteModel(builder);
        }

        private static void configConfigurationEntity(ModelBuilder builder)
        {
            builder.Entity<ConfigurationModel>(
                            entity =>
                            {
                                entity.HasKey(c => c.ConfigurationId);
                                entity.Property(c => c.BotUserId).HasMaxLength(70);
                                entity.Property(c => c.BotAccessToken).HasMaxLength(70);
                                entity.Property(c => c.ClientId).HasMaxLength(70);
                                entity.Property(c => c.ClientSecret).HasMaxLength(70);
                                entity.Property(c => c.LastAuthToken).HasMaxLength(70);

                            }
                            );
        }

        private static void configQuoteEntity(ModelBuilder builder)
        {
            builder.Entity<QuoteModel>(
                            entity =>
                            {
                                entity.HasKey(q => q.QuoteId);
                                entity.Property(q => q.Content).IsRequired();
                                entity.Property(q => q.Date).IsRequired();
                                entity.Property(q => q.Upvotes);
                                entity.Property(q => q.Downvotes);
                                entity.Property(q => q.SnitchId);
                                entity.Property(q => q.UserId);
                                entity.HasOne(q => q.User)
                                .WithMany(u => u.Quotes)
                                .HasForeignKey(q => q.UserId)
                                .IsRequired(false);
                            }
                            );
        }

        private static void configVoteModel(ModelBuilder builder)
        {
            builder.Entity<VoteModel>(
                entity =>
                {
                    entity.HasKey(v =>
                       new { v.UserSlackId, v.QuoteVoteUid }
                        );
                    entity.Property(v => v.Vote);
                    entity.HasOne(v => v.User)
                    .WithMany(u => u.MyVotes)
                    .HasForeignKey(v => v.UserSlackId);
                    entity.HasOne(v => v.Quote)
                    .WithMany(v => v.Votes)
                    .HasForeignKey(v => v.QuoteId);
                }
                );
        }

        private static void configUserEntity(ModelBuilder builder)
        {
            builder.Entity<UserModel>(entity =>
            {
                entity.HasKey(u => u.UserId);
                entity.Property(u => u.UserId).IsRequired();
                entity.Property(u => u.Name).IsRequired().HasMaxLength(200);
                entity.Property(u => u.RealName).HasMaxLength(200);
                entity.Property(u => u.SlackId).IsRequired().HasMaxLength(50);
                entity.Property(u => u.Deleted).IsRequired();
                entity.Property(u => u.StatusText).HasMaxLength(500);
                entity.Property(u => u.ProfilePicutre_72).HasMaxLength(500);
                entity.Property(u => u.ProfilePicture_192).HasMaxLength(500);
                entity.Property(u => u.ProfilePicutre_512).HasMaxLength(500);
                entity.Property(u => u.PasswordHash).HasMaxLength(400);
                entity.Property(u => u.Salt).HasMaxLength(100);
                entity.Property(u => u.Ranking);
                entity.HasMany(u => u.Quotes)
                .WithOne(q => q.User)
                .IsRequired(false);
                ;
            });
        }
    }
}
