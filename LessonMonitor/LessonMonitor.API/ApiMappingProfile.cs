using AutoMapper;
using LessonMonitor.Core.CoreModels;

namespace LessonMonitor.API
{
    public class ApiMappingProfile : Profile
    {
        public ApiMappingProfile()
        {
            CreateMap<Contracts.NewMember, Member>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<Member, Contracts.Member>().ReverseMap();

            CreateMap<Contracts.NewLesson, Lesson>();
            CreateMap<Lesson, Contracts.Lesson>().ReverseMap();

            CreateMap<Contracts.Question, Question>();
            CreateMap<Question, Contracts.Question>().ReverseMap();

            CreateMap<Contracts.Homework, Homework>();
            CreateMap<Homework, Contracts.Homework>().ReverseMap();
        }
    }
}
