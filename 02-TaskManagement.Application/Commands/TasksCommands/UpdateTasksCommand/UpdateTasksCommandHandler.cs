using System.Net;
using AutoMapper;
using MediatR;
using TaskManagement.Core.Comunications.Messages.Notifications;
using TaskManagement.Core.Enums;
using TaskManagement.Domain.DTO.TasksDTOs;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Entities.Commands.TasksCommands.UpdateTasks;
using TaskManagement.Domain.Interfaces.Repositories;

namespace TaskManagement.Application.Commands.TasksCommands.UpdateTasks;

public class UpdateTasksCommandHandler: IRequestHandler<UpdateTasksCommand, NotificationResult<BaseTaskDTO>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateTasksCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<NotificationResult<BaseTaskDTO>> Handle(UpdateTasksCommand request, CancellationToken cancellationToken)
    {
        if(!request.LoggedUser.GetRule.Equals(UserRulesEnum.Administrator) && !request.LoggedUser.Id.Equals(request.Id))
        {
            return new NotificationResult<BaseTaskDTO>(false,
                new DomainNotification("Somante Administrador e o usuário da própria tarefa podem altera-la.", 
                [],
                HttpStatusCode.NotModified),
                new BaseTaskDTO());
        }   

        request.ValidateTask();

        if(!request.IsValid())
            return new NotificationResult<BaseTaskDTO>(false,
                new DomainNotification("Houve um problema ao Editar a Tarefa", 
                request.GetErrors(),
                HttpStatusCode.UnprocessableEntity),
                new BaseTaskDTO());

        // valida se a task existe
        var taskExists = await _unitOfWork.TaskRepository.GetByIdAsync(request.Id);

        if(taskExists == null || taskExists.Id.Equals(0))
            return new NotificationResult<BaseTaskDTO>(false,
                new DomainNotification("Essa Tarefa não existe.", 
                [],
                HttpStatusCode.NotFound),
                new BaseTaskDTO());

        // se a task existe faz o update
        var taskMap = _mapper.Map<TaskEntity>(request);
        var taskUpdate = _unitOfWork.TaskRepository.UpdateTask(taskMap);
        await _unitOfWork.CommitAsync();

        var taskDTO = _mapper.Map<BaseTaskDTO>(taskUpdate);

        return new NotificationResult<BaseTaskDTO>(true,
                new DomainNotification("Tarefa cadastrada com sucesso.", HttpStatusCode.Accepted),
                taskDTO);
    }
}