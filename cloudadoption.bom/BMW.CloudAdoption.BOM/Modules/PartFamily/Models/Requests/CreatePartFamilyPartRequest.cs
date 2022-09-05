using System.Text.Json.Serialization;

namespace BMW.CloudAdoption.BOM.Modules.PartFamily.Models.Requests;

public class CreatePartFamilyPartRequest
{
     [JsonPropertyName("part_number")] public string PartId { get; set; } = string.Empty;
     [JsonPropertyName("quantity")] public int Quantity { get; set; }
}