using System;
using LessonMonitor.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using AutoMapper;

namespace LessonMonitor.DataAccess.MSSQL.Repositories
{
    public class QuestionsRepository : IQuestionsRepository
    {
        private LessonMonitorDbContext _context;
        private readonly IMapper _mapper;

        public QuestionsRepository(LessonMonitorDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Add(Core.CoreModels.Question newQuestion)
        {
            if (newQuestion is null)
                throw new ArgumentNullException(nameof(newQuestion));

            var newQuestionEntity = _mapper.Map<Core.CoreModels.Question, Entities.Question>(newQuestion);

            await _context.AddAsync(newQuestionEntity);
            await _context.SaveChangesAsync();

            return newQuestionEntity.Id;
        }

        public async Task<Core.CoreModels.Question> Get(int questionId)
        {
            if (questionId == default)
                throw new ArgumentException(nameof(questionId));

            var questionExist = await _context.Questions.SingleOrDefaultAsync(f => f.Id == questionId);

            if (questionExist != null)
            {
                return _mapper.Map<Entities.Question, Core.CoreModels.Question>(questionExist);
            }
            else
            {
                return null;
            }
        }

        public async Task<Core.CoreModels.Question[]> Get()
        {

            var getQuestions = await _context.Questions.ToArrayAsync();

            if (getQuestions.Length != 0 || getQuestions is null)
            {
                return _mapper.Map<Entities.Question[], Core.CoreModels.Question[]>(getQuestions);
            }
            else
            {
                return null;
            }
        }
    }
}
