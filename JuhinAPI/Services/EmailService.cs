using JuhinAPI.Data;
using MimeKit;
using MailKit.Net.Smtp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MimeKit.Text;

namespace JuhinAPI.Services
{
    public class EmailService : IEmailService
    {
        private readonly IEmailConfiguration _emailConfiguration;

        public EmailService(IEmailConfiguration emailConfiguration)
        {
            _emailConfiguration = emailConfiguration;
        }

        public List<EmailMessage> ReceiveEmail(int maxCount)
        {
            throw new NotImplementedException();
        }

        public async Task Send(EmailMessage emailMessage)
        {
			var message = new MimeMessage();
			message.To.AddRange(emailMessage.ToAddresses.Select(x => new MailboxAddress(x.Name, x.Address)));
			message.From.Add(new MailboxAddress(emailMessage.FromAddress.Name , emailMessage.FromAddress.Address));

			message.Subject = emailMessage.Subject;
			message.Body = new TextPart(TextFormat.Html)
			{
				Text = emailMessage.Content
			};

			
			using (var emailClient = new SmtpClient())
			{
                try
                {
				    emailClient.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.SmtpPort, true);
				    emailClient.AuthenticationMechanisms.Remove("XOAUTH2");
				    emailClient.Authenticate(_emailConfiguration.SmtpUsername, _emailConfiguration.SmtpPassword);
				    await emailClient.SendAsync(message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("EMAIL CLIENT EXCEPTION: "+ex.Message);
                }
                finally
                {
				    emailClient.Disconnect(true);
                }

			}
		}
    }
}
