using BMW.CloudAdoption.BOM.Modules.Part.Models.Responses;
using BMW.CloudAdoption.BOM.Modules.Part.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BMW.CloudAdoption.BOM.Modules.Part;

public class PartModule : IModule
{
    private const string ModulePath = "api/v1/part";
    private const string ModuleTag = "Part";

    public IServiceCollection RegisterModule(IServiceCollection services)
        => services;

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet(ModulePath, async ([FromServices] IMediator mediatr) => await mediatr.Send(new GetAllParts()))
            .Produces<IEnumerable<PartDetailResponse>>()
            .WithTags(ModuleTag);
        return endpoints;
    }
}