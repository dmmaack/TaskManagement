using FluentValidation;
using TaskManagement.Application.Commands.TaskCommands.CreateTaskCommand;

namespace TaskManagement.Application.Commands.Validators.TaskCommands;

public class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
{
    public CreateTaskCommandValidator()
    {
        RuleFor(x => x)
            .NotEmpty().WithMessage($"A Entidade {nameof(CreateTaskCommand)} não pode ser vazia.")
            .NotNull().WithMessage($"A Entidade {nameof(CreateTaskCommand)} não pode ser Nula.")
            .WithName(nameof(CreateTaskCommand));

        RuleFor(x => x.Title)
            .NotNull().NotEmpty().WithMessage("O titulo da tarefa não pode ser vazio.")
            .MinimumLength(3).WithMessage("O titulo da tarefa deve ter no mínimo 3 caracteres.")
            .MaximumLength(80).WithMessage("O titulo da tarefa deve ter no máximo 80 caracteres.")
            .WithName(nameof(CreateTaskCommand.Title));

        RuleFor(x => x.Description)            
            .MaximumLength(5000).WithMessage("A descrição da tarefa deve ter no máximo 5000 caracteres.")
            .WithName(nameof(CreateTaskCommand.Description));

        RuleFor(x => x.StartDate)
            .LessThan(DateTime.UtcNow).WithMessage("A Data de Início deve ser maior ou igual ao dia de hoje.");
        
        RuleFor(x => x.EndDate)
            .LessThan(DateTime.UtcNow).WithMessage("A Data de Final deve ser maior ou igual ao dia de hoje.")
            .LessThan(r => r.StartDate).WithMessage("A Data de Final deve ser maior ou igual à Data de Inicio.");
        
        RuleFor(x => x.RegisterDate)
            .NotEmpty().NotNull().LessThanOrEqualTo(DateTime.MinValue).WithMessage("A data de Registro deve ser preenchida.");

        RuleFor(x => x.UserId)
            .NotNull().NotEmpty().WithMessage("É necessário informar um Usuario criador da Tarefa.");
    }
}