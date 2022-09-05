using BMW.CloudAdoption.BOM.Modules.PartFamily.Models.Requests;
using MediatR;

namespace BMW.CloudAdoption.BOM.Modules.PartFamily.Commands;

public class CreatePartFamilyPart: IRequest<IResult>
{
    public int Id { get; }
    public CreatePartFamilyPartRequest CreatePartFamilyPartRequest { get; }

    public CreatePartFamilyPart(int id, CreatePartFamilyPartRequest createPartFamilyPartRequest)
    {
        Id = id;
        CreatePartFamilyPartRequest = createPartFamilyPartRequest;
    }
}