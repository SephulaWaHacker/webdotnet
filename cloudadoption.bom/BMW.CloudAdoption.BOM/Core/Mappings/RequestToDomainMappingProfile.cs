using AutoMapper;
using BMW.CloudAdoption.BOM.Domain.Entities;
using BMW.CloudAdoption.BOM.Modules.Bom.Models.Requests;
using BMW.CloudAdoption.BOM.Modules.PartFamily.Models.Requests;

namespace BMW.CloudAdoption.BOM.Core.Mappings;

public class RequestToDomainMappingProfile : Profile
{
    public RequestToDomainMappingProfile()
    {
        CreateMap<BomRequest, Bom>()
            .ForMember(x => x.Status, expression => expression.NullSubstitute(BillOfMaterialStatus.DRAFT))
            .ForMember(x => x.StartDate, opt => opt.MapFrom(y => y.StartDate.ToDateTime(TimeOnly.MinValue)))
            .ForMember(x => x.EndDate, opt => opt.MapFrom(y => y.EndDate.ToDateTime(TimeOnly.MinValue)));
        CreateMap<PartFamilyRequest, PartFamily>();
    }
}