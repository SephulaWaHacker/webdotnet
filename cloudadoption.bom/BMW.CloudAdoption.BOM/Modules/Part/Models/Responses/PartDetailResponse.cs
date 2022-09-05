using System.Text.Json.Serialization;
using BMW.CloudAdoption.BOM.Core.Models;

namespace BMW.CloudAdoption.BOM.Modules.Part.Models.Responses;

public class PartDetailResponse
{
    [JsonPropertyName("part_number")] public string Number { get; set; } = string.Empty;
    [JsonPropertyName("part_string")] public string String { get; set; } = string.Empty;
    [JsonPropertyName("unit_type")] public string UnitType { get; set; } = string.Empty;
    [JsonPropertyName("assembled")] public bool Assembled { get; set; }
    [JsonPropertyName("status")] public PartStatus Status { get; set; }
    [JsonPropertyName("gross_weight")] public decimal GrossWeight { get; set; }
    [JsonPropertyName("net_weight")] public decimal NetWeight { get; set; }
    [JsonPropertyName("weight_unit")] public string WeightUnit { get; set; } = string.Empty;

    [JsonPropertyName("plant")] public DetailedPartsPlant Plants { get; set; } = default!;
    [JsonPropertyName("supplier")] public PartsSupplier Supplier { get; set; } = default!;
}

public class DetailedPartsPlant
{
    [JsonPropertyName("id")] public string Id { get; set; } = string.Empty;
    [JsonPropertyName("unload_point")] public string UnloadPoint { get; set; } = string.Empty;
}