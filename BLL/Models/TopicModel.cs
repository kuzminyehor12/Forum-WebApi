using System;
using System.Collections.Generic;
using System.Text;
using DAL.Enums;

namespace BLL.Models
{
    public class TopicModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublicationDate { get; set; }
        public States TopicState { get; set; }
        public int AuthorId { get; set; }
        public ICollection<int> TopicTagIds { get; set; }
        public ICollection<int> ResponsesIds { get; set; }
    }
}
