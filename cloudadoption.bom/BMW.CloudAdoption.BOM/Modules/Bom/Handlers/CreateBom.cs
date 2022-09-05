using AutoMapper;
using BMW.CloudAdoption.BOM.Modules.Bom.Models.Responses;
using BMW.CloudAdoption.BOM.Persistence.Context;
using MediatR;

namespace BMW.CloudAdoption.BOM.Modules.Bom.Handlers;

public class CreateBom : IRequestHandler<Commands.CreateBom, IResult>
{
    private readonly BomContext _bomContext;
    private readonly IMapper _mapper;

    public CreateBom(BomContext bomContext, IMapper mapper)
    {
        _bomContext = bomContext;
        _mapper = mapper;
    }
    // Task<ActionResult<IList<Todo>>>
    public async Task<IResult> Handle(Commands.CreateBom request, CancellationToken cancellationToken)
    {
        var bom = _mapper.Map<Domain.Entities.Bom>(request.BomRequest);

        if (bom.StartDate < bom.EndDate)
        {
            return Results.Text("Start date cannot be greater than End Date");
        }

        var entry = await _bomContext.BillOfMaterials.AddAsync(bom, cancellationToken);
        await _bomContext.SaveChangesAsync(cancellationToken);

        var bomResponse = _mapper.Map<BomResponse>(entry.Entity);
        
        return Results.CreatedAtRoute("GetBomById", new {id = bomResponse.Id}, bomResponse);
    }
}