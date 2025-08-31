using MehnahFinalApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MehnahFinalApi.Data
{
    public class AppicatDbContext : DbContext
    {
        public AppicatDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Work> Works { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<User> Users { get; set; }
        protected AppicatDbContext()
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // منع الحذف التلقائي بين Rating و Work
            modelBuilder.Entity<Rating>()
                .HasOne(r => r.Work)
                .WithMany(w => w.Ratings)
                .HasForeignKey(r => r.WorkId)
                .OnDelete(DeleteBehavior.Restrict); // 🔴 الحل الأساسي

            // منع الحذف التلقائي بين Rating و User (Reviewer)
            modelBuilder.Entity<Rating>()
                .HasOne(r => r.Reviewer)
                .WithMany(u => u.RatingsGiven)
                .HasForeignKey(r => r.ReviewerId)
                .OnDelete(DeleteBehavior.Restrict); // 🔴

            // منع الحذف التلقائي بين Work و User (صاحب العمل)
            modelBuilder.Entity<Work>()
                .HasOne(w => w.User)
                .WithMany(u => u.Works)
                .HasForeignKey(w => w.UserId)
                .OnDelete(DeleteBehavior.Restrict); // 🔴
        }



    }
}
