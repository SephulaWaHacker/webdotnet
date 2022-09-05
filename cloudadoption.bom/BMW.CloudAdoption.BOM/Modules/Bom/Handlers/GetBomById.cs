using AutoMapper;
using BMW.CloudAdoption.BOM.Modules.Bom.Models.Responses;
using BMW.CloudAdoption.BOM.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BMW.CloudAdoption.BOM.Modules.Bom.Handlers;

public class GetBomById : IRequestHandler<Queries.GetBomById, IResult>
{
    private readonly BomContext _bomContext;
    private readonly IMapper _mapper;

    public GetBomById(BomContext bomContext, IMapper mapper)
    {
        _bomContext = bomContext;
        _mapper = mapper;
    }
    
    public async Task<IResult> Handle(Queries.GetBomById request, CancellationToken cancellationToken)
    {
        var boms = await _bomContext.BillOfMaterials
            .Include(x => x.BomPartFamilies)
            .ThenInclude(x => x.PartFamily)
            .ThenInclude(y => y.PartIds).FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            
        var data = _mapper.Map<BomResponse>(boms);
        return Results.Ok(data);
    }
}