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
    public class TagRepository : ITagRepository
    {
        private ForumDataContext _dbContext;
        public TagRepository(ForumDataContext context)
        {
            _dbContext = context;
        }
        public async Task AddAsync(Tag entity)
        {
            await _dbContext.Tags.AddAsync(entity);
        }

        public void Delete(Tag entity)
        {
            _dbContext.Tags.Remove(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await Task.Run(() => _dbContext.Tags.Remove(_dbContext.Tags.Find(id)));
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            return await _dbContext.Tags.ToListAsync();
        }

        public async Task<IEnumerable<Tag>> GetAllWithDetailsAsync()
        {
            return await _dbContext.Tags
                .Include(t => t.TopicTags)
                .ThenInclude(tt => tt.Topic)
                .ToListAsync();
        }

        public async Task<Tag> GetByIdAsync(int id)
        {
            return await _dbContext.Tags.FindAsync(id);
        }

        public async Task<Tag> GetByIdWithDetailsAsync(int id)
        {
            return await _dbContext.Tags
               .Include(t => t.TopicTags)
               .ThenInclude(tt => tt.Topic)
               .FirstAsync(t => t.Id == id);
        }

        public void Update(Tag entity)
        {
            Tag findingTag = _dbContext.Tags.Find(entity.Id);

            if (findingTag != null)
            {
                _dbContext.Entry(findingTag).CurrentValues.SetValues(entity);
            }
        }
    }
}
