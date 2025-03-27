using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DataExporter.Dtos;

public class CreatePolicyDto
{
    [Required]
    [JsonPropertyName("policyNumber")]
    public string PolicyNumber { get; set; }

    [Required]
    [JsonPropertyName("premium")]
    public decimal Premium { get; set; }

    [Required]
    [JsonPropertyName("startDate")]
    public DateTime StartDate { get; set; }
}
