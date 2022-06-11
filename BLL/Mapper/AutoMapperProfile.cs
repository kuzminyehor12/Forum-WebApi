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
               .ForMember(tm => tm.TopicTagIds, t => t.MapFrom(x => x.TopicTags.Select(tt => tt.Id)))
               .ReverseMap();

            CreateMap<Response, ResponseModel>()
               .ForMember(rm => rm.CommentIds, r => r.MapFrom(x => x.Comments.Select(c => c.Id)))
               .ReverseMap();

            CreateMap<User, UserModel>()
               .ForMember(um => um.TopicIds, u => u.MapFrom(x => x.Topics.Select(c => c.Id)))
               .ForMember(um => um.ResponseIds, u => u.MapFrom(x => x.Responses.Select(c => c.Id)))
               .ForMember(um => um.CommentIds, u => u.MapFrom(x => x.Comments.Select(c => c.Id)))
               .ReverseMap();

            CreateMap<Tag, TagModel>()
               .ForMember(tm => tm.TopicTagIds, t => t.MapFrom(x => x.TopicTags.Select(tt => tt.Id)))
               .ReverseMap();
        }
    }
}
