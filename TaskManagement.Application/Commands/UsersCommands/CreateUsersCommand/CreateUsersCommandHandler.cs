using System.Linq.Expressions;
using System.Net;
using AutoMapper;
using MediatR;
using TaskManagement.Core.Comunications.Messages.Notifications;
using TaskManagement.Domain.DTO.UsersDTOs;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces.Repositories;

namespace TaskManagement.Application.Commands.UsersCommands.CreateUsersCommand;

public class CreateUsersCommandHandler : IRequestHandler<CreateUsersCommand, NotificationResult<BaseUserDTO>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateUsersCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<NotificationResult<BaseUserDTO>> Handle(CreateUsersCommand request, CancellationToken cancellationToken)
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

        // caso usuario não exista, insere na base  
        var userEntity = _mapper.Map<UserEntity>(request);

        //TODO: aplicar criptografia no password

        var userToCreate = await _unitOfWork.UserRepository.CreateAsync(userEntity);
        await _unitOfWork.CommitAsync();
        userToCreate.SetUserId(userEntity.Id);

        // mapeia a Entity para DTO de retorno
        var userDTO = _mapper.Map<BaseUserDTO>(userToCreate);
        
        return new NotificationResult<BaseUserDTO>(true,
                new DomainNotification("Usuáro cadastrado com sucesso.", HttpStatusCode.Created),
                userDTO);
    }
}