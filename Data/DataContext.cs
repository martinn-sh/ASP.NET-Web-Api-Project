using Project.Models;
using Microsoft.EntityFrameworkCore;

namespace Project.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options) { }

        public DbSet<Mark> Marks { get; set; } = null!;
        public DbSet<Model> Models { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Model>()
                .HasOne(m => m.Mark)
                .WithMany(m => m.Models)
                .HasForeignKey(m => m.MarkId);
        }
    }
}