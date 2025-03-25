using DataExporter.Dtos;

namespace DataExporter.Interfaces;

public interface IPolicyService
{
    Task<ReadPolicyDto?> CreatePolicyAsync(CreatePolicyDto createPolicyDto);

    Task<IList<ReadPolicyDto>> ReadPoliciesAsync();

    Task<ReadPolicyDto?> ReadPolicyAsync(int id);

    Task<IEnumerable<ExportDto>> ExportPoliciesFromRange(DateTime startDate, DateTime endDate);
}
