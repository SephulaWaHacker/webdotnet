using BMW.CloudAdoption.BOM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BMW.CloudAdoption.BOM.Persistence.Context.Configurations;

public class PartIdConfigurator : IEntityTypeConfiguration<PartId>
{
    public void Configure(EntityTypeBuilder<PartId> builder)
    {
        builder.ToTable("part_ids");
        builder.HasKey(x => new { x.Id, x.PartFamilyId });
    }
}