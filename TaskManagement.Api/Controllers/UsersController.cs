using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Api.Auth;
using TaskManagement.Application.Commands.UsersCommands.CreateUsersCommand;
using TaskManagement.Application.Commands.UsersCommands.QueriesCommand;
using TaskManagement.Core.Enums;
using TaskManagement.Domain.DTO.UsersDTOs;

namespace TaskManagement.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;
    public LoggedUserDTO _loggedUser = new LoggedUserDTO();

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize]
    [Route("GetLoggedUser")]
    public IActionResult GetLoggedUser()
    {
        _loggedUser = AuthControl.GetLoggedUser((ClaimsIdentity)HttpContext.User.Identity);

        return Ok(_loggedUser);
    }

    [HttpGet]
    [Authorize]
    [Route("GetUsers")]
    public async Task<IActionResult> GetUsers()
    {
        var query = new GetAllUsersCommand();
        var objResponse = await _mediator.Send(query);

        if (!objResponse.Success)
            return CreatedAtAction(nameof(GetUserById), new { id = objResponse.Data.Count() }, objResponse);

        return Ok(objResponse);
    }

    [HttpGet]
    [Authorize(Roles = "1")]
    [Route("GetUserById/{id}")]
    public async Task<IActionResult> GetUserById(long id)
    {
        var query = new GetUserByIdCommand() { Id = id };
        var objResponse = await _mediator.Send(query);

        if (!objResponse.Success)
            return CreatedAtAction(nameof(GetUserById), new { id = objResponse.Data.Id }, objResponse);

        return Ok(objResponse);
    }

    [HttpGet]
    [Authorize(Roles = "1")]
    [Route("SearchUsers")]
    public async Task<IActionResult> SearchUsers(string search)
    {
        var query = new SearchUserByPredicateCommand() { Search = search };
        var objResponse = await _mediator.Send(query);

        if (!objResponse.Success)
            return CreatedAtAction(nameof(SearchUsers), new { id = objResponse.Data.Count() }, objResponse);

        return Ok(objResponse);
    }

    [HttpPost]
    [Authorize(Roles = "1")]
    [Route("CreateUser")]
    public async Task<IActionResult> CreateUser(CreateUsersCommand userCommand)
    {
        var objResponse = await _mediator.Send(userCommand);

        if (!objResponse.Success)
            return CreatedAtAction(nameof(CreateUser), new { id = objResponse.Data.Id }, objResponse);

        return Ok(objResponse);
    }
}