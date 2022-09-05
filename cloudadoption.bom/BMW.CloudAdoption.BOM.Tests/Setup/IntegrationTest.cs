using System.Reflection;
using BMW.CloudAdoption.BOM.BackgroundWorkers;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using NUnit.Framework;

namespace BMW.CloudAdoption.BOM.Tests.Setup;

[SetUpFixture]
public class IntegrationTest
{
    protected HttpClient TestClient = new();
    protected TestServer Server = default!;
    private WebApplicationFactory<Program> _appFactory = default!;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        LoadConfiguration();
        _appFactory = new WebApplicationFactory<Program>().WithWebHostBuilder(
            builder =>
            {
                builder
                    .ConfigureTestServices(
                        services =>
                        {
                            var bomDescriptors = services.SingleOrDefault(
                                d => d.ImplementationType ==
                                     typeof(BomProducer));

                            var partDescriptors = services.SingleOrDefault(
                                d => d.ImplementationType ==
                                     typeof(PartsConsumer));

                            services.Remove(bomDescriptors!);
                            services.Remove(partDescriptors!);
                        });
            });
        TestClient = _appFactory.CreateClient();
        Server = _appFactory.Server;
    }

    [OneTimeTearDown]
    public void OneTimeTeardown()
    {
        TestClient.Dispose();
        _appFactory.Dispose();
    }

    private static void LoadConfiguration()
    {
        var directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;
        var text = File.ReadAllText(Path.Combine(directory, "appsettings.IntegrationTests.json"));
        var settings = JsonConvert.DeserializeObject<Dictionary<string, string>>(text);

        if (settings == null) return;

        foreach (var setting in settings)
        {
            Environment.SetEnvironmentVariable(setting.Key, setting.Value);
        }
    }
}