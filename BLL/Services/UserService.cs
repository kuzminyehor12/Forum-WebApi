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
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly AbstractValidator<UserModel> _validator;
        public UserService(IUnitOfWork uow, IMapper mapper, AbstractValidator<UserModel> validator)
        {
            _uow = uow;
            _mapper = mapper;
            _validator = validator;
        }
        public async Task AddAsync(UserModel model)
        {
            if(_validator.Validate(model).Errors.Count != 0)
            {
                throw new ForumException("The user has incorrect data!");
            }

            var user = _mapper.Map<User>(model);

            await _uow.UserRepository.AddAsync(user);
            await _uow.SaveAsync();
        }

        public async Task DeleteAsync(int modelId)
        {
            await _uow.UserRepository.DeleteByIdAsync(modelId);
            await _uow.SaveAsync();
        }

        public async Task<IEnumerable<UserModel>> GetAllAsync()
        {
            var users = await _uow.UserRepository.GetAllWithDetailsAsync();
            return _mapper.Map<IEnumerable<UserModel>>(users);
        }

        public async Task<UserModel> GetByIdAsync(int id)
        {
            var user = await _uow.UserRepository.GetByIdWithDetailsAsync(id);
            return _mapper.Map<UserModel>(user);
        }

        public async Task<IEnumerable<CommentModel>> GetCommentsByUserIdAsync(int userId)
        {
            var users = await _uow.UserRepository.GetAllWithDetailsAsync();
            return _mapper.Map<IEnumerable<CommentModel>>(users.Select(u => u.CreatedComments));
        }

        public async Task<IEnumerable<ResponseModel>> GetResponsesByUserIdAsync(int userId)
        {
            var users = await _uow.UserRepository.GetAllWithDetailsAsync();
            return _mapper.Map<IEnumerable<ResponseModel>>(users.Select(u => u.CreatedResponses));
        }

        public async Task<IEnumerable<TopicModel>> GetTopicsByUserIdAsync(int userId)
        {
            var users = await _uow.UserRepository.GetAllWithDetailsAsync();
            return _mapper.Map<IEnumerable<TopicModel>>(users.Select(u => u.CreatedTopics));
        }

        public async Task LikeResponseAsync(LikerResponseModel model)
        {
            var likerResponse = _mapper.Map<LikerResponse>(model);
            await _uow.LikerResponseRepository.AddAsync(likerResponse);
            await _uow.SaveAsync();
        }

        public async Task LikeTopicAsync(LikerTopicModel model)
        {
            var likerTopic = _mapper.Map<LikerTopic>(model);
            await _uow.LikerTopicRepository.AddAsync(likerTopic);
            await _uow.SaveAsync();
        }

        public async Task RemoveLikeResponseAsync(LikerResponseModel model)
        {
            var likerResponse = _mapper.Map<LikerResponse>(model);
            await Task.Run(() => _uow.LikerResponseRepository.Delete(likerResponse));
            await _uow.SaveAsync();
        }

        public async Task RemoveLikeTopicAsync(LikerTopicModel model)
        {
            var likerTopic = _mapper.Map<LikerTopic>(model);
            await Task.Run(() => _uow.LikerTopicRepository.Delete(likerTopic));
            await _uow.SaveAsync();
        }

        public async Task UpdateAsync(UserModel model)
        {
            if (_validator.Validate(model).Errors.Count != 0)
            {
                throw new ForumException("The user has incorrect data!");
            }

            var user = _mapper.Map<User>(model);

            _uow.UserRepository.Update(user);
            await _uow.SaveAsync();
        }
    }
}
