using AutoMapper;
using BMW.CloudAdoption.BOM.Modules.PartFamily.Models.Responses;
using BMW.CloudAdoption.BOM.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BMW.CloudAdoption.BOM.Modules.PartFamily.Handlers;

public class GetPartFamilyById: IRequestHandler<Queries.GetPartFamilyById, IResult>
{
    private readonly BomContext _bomContext;
    private readonly IMapper _mapper;

    public GetPartFamilyById(BomContext bomContext, IMapper mapper)
    {
        _bomContext = bomContext;
        _mapper = mapper;
    }
    
    public async Task<IResult> Handle(Queries.GetPartFamilyById request, CancellationToken cancellationToken)
    {
        var partFamily = await _bomContext.PartFamilies.Include(x => x.PartIds).FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (partFamily == null)
            return Results.NotFound("PartFamily record not found");

        var partFamilyResponse = _mapper.Map<PartFamilyResponse>(partFamily);
        return Results.Ok(partFamilyResponse);
    }
}