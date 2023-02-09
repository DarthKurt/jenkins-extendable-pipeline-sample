namespace Some.Company.Tool.EnvironmentsApi;

internal static class Utilities
{
    public static Dictionary<string, string[]> IsValid(Environment td)
    {
        Dictionary<string, string[]> errors = new();

        if (string.IsNullOrEmpty(td.Title))
        {
            errors.TryAdd("environment.name.errors", new[] { "Name is empty" });
        }

        if (td.Title.Length < 3)
        {
            errors.TryAdd("environment.name.errors", new[] { "Name length < 3" });
        }

        return errors;
    }
}