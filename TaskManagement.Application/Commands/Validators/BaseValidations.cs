using System.Text;
using FluentValidation;
using FluentValidation.Results;

namespace TaskManagement.Application.Commands.Validators;

public class BaseValidations
{
    ICollection<string> _errors { get; set; }

    protected bool Validate<T, J>(T validator, J data) where T : AbstractValidator<J>
    {
        var validation = validator.Validate(data);

        _errors = new List<string>();

        if(validation.Errors.Count > 0)
        {
            AdderrorList(validation.Errors);
        }

        return _errors.Count.Equals(0);
    }

    private void AdderrorList(List<ValidationFailure> errors)
    {
        foreach(var error in errors)
            _errors.Add(error.ErrorMessage);

        //TODO: Criar um objeto de erros para conter a mensagem e o campo pegando a propriedade error.PropertyName
    }

    public bool IsValid() => _errors.Count.Equals(0);

    public ICollection<string> GetErrors() => _errors;

    public string ErrorsToString()
    {
        var builder = new StringBuilder();

        foreach (var error in _errors)
            builder.Append(" " + error);

        return builder.ToString();
    }
}