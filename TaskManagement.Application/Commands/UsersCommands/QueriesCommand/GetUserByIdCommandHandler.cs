using AutoMapper;
using MediatR;
using TaskManagement.Core.Comunications.Messages.Notifications;
using TaskManagement.Domain.DTO.UsersDTOs;
using TaskManagement.Domain.Interfaces.Repositories;

namespace TaskManagement.Application.Commands.UsersCommands.QueriesCommand;

public class GetUserByIdCommandHandler : IRequestHandler<GetUserByIdCommand, NotificationResult<BaseUserDTO>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUserByIdCommandHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<NotificationResult<BaseUserDTO>> Handle(GetUserByIdCommand request, CancellationToken cancellationToken)
    {
        var selectedUser = await _userRepository.GetByIdAsync(request.Id);

        if(selectedUser == null || selectedUser.Id != request.Id)
            return new NotificationResult<BaseUserDTO>(false, 
                        new DomainNotification("Identificador de usuário não existe.", System.Net.HttpStatusCode.UnprocessableEntity),
                        new BaseUserDTO());
        
        var userDTO = _mapper.Map<BaseUserDTO>(selectedUser);

        return new NotificationResult<BaseUserDTO>(true, 
                    new DomainNotification("1 usuário(s) encontrado.", 
                        System.Net.HttpStatusCode.Found),
                    userDTO);

    }
}