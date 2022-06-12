using System;
using System.Collections.Generic;
using System.Text;
using DAL.Entities;
using DAL.Extensions;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data
{
    public class ForumDataContext : DbContext
    {
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Response> Responses { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TopicTag> TopicTags { get; set; }
        public DbSet<LikerResponse> LikerResponses { get; set; }
        public DbSet<LikerTopic> LikerTopics { get; set; }
        public ForumDataContext(DbContextOptions<ForumDataContext> options) : base(options)
        {
        }

        public ForumDataContext() { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Topic>()
                .HasOne(t => t.Author)
                .WithMany(u => u.CreatedTopics);

            modelBuilder.Entity<Topic>()
                .HasMany(t => t.Responses)
                .WithOne(r => r.Topic);

            modelBuilder.Entity<TopicTag>().Many2Many();
            modelBuilder.Entity<LikerTopic>().Many2Many();
            modelBuilder.Entity<LikerResponse>().Many2Many();

            modelBuilder.Entity<Response>()
                .HasOne(r => r.Author)
                .WithMany(u => u.CreatedResponses);

            modelBuilder.Entity<Response>()
                .HasMany(r => r.Comments)
                .WithOne(c => c.Response);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Author)
                .WithMany(u => u.CreatedComments);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=ForumDb;Trusted_Connection=True;");
        }
    }
}
