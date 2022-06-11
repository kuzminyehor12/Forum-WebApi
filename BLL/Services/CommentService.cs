using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using BLL.Models;

namespace BLL.Services
{
    public class CommentService : ICommentService
    {
        public Task AddAsync(CommentModel model)
        {
            throw new NotImplementedException();
        }

        public Task CompainAboutComment(int commentId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int modelId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CommentModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CommentModel> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task LikeComment(int commentId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(CommentModel model)
        {
            throw new NotImplementedException();
        }
    }
}
