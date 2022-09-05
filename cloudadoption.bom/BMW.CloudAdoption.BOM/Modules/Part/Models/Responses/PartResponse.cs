using System.Text.Json.Serialization;

namespace BMW.CloudAdoption.BOM.Modules.Part.Models.Responses;

public class PartResponse
{
    [JsonPropertyName("part_number")] public string Id { get; set; } = string.Empty;
    [JsonPropertyName("quantity")] public int Quantity { get; set; }
    [JsonPropertyName("unit_type")] public string UnitType { get; set; } = string.Empty;
    [JsonPropertyName("assembled")] public bool? Assembled { get; set; }
}