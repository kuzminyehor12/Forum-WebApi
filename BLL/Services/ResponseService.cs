using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using BLL.Validation;
using DAL.Entities;
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
        public async Task AddAsync(ResponseModel model)
        {
            if (_validator.Validate(model).Errors.Count != 0)
            {
                throw new ForumException("The response has incorrect data!");
            }

            var response = _mapper.Map<Response>(model);

            await _uow.ResponseRepository.AddAsync(response);
            await _uow.SaveAsync();
        }

        public async Task ComplainAboutResponseAsync(int responseId)
        {
            var response = await _uow.ResponseRepository.GetByIdWithDetailsAsync(responseId);
            response.Complaints++;

            _uow.ResponseRepository.Update(response);
            await _uow.SaveAsync();
        }

        public async Task DeleteAsync(int modelId)
        {
            await _uow.ResponseRepository.DeleteByIdAsync(modelId);
            await _uow.SaveAsync();
        }

        public async Task<IEnumerable<ResponseModel>> GetAllAsync()
        {
            var responses = await _uow.ResponseRepository.GetAllWithDetailsAsync();
            return _mapper.Map<IEnumerable<ResponseModel>>(responses);
        }

        public async Task<ResponseModel> GetByIdAsync(int id)
        {
            var response = await _uow.ResponseRepository.GetByIdWithDetailsAsync(id);
            return _mapper.Map<ResponseModel>(response);
        }

        public async Task<IEnumerable<ResponseModel>> SortByLikes()
        {
            var responses = await _uow.ResponseRepository.GetAllWithDetailsAsync();
            return _mapper.Map<IEnumerable<ResponseModel>>(responses.OrderBy(r => r.LikedBy.Count));
        }

        public async Task<IEnumerable<ResponseModel>> SortByPublicationDate()
        {
            var responses = await _uow.ResponseRepository.GetAllWithDetailsAsync();
            return _mapper.Map<IEnumerable<ResponseModel>>(responses.OrderBy(r => r.PublicationDate));
        }

        public async Task UpdateAsync(ResponseModel model)
        {
            if (_validator.Validate(model).Errors.Count != 0)
            {
                throw new ForumException("The response has incorrect data!");
            }

            var response = _mapper.Map<Response>(model);

            _uow.ResponseRepository.Update(response);
            await _uow.SaveAsync();
        }
    }
}
