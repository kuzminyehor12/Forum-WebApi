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
        Task<IEnumerable<TagModel>> GetTags();
        Task<IEnumerable<TopicModel>> SortByPublicationDate();
        Task<IEnumerable<TopicModel>> SortByLikes();
        Task ComplainAboutTopicAsync(int topicId);
        Task AddTag(TopicTagModel model);
        Task RemoveTag(TopicTagModel model);
    }
}
