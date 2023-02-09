using Microsoft.EntityFrameworkCore;
using Some.Company.Tool.EnvironmentsApi;
using Some.Company.Tool.EnvironmentsApi.Endpoints.V1;
using Some.Company.Tool.EnvironmentsApi.Endpoints.V2;
using Some.Company.Tool.EnvironmentsApi.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IEnvironmentNotification, EnvironmentNotification>();
builder.Services.AddDbContext<EnvironmentDb>(
    opt => opt.UseInMemoryDatabase("TodoList")
);
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

app.MapGet("/", () => $"Environment API v.{VersionString.Get()}");
app.MapGroup("/v1/environment")
    .MapApiV1()
    .WithTags("Environment endpoints");

app.MapGroup("/v2/environment")
    .MapApiV2()
    .WithTags("Environment endpoints");

app.Run();