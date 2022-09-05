using BMW.CloudAdoption.BOM.Modules.Bom.Models.Requests;
using MediatR;

namespace BMW.CloudAdoption.BOM.Modules.Bom.Commands;

public class UpdateBom : IRequest<IResult>
{
    public int Id { get; }
    public BomRequest BomRequest { get; }

    public UpdateBom(int id, BomRequest bomRequest)
    {
        Id = id;
        BomRequest = bomRequest;
    }
}