using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BLL.Models;
using DAL.Entities;

namespace BLL.Interfaces
{
    public interface IUserService : IService<UserModel>
    {
        Task<IEnumerable<ResponseModel>> GetResponsesByUserIdAsync(int userId);
        Task<IEnumerable<TopicModel>> GetTopicsByUserIdAsync(int userId);
        Task<IEnumerable<CommentModel>> GetCommentsByUserIdAsync(int userId);
        Task LikeTopicAsync(LikerTopicModel model);
        Task LikeResponseAsync(LikerResponseModel model);
        Task RemoveLikeTopicAsync(LikerTopicModel model);
        Task RemoveLikeResponseAsync(LikerResponseModel model);
        //Task AddTopic(int userId, int topicId);
        //Task AddResponse(int userId, int topicId, int responseId);
        //Task AddComment(int userId, int responseId, int commentId);
    }
}
