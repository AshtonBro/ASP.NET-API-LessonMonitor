using AutoMapper;
using LessonMonitor.Core.CoreModels;
using System.Collections.Generic;

namespace LessonMonitor.DataAccess.MSSQL
{
    public class MemberStatisticConverter : ITypeConverter<Entities.Member, MemberStatistic[]>
    {
        public MemberStatistic[] Convert(
            Entities.Member source,
            MemberStatistic[] destination,
            ResolutionContext context)
        {
            var result = new List<MemberStatistic>();

            foreach (var visitedLesson in source.VisitedLessons)
            {
                var statistic = new Core.CoreModels.MemberStatistic
                {
                    MemberName = source.Name,
                    LessonDate = visitedLesson.Lesson.StartDate,
                    LessonTitle = visitedLesson.Lesson.Title,
                    LessonVisitedDate = visitedLesson.Date,
                    QuestiontsQuantity = visitedLesson.Questions.Count,
                    TimecodesQuantity = visitedLesson.Timecodes.Count
                };

                result.Add(statistic);
            }

            return result.ToArray();
        }
    }
}
