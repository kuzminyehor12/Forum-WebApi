using System;
using System.Collections.Generic;
using System.Text;
using DAL.Entities;

namespace BLL.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Image { get; set; }
        public DateTime BirthDate { get; set; }
        public string UserCredentialsId { get; set; }
        public Roles Role { get; set; }
        public ICollection<int> CreatedTopicIds { get; set; }
        public ICollection<int> CreatedResponseIds { get; set; }
        public ICollection<LikerResponseModel> LikedTopicIds { get; set; }
        public ICollection<LikerTopicModel> LikedResponseIds { get; set; }
        public ICollection<int> CreatedCommentIds { get; set; }
    }
}
