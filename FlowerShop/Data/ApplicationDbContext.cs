using FlowerShop.Models;
using Microsoft.EntityFrameworkCore;


namespace FlowerShop.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Flower> Flowers { get; set; }
        public DbSet<FlowerCategory> FlowerCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FlowerCategory>(entity =>
            {
                entity.ToTable("FlowerCategories");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name)
                      .IsRequired()
                      .HasMaxLength(100);
                entity.HasMany(e => e.Flowers)
                      .WithOne(e => e.Category)
                      .HasForeignKey(e => e.CategoryId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Flower>(entity =>
            {
                entity.ToTable("Flowers");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name)
                      .IsRequired()
                      .HasMaxLength(100);
                entity.Property(e => e.CategoryId)
                       .IsRequired();
                entity.Property(e => e.Price)
                      .IsRequired()
                      .HasColumnType("decimal(5,2)");
            });
        }
    }
}
