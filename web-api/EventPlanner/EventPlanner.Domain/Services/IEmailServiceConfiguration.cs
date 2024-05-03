namespace EventPlanner.Domain.Services
{
    public interface IEmailServiceConfiguration
    {
        string SmtpProviderHost { get; }
        int SmtpProviderPort { get; }
        string SmtpProviderUsername { get; }
        string SmtpProviderPassword { get; }
    }
}
