using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using DAL.Enums;

namespace DAL.Entities
{
    public class Response : BaseEntity
    {
        public string Text { get; set; }
        public int? TopicId { get; set; }
        public int? AuthorId { get; set; }
        public DateTime PublicationDate { get; set; }
        public States ResponseState { get; set; }
        public int Complaints { get; set; }
        public User Author { get; set; }
        public Topic Topic { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<LikerResponse> LikedBy { get; set; }
    }
}
