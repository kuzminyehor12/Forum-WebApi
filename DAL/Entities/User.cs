using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        [EnumDataType(typeof(Roles))]
        public Roles Role { get; set; }
        public ICollection<Topic> Topics { get; set; }
        public ICollection<Response> Responses { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
