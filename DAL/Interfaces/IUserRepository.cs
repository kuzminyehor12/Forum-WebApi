using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IUserRepository : IRepo<User>
    {
        Task<User> GetByIdWithDetailsAsync(int id);
        Task<IEnumerable<User>> GetAllWithDetailsAsync();
    }
}
