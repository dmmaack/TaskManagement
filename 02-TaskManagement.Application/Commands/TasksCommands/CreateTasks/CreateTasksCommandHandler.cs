using System.Net;
using AutoMapper;
using MediatR;
using TaskManagement.Core.Comunications.Messages.Notifications;
using TaskManagement.Domain.DTO.TasksDTOs;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Entities.Commands.TasksCommands.CreateTasks;
using TaskManagement.Domain.Interfaces.Repositories;

namespace TaskManagement.Application.Commands.TasksCommands.CreateTasks;

public class CreateTasksCommandHandler: IRequestHandler<CreateTasksCommand, NotificationResult<BaseTaskDTO>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateTasksCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<NotificationResult<BaseTaskDTO>> Handle(CreateTasksCommand request, CancellationToken cancellationToken)
    {
        request.ValidateTask();

        if(!request.IsValid())
            return new NotificationResult<BaseTaskDTO>(false,
                new DomainNotification("Houve um problema ao cadastrar a Tarefa", 
                    request.GetErrors(),
                    HttpStatusCode.UnprocessableEntity),
                new BaseTaskDTO());
        
        var taskToCreate = _mapper.Map<TaskEntity>(request);
        var taskCreated = await _unitOfWork.TaskRepository.CreateAsync(taskToCreate);
        await _unitOfWork.CommitAsync();
        taskCreated.SetTaskId(taskToCreate.Id);

        // mapeia a entidade para a DTO de retorno
        var taskDTO = _mapper.Map<BaseTaskDTO>(taskCreated);

        return new NotificationResult<BaseTaskDTO>(true,
                new DomainNotification("Tarefa cadastrada com sucesso.", HttpStatusCode.Created),
                taskDTO);
    }
}