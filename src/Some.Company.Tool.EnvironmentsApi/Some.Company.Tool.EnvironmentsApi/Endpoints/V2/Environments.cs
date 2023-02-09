using Microsoft.EntityFrameworkCore;
using Some.Company.Tool.EnvironmentsApi.Services;

namespace Some.Company.Tool.EnvironmentsApi.Endpoints.V2;

internal static class Environments
{
    private const string VersionLabel = "V2";
    private const string ApiPath = $"/{VersionLabel}/environments";
    
    public static RouteGroupBuilder MapApiV2(this RouteGroupBuilder group)
    {
        group.MapGet("/", GetAll);
        group.MapGet("/{id:int}", GetById);
        group.MapPost("/", Create)
            .AddEndpointFilter(async (efiContext, next) =>
            {
                var param = efiContext.GetArgument<Environment>(0);

                var validationErrors = Utilities.IsValid(param);

                return validationErrors.Any()
                    ? Results.ValidationProblem(validationErrors)
                    : await next(efiContext);
            });

        group.MapPut("/{id:int}", Update);
        group.MapDelete("/{id:int}", Delete);

        return group;
    }

    private static async Task<IResult> GetAll(EnvironmentDb database)
    {
        var todos = await database.Environments.ToListAsync();
        return TypedResults.Ok(todos);
    }

    private static async Task<IResult> GetById(int id, EnvironmentDb database) =>
        await database.Environments.FindAsync(id) is { } environment
            ? Results.Ok(environment)
            : Results.NotFound();

    private static async Task<IResult> Create(
        Environment environment, EnvironmentDb database, IEnvironmentNotification notification)
    {
        database.Environments.Add(environment);
        await database.SaveChangesAsync();
        await notification.NotifyCreated(environment);
        return Results.Created($"{ApiPath}/{environment.Id}", environment);
    }

    private static async Task<IResult> Update(
        EnvironmentDb database, IEnvironmentNotification notification, Environment environment, int id)
    {
        var existingEnvironment = await database.Environments.FindAsync(id);

        if (existingEnvironment == null)
            return TypedResults.NotFound();

        var newEnvironment = existingEnvironment with
        {
            Title = environment.Title,
            Description = environment.Description
        };

        await database.SaveChangesAsync();

        await notification.NotifyUpdated(existingEnvironment, newEnvironment);

        return TypedResults.Created($"{ApiPath}/{id}", newEnvironment);
    }

    private static async Task<IResult> Delete(
        int id, EnvironmentDb database, IEnvironmentNotification notification)
    {
        var todo = await database.Environments.FindAsync(id);

        if (todo == null)
            return TypedResults.NotFound();

        database.Environments.Remove(todo);
        await database.SaveChangesAsync();
        await notification.NotifyDeleted(id);
        return TypedResults.NoContent();
    }
}