using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class LikerTopic
    {
        public int TopicId { get; set; }
        public Topic Topic { get; set; }
        public int UserId { get; set; }
        public User Liker { get; set; }
    }
}
