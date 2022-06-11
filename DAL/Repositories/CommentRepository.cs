using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Data;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private ForumDataContext _dbContext;
        public CommentRepository(ForumDataContext context)
        {
            _dbContext = context;
        }
        public async Task AddAsync(Comment entity)
        {
            await _dbContext.Comments.AddAsync(entity);
        }

        public void Delete(Comment entity)
        {
            _dbContext.Comments.Remove(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await Task.Run(() => _dbContext.Comments.Remove(_dbContext.Comments.Find(id)));
        }

        public async Task<IEnumerable<Comment>> GetAllAsync()
        {
            return await _dbContext.Comments.ToListAsync();
        }

        public async Task<IEnumerable<Comment>> GetAllWithDetailsAsync()
        {
            return await _dbContext.Comments
                .Include(c => c.Author)
                .Include(c => c.Response)
                .ThenInclude(r => r.Topic)
                .ToListAsync();
        }

        public async Task<Comment> GetByIdAsync(int id)
        {
            return await _dbContext.Comments.FindAsync(id);
        }

        public async Task<Comment> GetByIdWithDetailsAsync(int id)
        {
            return await _dbContext.Comments
               .Include(c => c.Author)
               .Include(c => c.Response)
               .ThenInclude(r => r.Topic)
               .FirstAsync(c => c.Id == id);
               
        }

        public void Update(Comment entity)
        {
            Comment findingComment = _dbContext.Comments.Find(entity.Id);

            if (findingComment != null)
            {
                _dbContext.Entry(findingComment).CurrentValues.SetValues(entity);
            }
        }
    }
}
