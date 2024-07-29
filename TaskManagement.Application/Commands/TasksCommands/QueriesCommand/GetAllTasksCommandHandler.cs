using AutoMapper;
using MediatR;
using TaskManagement.Core.Comunications.Messages.Notifications;
using TaskManagement.Core.Enums;
using TaskManagement.Domain.DTO.TasksDTOs;
using TaskManagement.Domain.Entities;
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
        IEnumerable<TaskEntity> allTaks;
        
        // regra: caso o usuario logado seja administrador, busca todas as tasks
        // caso for usuario normal, busca somente as suas tarefas
        if(request.LoggedUser.GetRule.Equals(UserRulesEnum.Administrator))
            allTaks = await _taskRepository.GetAllAsync();
        else
            allTaks = await _taskRepository.GetAllByAssignedToAsync(request.LoggedUser.Id);


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