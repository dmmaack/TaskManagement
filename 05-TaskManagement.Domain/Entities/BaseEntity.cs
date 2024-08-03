using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TaskManagement.Domain.Entities;

public class BaseEntity
{
    public BaseEntity() { }

    protected BaseEntity(long id)
    {
        Id = id;
    }

    public long Id { get; set; }

    private ICollection<string> _errors { get; set; }

    protected void Validate<T>(T obj)
    {
        var resultadoValidacao = new List<ValidationResult>();

        var contexto = new ValidationContext(obj, null, null);
        Validator.TryValidateObject(obj, contexto, resultadoValidacao, true);

        _errors = new List<string>();

        foreach (var error in resultadoValidacao)
            _errors.Add(error.ErrorMessage);
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