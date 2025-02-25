using EventPlanner.Domain.Services;
using System.Reflection;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using EventPlanner.Domain.Entities.Email;

namespace EventPlanner.EmailSender
{
    public class EmailService : IEmailService
    {
        private readonly IEmailServiceConfiguration emailServiceConfiguration;
        private readonly InternetAddress internetAddress;

        public EmailService(IEmailServiceConfiguration emailServiceConfiguration)
        {
            this.emailServiceConfiguration = emailServiceConfiguration;
            internetAddress = MailboxAddress.Parse(emailServiceConfiguration.SmtpProviderUsername);
        }

        private async Task SendEmail(ReceiverInfo receiverInfoDto, string htmlEmailBody, string subject, string status = "")
        {
            MimeMessage email = new MimeMessage();
            email.From.Add(internetAddress);
            email.To.Add(new MailboxAddress(receiverInfoDto.FirstName, receiverInfoDto.Email));
            email.Body = new TextPart(TextFormat.Html) { Text = htmlEmailBody };

            if (status == "")
            {
                email.Subject = subject;
            }
            else
            {
                email.Subject = subject + " " + status;
            }

            await SendEmailViaSmtp(email);
        }

        public async Task SendEmailViaSmtp(MimeMessage email)
        {
            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            await smtp.ConnectAsync(emailServiceConfiguration.SmtpProviderHost,
                emailServiceConfiguration.SmtpProviderPort,
                SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(emailServiceConfiguration.SmtpProviderUsername,
                emailServiceConfiguration.SmtpProviderPassword);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }

        public async Task SendCreatedEventNotification(CreatedEventInfo createdEventInfo)
        {
            ReceiverInfo administratorInfo = new ReceiverInfo()
            {
                Email = emailServiceConfiguration.AdministratorEmail,
                FirstName = emailServiceConfiguration.AdministratorName
            };
            string htmlEmailBody = GetCreatedEventEmailBody(createdEventInfo, "You have a new event request: ");
            await SendEmail(administratorInfo, htmlEmailBody, "New event request!");
        }

        public async Task SendApprovedEventNotification(ReceiverInfo receiverInfo, UpdatedEventInfo updatedEventInfo)
        {
            string htmlEmailBody = GetUpdatedEventEmailBody(updatedEventInfo, "Your event request has been approved: ");
            await SendEmail(receiverInfo, htmlEmailBody, "Event request approved!");
        }

        public async Task SendRejectedEventNotification(ReceiverInfo receiverInfo, UpdatedEventInfo updatedEventInfo)
        {
            string htmlEmailBody = GetUpdatedEventEmailBody(updatedEventInfo, "Your event request has been rejected: ");
            await SendEmail(receiverInfo, htmlEmailBody, "Event request rejected");
        }

        private string ReadEmailTemplate(string templatePath)
        {
            string assemblyLocation = Assembly.GetExecutingAssembly().Location;
            string projectPath = Directory.GetParent(assemblyLocation)?.Parent?.Parent?.Parent?.Parent?.FullName;
            string buildDirectory = Path.Combine(projectPath, "EventPlanner.EmailSender");
            string templateFullPath = $@"{buildDirectory}\{templatePath}";
            return File.ReadAllText(templateFullPath);
        }

        private string GetCreatedEventEmailBody(CreatedEventInfo createdEventInfo, string introduction)
        {
            string htmlTemplate = ReadEmailTemplate(@"EmailTemplates\CreatedEventEmailTemplate.html");
            htmlTemplate = htmlTemplate.Replace("{introduction}", introduction);
            htmlTemplate = htmlTemplate.Replace("{event}", createdEventInfo.EventName.ToString());
            htmlTemplate = htmlTemplate.Replace("{location}", createdEventInfo.LocationName.ToString());
            htmlTemplate = htmlTemplate.Replace("{creator}", createdEventInfo.Creator);
            htmlTemplate = htmlTemplate.Replace("{createdDate}", createdEventInfo.CreatedDate.ToShortDateString());
            htmlTemplate = htmlTemplate.Replace("{link}", "http://localhost:4200/app/view-event-requests");
            htmlTemplate = htmlTemplate.Replace("{buttonContent}", "Click to view event");
            return htmlTemplate;
        }

        private string GetUpdatedEventEmailBody(UpdatedEventInfo updatedEventInfo, string introduction)
        {
            string htmlTemplate = ReadEmailTemplate(@"EmailTemplates\UpdatedEventEmailTemplate.html");
            htmlTemplate = htmlTemplate.Replace("{introduction}", introduction);
            htmlTemplate = htmlTemplate.Replace("{event}", updatedEventInfo.EventName.ToString());
            htmlTemplate = htmlTemplate.Replace("{location}", updatedEventInfo.LocationName.ToString());
            htmlTemplate = htmlTemplate.Replace("{responseDate}", updatedEventInfo.ResponseDate.ToShortDateString());
            htmlTemplate = htmlTemplate.Replace("{link}", "http://localhost:4200/app/view-event-requests");
            htmlTemplate = htmlTemplate.Replace("{buttonContent}", "Click to view event");
            return htmlTemplate;
        }
    }
}
