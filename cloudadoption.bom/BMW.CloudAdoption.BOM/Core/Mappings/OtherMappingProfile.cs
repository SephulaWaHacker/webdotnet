using AutoMapper;
using BMW.CloudAdoption.BOM.Core.Models;
using BMW.CloudAdoption.BOM.Modules.Part.Models.Responses;

namespace BMW.CloudAdoption.BOM.Core.Mappings;

public class OtherMappingProfiles : Profile
{
    public OtherMappingProfiles()
    {
        CreateMap<Part, PartDetailResponse>();
        CreateMap<PartsPlant, DetailedPartsPlant>();
    }
}