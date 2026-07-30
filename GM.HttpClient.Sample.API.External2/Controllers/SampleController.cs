using Microsoft.AspNetCore.Mvc;

namespace GM.HttpClient.Sample.API.External2.Controllers;

[ApiController]
[Route("[controller]")]
public class SampleController : ControllerBase
{
    [HttpPost]
    public SampleModel Post(
        SampleModel request, 
        CancellationToken cancellationToken)
    {
        return request;
    }
}

public class SampleModel
{
    public int Sample1 { get; set; }
    public string? Sample2 { get; set; }
}