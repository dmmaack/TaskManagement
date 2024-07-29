using FluentValidation;
using TaskManagement.Application.Commands.TasksCommands.CreateTasksCommand;

namespace TaskManagement.Application.Commands.Validators.TasksCommands;

public class CreateTasksCommandValidator : AbstractValidator<CreateTasksCommand>
{
    public CreateTasksCommandValidator()
    {
        RuleFor(x => x)
            .NotEmpty().WithMessage($"A Entidade {nameof(CreateTasksCommand)} não pode ser vazia.")
            .NotNull().WithMessage($"A Entidade {nameof(CreateTasksCommand)} não pode ser Nula.")
            .WithName(nameof(CreateTasksCommand));

        RuleFor(x => x.Title)
            .NotNull().NotEmpty().WithMessage("O titulo da tarefa não pode ser vazio.")
            .MinimumLength(3).WithMessage("O titulo da tarefa deve ter no mínimo 3 caracteres.")
            .MaximumLength(80).WithMessage("O titulo da tarefa deve ter no máximo 80 caracteres.")
            .WithName(nameof(CreateTasksCommand.Title));

        RuleFor(x => x.Description)            
            .MaximumLength(5000).WithMessage("A descrição da tarefa deve ter no máximo 5000 caracteres.")
            .WithName(nameof(CreateTasksCommand.Description));

        RuleFor(x => x.StartDate)
            .NotEmpty().NotNull()
            .GreaterThan(DateTime.UtcNow).WithMessage("A Data de Início deve ser maior ou igual ao dia de hoje.")
            .WithName(nameof(CreateTasksCommand.Description));
        
        RuleFor(x => x.EndDate)
            .GreaterThan(DateTime.UtcNow).WithMessage("A Data de Final deve ser maior ou igual ao dia de hoje.")
            .GreaterThan(r => r.StartDate).WithMessage("A Data de Final deve ser maior ou igual à Data de Inicio.")
            .WithName(nameof(CreateTasksCommand.Description));
        
        RuleFor(x => x.RegisterDate)
            .NotEmpty().NotNull().WithMessage("A data de Registro deve ser preenchida.")
            .GreaterThanOrEqualTo(DateTime.UtcNow.Date).WithMessage("A Data de Registro deve ser maior ou igual ao dia de hoje.")
            .WithName(nameof(CreateTasksCommand.Description));

        RuleFor(x => x.UserId)
            .NotNull().NotEmpty().WithMessage("É necessário informar um Usuario criador da Tarefa.");
    }
}