using MediatR;

namespace BMW.CloudAdoption.BOM.Modules.Bom.Commands;

public class PublishBom : IRequest<IResult>
{
    public int Id { get; }

    public PublishBom(int id) 
        => Id = id;
}