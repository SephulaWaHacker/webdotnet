using AutoMapper;
using BMW.CloudAdoption.BOM.Core.Models;
using BMW.CloudAdoption.BOM.Modules.Part.Models.Responses;
using BMW.CloudAdoption.BOM.Persistence.Cache;
using MediatR;

namespace BMW.CloudAdoption.BOM.Modules.Part.Handlers;

public class GetAllParts : IRequestHandler<Queries.GetAllParts, IResult>
{
    private readonly IPartsCache _partsCache;

    private IMapper _mapper;

    public GetAllParts(IPartsCache partsCache, IMapper mapper)
    {
        _partsCache = partsCache;
        _mapper = mapper;
    }

    public Task<IResult> Handle(Queries.GetAllParts request, CancellationToken cancellationToken)
    {
        var parts = _partsCache.GetAll().Where(x => x?.Status == PartStatus.VALID);
        var results = _mapper.Map<IEnumerable<PartDetailResponse>>(parts);
        return Task.FromResult(Results.Ok(results));
    }
}