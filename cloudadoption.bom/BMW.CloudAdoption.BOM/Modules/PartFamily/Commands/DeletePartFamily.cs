using MediatR;

namespace BMW.CloudAdoption.BOM.Modules.PartFamily.Commands;

public class DeletePartFamily : IRequest<IResult>
{
    public int Id { get; }

    public DeletePartFamily(int id)
    {
        Id = id;
    }
}