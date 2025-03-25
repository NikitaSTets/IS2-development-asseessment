using System.Text.Json.Serialization;

namespace DataExporter.Dtos;

public class ExportDto
{
    [JsonPropertyName("policyNumber")]
    public string? PolicyNumber { get; set; }

    [JsonPropertyName("premium")]
    public decimal Premium { get; set; }
    
    [JsonPropertyName("startDate")]
    public DateTime StartDate { get; set; }

    [JsonPropertyName("notes")]
    // A list of the notes' text.
    public IList<string> Notes { get; set; }
}
