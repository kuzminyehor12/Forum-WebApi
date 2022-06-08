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

        [EnumDataType(typeof(States))]
        public States TopicState { get; set; }
        public int AuthorId { get; set; }
        public User Author { get; set; }
        public ICollection<Response> Responses { get; set; }
    }
}
