using System.Text.Json.Serialization;
using BMW.CloudAdoption.BOM.Domain.Entities;
using BMW.CloudAdoption.BOM.Modules.PartFamily.Models.Responses;

namespace BMW.CloudAdoption.BOM.Modules.Bom.Models.Responses;

public class BomResponse
{
   [JsonPropertyName("bill_of_material_id")] public string Id { get; set; } = string.Empty;
   [JsonPropertyName("vehicle_id")] public string VehicleId { get; set; } = string.Empty;
   [JsonPropertyName("start_date")] public DateOnly StartDate { get; set; }
   [JsonPropertyName("end_date")] public DateOnly EndDate { get; set; }
   [JsonPropertyName("status")] public BillOfMaterialStatus Status { get; set; }
   [JsonPropertyName("part_families")] public List<PartFamilyResponse>? PartFamilies { get; set; }
}