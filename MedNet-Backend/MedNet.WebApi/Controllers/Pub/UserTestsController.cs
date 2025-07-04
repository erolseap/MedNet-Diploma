using AutoMapper;
using MediatR;
using MedNet.Application.CQRS.Commands;
using MedNet.Application.CQRS.Queries;
using MedNet.Application.DTOs;
using MedNet.Domain.Entities;
using MedNet.Infrastructure.Entities;
using MedNet.WebApi.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MedNet.WebApi.Controllers.Pub;

[ApiController]
[Authorize]
[Route("my-tests")]
[ProducesResponseType(StatusCodes.Status403Forbidden)]
public sealed class UserTestsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly UserManager<AppUser> _userManager;

    private Task<AppUser> GetUserEntity() => _userManager.GetUserAsync(User)!;
    private async Task<int> GetUserEntityId() => (await GetUserEntity()).Id;

    public UserTestsController(IMediator mediator, IMapper mapper, UserManager<AppUser> userManager)
    {
        _mediator = mediator;
        _mapper = mapper;
        _userManager = userManager;
    }

    [HttpGet("", Name = "List all my generated tests")]
    [ProducesResponseType(typeof(IEnumerable<UserTestSessionDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAsync(CancellationToken cancellationToken = default)
    {
        return Ok(await _mediator.Send(new ListUserTestsQuery() { UserId = (await GetUserEntity()).Id}, cancellationToken));
    }

    [HttpGet("generatable", Name = "Get a list of generatable tests")]
    [ProducesResponseType(typeof(IEnumerable<QuestionsSetWithNumOfQuestionsDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetGeneratablesAsync([FromQuery] UserTestsControllerGetGeneratablesDto data, CancellationToken cancellationToken = default)
    {
        var command = new ListQuestionsSetsQuery()
        {
            Limit = data.Limit,
            Offset = data.Offset,
        };
        return Ok(await _mediator.Send(command, cancellationToken));
    }
    
    [HttpPost("generate", Name = "Generate a new test")]
    [ProducesResponseType(typeof(UserTestSessionDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> GenerateAsync([FromBody] UserTestsControllerGenerateDto data, CancellationToken cancellationToken = default)
    {
        var command = new GenerateUserTestSessionCommand()
        {
            QuestionsSetId = data.SetId.Value,
            UserId = await GetUserEntityId()
        };
        var result = await _mediator.Send(command, cancellationToken);
        if (!result.IsSuccess)
        {
            return result.ErrorCode switch
            {
                "set_not_found" => Problem(statusCode: StatusCodes.Status400BadRequest, detail: result.Error),
                "set_empty" => Problem(statusCode: StatusCodes.Status400BadRequest, detail: result.Error),
                _ => throw new NotImplementedException(result.Error)
            };
        }
        
        var test = await _mediator.Send(new GetUserTestByIdQuery() { Id = result.Value }, cancellationToken);
        return Created((Uri?)null, test);
    }
}
