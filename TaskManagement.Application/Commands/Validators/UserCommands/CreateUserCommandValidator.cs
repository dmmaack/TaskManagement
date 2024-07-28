using FluentValidation;
using TaskManagement.Application.Commands.UsersCommands.CreateUsersCommand;

namespace TaskManagement.Application.Commands.Validators.UsersCommands;

public class CreateUsersCommandValidator : AbstractValidator<CreateUsersCommand>
{
    public CreateUsersCommandValidator()
    {
        RuleFor(x => x)
            .NotEmpty().WithMessage($"A Entidade {nameof(CreateUsersCommand)} não pode ser vazia.")
            .NotNull().WithMessage($"A Entidade {nameof(CreateUsersCommand)} não pode ser Nula.")
            .WithName(nameof(CreateUsersCommand));

        RuleFor(x => x.Name)
            .NotNull().WithMessage("O Nome não pode ser nulo.")
            .NotEmpty().WithMessage("O Nome não pode ser vazio.")
            .MinimumLength(3).WithMessage("O Nome deve conter no mínimo 3 caracteres.")
            .MaximumLength(40).WithMessage("O Nome deve conter no máximo 40 caracteres.")
            .WithName(nameof(CreateUsersCommand.Name));

        RuleFor(x => x.UserName)
            .NotNull().WithMessage("O Nome de Usuário não pode ser nulo.")
            .NotEmpty().WithMessage("O Nome de Usuário não pode ser vazio.")
            .MinimumLength(3).WithMessage("O Nome de Usuário deve conter no mínimo 3 caracteres.")
            .MaximumLength(12).WithMessage("O Nome de Usuário deve conter no máximo 12 caracteres.")
            .WithName(nameof(CreateUsersCommand.UserName));

        RuleFor(x=>x.Email)
            .NotNull().WithMessage("O email não pode ser nulo.")
            .NotEmpty().WithMessage("O email não pode ser vazio.")                
            .MinimumLength(10).WithMessage("O email deve ter no mínimo 10 caracteres.")
            .MaximumLength(50).WithMessage("O email deve ter no máximo 50 caracteres.")
            .EmailAddress().WithMessage("O email informado não é válido.")
            .WithName(nameof(CreateUsersCommand.Email));

        RuleFor(x => x.Password)
            .NotNull().NotEmpty().WithMessage("A Senha é requerida")
            .MinimumLength(10).WithMessage("A Senha deve ter no mínimo 10 caracteres.")
            .MaximumLength(15).WithMessage("A Senha deve ter no máximo 15 caracteres.")
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$").WithMessage("A Senha informada não é válida.")
            .WithName(nameof(CreateUsersCommand.Password));
    }
}