using Microsoft.EntityFrameworkCore;
using NovaBot.Models;

namespace NovaBot.Data
{
    public class ApplicationDbContext : DbContext
    {
        public virtual DbSet<QuoteModel> Quote { get; set; }
        public virtual DbSet<UserModel> User { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public ApplicationDbContext() : base() { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserModel>(entity =>
            {
                entity.HasKey(u => u.UserId);
                entity.Property(u => u.Name).IsRequired().HasMaxLength(200);
                entity.Property(u => u.ProfilePicture).HasMaxLength(500);
                entity.Property(u => u.Ranking);
                entity.HasMany(u => u.Quotes)
                .WithOne(q => q.User)
                .OnDelete(DeleteBehavior.Cascade)
                ;
            });

            builder.Entity<QuoteModel>(
                entity =>
                {
                    entity.HasKey(q => q.QuoteId);
                    entity.Property(q => q.Content).IsRequired();
                    entity.Property(q => q.Date).IsRequired();
                    entity.Property(q => q.Upvotes);
                    entity.Property(q => q.Downvotes);
                    entity.HasOne(q => q.User)
                    .WithMany(u => u.Quotes)
                    .HasForeignKey(u => u.UserId);
                }
                );


        }
    }
}
