using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;

namespace DataExporter.ManagedResponse;

public class ManagedResultFactory
{
    public IActionResult CreateManagedOkResult<T>(T model) where T : class
    {
        return new ManagedOkResult<T>(model);
    }

    public IActionResult CreateActionResultFromInvalidModelState(ModelStateDictionary modelState)
    {
        if (modelState.IsValid)
            throw new ArgumentException("Cannot create errors from valid model state", nameof(modelState));

        var errors = ConvertModelStateDictionaryToManagedErrors(modelState);

        return new ManagedBadRequestResult(errors);
    }

    public IActionResult CreateActionResultFromException(Exception ex)
    {
        var errors = ConvertExceptionToManagedErrors(ex);

        return new ManagedInternalServerResult(errors);
    }

    private static ManagedError[] ConvertModelStateDictionaryToManagedErrors(ModelStateDictionary modelState)
    {
        return modelState.Keys
            .SelectMany(key => modelState[key].Errors.Select(e => new ManagedError(ManagedErrorType.ValidationFailed, key, e.ErrorMessage)))
            .ToArray();
    }

    private static ManagedError[] ConvertExceptionToManagedErrors(Exception ex)
    {
        return new[]
         {
            new ManagedError(ManagedErrorType.GenericError, "ServerError-Message", ex.Message),
            new ManagedError(ManagedErrorType.GenericError, "ServerError-StackTrace", ex.StackTrace ?? string.Empty)
        };
    }
}
