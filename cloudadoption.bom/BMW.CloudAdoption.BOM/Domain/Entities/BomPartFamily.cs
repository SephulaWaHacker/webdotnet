namespace BMW.CloudAdoption.BOM.Domain.Entities;

public class BomPartFamily
{
    public int BomId { get; set; }
    public Bom Bom { get; set; } = default!;

    public int PartFamilyId { get; set; }
    public PartFamily PartFamily { get; set; } = default!;
}