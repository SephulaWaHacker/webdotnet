using BMW.CloudAdoption.BOM.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BMW.CloudAdoption.BOM.Modules.Bom.Handlers;

public class UpdateBom : IRequestHandler<Commands.UpdateBom, IResult>
{
    private readonly BomContext _bomContext;

    public UpdateBom(BomContext bomContext) => _bomContext = bomContext;

    public async Task<IResult> Handle(Commands.UpdateBom request, CancellationToken cancellationToken)
    {
        var currentBom = await _bomContext.BillOfMaterials.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (currentBom == null)
           return Results.NotFound("Bom Record not found");
        
        
        currentBom!.EndDate = request.BomRequest.EndDate.ToDateTime(TimeOnly.MinValue);
        currentBom.StartDate = request.BomRequest.StartDate.ToDateTime(TimeOnly.MinValue);
        currentBom.VehicleId = request.BomRequest.VehicleId;

        await _bomContext.SaveChangesAsync(cancellationToken);

        return Results.NoContent();
    }
}