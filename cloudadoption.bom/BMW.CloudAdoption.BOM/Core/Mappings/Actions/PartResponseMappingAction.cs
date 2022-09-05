using AutoMapper;
using BMW.CloudAdoption.BOM.Domain.Entities;
using BMW.CloudAdoption.BOM.Modules.Part.Models.Responses;
using BMW.CloudAdoption.BOM.Persistence.Cache;

namespace BMW.CloudAdoption.BOM.Core.Mappings.Actions;

public class PartResponseMappingAction: IMappingAction<PartId, PartResponse>
{
    private readonly IPartsCache _partsCache;

    public PartResponseMappingAction(IPartsCache partsCache) 
        => _partsCache = partsCache;

    public void Process(PartId source, PartResponse destination, ResolutionContext context)
    {
        var part = _partsCache.Get(source.Id);
        if (part == null) return;

        destination.Assembled = part.Assembled;
        destination.UnitType = part.UnitType;
    }
}