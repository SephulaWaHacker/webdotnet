using AutoMapper;
using BMW.CloudAdoption.BOM.Core.Mappings.Actions;
using BMW.CloudAdoption.BOM.Domain.Entities;
using BMW.CloudAdoption.BOM.Modules.Bom.Models.Responses;
using BMW.CloudAdoption.BOM.Modules.Part.Models.Responses;
using BMW.CloudAdoption.BOM.Modules.PartFamily.Models.Responses;

namespace BMW.CloudAdoption.BOM.Core.Mappings;

public class DomainToResponseMappingProfile : Profile
{
    
    public DomainToResponseMappingProfile()
    {
        CreateMap<Bom, BomResponse>()
            .ForMember(x => x.EndDate, opt => opt.MapFrom(y => DateOnly.FromDateTime(y.EndDate)))
            .ForMember(x => x.StartDate, opt => opt.MapFrom(y => DateOnly.FromDateTime(y.StartDate)))
            .ForMember(x => x.PartFamilies, opt => opt.MapFrom(x => x.BomPartFamilies.Select(y => y.PartFamily)));
        CreateMap<PartFamily, PartFamilyResponse>()
            .ForMember(x => x.Parts, opt => opt.MapFrom(x => x.PartIds));
        CreateMap<PartId, PartResponse>()
            .AfterMap<PartResponseMappingAction>();
    }
}