using EventPlanner.Domain.Services;

namespace EventPlanner.Api
{
    public class EmailServiceConfiguration : IEmailServiceConfiguration
    {
        private readonly IConfiguration _configuration;

        public EmailServiceConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string SmtpProviderHost => _configuration.GetValue<string>("EmailConfig:SmtpProviderHost");
        public int SmtpProviderPort => _configuration.GetValue<int>("EmailConfig:SmtpProviderPort");
        public string SmtpProviderUsername => _configuration.GetValue<string>("EmailConfig:SmtpProviderUsername");
        public string SmtpProviderPassword => _configuration.GetValue<string>("EmailConfig:SmtpProviderPassword");
        public string AdministratorEmail => _configuration.GetValue<string>("EmailConfig:AdministratorEmail");
        public string AdministratorName => _configuration.GetValue<string>("EmailConfig:AdministratorName");
    }
}
