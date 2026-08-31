using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http.Json;
using GM.HttpClient.Sample.API;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace GM.HttpClient.Sample.Tests;

/// <summary>
/// Boots the consumer API in-memory and substitutes the two Refit downstream clients with fakes,
/// so the controller + GM.HttpClient registration are exercised end-to-end without the external
/// services running.
/// </summary>
public class SampleEndpointsTests(WebApplicationFactory<Program> factory)
    : IClassFixture<WebApplicationFactory<Program>>
{
    private System.Net.Http.HttpClient CreateClientWithFakes() =>
        factory.WithWebHostBuilder(builder =>
            builder.ConfigureTestServices(services =>
            {
                services.AddSingleton<ISampleAPIService1>(new FakeSampleAPIService1());
                services.AddSingleton<ISampleAPIService2>(new FakeSampleAPIService2());
            }))
            .CreateClient();

    [Fact]
    public async Task Get_returns_downstream_service1_result()
    {
        var client = CreateClientWithFakes();

        var response = await client.GetAsync("/api/v1/Sample");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("test is successful", await response.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task Post_returns_downstream_service2_result()
    {
        var client = CreateClientWithFakes();

        var response = await client.PostAsync("/api/v1/Sample", content: null);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var model = await response.Content.ReadFromJsonAsync<SampleModel>();
        Assert.NotNull(model);
        Assert.Equal(1, model.Sample1);
        Assert.Equal("Test", model.Sample2);
    }

    [Theory]
    [InlineData("/health/live")]
    [InlineData("/health/ready")]
    public async Task Health_endpoints_report_healthy(string path)
    {
        var client = CreateClientWithFakes();

        var response = await client.GetAsync(path);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [SuppressMessage("Major Code Smell", "S101:Types should be named in PascalCase",
        Justification = "Name mirrors the ISampleAPIService1 interface it fakes; API is kept uppercase for consistency.")]
    private sealed class FakeSampleAPIService1 : ISampleAPIService1
    {
        public Task<string> GetSample(string request, CancellationToken cancellationToken)
            => Task.FromResult($"{request} is successful");
    }

    [SuppressMessage("Major Code Smell", "S101:Types should be named in PascalCase",
        Justification = "Name mirrors the ISampleAPIService2 interface it fakes; API is kept uppercase for consistency.")]
    private sealed class FakeSampleAPIService2 : ISampleAPIService2
    {
        public Task<SampleModel> PostSample(SampleModel request, CancellationToken cancellationToken)
            => Task.FromResult(request);
    }
}
