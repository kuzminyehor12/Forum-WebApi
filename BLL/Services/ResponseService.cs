using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Interfaces;
using FluentValidation;

namespace BLL.Services
{
    public class ResponseService : IResponseService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly AbstractValidator<ResponseModel> _validator;
        public ResponseService(IUnitOfWork uow, IMapper mapper, AbstractValidator<ResponseModel> validator)
        {
            _uow = uow;
            _mapper = mapper;
            _validator = validator;
        }
        public Task AddAsync(ResponseModel model)
        {
            throw new NotImplementedException();
        }

        public Task CompainAboutTopic(int responseId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int modelId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ResponseModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task LikeResponse(int responseId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ResponseModel>> SortByPublicationDate()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(ResponseModel model)
        {
            throw new NotImplementedException();
        }
    }
}
