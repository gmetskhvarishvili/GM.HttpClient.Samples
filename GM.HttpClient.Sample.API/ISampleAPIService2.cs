using System.Diagnostics.CodeAnalysis;
using Refit;

namespace GM.HttpClient.Sample.API;

[SuppressMessage("Major Code Smell", "S101:Types should be named in PascalCase",
    Justification = "API is kept uppercase to match this project's own name and namespace (GM.HttpClient.Sample.API).")]
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