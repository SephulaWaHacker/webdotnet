using BMW.CloudAdoption.BOM.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BMW.CloudAdoption.BOM.Modules.Bom.Handlers;

public class DeleteBomPartFamily : IRequestHandler<Commands.DeleteBomPartFamily, IResult>
{
    private readonly BomContext _bomContext;

    public DeleteBomPartFamily(BomContext bomContext) => _bomContext = bomContext;

    public async Task<IResult> Handle(Commands.DeleteBomPartFamily request, CancellationToken cancellationToken)
    {
        var currentBom = await _bomContext.BillOfMaterials.Include(x => x.BomPartFamilies)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (currentBom == null)
            return Results.NotFound("Bom Record not found");
        
        currentBom.BomPartFamilies.RemoveAll(x => x.PartFamilyId == request.PartFamilyId);
        await _bomContext.SaveChangesAsync(cancellationToken);

       return Results.NoContent();
    }
}