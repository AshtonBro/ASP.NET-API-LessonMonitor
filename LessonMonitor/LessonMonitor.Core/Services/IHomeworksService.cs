using LessonMonitor.Core.CoreModels;
using System.Threading.Tasks;

namespace LessonMonitor.Core.Services
{
    public interface IHomeworksService
    {
        Task<int> Create(Homework homework);

        Task<bool> Delete(int homeworkId);

        Task<Homework> Get(int homeworkId);

        Task<Homework[]> Get();

        Task<int> Update(Homework homework);
    }
}
