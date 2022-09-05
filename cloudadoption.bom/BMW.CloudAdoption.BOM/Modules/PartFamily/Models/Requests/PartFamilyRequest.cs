using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BMW.CloudAdoption.BOM.Modules.PartFamily.Models.Requests;

public class PartFamilyRequest
{
    [JsonPropertyName("name"), Required] public string Name { get; set; } = string.Empty;
}