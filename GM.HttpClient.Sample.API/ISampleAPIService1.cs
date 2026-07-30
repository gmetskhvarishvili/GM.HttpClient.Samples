using Refit;

namespace GM.HttpClient.Sample.API;

public interface ISampleAPIService1
{
    [Get("/sample")]
    Task<string> GetSample(string request, CancellationToken cancellationToken);
}