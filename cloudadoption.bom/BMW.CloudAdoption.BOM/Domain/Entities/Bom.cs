using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System; 

namespace BMW.CloudAdoption.BOM.Domain.Entities;

public class Bom
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    public string VehicleId { get; set; } = string.Empty;

    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public DateTime EndDate { get; set; }
    public BillOfMaterialStatus Status { get; set; } // int
    public List<BomPartFamily> BomPartFamilies { get; set; } = default!;
}

public enum BillOfMaterialStatus
{
    DRAFT,
    ACTIVE
}