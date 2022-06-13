using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAL.Data;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class LikerTopicRepository : ILikerTopicRepository
    {
        private ForumDataContext _dbContext;
        public LikerTopicRepository(ForumDataContext context)
        {
            _dbContext = context;
        }
        public async Task AddAsync(LikerTopic entity)
        {
            await _dbContext.LikerTopics.AddAsync(entity);
        }

        public void Delete(LikerTopic entity)
        {
            _dbContext.LikerTopics.Remove(entity);
        }
    }
}
