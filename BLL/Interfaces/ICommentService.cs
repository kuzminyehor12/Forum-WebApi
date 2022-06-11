using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BLL.Models;

namespace BLL.Interfaces
{
    public interface ICommentService : IService<CommentModel>
    {
        Task LikeComment(int commentId);
        Task CompainAboutComment(int commentId);
    }
}
