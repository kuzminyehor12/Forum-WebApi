using System;
using System.Collections.Generic;
using System.Text;
using DAL.Enums;

namespace BLL.Models
{
    public class CommentModel
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public int ResponseId { get; set; }
        public string Text { get; set; }
        public DateTime PublicationDate { get; set; }
        public States CommentState { get; set; }
    }
}
