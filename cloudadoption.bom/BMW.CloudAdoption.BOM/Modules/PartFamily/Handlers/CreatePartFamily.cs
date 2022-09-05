using AutoMapper;
using BMW.CloudAdoption.BOM.Modules.PartFamily.Models.Responses;
using BMW.CloudAdoption.BOM.Persistence.Context;
using MediatR;

namespace BMW.CloudAdoption.BOM.Modules.PartFamily.Handlers;

public class CreatePartFamily: IRequestHandler<Commands.CreatePartFamily, IResult>
{
    private readonly BomContext _bomContext;
    private readonly IMapper _mapper;

    public CreatePartFamily(BomContext bomContext, IMapper mapper)
    {
        _bomContext = bomContext;
        _mapper = mapper;
    }
    
    public async Task<IResult> Handle(Commands.CreatePartFamily request, CancellationToken cancellationToken)
    {
        var partFamily = _mapper.Map<Domain.Entities.PartFamily>(request.PartFamilyRequest);
        var entry = await _bomContext.PartFamilies.AddAsync(partFamily, cancellationToken);
        await _bomContext.SaveChangesAsync(cancellationToken);

        var partFamilyResponse = _mapper.Map<PartFamilyResponse>(entry.Entity);
        return Results.CreatedAtRoute("GetPartFamilyById", new {id = partFamilyResponse.Id}, partFamilyResponse);
    }
}