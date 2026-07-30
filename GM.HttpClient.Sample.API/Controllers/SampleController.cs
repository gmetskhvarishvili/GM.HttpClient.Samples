using Microsoft.AspNetCore.Mvc;

namespace GM.HttpClient.Sample.API.Controllers;

[ApiController]
[Route("[controller]")]
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