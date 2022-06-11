using System;
using System.Collections.Generic;
using System.Text;
using DAL.Enums;

namespace BLL.Models
{
    public class ResponseModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int? TopicId { get; set; }
        public DateTime PublicationDate { get; set; }
        public States ResponseState { get; set; }
        public int Likes { get; set; }
        public int Complaints { get; set; }
        public int AuthorId { get; set; }
        public ICollection<int> CommentIds { get; set; }
}
}
