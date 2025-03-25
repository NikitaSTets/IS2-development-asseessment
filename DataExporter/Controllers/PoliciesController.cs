using DataExporter.Dtos;
using DataExporter.Interfaces;
using DataExporter.ManagedResponse;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DataExporter.Controllers;

[ApiController]
[Route("[controller]")]
public class PoliciesController : ControllerBase
{
    private IPolicyService _policyService;
    private readonly ManagedResultFactory _managedResultFactory;

    public PoliciesController(IPolicyService policyService, ManagedResultFactory managedResultFactory)
    {
        _policyService = policyService;
        _managedResultFactory = managedResultFactory;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePolicies([FromBody] CreatePolicyDto createPolicyDto)
    {
        var policies = await _policyService.CreatePolicyAsync(createPolicyDto);

        return _managedResultFactory.CreateManagedOkResult(createPolicyDto);
    }


    [HttpGet]
    public async Task<IActionResult> GetPolicies()
    {
        //What about paging? 
        var policies = await _policyService.ReadPoliciesAsync();

        return _managedResultFactory.CreateManagedOkResult(policies);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetPolicy(int id)
    {
        var policy = await _policyService.ReadPolicyAsync(id);

        return _managedResultFactory.CreateManagedOkResult(policy);
    }


    /// <summary>
    /// Exports policies with their notes between a date range.
    /// </summary>
    /// <param name="startDate">The start date (inclusive).</param>
    /// <param name="endDate">The end date (inclusive).</param>
    /// <returns>A list of policies with their notes within the given range.</returns>
    [HttpGet("export")]
    public async Task<IActionResult> Export([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        if (startDate > endDate)
        {
            var modelState = new ModelStateDictionary();
            modelState.AddModelError("InvalidDate", "Invalid dates");

            return _managedResultFactory.CreateActionResultFromInvalidModelState(modelState);
        }
        var exportPolicies = await _policyService.ExportPoliciesFromRange(startDate, endDate);

        return _managedResultFactory.CreateManagedOkResult(exportPolicies);
    }
}
