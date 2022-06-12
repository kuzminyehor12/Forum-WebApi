using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface ILikerTopicRepository
    {
        Task AddAsync(LikerTopic entity);
        void Delete(LikerTopic entity);
        Task DeleteByCompositeKey(Tuple<int, int> compositeKey);
    }
}
