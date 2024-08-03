using System.Net;
using AutoMapper;
using TaskManagement.Core.Comunications.Messages.Notifications;
using TaskManagement.Domain.DTO.UsersDTOs;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Entities.Commands.UsersCommands.CreateUsers;
using TaskManagement.Domain.Interfaces.Repositories;
using TaskManagement.Domain.Interfaces.Services;

namespace TaskManagement.Application.Services.UsersServices;

public class CreateUserServices : ICreateUserServices
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateUserServices(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<NotificationResult<BaseUserDTO>> Handle(ProducerUsersRabbitMQCommand request)
    {
        var userEntity = _mapper.Map<UserEntity>(request);
        return await Handle(userEntity);
    }

    public async Task<NotificationResult<BaseUserDTO>> Handle(UserEntity request)
    {
        request.ValidateUser();
        
        if(!request.IsValid())
            return new NotificationResult<BaseUserDTO>(false,
                new DomainNotification("houve um problema ao cadastrar o usuário", 
                request.GetErrors(),
                HttpStatusCode.UnprocessableEntity),
                new BaseUserDTO());
        
        //TODO: aplicar criptografia no password

        var userToCreate = await _unitOfWork.UserRepository.CreateAsync(request);
        await _unitOfWork.CommitAsync();
        userToCreate.SetUserId(request.Id);

        // mapeia a Entity para DTO de retorno
        var userDTO = _mapper.Map<BaseUserDTO>(userToCreate);

        return new NotificationResult<BaseUserDTO>(true,
                new DomainNotification("Usuáro cadastrado com sucesso.", HttpStatusCode.Created),
                userDTO);
    }
}