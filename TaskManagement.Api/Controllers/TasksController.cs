using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Commands.TasksCommands.CreateTasksCommand;
using TaskManagement.Application.Commands.TasksCommands.DeleteTasksCommand;
using TaskManagement.Application.Commands.TasksCommands.QueriesCommand;
using TaskManagement.Application.Commands.TasksCommands.UpdateTasksCommand;

namespace TaskManagement.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class TasksController : ControllerBase
{
    private readonly IMediator _mediator;

    public TasksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("GetTasks")]
    public async Task<IActionResult> GetTasks()
    {
        var query = new GetAllTasksCommand();
        var objResponse = await _mediator.Send(query);

        if (!objResponse.Success)
            return CreatedAtAction(nameof(GetTasksByUserAssigned), new { id = objResponse.Data.Count() }, objResponse);
            
        return Ok(objResponse);
    }

    [HttpGet]
    [Route("GetTasksByUserAssigned/{id}")]
    public async Task<IActionResult> GetTasksByUserAssigned(long id)
    {
        var taskCommand = new GetTasksByAssignedToCommand() { AssegnedTo = id };
        var objResponse = await _mediator.Send(taskCommand);

        if (!objResponse.Success)
            return CreatedAtAction(nameof(GetTasksByUserAssigned), new { id = objResponse.Data.Count() }, objResponse);
            
        return Ok(objResponse);
    }

    [HttpPost]
    [Route("CreateTask")]
    public async Task<IActionResult> CreateTask(CreateTasksCommand taskCommand)
    {
        var objResponse = await _mediator.Send(taskCommand);

        if (!objResponse.Success)
            return CreatedAtAction(nameof(CreateTask), new { id = objResponse.Data.Id }, objResponse);
            
        return Ok(objResponse);
    }

    [HttpPut]
    [Route("UpdateTask/{id}")]
    public async Task<IActionResult> UpdateTask(UpdateTasksCommand taskCommand, long id)
    {
        taskCommand.Id = id;        
        var objResponse = await _mediator.Send(taskCommand);

        if (!objResponse.Success)
            return CreatedAtAction(nameof(UpdateTask), new { id = objResponse.Data.Id }, objResponse);
            
        return Ok(objResponse);
    }

    [HttpDelete]
    [Route("DeleteTask/{id}")]
    public async Task<IActionResult> DeleteTask(long id)
    {
        var taskCommand = new DeleteTasksCommand() { TaskId = id };
        var objResponse = await _mediator.Send(taskCommand);

        if (!objResponse.Success)
            return CreatedAtAction(nameof(DeleteTask), new { id = taskCommand.TaskId }, objResponse);
            
        return Ok(objResponse);
    }
}