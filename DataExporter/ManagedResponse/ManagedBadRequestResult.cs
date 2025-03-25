using Microsoft.AspNetCore.Mvc;

namespace DataExporter.ManagedResponse;

public class ManagedBadRequestResult : ObjectResult
{
    public ManagedBadRequestResult(ManagedError[] errors)
        : base(new ManagedResponse<object?>(null, errors))
    {
        StatusCode = StatusCodes.Status400BadRequest;
    }

    public ManagedBadRequestResult(string scope, string message)
        : this(new[] { new ManagedError(ManagedErrorType.InvalidOperation, scope, message) })
    { }
}
