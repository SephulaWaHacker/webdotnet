using BMW.CloudAdoption.BOM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BMW.CloudAdoption.BOM.Persistence.Context.Configurations;

public class BomPartFamilyConfigurator : IEntityTypeConfiguration<BomPartFamily>
{
    public void Configure(EntityTypeBuilder<BomPartFamily> builder)
    {
        builder.ToTable("bom_part_family");
        builder.HasKey(sc => new { sc.BomId, sc.PartFamilyId });
    }
}