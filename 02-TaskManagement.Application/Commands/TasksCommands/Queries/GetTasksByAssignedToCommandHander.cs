using System.Data.Common;
using AutoMapper;
using MediatR;
using TaskManagement.Core.Comunications.Messages.Notifications;
using TaskManagement.Core.Enums;
using TaskManagement.Domain.DTO.TasksDTOs;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Entities.Commands.TasksCommands.Queries;
using TaskManagement.Domain.Interfaces.Repositories;

namespace TaskManagement.Application.Commands.TasksCommands.Queries;

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
        IEnumerable<TaskEntity> allTasks = new List<TaskEntity>();
        
        // regra: caso o usuario logado seja administrador, busca o Id solicitado
        // caso for usuario normal, busca somente do Id Logado
        if(request.LoggedUser.GetRule.Equals(UserRulesEnum.Administrator))
            allTasks = await _taskRepository.GetAllByAssignedToAsync(request.AssegnedTo);
        else if (request.AssegnedTo.Equals(request.LoggedUser.Id))
            allTasks = await _taskRepository.GetAllByAssignedToAsync(request.LoggedUser.Id);

            
        if (allTasks == null || !allTasks.Any())
            return new NotificationResult<IEnumerable<BaseTaskDTO>>(false, 
                    new DomainNotification($"Nenhuma tarefa encontrada.", 
                                                System.Net.HttpStatusCode.Found),
                    []);
        
        var allTasksDTO = _mapper.Map<IList<BaseTaskDTO>>(allTasks);

        return new NotificationResult<IEnumerable<BaseTaskDTO>>(true, 
                    new DomainNotification($"{allTasksDTO.Count} tarefa(s) encontrada(s).", 
                                                System.Net.HttpStatusCode.Found),
                    allTasksDTO);
    }
}