using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAL.Data;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class LikerResponseRepository : ILikerResponseRepository
    {

        private ForumDataContext _dbContext;
        public LikerResponseRepository(ForumDataContext context)
        {
            _dbContext = context;
        }
        public async Task AddAsync(LikerResponse entity)
        {
            await _dbContext.LikerResponses.AddAsync(entity);
        }

        public void Delete(LikerResponse entity)
        {
            _dbContext.LikerResponses.Remove(entity);
        }

        public async Task DeleteByCompositeKey(Tuple<int, int> compositeKey)
        {
            await Task.Run(() => _dbContext.LikerResponses.Remove(_dbContext.LikerResponses.Find(compositeKey.Item1, compositeKey.Item2)));
        }
    }
}
