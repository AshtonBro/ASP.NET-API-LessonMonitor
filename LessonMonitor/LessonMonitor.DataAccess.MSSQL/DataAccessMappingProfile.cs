using AutoMapper;
using LessonMonitor.Core.CoreModels;

namespace LessonMonitor.DataAccess.MSSQL
{
    public class DataAccessMappingProfile : Profile
    {
        public DataAccessMappingProfile()
        {
            CreateMap<Member, Entities.Member>().ReverseMap();
            CreateMap<Lesson, Entities.Lesson>().ReverseMap();

            CreateMap<Entities.Member, MemberStatistic[]>()
                .ConvertUsing(new MemberStatisticConverter());

            CreateMap<Entities.GithubAccount, GitHubAccount>();
        }
    }
}
