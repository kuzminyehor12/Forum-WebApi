using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface ITopicTagRepository
    {
        Task AddAsync(TopicTag entity);
        void Delete(TopicTag entity);
    }
}
