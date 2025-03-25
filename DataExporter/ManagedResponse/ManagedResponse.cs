namespace DataExporter.ManagedResponse;

public class ManagedResponse<T>
{
    public T Value { get; }
    public ManagedError[] Errors { get; }

    public ManagedResponse(T value, ManagedError[] errors)
    {
        Value = value;
        Errors = errors;
    }
}
