using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class Tag : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<TopicTag> TopicTags { get; set; }
    }
}
