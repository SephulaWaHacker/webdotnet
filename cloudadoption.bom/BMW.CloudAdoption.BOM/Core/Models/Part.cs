using System.Text.Json.Serialization;

namespace BMW.CloudAdoption.BOM.Core.Models;

public class Part
{
    [JsonPropertyName("partNumber")] public string Number { get; set; } = string.Empty;
    [JsonPropertyName("partString")] public string String { get; set; } = string.Empty;
    [JsonPropertyName("unitType")] public string UnitType { get; set; } = string.Empty;
    [JsonPropertyName("assembled")] public bool Assembled { get; set; }
    [JsonPropertyName("status")] public PartStatus Status { get; set; }
    [JsonPropertyName("grossWeight")] public decimal GrossWeight { get; set; }
    [JsonPropertyName("netWeight")] public decimal NetWeight { get; set; }
    [JsonPropertyName("weightUnit")] public string WeightUnit { get; set; } = string.Empty;

    [JsonPropertyName("plant")] public PartsPlant Plants { get; set; } = default!;
    [JsonPropertyName("supplier")] public PartsSupplier Supplier { get; set; } = default!;
}

public class PartsSupplier
{
    [JsonPropertyName("id")] public string Id { get; set; } = string.Empty;
    [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;
}

public class PartsPlant
{
    [JsonPropertyName("id")] public string Id { get; set; } = string.Empty;
    [JsonPropertyName("unloadPoint")] public string UnloadPoint { get; set; } = string.Empty;
}

public enum PartStatus
{
    NEW, 
    VALID, 
    DISCONTINUED
}