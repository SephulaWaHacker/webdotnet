using MediatR;

namespace BMW.CloudAdoption.BOM.Modules.Bom.Commands;

public class DeleteBomPartFamily : IRequest<IResult>
{
    public int Id { get; }
    public int PartFamilyId { get; }

    public DeleteBomPartFamily(int id, int partFamilyId)
    {
        Id = id;
        PartFamilyId = partFamilyId;
    }
}