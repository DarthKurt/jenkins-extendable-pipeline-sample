namespace Some.Company.Tool.EnvironmentsApi.Services;

internal sealed class EnvironmentNotification : IEnvironmentNotification
{
    public Task NotifyCreated(Environment environment)
    {
        throw new NotImplementedException();
    }

    public Task NotifyDeleted(int environment)
    {
        throw new NotImplementedException();
    }

    public Task NotifyUpdated(Environment previous, Environment current)
    {
        throw new NotImplementedException();
    }
}