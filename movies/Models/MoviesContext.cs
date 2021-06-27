using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace movies.Models
{
    public partial class MoviesContext : DbContext
    {
        public MoviesContext()
        {
        }

        public MoviesContext(DbContextOptions<MoviesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Like> Likes { get; set; }
        public virtual DbSet<Movei> Moveis { get; set; }
        public virtual DbSet<Recommend> Recommends { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.;Database=Movies;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Chinese_PRC_CI_AS");

            modelBuilder.Entity<Like>(entity =>
            {
                entity.ToTable("like");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.MainImg).HasColumnName("mainImg");

                entity.Property(e => e.MovieImgs).HasColumnName("movieImgs");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.ShoTime)
                    .HasColumnType("date")
                    .HasColumnName("shoTime");

                entity.Property(e => e.Zhuyan).HasColumnName("zhuyan");
            });

            modelBuilder.Entity<Movei>(entity =>
            {
                entity.ToTable("moveis");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.PlayTime)
                    .HasColumnType("datetime")
                    .HasColumnName("playTime");

                entity.Property(e => e.PriviewImgPath)
                    .IsRequired()
                    .HasColumnName("priviewImgPath");

                entity.Property(e => e.Score)
                    .HasMaxLength(50)
                    .HasColumnName("score");

                entity.Property(e => e.Star).HasColumnName("star");

                entity.Property(e => e.SwiperImgPath).IsRequired();

                entity.Property(e => e.VedioPath).HasColumnName("vedioPath");
            });

            modelBuilder.Entity<Recommend>(entity =>
            {
                entity.ToTable("recommend");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Pic).HasColumnName("pic");

                entity.Property(e => e.Price)
                    .HasMaxLength(50)
                    .HasColumnName("price");

                entity.Property(e => e.Title).HasColumnName("title");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
