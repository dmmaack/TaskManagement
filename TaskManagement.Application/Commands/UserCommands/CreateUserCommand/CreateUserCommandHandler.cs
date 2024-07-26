using System.Linq.Expressions;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using AutoMapper;
using EscNet.Hashers.Interfaces.Algorithms;
using MediatR;
using Microsoft.AspNetCore.Http;
using TaskManagement.Core.Comunications.Messages.Notifications;
using TaskManagement.Domain.DTO.UsersDTOs;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces.Repositories;

namespace TaskManagement.Application.Commands.UserCommands.CreateUserCommand;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, NotificationResult<BaseUserDTO>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IArgon2IdHasher _argon2IdHasher;

    public CreateUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IArgon2IdHasher argon2IdHasher)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _argon2IdHasher = argon2IdHasher;
    }

    public async Task<NotificationResult<BaseUserDTO>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
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
        if (userByEmail.Any())
            return new NotificationResult<BaseUserDTO>(false,
                new DomainNotification("Email informado já está cadastrado para outro Usuário!", HttpStatusCode.UnprocessableEntity),
                new BaseUserDTO());

        // caso usuario não exista, insere na base  
        var userEntity = _mapper.Map<UserEntity>(request);

        var passwordEncrypted = _argon2IdHasher.Hash(userEntity.Password);
        userEntity.SetPassword(request.Password);

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