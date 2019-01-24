using AutoMapper;
using Domain;
using TestApp.Models;

namespace TestApp.Mappers
{
    public class MappingEntity : Profile
    {
        public MappingEntity()
        {
            CreateMap<PostModel, Post>();
            CreateMap<CommentModel, Comment>();

            CreateMap<Post, PostModel>();
            CreateMap<Comment, CommentModel>();
        }
    }
}
