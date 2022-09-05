using BMW.CloudAdoption.BOM.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BMW.CloudAdoption.BOM.Persistence.Context;

public class BomContext : DbContext, BomInterface
{
    private const string Schema = "bom";

    public DbSet<Bom> BillOfMaterials { get; set; } = default!;
    public DbSet<PartFamily> PartFamilies { get; set; } = default!;

    public BomContext(DbContextOptions<BomContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schema);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BomContext).Assembly);
    }
}

public interface BomInterface
{
    DbSet<Bom> BillOfMaterials { get; set; }
    DbSet<PartFamily> PartFamilies { get; set; }
}
// Commands:

// "dotnet ef database update 0" - Remove all migrations
// "dotnet ef database update" - Apply all migrations
// "dotnet ef migrations add {name}" - Create new migration
// "dotnet ef migrations remove" - Deletes last created migrations