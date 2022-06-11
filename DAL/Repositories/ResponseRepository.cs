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
    public class ResponseRepository : IResponseRepository
    {
        private ForumDataContext _dbContext;
        public ResponseRepository(ForumDataContext context)
        {
            _dbContext = context;
        }
        public async Task AddAsync(Response entity)
        {
            await _dbContext.Responses.AddAsync(entity);
        }

        public void Delete(Response entity)
        {
            _dbContext.Responses.Remove(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await Task.Run(() => _dbContext.Responses.Remove(_dbContext.Responses.Find(id)));
        }

        public async Task<IEnumerable<Response>> GetAllAsync()
        {
            return await _dbContext.Responses.ToListAsync();
        }

        public async Task<IEnumerable<Response>> GetAllWithDetailsAsync()
        {
            return await _dbContext.Responses
                .Include(r => r.Author)
                .Include(r => r.Topic)
                .Include(r => r.Comments)
                .ToListAsync();
        }

        public async Task<Response> GetByIdAsync(int id)
        {
            return await _dbContext.Responses.FindAsync(id);
        }

        public async Task<Response> GetByIdWithDetailsAsync(int id)
        {
            return await _dbContext.Responses
                .Include(r => r.Author)
                .Include(r => r.Topic)
                .Include(r => r.Comments)
                .FirstAsync(r => r.Id == id);
        }

        public void Update(Response entity)
        {
            Response findingResponse = _dbContext.Responses.Find(entity);

            if (findingResponse != null)
            {
                _dbContext.Entry(findingResponse).CurrentValues.SetValues(entity);
            }
        }
    }
}
