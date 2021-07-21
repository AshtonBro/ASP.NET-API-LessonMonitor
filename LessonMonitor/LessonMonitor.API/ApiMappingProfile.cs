using AutoMapper;

namespace LessonMonitor.API
{
    public class ApiMappingProfile : Profile
    {
        public ApiMappingProfile()
        {
            CreateMap<Contracts.NewMember, Core.CoreModels.Member>();
            CreateMap<Core.CoreModels.Member, Contracts.Member>().ReverseMap();

            CreateMap<Contracts.NewHomework, Core.CoreModels.Homework>();
            CreateMap<Core.CoreModels.Homework, Contracts.Homework>().ReverseMap();

            CreateMap<Contracts.NewLesson, Core.CoreModels.Lesson>();
            CreateMap<Core.CoreModels.Lesson, Contracts.Lesson>().ReverseMap();

            CreateMap<Contracts.NewQuestion, Core.CoreModels.Question>();
            CreateMap<Core.CoreModels.Question, Contracts.Question>().ReverseMap();
        }
    }
}
