using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IResponseRepository : IRepo<Response>
    {
        Task<IEnumerable<Response>> GetAllWithDetailsAsync();
        Task<Response> GetByIdWithDetailsAsync(int id);
    }
}
