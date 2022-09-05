using MediatR;

namespace BMW.CloudAdoption.BOM.Modules.Bom.Queries;

public class GetBomById : IRequest<IResult>
{
    public int Id { get; }

    public GetBomById(int id) 
        => Id = id;
}