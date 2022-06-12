using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAL.Data;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private ForumDataContext _dbContext;
        public UserRepository(ForumDataContext context)
        {
            _dbContext = context;
        }
        public async Task AddAsync(User entity)
        {
            await _dbContext.Users.AddAsync(entity);
        }

        public void Delete(User entity)
        {
            _dbContext.Users.Remove(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await Task.Run(() => _dbContext.Users.Remove(_dbContext.Users.Find(id)));
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<IEnumerable<User>> GetAllWithDetailsAsync()
        {
            return await _dbContext.Users
                .Include(u => u.CreatedTopics)
                .Include(u => u.CreatedResponses)
                .Include(u => u.CreatedComments)
                .Include(u => u.LikedTopics)
                .ThenInclude(lt => lt.Topic)
                .Include(u => u.LikedResponses)
                .ThenInclude(lr => lr.Response)
                .ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _dbContext.Users.FindAsync(id);
        }

        public async Task<User> GetByIdWithDetailsAsync(int id)
        {
            return await _dbContext.Users
               .Include(u => u.CreatedTopics)
               .Include(u => u.CreatedResponses)
               .Include(u => u.CreatedComments)
               .Include(u => u.LikedTopics)
               .ThenInclude(lt => lt.Topic)
               .Include(u => u.LikedResponses)
               .ThenInclude(lr => lr.Response)
               .FirstAsync(u => u.Id == id);
        }

        public void Update(User entity)
        {
            User findingUser = _dbContext.Users.Find(entity);

            if (findingUser != null)
            {
                _dbContext.Entry(findingUser).CurrentValues.SetValues(entity);
            }
        }
    }
}
