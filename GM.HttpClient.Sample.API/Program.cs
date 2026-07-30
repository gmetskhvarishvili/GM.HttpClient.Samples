using GM.HttpClient;
using GM.HttpClient.Sample.API;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddGMHttpClient<ISampleAPIService1, GMAPIClientOptions>(
    builder.Configuration.GetSection("ApiServices:SampleAPIService1"),
    "SampleAPIService1");

builder.Services.AddGMHttpClient<ISampleAPIService2, GMAPIClientOptions>(
    builder.Configuration.GetSection("ApiServices:SampleAPIService2"),
    "SampleAPIService2");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

// Exposed so the integration test project can bootstrap the app via WebApplicationFactory.
public partial class Program;