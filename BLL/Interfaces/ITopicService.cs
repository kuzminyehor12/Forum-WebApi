using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BLL.Models;

namespace BLL.Interfaces
{
    public interface ITopicService : IService<TopicModel>
    {
        Task<IEnumerable<TopicModel>> GetByFilterAsync(FilterModel filter);
        Task<IEnumerable<TopicModel>> SortByPublicationDate();
        Task<IEnumerable<TopicModel>> SortByLikes();
        Task LikeTopicAsync(int topicId);
        Task CompainAboutTopicAsync(int topicId);
    }
}
