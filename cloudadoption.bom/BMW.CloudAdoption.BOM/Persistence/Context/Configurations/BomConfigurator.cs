using BMW.CloudAdoption.BOM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BMW.CloudAdoption.BOM.Persistence.Context.Configurations;

public class BomConfigurator : IEntityTypeConfiguration<Bom>
{
    public void Configure(EntityTypeBuilder<Bom> builder)
    {
        builder.ToTable("bill_of_materials");
        builder.HasKey(x => x.Id);
        
       
    }
}