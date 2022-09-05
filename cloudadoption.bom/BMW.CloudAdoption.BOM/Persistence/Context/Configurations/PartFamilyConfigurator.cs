using BMW.CloudAdoption.BOM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BMW.CloudAdoption.BOM.Persistence.Context.Configurations;

public class PartFamilyConfigurator : IEntityTypeConfiguration<PartFamily>
{
    public void Configure(EntityTypeBuilder<PartFamily> builder)
    {
        builder.ToTable("part_families");
    }
}