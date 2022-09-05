using BMW.CloudAdoption.BOM.Modules.PartFamily.Models.Requests;
using MediatR;

namespace BMW.CloudAdoption.BOM.Modules.PartFamily.Commands;

public class CreatePartFamily : IRequest<IResult>
{
    public PartFamilyRequest PartFamilyRequest { get; }

    public CreatePartFamily(PartFamilyRequest partFamilyRequest)
    {
        PartFamilyRequest = partFamilyRequest;
    }
}