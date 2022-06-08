using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using DAL.Enums;

namespace DAL.Entities
{
    public class Comment : BaseEntity
    {
        public int AuthorId { get; set; }
        public int ResponseId { get; set; }
        public string Text { get; set; }
        public DateTime PublicationDate { get; set; }

        [EnumDataType(typeof(States))]
        public States TopicState { get; set; }
        public User Author { get; set; }
        public Response Response { get; set; }
    }
}
