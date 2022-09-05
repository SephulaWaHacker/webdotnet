using BMW.CloudAdoption.BOM.Modules.PartFamily.Models.Requests;
using MediatR;

namespace BMW.CloudAdoption.BOM.Modules.PartFamily.Commands;

public class UpdatePartFamily : IRequest<IResult>
{
    public int Id { get; }
    public PartFamilyRequest PartFamilyRequest { get; }

    public UpdatePartFamily(int id,PartFamilyRequest partFamilyRequest)
    {
        Id = id;
        PartFamilyRequest = partFamilyRequest;
    }
}