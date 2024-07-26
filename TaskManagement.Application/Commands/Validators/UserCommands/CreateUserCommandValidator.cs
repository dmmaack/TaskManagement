using FluentValidation;
using TaskManagement.Application.Commands.UserCommands.CreateUserCommand;

namespace TaskManagement.Application.Commands.Validators.UserCommands;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x)
            .NotEmpty().WithMessage($"A Entidade {nameof(CreateUserCommand)} não pode ser vazia.")
            .NotNull().WithMessage($"A Entidade {nameof(CreateUserCommand)} não pode ser Nula.")
            .WithName(nameof(CreateUserCommand));

        RuleFor(x => x.Name)
            .NotNull().WithMessage("O Nome não pode ser nulo.")
            .NotEmpty().WithMessage("O Nome não pode ser vazio.")
            .MinimumLength(3).WithMessage("O Nome deve conter no mínimo 3 caracteres.")
            .MaximumLength(40).WithMessage("O Nome deve conter no máximo 40 caracteres.")
            .WithName(nameof(CreateUserCommand.Name));

        RuleFor(x => x.UserName)
            .NotNull().WithMessage("O Nome de Usuário não pode ser nulo.")
            .NotEmpty().WithMessage("O Nome de Usuário não pode ser vazio.")
            .MinimumLength(3).WithMessage("O Nome de Usuário deve conter no mínimo 3 caracteres.")
            .MaximumLength(12).WithMessage("O Nome de Usuário deve conter no máximo 12 caracteres.")
            .WithName(nameof(CreateUserCommand.UserName));

        RuleFor(x=>x.Email)
            .NotNull().WithMessage("O email não pode ser nulo.")
            .NotEmpty().WithMessage("O email não pode ser vazio.")                
            .MinimumLength(10).WithMessage("O email deve ter no mínimo 10 caracteres.")
            .MaximumLength(50).WithMessage("O email deve ter no máximo 50 caracteres.")
            .EmailAddress().WithMessage("O email informado não é válido.")
            .WithName(nameof(CreateUserCommand.Email));

        RuleFor(x => x.Password)
            .NotNull().NotEmpty().WithMessage("A Senha é requerida")
            .MinimumLength(10).WithMessage("A Senha deve ter no mínimo 10 caracteres.")
            .MaximumLength(15).WithMessage("A Senha deve ter no máximo 15 caracteres.")
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$").WithMessage("A Senha informada não é válida.")
            .WithName(nameof(CreateUserCommand.Password));
    }
}