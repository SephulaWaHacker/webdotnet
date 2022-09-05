using BMW.CloudAdoption.BOM.Modules.Bom.Commands;
using BMW.CloudAdoption.BOM.Modules.Bom.Models.Requests;
using BMW.CloudAdoption.BOM.Modules.Bom.Models.Responses;
using BMW.CloudAdoption.BOM.Modules.Bom.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BMW.CloudAdoption.BOM.Modules.Bom;

public class BomModule : IModule
{
    private const string ModulePath = "api/v1/bom";
    private const string ModuleTag = "Bom";

    public IServiceCollection RegisterModule(IServiceCollection services)
        => services;

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet(ModulePath, async ([FromServices] IMediator mediatr) => await mediatr.Send(new GetAllBoms()))
            .Produces<IEnumerable<BomResponse>>()
            .WithTags(ModuleTag);

        endpoints.MapGet($"{ModulePath}/{{id:int}}",
                async ([FromServices] IMediator mediatr, int id) => await mediatr.Send(new GetBomById(id)))
            .Produces<BomResponse>()
            .WithName("GetBomById").WithTags(ModuleTag);

        endpoints.MapPost(ModulePath,
            async ([FromServices] IMediator mediatr, BomRequest bomRequest) =>
                await mediatr.Send(new CreateBom(bomRequest)))
            .Produces<BomResponse>()
            .WithTags(ModuleTag);

        endpoints.MapPut($"{ModulePath}/{{id:int}}",
            async ([FromServices] IMediator mediatr, int id, BomRequest bomRequest) =>
                await mediatr.Send(new UpdateBom(id, bomRequest))).WithTags(ModuleTag);

        endpoints.MapPost($"{ModulePath}/{{id:int}}/publish",
            async ([FromServices] IMediator mediatr, int id) =>
                await mediatr.Send(new PublishBom(id))).WithTags(ModuleTag);

        endpoints.MapPost($"{ModulePath}/{{id:int}}/part-family/{{partFamilyId}}",
            async ([FromServices] IMediator mediatr, int id, int partFamilyId) =>
                await mediatr.Send(new CreateBomPartFamily(id, partFamilyId)))
            .Produces<BomResponse>()
            .WithTags(ModuleTag);

        endpoints.MapDelete($"{ModulePath}/{{id:int}}/part-family/{{partFamilyId}}",
            async ([FromServices] IMediator mediatr, int id, int partFamilyId) =>
                await mediatr.Send(new DeleteBomPartFamily(id, partFamilyId))).WithTags(ModuleTag);

        return endpoints;
    }
}