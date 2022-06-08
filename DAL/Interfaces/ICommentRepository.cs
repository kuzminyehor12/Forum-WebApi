using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface ICommentRepository : IRepo<Comment>
    {
        Task<IEnumerable<Comment>> GetAllByResponseIdAsync(int responseId);
        Task<IEnumerable<Comment>> GetAllByAuthorIdAsync(int authorId);
        Task<IEnumerable<Comment>> GetAllWithDetailsAsync();
        Task<Comment> GetByIdWithDetails(int id);
    }
}
