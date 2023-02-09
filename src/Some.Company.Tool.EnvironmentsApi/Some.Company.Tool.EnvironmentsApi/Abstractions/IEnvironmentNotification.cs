namespace Some.Company.Tool.EnvironmentsApi.Services;

public interface IEnvironmentNotification
{
    Task NotifyCreated(Environment environment);
    Task NotifyDeleted(int environment);
    Task NotifyUpdated(Environment previous, Environment current);
}