using AutoMapper;
using MediatR;
using TaskManagement.Core.Comunications.Messages.Notifications;
using TaskManagement.Domain.DTO.TasksDTOs;
using TaskManagement.Domain.Interfaces.Repositories;

namespace TaskManagement.Application.Commands.TasksCommands.QueriesCommand;

public class GetTasksByAssignedToCommandHander : IRequestHandler<GetTasksByAssignedToCommand, NotificationResult<IEnumerable<BaseTaskDTO>>>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IMapper _mapper;

    public GetTasksByAssignedToCommandHander(ITaskRepository taskRepository, IMapper mapper)
    {
        _taskRepository = taskRepository;
        _mapper = mapper;
    }

    public async Task<NotificationResult<IEnumerable<BaseTaskDTO>>> Handle(GetTasksByAssignedToCommand request, CancellationToken cancellationToken)
    {
        var tasks = await _taskRepository.GetAllByAssignedToAsync(request.AssegnedTo);

        if (tasks == null || !tasks.Any())
            return new NotificationResult<IEnumerable<BaseTaskDTO>>(false, 
                    new DomainNotification($"Nenhuma tarefa encontrada.", 
                                                System.Net.HttpStatusCode.Found),
                    []);
        
        var allTasksDTO = _mapper.Map<IList<BaseTaskDTO>>(tasks);

        return new NotificationResult<IEnumerable<BaseTaskDTO>>(true, 
                    new DomainNotification($"{allTasksDTO.Count} tarefa(s) encontrada(s).", 
                                                System.Net.HttpStatusCode.Found),
                    allTasksDTO);
    }
}