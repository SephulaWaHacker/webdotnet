using MediatR;

namespace BMW.CloudAdoption.BOM.Modules.PartFamily.Queries;

public class GetPartFamilyById : IRequest<IResult>
{
    public int Id { get; }

    public GetPartFamilyById(int id)
    {
        Id = id;
    }
}