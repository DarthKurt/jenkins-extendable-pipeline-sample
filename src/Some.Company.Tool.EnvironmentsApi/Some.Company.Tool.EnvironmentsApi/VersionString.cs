namespace Some.Company.Tool.EnvironmentsApi;

internal static class VersionString
{
    public static string Get() => typeof(VersionString).Assembly.GetName().Version?.ToString() ?? "UNKNOWN"; 
}