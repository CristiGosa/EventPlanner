namespace EventPlanner.Domain.Services
{
    public interface IEmailServiceConfiguration
    {
        string SmtpProviderHost { get; }
        int SmtpProviderPort { get; }
        string SmtpProviderUsername { get; }
        string SmtpProviderPassword { get; }
        string AdministratorEmail { get; }
        string AdministratorName { get; }
    }
}
