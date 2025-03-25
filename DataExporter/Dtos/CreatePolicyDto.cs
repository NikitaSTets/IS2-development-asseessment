using System.Text.Json.Serialization;

namespace DataExporter.Dtos;

public class CreatePolicyDto
{
    [JsonPropertyName("policyNumber")]
    public string PolicyNumber { get; set; }

    [JsonPropertyName("premium")]
    public decimal Premium { get; set; }

    [JsonPropertyName("startDate")]
    public DateTime StartDate { get; set; }
}
