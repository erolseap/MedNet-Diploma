using MediatR;
using MedNet.Application.CQRS.Commands;
using MedNet.Application.CQRS.Queries;
using MedNet.Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedNet.WebApi.Controllers.Admin;

[ApiController]
[Authorize(Roles = "Admin")]
[Route("admin/sets")]
[ProducesResponseType(StatusCodes.Status403Forbidden)]
public sealed class QuestionsSetsController : ControllerBase
{
    private readonly IMediator _mediator;

    public QuestionsSetsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("", Name = "List all questions sets")]
    [ProducesResponseType(typeof(IEnumerable<QuestionsSetWithNumOfQuestionsDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAsync([FromQuery] ListQuestionsSetsQuery query,
        CancellationToken cancellationToken = default)
    {
        return Ok(await _mediator.Send(query, cancellationToken));
    }

    [HttpPost("", Name = "Create an empty questions set")]
    [ProducesResponseType(typeof(QuestionsSetWithNumOfQuestionsDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateQuestionsSetCommand command,
        CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(command, cancellationToken);
        if (!result.IsSuccess)
        {
            return result.ErrorCode switch
            {
                "set_already_exists" => Problem(statusCode: StatusCodes.Status400BadRequest, detail: result.Error),
                _ => throw new NotImplementedException(result.Error)
            };
        }

        var qs = await _mediator.Send(new GetQuestionsSetByIdQuery() { Id = result.Value }, cancellationToken);
        return Created((Uri?)null, qs);
    }
}
