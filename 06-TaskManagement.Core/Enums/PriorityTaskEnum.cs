using System.ComponentModel;

namespace TaskManagement.Core.Enums;

public enum PriorityTaskEnum
{
    [Description("Baixa")]
    Low = 1,
    [Description("Média")]
    Middle = 2,
    [Description("Alta")]
    High = 3,
}