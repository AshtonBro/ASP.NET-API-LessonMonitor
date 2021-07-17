using LessonMonitor.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LessonMonitor.API.Contracts;
using System.Net;

namespace LessonMonitor.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MembersController : ControllerBase
    {
        private readonly IMembersService _membersService;

        public MembersController(IMembersService membersService)
        {
            _membersService = membersService;
        }

        [HttpPost]
        public async Task<ActionResult> Create(NewMember request)
        {
            var member = new Core.CoreModels.Member
            {
                Name = request.Name,
                YouTubeAccountId = request.YouTubeUserId
            };

            var memberId = await _membersService.Create(member);

            if (memberId == default)
                return BadRequest();

            return Ok(new CreatedMember { MemberId = memberId });
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int memberId)
        {
            var result = await _membersService.Delete(memberId);

            if (result)
            {
                return Ok(new { Successful = "Member is deleted" });
            }
            else
            {
                return NotFound(new { Error = "Member has already been deleted or not an invalid id" });
            }
        }

        [HttpGet("GetMemberById")]
        [ProducesResponseType(typeof(Member), (int)HttpStatusCode.OK)]
        public async Task<Member> Get(int memberId)
        {
            var member = await _membersService.Get(memberId);

            if (member is not null)
            {
                return new Contracts.Member
                {
                    Id = member.Id,
                    Name = member.Name,
                    YouTubeAccountId = member.YouTubeAccountId
                };
            }
            else
            {
                throw new ArgumentNullException("No member has been created");
            }
        }

        [HttpGet("GetAllMembers")]
        [ProducesResponseType(typeof(Member[]), (int)HttpStatusCode.OK)]
        public async Task<Contracts.Member[]> Get()
        {
            var memberModels = new List<Member>();

            var members = await _membersService.Get();

            if (members.Length != 0 || members is null)
            {
                foreach (var member in members)
                {
                    memberModels.Add(new Member
                    {
                        Id = member.Id,
                        Name = member.Name,
                        YouTubeAccountId = member.YouTubeAccountId
                    });
                }
                return memberModels.ToArray();
            }
            else
            {
                throw new ArgumentNullException("No member has been created");
            }
        }

        [HttpPost("UpdateMember")]
        public async Task<ActionResult> Update(Member request)
        {
            var member = new Core.CoreModels.Member
            {
                Id = request.Id,
                Name = request.Name,
                YouTubeAccountId = request.YouTubeAccountId
            };

            var memberId = await _membersService.Update(member);

            if (memberId != default)
            {
                return Ok(new { Successful = $"Member updated: id {memberId}" });
            }
            else
            {
                return NotFound(new { Error = "Member is not updated" });
            }
        }

    }
}
