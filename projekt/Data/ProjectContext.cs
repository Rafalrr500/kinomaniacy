using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using projekt.Models;

namespace projekt.Data
{
    public class ProjectContext : DbContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> options)
            : base(options)
        {
        }

        public DbSet<projekt.Models.Movie> Movie { get; set; }
        public DbSet<projekt.Models.Category> MovieShow { get; set; }
        public DbSet<projekt.Models.CategoryMovie> CategoryMovie { get; set; }
        public DbSet<projekt.Models.MovieShow> MovieShowing { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>().ToTable("Movie");
            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<MovieShow>().ToTable("MovieShowing");

            modelBuilder.Entity<CategoryMovie>()
            .HasKey(bc => new { bc.CategoryId, bc.MovieId });
            modelBuilder.Entity<CategoryMovie>()
                .HasOne(bc => bc.Category)
                .WithMany(b => b.CategoryMovies)
                .HasForeignKey(bc => bc.CategoryId);
            modelBuilder.Entity<CategoryMovie>()
                .HasOne(bc => bc.Movie)
                .WithMany(c => c.CategoryMovies)
                .HasForeignKey(bc => bc.MovieId);
        }
    }
}
