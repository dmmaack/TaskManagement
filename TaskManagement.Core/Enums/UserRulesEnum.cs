using System.ComponentModel;

namespace TaskManagement.Core.Enums;

public enum UserRulesEnum
{
    [Description("Sem Permissão")]
    NoPermission = 0,
    [Description("Administrador")]
    Administrator = 1,
    [Description("Usuario")]
    User = 2,
}