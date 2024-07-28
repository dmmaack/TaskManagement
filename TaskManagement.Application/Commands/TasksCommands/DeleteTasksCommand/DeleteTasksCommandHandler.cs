using System.Diagnostics;
using System.Net;
using AutoMapper;
using MediatR;
using TaskManagement.Core.Comunications.Messages.Notifications;
using TaskManagement.Domain.Interfaces.Repositories;

namespace TaskManagement.Application.Commands.TasksCommands.DeleteTasksCommand;

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
        var taskEntity = await _unitOfWork.TaskRepository.GetByIdAsync(request.TaskId);

        if (taskEntity == null)
            return new NotificationResult<bool>(false,
                new DomainNotification("Tarefa não deletada.",
                ["Tarefa não encontrada na base de dados."],
                HttpStatusCode.UnprocessableEntity),
                false);

        await _unitOfWork.TaskRepository.RemoveAsync(request.TaskId);
        await _unitOfWork.CommitAsync();


        return new NotificationResult<bool>(true,
                new DomainNotification("Tarefa deletada.",
                [],
                HttpStatusCode.UnprocessableEntity),
                true);
    }
}