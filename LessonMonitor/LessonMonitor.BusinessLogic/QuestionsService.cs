using LessonMonitor.Core.CoreModels;
using LessonMonitor.Core.Repositories;
using LessonMonitor.Core.Services;
using System;
using System.Threading.Tasks;

namespace LessonMonitor.BusinessLogic
{
    public class QuestionsService : IQuestionsService
    {
        private readonly IQuestionsRepository _questionsRepository;

        public const string QUESTION_IS_INVALID = "Question properties not be null or whitespace!";

        public QuestionsService(IQuestionsRepository questionsRepository)
        {
            _questionsRepository = questionsRepository;
        }

        public async Task<int> Create(Question question)
        {
            if (question is null) throw new ArgumentNullException(nameof(question));

            var isInvalid = string.IsNullOrWhiteSpace(question.MemberId.ToString())
               || string.IsNullOrWhiteSpace(question.Description)
               || question.MemberId == default;

            if (isInvalid) throw new ArgumentNullException(nameof(question));

            var questionId = await _questionsRepository.Add(question);

            return questionId;
        }

        public async Task<Question> Get(int questionId)
        {
            if (questionId == default || questionId <= 0)
                throw new ArgumentNullException(nameof(questionId));

            var question = await _questionsRepository.Get(questionId);

            if (question != null)
            {
                return question;
            }
            else
            {
                throw new ArgumentNullException(nameof(questionId));
            }
        }

        public async Task<Question[]> Get()
        {
            var questions = await _questionsRepository.Get();

            if (questions is null || questions.Length == 0) throw new ArgumentNullException(nameof(questions));

            return questions;
        }
    }
}