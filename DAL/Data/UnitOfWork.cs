using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using DAL.Repositories;

namespace DAL.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private ForumDataContext _dbContext;

        private ITopicRepository _topicRepository;
        private IResponseRepository _responseRepository;
        private ICommentRepository _commentRepository;
        private IUserRepository _userRepository;
        private ITagRepository _tagRepository;
        private ITopicTagRepository _topicTagRepository;
        private ILikerResponseRepository _likerResponseRepository;
        private ILikerTopicRepository _likerTopicRepository;

        public UnitOfWork(ForumDataContext context)
        {
            _dbContext = context;
        }
        public ITopicRepository TopicRepository
        {
            get
            {
                if(_topicRepository is null)
                {
                    _topicRepository = new TopicRepository(_dbContext);
                }

                return _topicRepository;
            }
        }
        public IResponseRepository ResponseRepository
        {
            get
            {
                if (_responseRepository is null)
                {
                    _responseRepository = new ResponseRepository(_dbContext);
                }

                return _responseRepository;
            }
        }

        public ICommentRepository CommentRepository
        {
            get
            {
                if (_commentRepository is null)
                {
                    _commentRepository = new CommentRepository(_dbContext);
                }

                return _commentRepository;
            }
        }

        public IUserRepository UserRepository
        {
            get
            {
                if (_userRepository is null)
                {
                    _userRepository = new UserRepository(_dbContext);
                }

                return _userRepository;
            }
        }

        public ITagRepository TagRepository
        {
            get
            {
                if (_tagRepository is null)
                {
                    _tagRepository = new TagRepository(_dbContext);
                }

                return _tagRepository;
            }
        }

        public ITopicTagRepository TopicTagRepository
        {
            get
            {
                if(_topicTagRepository is null)
                {
                    _topicTagRepository = new TopicTagRepository(_dbContext);
                }

                return _topicTagRepository;
            }
        }

        public ILikerResponseRepository LikerResponseRepository
        {
            get
            {
                if(_likerResponseRepository is null)
                {
                    _likerResponseRepository = new LikerResponseRepository(_dbContext);
                }

                return _likerResponseRepository;
            }
        }

        public ILikerTopicRepository LikerTopicRepository
        {
            get
            {
                if (_likerTopicRepository is null)
                {
                    _likerTopicRepository = new LikerTopicRepository(_dbContext);
                }

                return _likerTopicRepository;
            }
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
