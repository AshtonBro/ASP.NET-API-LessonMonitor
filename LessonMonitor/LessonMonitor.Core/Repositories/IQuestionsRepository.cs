using LessonMonitor.Core.CoreModels;
using System.Threading.Tasks;

namespace LessonMonitor.Core.Repositories
{
    public interface IQuestionsRepository
    {
        Task<int> Add(Question newHomework);
        Task<Question> Get(int questionId);
        Task<Question[]> Get();
    }
}
