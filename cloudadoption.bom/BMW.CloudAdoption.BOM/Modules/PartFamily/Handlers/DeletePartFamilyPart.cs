using BMW.CloudAdoption.BOM.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BMW.CloudAdoption.BOM.Modules.PartFamily.Handlers;

public class DeletePartFamilyPart : IRequestHandler<Commands.DeletePartFamilyPart, IResult>
{
    private readonly BomContext _bomContext;

    public DeletePartFamilyPart(BomContext bomContext) => _bomContext = bomContext;

    public async Task<IResult> Handle(Commands.DeletePartFamilyPart request, CancellationToken cancellationToken)
    {
        var partFamily = await _bomContext.PartFamilies.Include(x => x.PartIds).FirstOrDefaultAsync(x => x.Id == request.Id);
        if (partFamily == null) 
            return Results.NotFound("PartFamily record not found");

        partFamily.PartIds.RemoveAll(x => x.Id == request.PartId);
        await _bomContext.SaveChangesAsync(cancellationToken);

       return Results.NoContent();
    }
}