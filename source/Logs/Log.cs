using System.Security.AccessControl;
using Brief.Logs;
using Brief.Logs.Traits;

namespace Brief.Logs;

public class Log : ILog
{
    public string Message { get; set; } = string.Empty;

    public object? Item { get; } = null;

    public Severities Severity { get; } = Severities.INFO;
    public Priorities Priority { get; } = Priorities.NONE;
}
