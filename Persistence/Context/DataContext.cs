using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        {
            Database.Migrate();
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleProduct> ArticleProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ArticleProduct>().HasKey(bc => new { bc.ArticleId, bc.ProductId });
            modelBuilder.Entity<ArticleProduct>()
                .HasOne(ap => ap.Article).WithMany(a => a.ArticleProducts).HasForeignKey(ap => ap.ArticleId);
            modelBuilder.Entity<ArticleProduct>()
                .HasOne(ap => ap.Product).WithMany(p => p.ArticleProducts).HasForeignKey(ap => ap.ProductId);
        }
    }
}
