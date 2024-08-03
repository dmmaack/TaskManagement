using System.Linq.Expressions;
using System.Net;
using AutoMapper;
using MediatR;
using TaskManagement.Core.Comunications.Messages.Notifications;
using TaskManagement.Domain.DTO.UsersDTOs;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Entities.Commands.UsersCommands.CreateUsers;
using TaskManagement.Domain.Interfaces.RabbitMQ;
using TaskManagement.Domain.Interfaces.Repositories;

namespace TaskManagement.Application.Commands.UsersCommands.CreateUsers;

public class ProducerUsersRabbitMQCommandHandler : IRequestHandler<ProducerUsersRabbitMQCommand, NotificationResult<BaseUserDTO>>
{
    private readonly IRabbitMQProducer _rabbitMQProducer;
    private readonly IMapper _mapper;
    
    private readonly IUnitOfWork _unitOfWork;

    public ProducerUsersRabbitMQCommandHandler(IRabbitMQProducer rabbitMQProducer, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _rabbitMQProducer = rabbitMQProducer;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<NotificationResult<BaseUserDTO>> Handle(ProducerUsersRabbitMQCommand request, CancellationToken cancellationToken)
    {
        request.ValidateUser();

        if(!request.IsValid())
            return new NotificationResult<BaseUserDTO>(false,
                new DomainNotification("houve um problema ao cadastrar o usuário", 
                request.GetErrors(),
                HttpStatusCode.UnprocessableEntity),
                new BaseUserDTO());

        //pesquisar para saber se o email existe na base
        Expression<Func<UserEntity, bool>> predicate = predicate => predicate.Email == request.Email;
        var userByEmail = await _unitOfWork.UserRepository.GetAsync(predicate);

        // valida se o usuario existe, se sim retorna mensagem de erro
        if (userByEmail != null && userByEmail.Any())
            return new NotificationResult<BaseUserDTO>(false,
                new DomainNotification("Email informado já está cadastrado para outro Usuário!", HttpStatusCode.UnprocessableEntity),
                new BaseUserDTO());

        await _rabbitMQProducer.SendProductMessage<ProducerUsersRabbitMQCommand>(request);
        
        var userDTO = _mapper.Map<BaseUserDTO>(request);

        return new NotificationResult<BaseUserDTO>(true,
            new DomainNotification("Cadastro do Usuário enviado para a fila.", HttpStatusCode.OK),
            userDTO);
    }
}
