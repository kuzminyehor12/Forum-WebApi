using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using BLL.Models;
using DAL.Entities;

namespace BLL.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Topic, TopicModel>()
               .ForMember(tm => tm.ResponsesIds, t => t.MapFrom(x => x.Responses.Select(r => r.Id)))
               .ForMember(tm => tm.TopicTagIds, t => t.MapFrom(x => x.TopicTags.Select(tt => new Tuple<int, int>(tt.TagId, tt.TopicId))))
               .ForMember(rm => rm.LikedByIds, r => r.MapFrom(x => x.LikedBy.Select(lt => new Tuple<int, int>(lt.UserId, lt.TopicId))))
               .ReverseMap();

            CreateMap<Response, ResponseModel>()
               .ForMember(rm => rm.CommentIds, r => r.MapFrom(x => x.Comments.Select(c => c.Id)))
               .ForMember(rm => rm.LikedByIds, r => r.MapFrom(x => x.LikedBy.Select(lr => new Tuple<int, int>(lr.UserId, lr.ResponseId))))
               .ReverseMap();

            CreateMap<User, UserModel>()
               .ForMember(um => um.CreatedTopicIds, u => u.MapFrom(x => x.CreatedTopics.Select(c => c.Id)))
               .ForMember(um => um.CreatedResponseIds, u => u.MapFrom(x => x.CreatedResponses.Select(c => c.Id)))
               .ForMember(um => um.CreatedCommentIds, u => u.MapFrom(x => x.CreatedComments.Select(c => c.Id)))
               .ForMember(um => um.LikedResponseIds, u => u.MapFrom(x => x.LikedResponses.Select(lr =>
                    new LikerResponseModel { UserId = lr.UserId, ResponseId = lr.ResponseId })))
               .ForMember(um => um.LikedTopicIds, u => u.MapFrom(x => x.LikedTopics.Select(lt => 
                    new LikerTopicModel { UserId = lt.UserId, TopicId = lt.TopicId })))
               .ForMember(um => um.Email, u => u.MapFrom(x => x.UserCredentials.Email))
               .ForMember(um => um.Nickname, u => u.MapFrom(x => x.UserCredentials.UserName))
               .ReverseMap();

            CreateMap<Tag, TagModel>()
               .ForMember(tm => tm.TopicTagIds, t => t.MapFrom(x => x.TopicTags.Select(tt => new Tuple<int, int>(tt.TagId, tt.TopicId))))
               .ReverseMap();
        }
    }
}
