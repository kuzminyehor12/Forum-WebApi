using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Entities
{
    public class User : BaseEntity
    {
        public string Nickname { get; set; }
        public string Image { get; set; }
        public DateTime BirthDate { get; set; }
        public Roles Role { get; set; }
        public string UserCredentialsId { get; set; }
        public UserCredentials UserCredentials { get; set; }
        public virtual ICollection<LikerTopic> LikedTopics { get; set; }
        public virtual ICollection<LikerResponse> LikedResponses { get; set; }
        public virtual ICollection<Topic> CreatedTopics { get; set; }
        public virtual ICollection<Response> CreatedResponses { get; set; }
        public virtual ICollection<Comment> CreatedComments { get; set; }
    }
}
