using AutoMapper;
using BMW.CloudAdoption.BOM.Domain.Entities;
using BMW.CloudAdoption.BOM.Modules.Bom.Models.Responses;
using BMW.CloudAdoption.BOM.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BMW.CloudAdoption.BOM.Modules.Bom.Handlers;

public class CreateBomPartFamily : IRequestHandler<Commands.CreateBomPartFamily, IResult>
{
    private readonly BomContext _bomContext;
    private readonly IMapper _mapper;

    public CreateBomPartFamily(BomContext bomContext, IMapper mapper)
    {
        _bomContext = bomContext;
        _mapper = mapper;
    }

    public async Task<IResult> Handle(Commands.CreateBomPartFamily request, CancellationToken cancellationToken)
    {
        var currentBom = await _bomContext.BillOfMaterials.Include(x => x.BomPartFamilies)
            .ThenInclude(x => x.PartFamily)
            .ThenInclude(x => x.PartIds)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        
        if (currentBom == null)
            return Results.NotFound("Bom Record not found");

        var partFamily = await _bomContext.PartFamilies.FirstOrDefaultAsync(x => x.Id == request.PartFamilyId, cancellationToken);
        if (partFamily == null)
            return Results.NotFound("PartFamily Record not found");


        if (currentBom.BomPartFamilies.Any(x => x.PartFamilyId == request.PartFamilyId)) 
           return Results.NoContent();

        currentBom.BomPartFamilies.Add(new BomPartFamily{BomId = currentBom.Id, PartFamilyId = partFamily.Id});
        await _bomContext.SaveChangesAsync(cancellationToken);
        
        var bomResponse = _mapper.Map<BomResponse>(currentBom);
        
        return Results.CreatedAtRoute("GetBomById", new {id = bomResponse.Id}, bomResponse);
    }
}