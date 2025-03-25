namespace DataExporter.ManagedResponse;

public class ManagedError
{
    public ManagedErrorType ErrorType { get; }
    public string Scope { get; }
    public string Message { get; }

    public ManagedError(ManagedErrorType errorType, string scope, string message)
    {
        ErrorType = errorType;
        Scope = scope;
        Message = message;
    }
}
