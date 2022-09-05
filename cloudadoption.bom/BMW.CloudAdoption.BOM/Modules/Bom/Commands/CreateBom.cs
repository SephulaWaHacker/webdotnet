using BMW.CloudAdoption.BOM.Modules.Bom.Models.Requests;
using MediatR;

namespace BMW.CloudAdoption.BOM.Modules.Bom.Commands;

public class CreateBom : IRequest<IResult>
{
    public BomRequest BomRequest { get; }

    public CreateBom(BomRequest bomRequest) 
        => BomRequest = bomRequest;
}