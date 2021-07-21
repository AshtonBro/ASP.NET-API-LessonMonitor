using AutoFixture;
using AutoMapper;
using LessonMonitor.DataAccess.MSSQL.Repository;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;

namespace LessonMonitor.DataAccess.MSSQL.XTests
{
    public class MembersRepositoryXTests
    {
        private LMonitorDbContext _context;
        private MembersRepository _repository;
        private IMapper _mapper;

        public MembersRepositoryXTests() 
        {
            var optionsBuilder = new DbContextOptionsBuilder<LMonitorDbContext>();

            var options = optionsBuilder
                    .UseSqlServer(@"Data Source=ASHTON\ASHTON;Initial Catalog=LessonMonitorDbMainTest;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
                    .Options;

            _context = new LMonitorDbContext(options);

            var configuration = new MapperConfiguration(cfg => {
                cfg.AddProfile<DataAccessMapperProfile>();
            });

            configuration.CompileMappings();

            _mapper = new Mapper(configuration);

            _repository = new MembersRepository(_context, _mapper);
        }

        [Fact]
        public async Task Add_ValidMember_ShouldCreateNewMember()
        {
            // arrange
            var fixture = new Fixture();
            var member = fixture.Build<Core.CoreModels.Member>()
                .Without(x => x.Id)
                .Create();
            
            // act
            var memberId = await _repository.Add(member);

            // assert
            Assert.True(memberId > 0);
        }

        [Fact]
        public async Task Update()
        {
            // arrange
            var fixture = new Fixture();
            var member = fixture.Build<Core.CoreModels.Member>()
                .Without(x => x.Id)
                .Create();

            var memberkId = await _repository.Add(member);

            var updatedMember = fixture.Build<Core.CoreModels.Member>().Create();
            updatedMember.Id = memberkId;

            // act
            var updatedMemberId = await _repository.Update(updatedMember);
            var updatedMemberGet = await _repository.Get(updatedMemberId);
            // assert
            Assert.Equal(memberkId, updatedMemberGet.Id);
            Assert.NotEqual(updatedMemberGet.Name, member.Name);
            Assert.NotEqual(updatedMemberGet.YouTubeAccountId, member.YouTubeAccountId);
        }

        [Fact]
        public async Task Get()
        {
            // arrange
            // act
            var members = await _repository.Get();

            // assert
            Assert.NotNull(members);
            Assert.NotEmpty(members);
        }

        [Fact]
        public async Task GetMemberWithId_ShuldReturnMemberWithUser()
        {
            // arrange
            var fixture = new Fixture();
            var member = fixture.Build<Core.CoreModels.Member>()
                .Without(x => x.Id)
                .Create();

            // act
            var memberId = await _repository.Add(member);
            var memberGetted = await _repository.Get(memberId);

            // assert
            Assert.NotNull(memberGetted);
        }

        [Fact]
        public async Task Delete()
        {
            var fixture = new Fixture();
            var member = fixture.Build<Core.CoreModels.Member>()
                .Without(x => x.Id)
                .Create();

            // act
            var memberId = await _repository.Add(member);
            var result = await _repository.Delete(memberId);
            var memberGetted = await _repository.Get(memberId);

            // assert
            Assert.True(result);
            Assert.Null(memberGetted);
        }
    }
}
