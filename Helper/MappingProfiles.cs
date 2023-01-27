using Project.Dto;
using Project.Models;
using AutoMapper;

namespace Project.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Mark, MarkDto>();
            CreateMap<MarkDto, Mark>();
            CreateMap<Model, GetModelDto>();
            CreateMap<GetModelDto, Model>();
            CreateMap<Model, CreateModelDto>();
            CreateMap<CreateModelDto, Model>();
        }
    }
}