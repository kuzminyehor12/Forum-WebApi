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
        Task<IEnumerable<UserCredentials>> GetCredentials();
        Task LikeTopicAsync(LikerTopicModel model);
        Task LikeResponseAsync(LikerResponseModel model);
        Task RemoveLikeTopicAsync(LikerTopicModel model);
        Task RemoveLikeResponseAsync(LikerResponseModel model);
    }
}
