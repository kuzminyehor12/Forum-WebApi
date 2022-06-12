using System;
using System.Collections.Generic;
using System.Text;
using DAL.Entities;

namespace BLL.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Image { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public Roles Role { get; set; }
        public ICollection<int> CreatedTopicIds { get; set; }
        public ICollection<int> CreatedResponseIds { get; set; }
        public ICollection<Tuple<int, int>> LikedTopicIds { get; set; }
        public ICollection<Tuple<int, int>> LikedResponseIds { get; set; }
        public ICollection<int> CreatedCommentIds { get; set; }
    }
}
