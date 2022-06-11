using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class TopicTag : BaseEntity
    {
        public int TopicId { get; set; }
        public Topic Topic { get; set; }
        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
