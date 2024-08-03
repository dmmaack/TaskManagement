using AutoMapper;
using MediatR;
using TaskManagement.Core.Comunications.Messages.Notifications;
using TaskManagement.Domain.Commands.UsersCommands.Queries;
using TaskManagement.Domain.DTO.UsersDTOs;
using TaskManagement.Domain.Interfaces.Repositories;

namespace TaskManagement.Application.Commands.UsersCommands.Queries;

public class GetAllUsersCommandHandler : IRequestHandler<GetAllUsersCommand, NotificationResult<IEnumerable<BaseUserDTO>>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetAllUsersCommandHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<NotificationResult<IEnumerable<BaseUserDTO>>> Handle(GetAllUsersCommand request, CancellationToken cancellationToken)
    {
        var searchUsers = await _userRepository.GetAllAsync();

        if(searchUsers == null || !searchUsers.Any())
            return new NotificationResult<IEnumerable<BaseUserDTO>>(false, 
                        new DomainNotification("Nenhum usuário encontrado", System.Net.HttpStatusCode.UnprocessableEntity),
                        []);

        var allUsersDTO = _mapper.Map<IList<BaseUserDTO>>(searchUsers);

        return new NotificationResult<IEnumerable<BaseUserDTO>>(true, 
                    new DomainNotification($"{allUsersDTO.Count} usuário(s) encontrado(s).", 
                        System.Net.HttpStatusCode.Found),
                    allUsersDTO);
    }
}