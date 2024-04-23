namespace Brief.Logs
{
    using Brief.Logs.Traits;

    public interface ILog
    {
        public string Message { get; }

        public object? Item { get; }

        public Severities Severity { get; }
        public Priorities Priority { get; }
    }
}
