using AutoMapper;
using MedNet.Application.DTOs;
using MedNet.Domain.Entities;
using MedNet.Infrastructure.Entities;
using MedNet.WebApi.Controllers.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MedNet.WebApi.Controllers.Pub;

[ApiController]
[Authorize]
[Route("my-tests/{id:int}")]
public class UserTestController : EntityControllerBase<UserTestSession>
{
    private readonly IMapper _mapper;
    private readonly UserManager<AppUser> _userManager;
    
    private Task<AppUser> GetUserEntity() => _userManager.GetUserAsync(User)!;
    private async Task<int> GetUserEntityId() => (await GetUserEntity()).Id;

    public UserTestController(IMapper mapper, UserManager<AppUser> userManager)
    {
        _mapper = mapper;
        _userManager = userManager;
    }

    [HttpGet("questions", Name = "List all test questions")]
    [ProducesResponseType(typeof(IEnumerable<UserTestSessionQuestionDto>), StatusCodes.Status200OK)]
    [WithEntityInclude<UserTestSession>(nameof(UserTestSession.Questions))]
    [WithEntityInclude<UserTestSession>(nameof(UserTestSession.Questions), nameof(UserTestSessionQuestion.Question))]
    [WithEntityInclude<UserTestSession>(nameof(UserTestSession.Questions), nameof(UserTestSessionQuestion.Question), nameof(Question.Answers))]
    public IActionResult GetQuestionsAsync(CancellationToken cancellationToken = default)
    {
        return Ok(_mapper.Map<IEnumerable<UserTestSessionQuestionDto>>(Entity.Questions));
    }

    protected override async Task<IActionResult?> OnEntityFetched()
    {
        var result = await base.OnEntityFetched();
        if (result != null)
        {
            return null;
        }

        if (Entity.UserId != await GetUserEntityId())
        {
            return NotFound();
        }

        return null;
    }
}
