using BMW.CloudAdoption.BOM.Modules.PartFamily.Commands;
using BMW.CloudAdoption.BOM.Modules.PartFamily.Models.Requests;
using BMW.CloudAdoption.BOM.Modules.PartFamily.Models.Responses;
using BMW.CloudAdoption.BOM.Modules.PartFamily.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BMW.CloudAdoption.BOM.Modules.PartFamily;

public class PartFamilyModule : IModule
{
    private const string ModulePath = "api/v1/part-family";
    private const string ModuleTag = "PartFamily";

    public IServiceCollection RegisterModule(IServiceCollection services)
        => services;

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet(ModulePath,
                async ([FromServices] IMediator mediatr) => await mediatr.Send(new GetAllPartFamilies()))
            .Produces<IEnumerable<PartFamilyResponse>>()
            .WithTags(ModuleTag);

        endpoints.MapGet($"{ModulePath}/{{id:int}}",
                async ([FromServices] IMediator mediatr, int id) => await mediatr.Send(new GetPartFamilyById(id)))
            .Produces<PartFamilyResponse>()
            .WithTags(ModuleTag).WithName("GetPartFamilyById");

        endpoints.MapPost(ModulePath,
            async ([FromServices] IMediator mediatr, PartFamilyRequest partFamilyRequest) =>
                await mediatr.Send(new CreatePartFamily(partFamilyRequest)))
            .Produces<PartFamilyResponse>()
            .WithTags(ModuleTag);

        endpoints.MapDelete($"{ModulePath}/{{id:int}}",
                async ([FromServices] IMediator mediatr, int id) => await mediatr.Send(new DeletePartFamily(id)))
            .WithTags(ModuleTag);

        endpoints.MapPut($"{ModulePath}/{{id:int}}",
            async ([FromServices] IMediator mediatr, PartFamilyRequest partFamilyRequest, int id) =>
                await mediatr.Send(new UpdatePartFamily(id, partFamilyRequest))).WithTags(ModuleTag);

        endpoints.MapPost($"{ModulePath}/{{id:int}}/part",
            async ([FromServices] IMediator mediatr, int id, CreatePartFamilyPartRequest createPartFamilyPartRequest) =>
                await mediatr.Send(new CreatePartFamilyPart(id, createPartFamilyPartRequest)))
            .Produces<PartFamilyResponse>()
            .WithTags(ModuleTag);

        endpoints.MapDelete($"{ModulePath}/{{id:int}}/part/{{partId}}",
            async ([FromServices] IMediator mediatr, int id, string partId) =>
                await mediatr.Send(new DeletePartFamilyPart(id, partId))).WithTags(ModuleTag);

        return endpoints;
    }
}