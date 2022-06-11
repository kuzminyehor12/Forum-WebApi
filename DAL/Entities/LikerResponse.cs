using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class LikerResponse
    {
        public int ResponseId { get; set; }
        public Response Response { get; set; }
        public int UserId { get; set; }
        public User Liker { get; set; }
    }
}
