using System.Diagnostics;
using AutoMapper;
using MediatR;
using MedNet.Application.CQRS.Commands;
using MedNet.Application.CQRS.Queries;
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
    private readonly IMediator _mediator;

    private Task<AppUser> GetUserEntity() => _userManager.GetUserAsync(User)!;
    private async Task<int> GetUserEntityId() => (await GetUserEntity()).Id;

    public UserTestController(IMapper mapper, UserManager<AppUser> userManager, IMediator mediator)
    {
        _mapper = mapper;
        _userManager = userManager;
        _mediator = mediator;
    }

    [HttpGet("", Name = "Get a specific test")]
    [ProducesResponseType(typeof(IEnumerable<UserTestSessionDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAsync(CancellationToken cancellationToken = default)
    {
        return Ok(await _mediator.Send(new GetUserTestByIdQuery() { Id = Entity.Id }, cancellationToken));
    }

    [HttpGet("questions", Name = "List all test questions")]
    [ProducesResponseType(typeof(IEnumerable<UserTestSessionQuestionDto>), StatusCodes.Status200OK)]
    [WithEntityInclude<UserTestSession>(nameof(UserTestSession.Questions))]
    [WithEntityInclude<UserTestSession>(nameof(UserTestSession.Questions), nameof(UserTestSessionQuestion.Question))]
    [WithEntityInclude<UserTestSession>(nameof(UserTestSession.Questions), nameof(UserTestSessionQuestion.Question),
        nameof(Question.Answers))]
    public IActionResult GetQuestionsAsync(CancellationToken cancellationToken = default)
    {
        return Ok(_mapper.Map<IEnumerable<UserTestSessionQuestionDto>>(Entity.Questions));
    }

    [HttpPost("questions/generate-more", Name = "Generate more test questions")]
    [ProducesResponseType(typeof(IEnumerable<UserTestSessionDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GenerateMoreAsync(CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new GenerateMoreUserTestSessionQuestionsCommand() { Id = Entity.Id },
            cancellationToken);
        if (!result.IsSuccess)
        {
            return result.ErrorCode switch
            {
                "user_test_not_found" => throw new UnreachableException(),
                "set_empty" => Problem(statusCode: StatusCodes.Status400BadRequest, detail: result.Error),
                "answer_not_found" => Problem(statusCode: StatusCodes.Status400BadRequest, detail: result.Error),
                _ => throw new NotImplementedException(result.Error)
            };
        }

        return Ok(await _mediator.Send(new GetUserTestByIdQuery() { Id = Entity.Id }, cancellationToken));
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
