using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Commands.UsersCommands.CreateUsersCommand;
using TaskManagement.Application.Commands.UsersCommands.QueriesCommand;
using TaskManagement.Core.Comunications.Messages.Notifications;
using TaskManagement.Domain.DTO.UsersDTOs;

namespace TaskManagement.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("GetUsers")]
    public async Task<IActionResult> GetUsers()
    {
        var query = new GetAllUsersCommand();
        var objResponse = await _mediator.Send(query);

        if(!objResponse.Success)
            return CreatedAtAction(nameof(GetUserById), new { id = objResponse.Data.Count() }, objResponse);

        return Ok(objResponse);
    }

    [HttpGet]
    [Route("GetUserById/{id}")]
    public async Task<IActionResult> GetUserById(long id)
    {
        var query = new GetUserByIdCommand() { Id = id };
        var objResponse = await _mediator.Send(query);

        if(!objResponse.Success)
            return CreatedAtAction(nameof(GetUserById), new { id = objResponse.Data.Id }, objResponse);

        return Ok(objResponse);
    }

    [HttpGet]
    [Route("SearchUsers")]
    public async Task<IActionResult> SearchUsers(string search)
    {
        var query = new SearchUserByPredicateCommand() { Search = search };
        var objResponse = await _mediator.Send(query);

        if(!objResponse.Success)
            return CreatedAtAction(nameof(SearchUsers), new { id = objResponse.Data.Count() }, objResponse);

        return Ok(objResponse);        
    }

    [HttpPost]
    [Route("CreateUser")]
    public async Task<IActionResult> CreateUser(CreateUsersCommand userCommand)
    {
        var objResponse = await _mediator.Send(userCommand);        

        if(!objResponse.Success)
            return CreatedAtAction(nameof(CreateUser), new { id = objResponse.Data.Id }, objResponse);

        return Ok(objResponse);
    }
}