using AutoMapper;
using BMW.CloudAdoption.BOM.Domain;
using BMW.CloudAdoption.BOM.Domain.Entities;
using BMW.CloudAdoption.BOM.Modules.Bom.Models.Responses;
using BMW.CloudAdoption.BOM.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BMW.CloudAdoption.BOM.Modules.Bom.Handlers;

public class PublishBom : IRequestHandler<Commands.PublishBom, IResult>
{
    private readonly BomContext _bomContext;
    private readonly IMapper _mapper;
    private readonly BomQueue _bomQueue;

    public PublishBom(BomContext bomContext, IMapper mapper, BomQueue bomQueue)
    {
        _bomContext = bomContext;
        _mapper = mapper;
        _bomQueue = bomQueue;
    }

    public async Task<IResult> Handle(Commands.PublishBom request, CancellationToken cancellationToken)
    {
        var currentBom = await _bomContext.BillOfMaterials
            .Include(x => x.BomPartFamilies)
            .ThenInclude(x => x.PartFamily)
            .ThenInclude(y => y.PartIds).FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        
        if (currentBom == null)
            return Results.NotFound("Bom Record not found" );

        var vehicleId = currentBom.VehicleId;
        var existingActive = await _bomContext.BillOfMaterials.AnyAsync(x =>
            x.Status == BillOfMaterialStatus.ACTIVE && x.VehicleId != vehicleId, cancellationToken);
        if (existingActive) 
            return Results.BadRequest("Only one Bom can be active per VehicleId" );
        
        currentBom.Status = BillOfMaterialStatus.ACTIVE;
        _bomContext.BillOfMaterials.Update(currentBom);
        await _bomContext.SaveChangesAsync(cancellationToken);

        var bomResponse = _mapper.Map<BomResponse>(currentBom);
        _bomQueue.Enqueue(bomResponse);
        
       return Results.NoContent();
    }
}