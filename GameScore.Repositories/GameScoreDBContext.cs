using GameScore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GameScore.Repositories
{
    public class GameScoreDBContext : IdentityDbContext
    {
        public DbSet<Pontuacao> Pontuacao { get; set; }

        public GameScoreDBContext(DbContextOptions<GameScoreDBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pontuacao>().HasKey(p => p.Id);

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            return base.SaveChanges();
        }
    }


}
