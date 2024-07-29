using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using TaskManagement.Application.Services.ApiAuth;
using TaskManagement.Core.Comunications.Messages.Notifications;
using TaskManagement.Domain.Interfaces.Repositories;

namespace TaskManagement.Application.Commands.AuthCommands;

public class AuthCommandHandler : IRequestHandler<AuthCommand, NotificationResult<AuthCommandResponse>>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private IConfiguration _configuration;

    public AuthCommandHandler(IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _configuration = configuration;
    }

    public async Task<NotificationResult<AuthCommandResponse>> Handle(AuthCommand request, CancellationToken cancellationToken)
    {
        // recupera o usuario com o email e senha
        var user = await _userRepository.GetUserLogin(request.Email, request.Password);

        if (user == null)
            return new NotificationResult<AuthCommandResponse>(false,
                        new DomainNotification("Não foi possível efetuar o login do Usuário.",
                        ["Email ou senha inválidos"],
                        System.Net.HttpStatusCode.NotFound),
                        null);

        var tokenGenerated = new TokenService(_configuration).GenerateToken(user);

        AuthCommandResponse result = new() { Email = request.Email, UserName = user.Name, Token = tokenGenerated };

        return new NotificationResult<AuthCommandResponse>(true,
                        new DomainNotification("Token gerado com sucesso.",
                        System.Net.HttpStatusCode.OK),
                        result);
    }
}