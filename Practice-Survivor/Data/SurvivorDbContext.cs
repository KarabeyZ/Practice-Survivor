using Microsoft.EntityFrameworkCore;
using Practice_Survivor.Enitities;
using Practice_Survivor.Enitity;

namespace Practice_Survivor.Data
{
    public class SurvivorDbContext : DbContext
    {
        public SurvivorDbContext(DbContextOptions<SurvivorDbContext> options) : base(options) { }

        public DbSet<CategoryEntity> Categories => Set<CategoryEntity>();
        public DbSet<CompetitorEntity> Competitor => Set<CompetitorEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CategoryEntity>().ToTable("Kategori").HasQueryFilter(c => c.IsDeleted == false);
            modelBuilder.Entity<CompetitorEntity>().ToTable("Yarışmacı").HasQueryFilter(c => c.IsDeleted == false);

        }

    }
}
