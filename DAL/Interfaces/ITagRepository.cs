using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Interfaces
{
   public interface ITagRepository : IRepo<Tag>
   {
        Task<IEnumerable<Tag>> GetAllWithDetailsAsync();
        Task<Tag> GetByIdWithDetailsAsync(int id);
    }
}
