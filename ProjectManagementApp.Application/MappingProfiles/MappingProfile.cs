
using AutoMapper;
using ProjectManagementApp.Application.DTO;
using ProjectManagementApp.Domain.Entities;


namespace ProjectManagementApp.Core.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Tasks, TaskResponseDto>().ReverseMap();
            CreateMap<Project, ProjectResponseDto>().ReverseMap();
            CreateMap<CreateTaskDto, Tasks>();
            CreateMap<UpdateTaskDto, Tasks>();
            CreateMap<CreateProjectDto, Project>();
            CreateMap<UpdateProjectDto, Project>();
            CreateMap<CommentDto, Comment>();
        }
    }
}
