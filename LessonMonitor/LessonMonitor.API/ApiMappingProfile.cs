using AutoMapper;

namespace LessonMonitor.API
{
    public class ApiMappingProfile : Profile
    {
        public ApiMappingProfile()
        {
            CreateMap<Contracts.NewMember, Core.CoreModels.Member>();
            CreateMap<Core.CoreModels.Member, Contracts.Member>().ReverseMap();

            CreateMap<Contracts.NewHomework, Core.CoreModels.Homework>().ReverseMap();
            CreateMap<Core.CoreModels.Homework, Contracts.Homework>().ReverseMap();

            CreateMap<Contracts.NewLesson, Core.CoreModels.Lesson>().ReverseMap();
            CreateMap<Core.CoreModels.Lesson, Contracts.Lesson>().ReverseMap();
        }
    }
}
