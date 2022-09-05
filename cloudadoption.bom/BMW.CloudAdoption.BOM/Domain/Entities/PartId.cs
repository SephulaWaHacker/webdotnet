using System.ComponentModel.DataAnnotations.Schema;

namespace BMW.CloudAdoption.BOM.Domain.Entities;

public class PartId
{
    public string Id { get; set; } = string.Empty;
    public int PartFamilyId { get; set; }
    public int Quantity { get; set; }
    
    [ForeignKey(nameof(PartFamilyId))]
    public PartFamily PartFamily { get; set; } = default!;
}