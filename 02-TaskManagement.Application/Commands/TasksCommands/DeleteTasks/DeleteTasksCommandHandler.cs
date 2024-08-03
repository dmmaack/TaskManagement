using System.Diagnostics;
using System.Net;
using AutoMapper;
using MediatR;
using TaskManagement.Core.Comunications.Messages.Notifications;
using TaskManagement.Core.Enums;
using TaskManagement.Domain.Entities.Commands.TasksCommands.DeleteTasks;
using TaskManagement.Domain.Interfaces.Repositories;

namespace TaskManagement.Application.Commands.TasksCommands.Delete;

public class DeleteTasksCommandHandler : IRequestHandler<DeleteTasksCommand, NotificationResult<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DeleteTasksCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<NotificationResult<bool>> Handle(DeleteTasksCommand request, CancellationToken cancellationToken)
    {
        if(!request.LoggedUser.GetRule.Equals(UserRulesEnum.Administrator) && !request.LoggedUser.Id.Equals(request.TaskId))
        {
            return new NotificationResult<bool>(false,
                new DomainNotification("Somante Administrador e o usuário da própria tarefa podem exclui-la.", 
                [],
                HttpStatusCode.NotModified),
                false);
        }   

        var taskEntity = await _unitOfWork.TaskRepository.GetByIdAsync(request.TaskId);

        if (taskEntity == null)
            return new NotificationResult<bool>(false,
                new DomainNotification("Tarefa não excluída.",
                ["Tarefa não encontrada na base de dados."],
                HttpStatusCode.UnprocessableEntity),
                false);

        await _unitOfWork.TaskRepository.RemoveAsync(request.TaskId);
        await _unitOfWork.CommitAsync();


        return new NotificationResult<bool>(true,
                new DomainNotification("Tarefa excluída com exito.",
                [],
                HttpStatusCode.UnprocessableEntity),
                true);
    }
}