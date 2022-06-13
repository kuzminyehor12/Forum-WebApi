using System;
using System.Collections.Generic;
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
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly AbstractValidator<CommentModel> _validator;
        public CommentService(IUnitOfWork uow, IMapper mapper, AbstractValidator<CommentModel> validator)
        {
            _uow = uow;
            _mapper = mapper;
            _validator = validator;
        }
        public async Task AddAsync(CommentModel model)
        {
            if (_validator.Validate(model).Errors.Count != 0)
            {
                throw new ForumException("The comment has incorrect data!");
            }

            var comment = _mapper.Map<Comment>(model);

            await _uow.CommentRepository.AddAsync(comment);
            await _uow.SaveAsync();
        }

        public async Task ComplainAboutCommentAsync(int commentId)
        {
            var comment = await _uow.CommentRepository.GetByIdWithDetailsAsync(commentId);
            comment.Complaints++;

            _uow.CommentRepository.Update(comment);
            await _uow.SaveAsync();
        }

        public async Task DeleteAsync(int modelId)
        {
            await _uow.CommentRepository.DeleteByIdAsync(modelId);
            await _uow.SaveAsync();
        }

        public async Task<IEnumerable<CommentModel>> GetAllAsync()
        {
            var comments = await _uow.CommentRepository.GetAllWithDetailsAsync();
            return _mapper.Map<IEnumerable<CommentModel>>(comments);
        }

        public async Task<CommentModel> GetByIdAsync(int id)
        {
            var comment = await _uow.CommentRepository.GetByIdWithDetailsAsync(id);
            return _mapper.Map<CommentModel>(comment);
        }

        public async Task UpdateAsync(CommentModel model)
        {
            if (_validator.Validate(model).Errors.Count != 0)
            {
                throw new ForumException("The comment has incorrect data!");
            }

            var comment = _mapper.Map<Comment>(model);

            _uow.CommentRepository.Update(comment);
            await _uow.SaveAsync();
        }
    }
}
