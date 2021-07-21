using System;
using LessonMonitor.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper;

namespace LessonMonitor.DataAccess.MSSQL.Repository
{
    public class MembersRepository : IMembersRepository
    {
        private LMonitorDbContext _context;
        private readonly IMapper _mapper;

        public MembersRepository(LMonitorDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Add(Core.CoreModels.Member newMember)
        {
            if (newMember is null)
                throw new ArgumentNullException(nameof(newMember));

            var newMemberEntity = _mapper.Map<Core.CoreModels.Member, Entities.Member>(newMember);

            await _context.AddAsync(newMemberEntity);
            await _context.SaveChangesAsync();

            return newMemberEntity.Id;
        }

        public async Task<bool> Delete(int memberId)
        {
            if (memberId == default)
                throw new ArgumentException(nameof(memberId));

            var memberExist = await _context.Members.SingleOrDefaultAsync(f => f.Id == memberId && f.DeletedDate == null);

            if (memberExist != null)
            {
                memberExist.DeletedDate = DateTime.Now;

                _context.Members.Update(memberExist);

                await _context.SaveChangesAsync();

                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<Core.CoreModels.Member> Get(int memberId)
        {
            if (memberId == default)
                throw new ArgumentException(nameof(memberId));

            var memberExist = await _context.Members.SingleOrDefaultAsync(f => f.Id == memberId && f.DeletedDate == null);

            if (memberExist != null)
            {
                return _mapper.Map<Entities.Member, Core.CoreModels.Member>(memberExist);
            }
            else
            {
                return null;
            }
        }

        public async Task<Core.CoreModels.Member[]> Get()
        {
            var members = await _context.Members.Where(f => f.DeletedDate == null).ToArrayAsync();

            if (members.Length != 0 || members is null)
            {
                return _mapper.Map<Entities.Member[], Core.CoreModels.Member[]>(members);
            }
            else
            {
                return null;
            }
        }

        public async Task<int> Update(Core.CoreModels.Member member)
        {
            if (member is null)
                throw new ArgumentNullException(nameof(member));

            var memberEntity = _context.Members.Where(f => f.Id == member.Id).FirstOrDefault();

            _context.Entry(memberEntity).State = EntityState.Modified;

            memberEntity.Id = member.Id;
            memberEntity.Name = member.Name;
            memberEntity.YouTubeAccountId = member.YouTubeAccountId;
            memberEntity.UpdatedDate = DateTime.Now;

            await _context.SaveChangesAsync();

            return memberEntity.Id;
        }

        public async Task<Core.CoreModels.Member> Get(string youTubeUserId)
        {
            if (youTubeUserId is null)
            {
                throw new ArgumentNullException(nameof(youTubeUserId));
            }

            var members = await _context.Members
                .AsNoTracking()
                .Select(x => new Core.CoreModels.Member
                {
                    Id = x.Id,
                    Name = x.Name,
                    YouTubeAccountId = x.YouTubeAccountId
                })
                .FirstOrDefaultAsync(x => x.YouTubeAccountId == youTubeUserId);

            return members;
        }
    }
}
