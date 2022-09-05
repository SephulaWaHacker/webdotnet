using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BMW.CloudAdoption.BOM.Domain.Entities;

public class PartFamily
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;
    
    //public int? BomId { get; set; }
    
    //[ForeignKey(nameof(BomId))]
    //public Bom? Bom { get; set; } = default!;

    public List<BomPartFamily> BomPartFamilies { get; set; } = default!;
    public List<PartId> PartIds { get; set; } = default!; // Part references
}