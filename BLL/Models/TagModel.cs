using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models
{
    public class TagModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<int> TopicTagIds { get; set; }
    }
}
