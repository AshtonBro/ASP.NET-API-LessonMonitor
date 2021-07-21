using AutoMapper;

namespace LessonMonitor.DataAccess.MSSQL
{
    public class DataAccessMapperProfile : Profile
    {
        public DataAccessMapperProfile()
        {
            CreateMap<Core.CoreModels.Member, Entities.Member>().ReverseMap();

            CreateMap<Core.CoreModels.Homework, Entities.Homework>().ReverseMap();

            CreateMap<Core.CoreModels.Lesson, Entities.Lesson>().ReverseMap();

            CreateMap<Core.CoreModels.Question, Entities.Question>().ReverseMap();
        }
    }
}