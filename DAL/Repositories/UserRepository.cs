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
            await _dbContext.RegisteredUsers.AddAsync(entity);
        }

        public void Delete(User entity)
        {
            _dbContext.RegisteredUsers.Remove(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await Task.Run(() => _dbContext.RegisteredUsers.Remove(_dbContext.RegisteredUsers.Find(id)));
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _dbContext.RegisteredUsers.ToListAsync();
        }

        public async Task<IEnumerable<User>> GetAllWithDetailsAsync()
        {
            return await _dbContext.RegisteredUsers
                .Include(u => u.CreatedTopics)
                .Include(u => u.CreatedResponses)
                .Include(u => u.CreatedComments)
                .Include(u => u.LikedTopics)
                .ThenInclude(lt => lt.Topic)
                .Include(u => u.LikedResponses)
                .ThenInclude(lr => lr.Response)
                .Include(u => u.UserCredentials)
                .ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _dbContext.RegisteredUsers.FindAsync(id);
        }

        public async Task<User> GetByIdWithDetailsAsync(int id)
        {
            return await _dbContext.RegisteredUsers
               .Include(u => u.CreatedTopics)
               .Include(u => u.CreatedResponses)
               .Include(u => u.CreatedComments)
               .Include(u => u.LikedTopics)
               .ThenInclude(lt => lt.Topic)
               .Include(u => u.LikedResponses)
               .ThenInclude(lr => lr.Response)
               .Include(u => u.UserCredentials)
               .FirstAsync(u => u.Id == id);
        }

        public void Update(User entity)
        {
            _dbContext.RegisteredUsers.Update(entity);
        }
    }
}
