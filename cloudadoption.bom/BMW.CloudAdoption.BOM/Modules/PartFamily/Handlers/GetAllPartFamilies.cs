using AutoMapper;
using BMW.CloudAdoption.BOM.Modules.PartFamily.Models.Responses;
using BMW.CloudAdoption.BOM.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BMW.CloudAdoption.BOM.Modules.PartFamily.Handlers;

public class GetAllPartFamilies: IRequestHandler<Queries.GetAllPartFamilies, IResult>
{
    private readonly BomContext _bomContext;
    private readonly IMapper _mapper;

    public GetAllPartFamilies(BomContext bomContext, IMapper mapper)
    {
        _bomContext = bomContext;
        _mapper = mapper;
    }
    
    public async Task<IResult> Handle(Queries.GetAllPartFamilies request, CancellationToken cancellationToken)
    {
        var partFamilies = await _bomContext.PartFamilies.Include(x => x.PartIds).ToListAsync(cancellationToken);
        var partFamilyResponse = _mapper.Map<List<PartFamilyResponse>>(partFamilies);

        return Results.Ok(partFamilyResponse);
    }
}