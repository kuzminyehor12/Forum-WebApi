using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUnitOfWork
    {
        ITopicRepository TopicRepository { get; }
        IResponseRepository ResponseRepository { get; }
        ICommentRepository CommentRepository { get; }
        IUserRepository UserRepository { get; }
        ITagRepository TagRepository { get; }
        ITopicTagRepository TopicTagRepository { get; }
        ILikerResponseRepository LikerResponseRepository { get; }
        ILikerTopicRepository LikerTopicRepository { get; }
        Task SaveAsync();
    }
}
