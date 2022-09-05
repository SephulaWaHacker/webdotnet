using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using BMW.CloudAdoption.BOM.Domain.Entities;
using BMW.CloudAdoption.BOM.Modules.Bom.Models.Responses;
using BMW.CloudAdoption.BOM.Persistence.Context;
using BMW.CloudAdoption.BOM.Tests.Setup;
using DateOnlyTimeOnly.AspNet.Converters;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace BMW.CloudAdoption.BOM.Tests.Integration;

[TestFixture]
public class BomIntegrationTests : IntegrationTest
{
    [SetUp]
    public async Task Setup()
    {
        using var scope = Server.Services.CreateScope();
        var bomContext = scope.ServiceProvider.GetRequiredService<BomContext>();
        bomContext.BillOfMaterials.Add(
            new Bom()
            {
                Status = BillOfMaterialStatus.DRAFT,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(5),
                VehicleId = "123",
                BomPartFamilies = new List<BomPartFamily>()
            });
        await bomContext.SaveChangesAsync();
    }

    [TearDown]
    public async Task Teardown()
    {
        using var scope = Server.Services.CreateScope();
        var bomContext = scope.ServiceProvider.GetRequiredService<BomContext>();
        bomContext.BillOfMaterials.RemoveRange(bomContext.BillOfMaterials);
        await bomContext.SaveChangesAsync();
    }

    private static JsonSerializerOptions CreatJsonOptions()
    {
        var options = new JsonSerializerOptions();
        options.Converters.Add(new JsonStringEnumConverter());
        options.Converters.Add(new DateOnlyJsonConverter());

        return options;
    }

    [Test]
    public async Task Get_All_Boms_Successfully()
    {
        var response = await TestClient.GetAsync("api/v1/bom");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        var boms = JsonSerializer.Deserialize<List<BomResponse>>(
            await response.Content.ReadAsStringAsync(), CreatJsonOptions());
        Assert.NotNull(boms);
        Assert.That(boms!.Count, Is.EqualTo(1));
    }
}