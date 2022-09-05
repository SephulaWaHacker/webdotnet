using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BMW.CloudAdoption.BOM.Modules.Bom.Models.Requests;

public class BomRequest
{
    [JsonPropertyName("vehicle_id"), Required]
    public string VehicleId { get; set; } = string.Empty;

    [JsonPropertyName("start_date"), Required]
    public DateOnly StartDate { get; set; }

    [JsonPropertyName("end_date"), Required]
    public DateOnly EndDate { get; set; }
}