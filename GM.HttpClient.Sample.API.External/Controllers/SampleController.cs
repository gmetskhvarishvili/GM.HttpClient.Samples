using Microsoft.AspNetCore.Mvc;

namespace GM.HttpClient.Sample.API.External.Controllers;

[ApiController]
[Route("[controller]")]
public class SampleController : ControllerBase
{
    [HttpGet]
    public string Get(string request, CancellationToken cancellationToken)
    {
        return $"{request} is successful";
    }
}