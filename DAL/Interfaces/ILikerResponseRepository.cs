using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface ILikerResponseRepository
    {
        Task AddAsync(LikerResponse entity);
        void Delete(LikerResponse entity);
    }
}
