using LessonMonitor.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LessonMonitor.API.Contracts;
using System.Net;
using AutoMapper;

namespace LessonMonitor.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MembersController : ControllerBase
    {
        private readonly IMembersService _membersService;
        private readonly IMapper _mapper;

        public MembersController(IMembersService membersService, IMapper mapper)
        {
            _membersService = membersService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> Create(NewMember request)
        {
            var member = _mapper.Map<NewMember, Core.CoreModels.Member>(request);

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
                return Ok(new { Successful = $"Member is deleted: {result}" });
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
                return _mapper.Map<Core.CoreModels.Member, Member>(member);
            }
            else
            {
                throw new ArgumentNullException("No member has been created");
            }
        }

        [HttpGet("GetAllMembers")]
        [ProducesResponseType(typeof(Member[]), (int)HttpStatusCode.OK)]
        public async Task<MembersArray> Get()
        {
            var getMembers = await _membersService.Get();
            
            if (getMembers.Length != 0 || getMembers is null)
            {
                var members = _mapper.Map<Core.CoreModels.Member[], Member[]>(getMembers);

                return new MembersArray() { Members = members };
            }
            else
            {
                throw new ArgumentNullException("No member has been created");
            }
        }

        [HttpPost("UpdateMember")]
        public async Task<ActionResult> Update(Member request)
        {
            var member = _mapper.Map<Member, Core.CoreModels.Member>(request);

            var memberId = await _membersService.Update(member);

            if (memberId != default)
            {
                return Ok( new { MemberUpdatedId = memberId } );
            }
            else
            {
                return NotFound( new { Error = "Member is not updated" } );
            }
        }

    }
}
