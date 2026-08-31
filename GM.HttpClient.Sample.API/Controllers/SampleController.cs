using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;

namespace GM.HttpClient.Sample.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[SuppressMessage("Major Code Smell", "S6960:Controllers should not have mixed responsibilities",
    Justification = "This sample deliberately keeps one GET and one POST action together to demonstrate both a GM.HttpClient GET and POST downstream call side by side.")]
public class SampleController(
    ISampleAPIService1 sampleApiService1,
    ISampleAPIService2 sampleApiService2)
    : ControllerBase
{
    [HttpGet]
    public async Task<string> Get(CancellationToken cancellationToken)
    {
        return await sampleApiService1.GetSample("test", cancellationToken);
    }
    
    [HttpPost]
    public async Task<SampleModel> Post(CancellationToken cancellationToken)
    {
        return await sampleApiService2.PostSample(new SampleModel
        {
            Sample1 = 1,
            Sample2 = "Test"
        }, cancellationToken);
    }
}