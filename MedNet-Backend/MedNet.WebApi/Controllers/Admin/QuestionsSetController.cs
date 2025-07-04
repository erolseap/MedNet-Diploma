using System.Diagnostics;
using AutoMapper;
using MediatR;
using MedNet.Application.CQRS.Commands;
using MedNet.Application.CQRS.Queries;
using MedNet.Application.DTOs;
using MedNet.Domain.Entities;
using MedNet.WebApi.Controllers.Abstract;
using MedNet.WebApi.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedNet.WebApi.Controllers.Admin;

[ApiController]
[Authorize(Roles = "Admin")]
[Route("admin/sets/{id:int}")]
public sealed class QuestionsSetController : EntityControllerBase<QuestionsSet>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public QuestionsSetController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("", Name = "Get a specific questions set")]
    [ProducesResponseType(typeof(QuestionsSetWithNumOfQuestionsDto), StatusCodes.Status200OK)]
    [WithEntityInclude<QuestionsSet>(nameof(QuestionsSet.Questions))] // required for number of questions information
    public IActionResult Get()
    {
        return Ok(_mapper.Map<QuestionsSetWithNumOfQuestionsDto>(Entity));
    }
    
    [HttpPatch("", Name = "Update a specific questions set")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateAsync(
        [FromBody] QuestionsSetControllerUpdateDto data,
        CancellationToken cancellationToken = default)
    {
        var command = new UpdateQuestionsSetCommand()
        {
            Id = Entity.Id,
            
            // dto properties
            Name = data.Name,
        };
        var result = await _mediator.Send(command, cancellationToken);

        if (!result.IsSuccess)
        {
            return result.ErrorCode switch
            {
                "set_not_found" => throw new UnreachableException(),
                "set_already_exists" => Problem(statusCode: StatusCodes.Status400BadRequest, detail: result.Error),
                _ => throw new NotImplementedException(result.Error)
            };
        }

        return Ok();
    }

    [HttpDelete("", Name = "Delete a specific questions set")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteAsync(CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(
            new DeleteQuestionsSetCommand()
            {
                Id = Entity.Id
            }, cancellationToken);

        if (!result.IsSuccess)
        {
            return result.ErrorCode switch
            {
                "set_not_found" => throw new UnreachableException(),
                _ => throw new NotImplementedException(result.Error)
            };
        }

        return Ok();
    }
    
    [HttpGet("questions", Name = "List all questions")]
    [ProducesResponseType(typeof(IEnumerable<QuestionDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetQuestionsAsync([FromQuery] QuestionsSetControllerGetQuestionsDto data,
        CancellationToken cancellationToken = default)
    {
        var query = new ListQuestionsQuery()
        {
            ParentQuestionsSetId = Entity.Id,
            Limit = data.Limit,
            Offset = data.Offset,
        };
        return Ok(await _mediator.Send(query, cancellationToken));
    }
    
    [HttpPost("questions", Name = "Create a new question")]
    [ProducesResponseType(typeof(QuestionDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateQuestionAsync([FromBody] QuestionsSetControllerCreateQuestionDto data,
        CancellationToken cancellationToken = default)
    {
        var command = new CreateQuestionCommand()
        {
            ParentQuestionSetId = Entity.Id,

            // dto properties
            Body = data.Body,
            BlankQuestionNumber = data.BlankQuestionNumber,
            Answers = data.Answers,
        };
        var result = await _mediator.Send(command, cancellationToken);
        if (!result.IsSuccess)
        {
            return result.ErrorCode switch
            {
                "set_not_found" => throw new UnreachableException(),
                "forbidden_answer_id" => Problem(statusCode: StatusCodes.Status400BadRequest, detail: result.Error),
                "question_already_exists" => Problem(statusCode: StatusCodes.Status400BadRequest, detail: result.Error),
                "conflicting_answer_body" => Problem(statusCode: StatusCodes.Status400BadRequest, detail: result.Error),
                _ => throw new NotImplementedException(result.Error)
            };
        }

        var question = await _mediator.Send(new GetQuestionByIdQuery() { Id = result.Value }, cancellationToken);
        return Created((Uri?)null, question);
    }
    
    [HttpPost("questions/batch", Name = "Create a new questions (batch)")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateQuestionAsync([FromBody] IReadOnlyCollection<QuestionsSetControllerCreateQuestionDto> data,
        CancellationToken cancellationToken = default)
    {
        if (data.Count == 0)
        {
            return Problem(statusCode: StatusCodes.Status400BadRequest, detail: "You must provide at least one question, zero given");
        }
        foreach (var item in data)
        {
            var command = new CreateQuestionCommand()
            {
                ParentQuestionSetId = Entity.Id,

                // dto properties
                Body = item.Body,
                Answers = item.Answers,
            };

            var result = await _mediator.Send(command, cancellationToken);
            if (!result.IsSuccess)
            {
                return result.ErrorCode switch
                {
                    "set_not_found" => throw new UnreachableException(),
                    "forbidden_answer_id" => Problem(statusCode: StatusCodes.Status400BadRequest, detail: result.Error),
                    "question_already_exists" => Problem(statusCode: StatusCodes.Status400BadRequest, detail: result.Error),
                    "conflicting_answer_body" => Problem(statusCode: StatusCodes.Status400BadRequest, detail: result.Error),
                    _ => throw new NotImplementedException(result.Error)
                };
            }
        }
        return Created();
    }
}
