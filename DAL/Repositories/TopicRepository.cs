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
    public class TopicRepository : ITopicRepository
    {
        private ForumDataContext _dbContext;
        public TopicRepository(ForumDataContext context)
        {
            _dbContext = context;
        }
        public async Task AddAsync(Topic entity)
        {
            await _dbContext.Topics.AddAsync(entity);
        }

        public void Delete(Topic entity)
        {
            _dbContext.Topics.Remove(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await Task.Run(() => _dbContext.Topics.Remove(_dbContext.Topics.Find(id)));
        }

        public async Task<IEnumerable<Topic>> GetAllAsync()
        {
            return await _dbContext.Topics.ToListAsync();
        }

        public async Task<IEnumerable<Topic>> GetAllWithDetailsAsync()
        {
            return await _dbContext.Topics
                .Include(t => t.Author)
                .Include(t => t.Responses)
                .Include(t => t.TopicTags)
                .ThenInclude(tt => tt.Tag)
                .Include(t => t.LikedBy)
                .ThenInclude(lt => lt.Liker)
                .ToListAsync();
        }

        public async Task<Topic> GetByIdAsync(int id)
        {
            return await _dbContext.Topics.FindAsync(id);
        }

        public async Task<Topic> GetByIdWithDetailsAsync(int id)
        {
            return await _dbContext.Topics
                .Include(t => t.Author)
                .Include(t => t.Responses)
                .Include(t => t.TopicTags)
                .ThenInclude(tt => tt.Tag)
                .Include(t => t.LikedBy)
                .ThenInclude(lt => lt.Liker)
                .FirstAsync(t => t.Id == id);
        }

        public void Update(Topic entity)
        {
            Topic findingTopic = _dbContext.Topics.Find(entity);

            if (findingTopic != null)
            {
                _dbContext.Entry(findingTopic).CurrentValues.SetValues(entity);
            }
        }
    }
}
