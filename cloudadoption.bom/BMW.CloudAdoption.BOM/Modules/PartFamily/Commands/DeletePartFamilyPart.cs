using MediatR;

namespace BMW.CloudAdoption.BOM.Modules.PartFamily.Commands;

public class DeletePartFamilyPart: IRequest<IResult>
{
    public int Id { get; }
    public string PartId { get; }

    public DeletePartFamilyPart(int id, string partId)
    {
        Id = id;
        PartId = partId;
    }
}