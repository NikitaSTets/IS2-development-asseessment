using Microsoft.AspNetCore.Mvc;

namespace DataExporter.ManagedResponse;

public class ManagedInternalServerResult : ObjectResult
{
    public ManagedInternalServerResult(ManagedError[] errors) : base(new ManagedResponse<object?>(null, errors))
    {
        StatusCode = StatusCodes.Status500InternalServerError;
    }
}
