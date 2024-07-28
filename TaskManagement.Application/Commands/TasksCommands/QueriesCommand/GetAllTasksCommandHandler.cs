using AutoMapper;
using MediatR;
using TaskManagement.Core.Comunications.Messages.Notifications;
using TaskManagement.Domain.DTO.TasksDTOs;
using TaskManagement.Domain.Interfaces.Repositories;

namespace TaskManagement.Application.Commands.TasksCommands.QueriesCommand;

public class GetAllTasksCommandHandler : IRequestHandler<GetAllTasksCommand, NotificationResult<IEnumerable<BaseTaskDTO>>>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IMapper _mapper;

    public GetAllTasksCommandHandler(ITaskRepository taskRepository, IMapper mapper)
    {
        _taskRepository = taskRepository;
        _mapper = mapper;
    }

    public async Task<NotificationResult<IEnumerable<BaseTaskDTO>>> Handle(GetAllTasksCommand request, CancellationToken cancellationToken)
    {
        var allTaks = await _taskRepository.GetAllAsync();

        if (allTaks == null || !allTaks.Any())
            return new NotificationResult<IEnumerable<BaseTaskDTO>>(false, 
                    new DomainNotification($"Nenhuma tarefa encontrada.", 
                                                System.Net.HttpStatusCode.Found),
                    []);

        var allTasksDTO = _mapper.Map<IList<BaseTaskDTO>>(allTaks);

        return new NotificationResult<IEnumerable<BaseTaskDTO>>(true, 
                    new DomainNotification($"{allTasksDTO.Count} tarefa(s) encontrada(s).", 
                                                System.Net.HttpStatusCode.Found),
                    allTasksDTO);
    }
}