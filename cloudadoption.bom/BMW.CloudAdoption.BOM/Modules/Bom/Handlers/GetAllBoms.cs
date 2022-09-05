using AutoMapper;
using BMW.CloudAdoption.BOM.Modules.Bom.Models.Responses;
using BMW.CloudAdoption.BOM.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BMW.CloudAdoption.BOM.Modules.Bom.Handlers;

public class GetAllBoms : IRequestHandler<Queries.GetAllBoms, IResult>
{
    private readonly BomContext _bomContext;
    private readonly IMapper _mapper;

    public GetAllBoms(BomContext bomContext, IMapper mapper)
    {
        _bomContext = bomContext;
        _mapper = mapper;
    }
    
    public async Task<IResult> Handle(Queries.GetAllBoms request, CancellationToken cancellationToken)
    {
            var boms = await _bomContext.BillOfMaterials
                .Include(x => x.BomPartFamilies)
                .ThenInclude(x => x.PartFamily)
                .ThenInclude(y => y.PartIds).ToListAsync(cancellationToken: cancellationToken);
            
            var data = _mapper.Map<List<BomResponse>>(boms);
            return Results.Ok(data);
    }
}