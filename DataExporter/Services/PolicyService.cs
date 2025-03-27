using Data.Interfaces;
using Data.Model;
using DataExporter.Dtos;
using DataExporter.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataExporter.Services;

public class PolicyService : IPolicyService
{
    private readonly IPolicyRepository _policyRepository;

    public PolicyService(IPolicyRepository policyRepository)
    {
        _policyRepository = policyRepository;
    }

    /// <summary>
    /// Creates a new policy from the DTO.
    /// </summary>
    /// <param name="createPolicyDto">Policy details.</param>
    /// <returns>Returns a ReadPolicyDto representing the new policy, or null if creation failed.</returns>
    public async Task<ReadPolicyDto?> CreatePolicyAsync(CreatePolicyDto createPolicyDto)
    {
        if (createPolicyDto == null)
            throw new ArgumentNullException(nameof(createPolicyDto));

        var policy = new Policy
        {
            PolicyNumber = createPolicyDto.PolicyNumber,
            Premium = createPolicyDto.Premium,
            StartDate = createPolicyDto.StartDate
        };
        _policyRepository.Add(policy);
        await _policyRepository.SaveChangesAsync();

        return new ReadPolicyDto
        {
            Id = policy.Id,
            PolicyNumber = policy.PolicyNumber,
            Premium = policy.Premium,
            StartDate = policy.StartDate
        };
    }

    /// <summary>
    /// Retrieves all policies.
    /// </summary>
    /// <returns>Returns a list of ReadPolicyDto.</returns>
    public async Task<IList<ReadPolicyDto>> ReadPoliciesAsync()
    {
        var policies = await _policyRepository.GetAll().ToListAsync();

        var policyModels = policies
            .Select(p => new ReadPolicyDto
            {
                Id = p.Id,
                PolicyNumber = p.PolicyNumber,
                Premium = p.Premium,
                StartDate = p.StartDate
            }).ToList();

        return policyModels;
    }

    /// <summary>
    /// Retrieves a policy by ID.
    /// </summary>
    /// <param name="id">Policy ID.</param>
    /// <returns>Returns a ReadPolicyDto, or null if not found.</returns>
    public async Task<ReadPolicyDto?> ReadPolicyAsync(int id)
    {
        var policy = await _policyRepository.GetByIdAsync(id);

        return policy == null ? null : new ReadPolicyDto
        {
            Id = policy.Id,
            PolicyNumber = policy.PolicyNumber,
            Premium = policy.Premium,
            StartDate = policy.StartDate
        };
    }

    /// <summary>
    /// Export Policies
    /// </summary>
    /// <param name="startDate">Start Date</param>
    /// <param name="endDate">End Date</param>
    /// <returns>Returns a ExportDtos.</returns>
    public async Task<IEnumerable<ExportDto>> ExportPoliciesFromRange(DateTime startDate, DateTime endDate)
    {
        var policies = await _policyRepository.GetWhereAsync(p => p.StartDate >= startDate && p.StartDate <= endDate);

        var exportDtos = policies.Select(p => new ExportDto
        {
            PolicyNumber = p.PolicyNumber,
            Premium = p.Premium,
            StartDate = p.StartDate,
            Notes = p.Notes.Select(n => n.Text).ToList()
        });

        return exportDtos;
    }
}
