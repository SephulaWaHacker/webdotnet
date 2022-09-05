using BMW.CloudAdoption.BOM.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BMW.CloudAdoption.BOM.Modules.PartFamily.Handlers;

public class UpdatePartFamily: IRequestHandler<Commands.UpdatePartFamily, IResult>
{
    private readonly BomContext _bomContext;

    public UpdatePartFamily(BomContext bomContext) => _bomContext = bomContext;

    public async Task<IResult> Handle(Commands.UpdatePartFamily request, CancellationToken cancellationToken)
    {
        var partFamily = await _bomContext.PartFamilies.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (partFamily == null)
            return Results.NotFound("PartFamily record not found");

        partFamily.Name = request.PartFamilyRequest.Name;

        _bomContext.PartFamilies.Update(partFamily);
        await _bomContext.SaveChangesAsync(cancellationToken);
        
        return Results.NoContent();
    }
}