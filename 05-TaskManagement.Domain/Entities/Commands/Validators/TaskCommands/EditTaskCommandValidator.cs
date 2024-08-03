using FluentValidation;
using TaskManagement.Domain.Entities.Commands.TasksCommands.UpdateTasks;

namespace TaskManagement.Domain.Entities.Commands.Validators.TaskCommands;

public class EditTaskCommandValidator : AbstractValidator<UpdateTasksCommand>
{
    public EditTaskCommandValidator()
    {
        RuleFor(x => x)
            .NotEmpty().WithMessage($"A Entidade {nameof(UpdateTasksCommand)} não pode ser vazia.")
            .NotNull().WithMessage($"A Entidade {nameof(UpdateTasksCommand)} não pode ser Nula.")
            .WithName(nameof(UpdateTasksCommand));

        RuleFor(x => x.Title)
            .NotNull().NotEmpty().WithMessage("O titulo da tarefa não pode ser vazio.")
            .MinimumLength(3).WithMessage("O titulo da tarefa deve ter no mínimo 3 caracteres.")
            .MaximumLength(80).WithMessage("O titulo da tarefa deve ter no máximo 80 caracteres.")
            .WithName(nameof(UpdateTasksCommand.Title));

        RuleFor(x => x.Description)            
            .MaximumLength(5000).WithMessage("A descrição da tarefa deve ter no máximo 5000 caracteres.")
            .WithName(nameof(UpdateTasksCommand.Description));

        RuleFor(x => x.StartDate)
            .NotEmpty().NotNull()
            .GreaterThanOrEqualTo(DateTime.MinValue).WithMessage("A data inicial deve ser preenchida.");
        
        RuleFor(x => x.EndDate)
            .NotEmpty().NotNull()
            .GreaterThanOrEqualTo(DateTime.MinValue).WithMessage("A data final deve ser preenchida.");
        
        RuleFor(x => x.RegisterDate)
            .NotEmpty().NotNull()
            .GreaterThanOrEqualTo(DateTime.MinValue).WithMessage("A data de registro deve ser preenchida.");

        RuleFor(x => x.UserId)
            .NotNull().NotEmpty().WithMessage("É necessário informar um Usuario criador da Tarefa.");
    }
}