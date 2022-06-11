using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Validation
{
    public class ForumException : Exception
    {
        public ForumException() : base()
        {

        }

        public ForumException(string msg) : base(msg)
        {

        }

        public ForumException(string msg, Exception ex) : base(msg, ex)
        {

        }
    }
}
