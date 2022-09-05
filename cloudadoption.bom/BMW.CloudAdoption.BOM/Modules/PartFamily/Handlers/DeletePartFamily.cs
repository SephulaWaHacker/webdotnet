using BMW.CloudAdoption.BOM.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BMW.CloudAdoption.BOM.Modules.PartFamily.Handlers;

public class DeletePartFamily: IRequestHandler<Commands.DeletePartFamily, IResult>
{
    private readonly BomContext _bomContext;

    public DeletePartFamily(BomContext bomContext) => _bomContext = bomContext;

    public async Task<IResult> Handle(Commands.DeletePartFamily request, CancellationToken cancellationToken)
    {
        var partFamily = await _bomContext.PartFamilies.Include(x => x.PartIds).FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (partFamily == null)
            return Results.NotFound("PartFamily record not found");
        
        _bomContext.PartFamilies.Remove(partFamily);
        await _bomContext.SaveChangesAsync(cancellationToken);
        
        return Results.NoContent();
    }
}