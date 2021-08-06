using AutoFixture;
using AutoMapper;
using LessonMonitor.DataAccess.MSSQL.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;

namespace LessonMonitor.DataAccess.MSSQL.XTests
{
    public class QuestionsRepositoryXTests
    {
        private LessonMonitorDbContext _context;
        private QuestionsRepository _repository;
        private IMapper _mapper;

        public QuestionsRepositoryXTests() 
        {
            var optionsBuilder = new DbContextOptionsBuilder<LessonMonitorDbContext>();

            var options = optionsBuilder
                    .UseSqlServer(@"Data Source=ASHTON\ASHTON;Initial Catalog=LessonMonitorDbMainTest;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
                    .Options;

            _context = new LessonMonitorDbContext(options);

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DataAccessMapperProfile>();
            });

            configuration.CompileMappings();

            _mapper = new Mapper(configuration);

            _repository = new QuestionsRepository(_context, _mapper);
        }

        [Fact]
        public async Task Add_ValidQuestion_ShouldCreateNewQuestion()
        {
            // arrange
            var fixture = new Fixture();
            var question = fixture.Build<Core.CoreModels.Question>()
                .Without(x => x.Id)
                .Create();
            question.MemberId = 1;

            // act
            var questionId = await _repository.Add(question);

            // assert
            Assert.True(questionId > 0);
        }

        [Fact]
        public async Task Get()
        {
            // arrange\
            var fixture = new Fixture();

            for (int i = 0; i < 10; i++)
            {
                var question = fixture.Build<Core.CoreModels.Question>()
                    .Without(x => x.Id)
                    .Create();
                question.MemberId = 1;

                var questionId = await _repository.Add(question);
            }

            // act
            var questions = await _repository.Get();

            // assert
            Assert.NotNull(questions);
            Assert.NotEmpty(questions);
        }

        [Fact]
        public async Task GetMQuestionWithId_ShuldReturnQuestionWithUser()
        {
            // arrange
            var fixture = new Fixture();
            var question = fixture.Build<Core.CoreModels.Question>()
                .Without(x => x.Id)
                .Create();
            question.MemberId = 1;

            // act
            var questionId = await _repository.Add(question);
            var questionGetted = await _repository.Get(questionId);

            // assert
            Assert.NotNull(questionGetted);
        }
    }
}
