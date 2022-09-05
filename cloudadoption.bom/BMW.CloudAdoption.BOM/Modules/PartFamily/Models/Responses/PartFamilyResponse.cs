using System.Text.Json.Serialization;
using BMW.CloudAdoption.BOM.Modules.Part.Models.Responses;

namespace BMW.CloudAdoption.BOM.Modules.PartFamily.Models.Responses;

public class PartFamilyResponse
{
    [JsonPropertyName("id")] public string Id { get; set; } = string.Empty;
    [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;
    [JsonPropertyName("parts")] public List<PartResponse> Parts { get; set; } = default!;
}