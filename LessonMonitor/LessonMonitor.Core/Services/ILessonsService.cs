using LessonMonitor.Core.CoreModels;
using System.Threading.Tasks;

namespace LessonMonitor.Core.Services
{
    public interface ILessonsService
    {
        Task<int> Create(Lesson newLesson);
    }
}
