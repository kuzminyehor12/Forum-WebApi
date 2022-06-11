using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using DAL.Enums;

namespace DAL.Entities
{
    public class Topic : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublicationDate { get; set; }
        public States TopicState { get; set; }
        public int Complaints { get; set; }
        public int? AuthorId { get; set; }
        public User Author { get; set; }
        public ICollection<TopicTag> TopicTags { get; set; }
        public ICollection<Response> Responses { get; set; }
        public ICollection<LikerTopic> LikedBy { get; set; }
    }
}
