using Refit;

namespace GM.HttpClient.Sample.API;

public interface ISampleAPIService2
{
    [Post("/sample")]
    Task<SampleModel> PostSample(SampleModel request, CancellationToken cancellationToken);
}

public class SampleModel
{
    public int Sample1 { get; set; }
    public string? Sample2 { get; set; }
}