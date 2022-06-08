using System;
using System.Collections.Generic;
using System.Text;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data
{
    public class ForumDataContext : DbContext
    {
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Response> Responses { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<User> Users { get; set; }
        public ForumDataContext(DbContextOptions<ForumDataContext> options) : base(options)
        {
        }

        public ForumDataContext() { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Topic>()
                .HasOne(t => t.Author)
                .WithMany(u => u.Topics)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Topic>()
                .HasMany(t => t.Responses)
                .WithOne(r => r.Topic)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Response>()
                .HasOne(r => r.Author)
                .WithMany(u => u.Responses)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Response>()
                .HasMany(r => r.Comments)
                .WithOne(c => c.Response)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Author)
                .WithMany(u => u.Comments)
                .OnDelete(DeleteBehavior.NoAction);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=ForumDb;Trusted_Connection=True;");
        }
    }
}
