using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LikeButton.Services.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


        public DbSet<Article> Articles { get; set; }
        public DbSet<Like> Likes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Article configuration
            modelBuilder.Entity<Article>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasColumnType("INTEGER");
                entity.Property(e => e.Title)
                    .HasColumnType("TEXT")
                    .IsRequired();
                entity.Property(e => e.Content)
                    .HasColumnType("TEXT")
                    .IsRequired();
                entity.Property(e => e.Likes)
                    .HasColumnType("INTEGER");
            });

            modelBuilder.Entity<Like>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasColumnType("INTEGER");
                entity.Property(e => e.UserId)
                    .HasColumnType("TEXT")
                    .IsRequired();
                entity.Property(e => e.ArticleId)
                    .HasColumnType("INTEGER");
                entity.Property(e => e.DateLiked)
                    .HasColumnType("TEXT");

                entity.HasOne(d => d.Article)
                    .WithMany()
                    .HasForeignKey(d => d.ArticleId);
            });
        }
    }
}
