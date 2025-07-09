using System.Diagnostics;
using AutoMapper;
using MediatR;
using MedNet.Application.CQRS.Commands;
using MedNet.Application.DTOs;
using MedNet.Domain.Entities;
using MedNet.Infrastructure.Entities;
using MedNet.WebApi.Controllers.Abstract;
using MedNet.WebApi.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MedNet.WebApi.Controllers.Pub;

[ApiController]
[Authorize]
[Route("my-tests/{parentid:int}/questions/{id:int}")]
public sealed class UserTestQuestionController : EntityControllerBase<UserTestSession, UserTestSessionQuestion>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly UserManager<AppUser> _userManager;

    private Task<AppUser> GetUserEntity() => _userManager.GetUserAsync(User)!;
    private async Task<int> GetUserEntityId() => (await GetUserEntity()).Id;

    public UserTestQuestionController(IMediator mediator, IMapper mapper, UserManager<AppUser> userManager)
    {
        _mediator = mediator;
        _mapper = mapper;
        _userManager = userManager;
    }

    [HttpGet("", Name = "Get a specific test question")]
    [ProducesResponseType(typeof(UserTestSessionQuestionDto), StatusCodes.Status200OK)]
    [WithEntityInclude<UserTestSessionQuestion>(nameof(UserTestSessionQuestion.Question))]
    [WithEntityInclude<UserTestSessionQuestion>(nameof(UserTestSessionQuestion.Question), nameof(Question.Answers))]
    public IActionResult Get()
    {
        return Ok(_mapper.Map<UserTestSessionQuestionDto>(Entity));
    }
    
    [HttpPost("answer", Name = "Answer a specific test question")]
    [ProducesResponseType(typeof(UserTestSessionQuestionDto), StatusCodes.Status200OK)]
    [WithEntityInclude<UserTestSessionQuestion>(nameof(UserTestSessionQuestion.Question))]
    [WithEntityInclude<UserTestSessionQuestion>(nameof(UserTestSessionQuestion.Question), nameof(Question.Answers))]
    [TrackedEntity<UserTestSessionQuestion>]
    public async Task<IActionResult> AnswerAsync(
        [FromBody] UserTestQuestionControllerAnswerDto data,
        CancellationToken cancellationToken = default)
    {
        var command = new AnswerUserTestSessionQuestionCommand()
        {
            Id = Entity.Id,

            // dto properties
            AnswerId = data.AnswerId.Value,
        };
        var result = await _mediator.Send(command, cancellationToken);

        if (!result.IsSuccess)
        {
            return result.ErrorCode switch
            {
                "question_not_found" => throw new UnreachableException(),
                "question_already_answered" => Problem(statusCode: StatusCodes.Status400BadRequest, detail: result.Error),
                "answer_not_found" => Problem(statusCode: StatusCodes.Status400BadRequest, detail: result.Error),
                _ => throw new NotImplementedException(result.Error)
            };
        }

        return Ok(_mapper.Map<UserTestSessionQuestionDto>(Entity));
    }

    [NonAction]
    protected override async Task<IActionResult?> OnEntitiesFetched()
    {
        var result = await base.OnEntitiesFetched();
        if (result != null)
        {
            return null;
        }

        if (ParentEntity.UserId != await GetUserEntityId())
        {
            return NotFound();
        }

        if (Entity.SessionId != ParentEntity.Id)
        {
            return Problem(
                statusCode: StatusCodes.Status404NotFound,
                detail: $"There's no such {nameof(UserTestSessionQuestion)} ({Entity.Id}) referenced to {nameof(UserTestSession)} ({ParentEntity.Id})");
        }

        return null;
    }
}
