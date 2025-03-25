using DataExporter.ManagedResponse;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DataExporter.Filters;

public class ManagedExceptionFilter : IActionFilter, IOrderedFilter
{
    private readonly ManagedResultFactory _managedResultFactory;

    public ManagedExceptionFilter(ManagedResultFactory managedResultFactory)
    {
        _managedResultFactory = managedResultFactory;
    }

    public int Order { get; set; } = int.MaxValue - 10;

    public void OnActionExecuting(ActionExecutingContext context)
    { }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception == null) return;

        context.Result = _managedResultFactory.CreateActionResultFromException(context.Exception);
        context.ExceptionHandled = true;
    }
}
