using System;
using System.Collections.Generic;
using System.Text;
using DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Extensions
{
    public static class EntityBuilderExtension
    {
        public static void Many2Many(this EntityTypeBuilder<TopicTag> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(tt => new { tt.TopicId, tt.TagId });

            entityTypeBuilder
                .HasOne(tt => tt.Topic)
                .WithMany(t => t.TopicTags)
                .HasForeignKey(tt => tt.TopicId);

            entityTypeBuilder
                .HasOne(tt => tt.Tag)
                .WithMany(tag => tag.TopicTags)
                .HasForeignKey(tt => tt.TagId);
        }

        public static void Many2Many(this EntityTypeBuilder<LikerTopic> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(lt => new { lt.TopicId, lt.UserId });

            entityTypeBuilder
                .HasOne(lt => lt.Topic)
                .WithMany(t => t.LikedBy)
                .HasForeignKey(lt => lt.TopicId);

            entityTypeBuilder
                .HasOne(lt => lt.Liker)
                .WithMany(u => u.LikedTopics)
                .HasForeignKey(lt => lt.UserId);
        }

        public static void Many2Many(this EntityTypeBuilder<LikerResponse> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(lr => new { lr.ResponseId, lr.UserId });

            entityTypeBuilder
                 .HasOne(lt => lt.Response)
                .WithMany(t => t.LikedBy)
                .HasForeignKey(lt => lt.ResponseId);

            entityTypeBuilder
                .HasOne(lt => lt.Liker)
                .WithMany(u => u.LikedResponses)
                .HasForeignKey(lt => lt.UserId);
        }
    }
}
