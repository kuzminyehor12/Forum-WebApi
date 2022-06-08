﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using DAL.Enums;

namespace DAL.Entities
{
    public class Response : BaseEntity
    {
        public string Text { get; set; }
        public int? TopicId { get; set; }
        public int AuthorId { get; set; }
        public DateTime PublicationDate { get; set; }

        [EnumDataType(typeof(States))]
        public States TopicState { get; set; }
        public User Author { get; set; }
        public Topic Topic { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}