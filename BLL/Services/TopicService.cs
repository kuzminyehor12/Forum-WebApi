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
    public class TopicService : ITopicService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly AbstractValidator<TopicModel> _validator;
        public TopicService(IUnitOfWork uow, IMapper mapper, AbstractValidator<TopicModel> validator)
        {
            _uow = uow;
            _mapper = mapper;
            _validator = validator;
        }
        public async Task AddAsync(TopicModel model)
        {
            if(_validator.Validate(model).Errors.Count != 0)
            {
                throw new ForumException("The topic has incorrect data!");
            }

            var topic = _mapper.Map<Topic>(model);

            await _uow.TopicRepository.AddAsync(topic);
            await _uow.SaveAsync();
        }

        public async Task CompainAboutTopicAsync(int topicId)
        {
            var topic = await _uow.TopicRepository.GetByIdWithDetailsAsync(topicId);
            topic.Complaints++;

            _uow.TopicRepository.Update(topic);
            await _uow.SaveAsync();
        }

        public async Task DeleteAsync(int modelId)
        {
            await _uow.TopicRepository.DeleteByIdAsync(modelId);
            await _uow.SaveAsync();
        }

        public async Task<IEnumerable<TopicModel>> GetAllAsync()
        {
            var topics = await _uow.TopicRepository.GetAllWithDetailsAsync();
            return _mapper.Map<IEnumerable<TopicModel>>(topics);
        }

        public async Task<IEnumerable<TopicModel>> GetByFilterAsync(FilterModel filter)
        {
            var topics = await _uow.TopicRepository.GetAllWithDetailsAsync();

            if (filter != null)
            {
                if (filter.PublicationDate.HasValue)
                {
                    topics = topics.Where(t => t.PublicationDate == filter.PublicationDate);
                }

                if (filter.TagIds != null)
                {
                    topics = topics.Where(t => t.TopicTags
                            .Select(tt => tt.TagId)
                            .Intersect(filter.TagIds) != null);
                }
            }

            return _mapper.Map<IEnumerable<TopicModel>>(topics);
        }

        public async Task<TopicModel> GetByIdAsync(int id)
        {
            var topic = await _uow.TopicRepository.GetByIdWithDetailsAsync(id);
            return _mapper.Map<TopicModel>(topic);
        }

        public Task LikeTopicAsync(int topicId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TopicModel>> SortByLikes()
        {
            throw new NotImplementedException();
        }

        //public async Task LikeTopicAsync(int topicId)
        //{
        //    var topic = await _uow.TopicRepository.GetByIdWithDetailsAsync(topicId);
        //    topic.Likes++;

        //    _uow.TopicRepository.Update(topic);
        //    await _uow.SaveAsync();
        //}

        //public async Task<IEnumerable<TopicModel>> SortByLikes()
        //{
        //    var topics = await _uow.TopicRepository.GetAllWithDetailsAsync();
        //    return _mapper.Map<IEnumerable<TopicModel>>(topics.OrderBy(t => t.PublicationDate));
        //}

        public async Task<IEnumerable<TopicModel>> SortByPublicationDate()
        {
            var topics = await _uow.TopicRepository.GetAllWithDetailsAsync();
            return _mapper.Map<IEnumerable<TopicModel>>(topics.OrderBy(t => t.PublicationDate));
        }

        public async Task UpdateAsync(TopicModel model)
        {
            if (_validator.Validate(model).Errors.Count != 0)
            {
                throw new ForumException("The topic has incorrect data!");
            }

            var topic = _mapper.Map<Topic>(model);

            _uow.TopicRepository.Update(topic);
            await _uow.SaveAsync();
        }
    }
}
