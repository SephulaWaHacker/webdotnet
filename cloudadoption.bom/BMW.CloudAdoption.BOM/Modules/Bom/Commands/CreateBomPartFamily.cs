using MediatR;

namespace BMW.CloudAdoption.BOM.Modules.Bom.Commands;

public class CreateBomPartFamily : IRequest<IResult>
{
    public int Id { get; }
    public int PartFamilyId { get; }

    public CreateBomPartFamily(int id, int partFamilyId)
    {
        Id = id;
        PartFamilyId = partFamilyId;
    }
}