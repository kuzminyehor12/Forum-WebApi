using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models
{
    public class FilterModel
    {
        public IEnumerable<int> TagIds { get; set; }
        public DateTime? PublicationDate { get; set; }
    }
}
