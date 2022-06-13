using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAL.Data;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class TopicTagRepository : ITopicTagRepository
    {
        private ForumDataContext _dbContext;
        public TopicTagRepository(ForumDataContext context)
        {
            _dbContext = context;
        }
        public async Task AddAsync(TopicTag entity)
        {
            await _dbContext.TopicTags.AddAsync(entity);
        }

        public void Delete(TopicTag entity)
        {
            _dbContext.TopicTags.Remove(entity);
        }
    }
}
