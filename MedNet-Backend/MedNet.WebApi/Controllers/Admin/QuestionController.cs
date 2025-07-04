using System.Diagnostics;
using AutoMapper;
using MediatR;
using MedNet.Application.CQRS.Commands;
using MedNet.Application.DTOs;
using MedNet.Domain.Entities;
using MedNet.WebApi.Controllers.Abstract;
using MedNet.WebApi.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedNet.WebApi.Controllers.Admin;

[ApiController]
[Authorize(Roles = "Admin")]
[Route("admin/sets/{parentid:int}/questions/{id:int}")]
public sealed class QuestionController : EntityControllerBase<QuestionsSet, Question>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public QuestionController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("", Name = "Get a specific question")]
    [ProducesResponseType(typeof(QuestionDto), StatusCodes.Status200OK)]
    [WithEntityInclude<Question>(nameof(Question.Answers))]
    public IActionResult Get()
    {
        return Ok(_mapper.Map<QuestionDto>(Entity));
    }
    
    [HttpPatch("", Name = "Update a specific question")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateAsync(
        [FromBody] QuestionControllerUpdateDto data,
        CancellationToken cancellationToken = default)
    {
        var command = new UpdateQuestionCommand()
        {
            Id = Entity.Id,

            // dto properties
            Body = data.Body,
            Answers = data.Answers,
        };
        var result = await _mediator.Send(command, cancellationToken);

        if (!result.IsSuccess)
        {
            return result.ErrorCode switch
            {
                "question_not_found" => throw new UnreachableException(),
                "question_already_exists" => Problem(statusCode: StatusCodes.Status400BadRequest, detail: result.Error),
                "forbidden_answer_id" => Problem(statusCode: StatusCodes.Status400BadRequest, detail: result.Error),
                "conflicting_answer_body" => Problem(statusCode: StatusCodes.Status400BadRequest, detail: result.Error),
                _ => throw new NotImplementedException(result.Error)
            };
        }

        return Ok();
    }

    [HttpDelete("", Name = "Delete a specific question")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteAsync(CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(
            new DeleteQuestionCommand()
            {
                Id = Entity.Id
            }, cancellationToken);

        if (!result.IsSuccess)
        {
            return result.ErrorCode switch
            {
                "question_not_found" => throw new UnreachableException(),
                _ => throw new NotImplementedException(result.Error)
            };
        }

        return Ok();
    }

    [NonAction]
    protected override Task<IActionResult?> OnEntitiesFetched()
    {
        if (Entity.ParentQuestionsSetId != ParentEntity.Id)
        {
            return Task.FromResult<IActionResult?>(Problem(
                statusCode: StatusCodes.Status404NotFound,
                detail: $"There's no such {nameof(Question)} ({Entity.Id}) referenced to {nameof(QuestionsSet)} ({ParentEntity.Id})"));
        }

        return Task.FromResult<IActionResult?>(null);
    }
}
