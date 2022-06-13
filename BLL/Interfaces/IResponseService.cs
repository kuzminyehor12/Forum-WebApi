using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BLL.Models;

namespace BLL.Interfaces
{
    public interface IResponseService : IService<ResponseModel>
    {
        Task ComplainAboutResponseAsync(int responseId);
        Task<IEnumerable<ResponseModel>> SortByPublicationDate();
        Task<IEnumerable<ResponseModel>> SortByLikes();
    }
}
