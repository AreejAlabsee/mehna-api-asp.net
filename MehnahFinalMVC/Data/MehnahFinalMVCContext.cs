using Microsoft.EntityFrameworkCore;

namespace MehnahFinalMVC.Data
{
    public class MehnahFinalMVCContext : DbContext
    {
        public MehnahFinalMVCContext(DbContextOptions<MehnahFinalMVCContext> options)
            : base(options)
        {
        }

        public DbSet<MehnahFinalMVC.Models.User> User { get; set; } = default!;
        public DbSet<MehnahFinalMVC.Models.Work> Work { get; set; } = default!;
        public DbSet<MehnahFinalMVC.Models.Rating> Rating { get; set; } = default!;
    }
}
