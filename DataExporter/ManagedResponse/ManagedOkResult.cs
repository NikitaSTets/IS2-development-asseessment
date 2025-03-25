using Microsoft.AspNetCore.Mvc;

namespace DataExporter.ManagedResponse;

public class ManagedOkResult<T> : ObjectResult where T : class
{
    public T Payload { get; }
    public ManagedError[] Errors { get; }

    public ManagedOkResult(T value)
        : this(value, new ManagedError[0])
    { }

    public ManagedOkResult(T value, ManagedError[] errors)
        : base(new ManagedResponse<T>(value, errors))
    {
        StatusCode = StatusCodes.Status200OK;
        Payload = value;
        Errors = errors;
    }
}
