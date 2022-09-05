using AutoMapper;
using BMW.CloudAdoption.BOM.Domain.Entities;
using BMW.CloudAdoption.BOM.Modules.PartFamily.Models.Responses;
using BMW.CloudAdoption.BOM.Persistence.Cache;
using BMW.CloudAdoption.BOM.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BMW.CloudAdoption.BOM.Modules.PartFamily.Handlers;

public class CreatePartFamilyPart : IRequestHandler<Commands.CreatePartFamilyPart, IResult>
{
    private readonly BomContext _bomContext;
    private readonly IPartsCache _partsCache;
    private readonly IMapper _mapper;

    public CreatePartFamilyPart(BomContext bomContext, IPartsCache partsCache, IMapper mapper)
    {
        _bomContext = bomContext;
        _partsCache = partsCache;
        _mapper = mapper;
    }

    public async Task<IResult> Handle(Commands.CreatePartFamilyPart request, CancellationToken cancellationToken)
    {
        var partFamily = await _bomContext.PartFamilies.Include(x => x.PartIds).FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (partFamily == null)
            return Results.NotFound("PartFamily record not found");

        if (!_partsCache.Exists(request.CreatePartFamilyPartRequest.PartId))
            return Results.NotFound("Part does not exist");

        if (partFamily.PartIds.Any(x => x.Id == request.CreatePartFamilyPartRequest.PartId))
            return Results.NoContent();

        partFamily.PartIds.Add(new PartId
            {Id = request.CreatePartFamilyPartRequest.PartId, Quantity = request.CreatePartFamilyPartRequest.Quantity});
        await _bomContext.SaveChangesAsync(cancellationToken);

        var partFamilyResponse = _mapper.Map<PartFamilyResponse>(partFamily);

        return Results.CreatedAtRoute("GetPartFamilyById", new {id = partFamilyResponse.Id}, partFamilyResponse);
    }
}