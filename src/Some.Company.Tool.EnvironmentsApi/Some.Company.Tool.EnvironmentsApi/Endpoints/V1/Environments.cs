using Microsoft.EntityFrameworkCore;

namespace Some.Company.Tool.EnvironmentsApi.Endpoints.V1;

internal static class Environments
{
    private const string VersionLabel = "V1";
    private const string ApiPath = $"/{VersionLabel}/environments";
    
    public static RouteGroupBuilder MapApiV1(this RouteGroupBuilder group)
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

    private static async Task<IResult> Create(Environment environment, EnvironmentDb database)
    {
        database.Environments.Add(environment);
        await database.SaveChangesAsync();
        return Results.Created($"{ApiPath}/{environment.Id}", environment);
    }

    private static async Task<IResult> Update(Environment environment, EnvironmentDb database, int id)
    {
        var existingTodo = await database.Environments.FindAsync(environment.Id);

        if (existingTodo == null)
            return TypedResults.NotFound();

        existingTodo.Title = environment.Title;
        existingTodo.Description = environment.Description;

        await database.SaveChangesAsync();

        return TypedResults.Created($"{ApiPath}/{existingTodo.Id}", existingTodo);
    }

    private static async Task<IResult> Delete(int id, EnvironmentDb database)
    {
        var todo = await database.Environments.FindAsync(id);

        if (todo == null)
            return TypedResults.NotFound();

        database.Environments.Remove(todo);
        await database.SaveChangesAsync();

        return TypedResults.NoContent();
    }
}