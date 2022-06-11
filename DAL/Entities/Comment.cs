using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using DAL.Enums;

namespace DAL.Entities
{
    public class Comment : BaseEntity
    {
        public int? AuthorId { get; set; }
        public int ResponseId { get; set; }
        public string Text { get; set; }
        public DateTime PublicationDate { get; set; }
        public States CommentState { get; set; }
        public int Complaints { get; set; }
        public User Author { get; set; }
        public Response Response { get; set; }
    }
}
