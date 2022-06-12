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
        public int UserCredentialsId { get; set; }
        public UserCredentials UserCredentials { get; set; }
        public ICollection<LikerTopic> LikedTopics { get; set; }
        public ICollection<LikerResponse> LikedResponses { get; set; }
        public ICollection<Topic> CreatedTopics { get; set; }
        public ICollection<Response> CreatedResponses { get; set; }
        public ICollection<Comment> CreatedComments { get; set; }
    }
}
