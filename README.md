<p align="center">
  <img src="icon.png" alt="GM.HttpClient Samples" width="140" height="140" />
</p>

# GM.HttpClient Samples

[![CI](https://github.com/gmetskhvarishvili/GM.HttpClient.Samples/actions/workflows/ci.yml/badge.svg)](https://github.com/gmetskhvarishvili/GM.HttpClient.Samples/actions/workflows/ci.yml)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE)

A runnable ASP.NET Core sample that shows how to use **[GM.HttpClient](https://www.nuget.org/packages/GM.HttpClient)**
to call downstream services through [Refit](https://github.com/reactiveui/refit) with a configurable
[Polly](https://github.com/App-vNext/Polly) resilience pipeline and delegating handlers (auth,
caching, logging, header injection, context propagation) — all wired from configuration. Targets `net10.0`.

## Projects

```
GM.HttpClient.Samples/
├── GM.HttpClient.Sample.API/            # Consumer API — uses GM.HttpClient to call the two downstream APIs
├── GM.HttpClient.Sample.API.External/   # Downstream mock #1 — GET /sample  (https://localhost:7082)
├── GM.HttpClient.Sample.API.External2/  # Downstream mock #2 — POST /sample (https://localhost:7081)
└── tests/
    └── GM.HttpClient.Sample.Tests/      # In-memory integration tests for the consumer API
```

The **consumer API** declares two Refit interfaces and registers each with a single call:

```csharp
builder.Services.AddGMHttpClient<ISampleAPIService1, GMAPIClientOptions>(
    builder.Configuration.GetSection("ApiServices:SampleAPIService1"),
    "SampleAPIService1");
```

Everything else — base address, retry, circuit breaker, timeout, fallback, Basic auth, response
caching and static headers — comes from `appsettings.json` under `ApiServices:<name>` (the shape
matches `GMAPIClientOptions`). Only `GM.HttpClient` is referenced; Refit and Polly flow in
transitively.

## Requirements

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- The [`GM.HttpClient`](https://www.nuget.org/packages/GM.HttpClient) package (restored automatically).

## Running

Start the two downstream mocks, then the consumer API (three terminals):

```bash
dotnet run --project GM.HttpClient.Sample.API.External     # GET  /sample  on :7082
dotnet run --project GM.HttpClient.Sample.API.External2    # POST /sample  on :7081
dotnet run --project GM.HttpClient.Sample.API              # consumer API
```

Open the consumer API's **/swagger** and try:

| Method | Route | Calls | Result |
| --- | --- | --- | --- |
| `GET`  | `/api/v1/Sample` | `External` GET `/sample`  | `"test is successful"` |
| `POST` | `/api/v1/Sample` | `External2` POST `/sample` | echoed `SampleModel` |

The consumer API also exposes `/health/live` and `/health/ready`.

## Testing

```bash
dotnet test
```

The suite boots the consumer API in-memory with `WebApplicationFactory` and substitutes the two
Refit downstream clients with fakes, so it verifies the controller and DI wiring end-to-end without
needing the external services running.

## License

MIT — see [LICENSE](LICENSE).
