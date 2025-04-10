using Lib.Core.Abstractions.Services;
using Lib.Core.Entities;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MailKit.Net.Smtp;
using MimeKit;

namespace Lib.Infrastructure.Services
{
    public class NotificationService : INotificationService
    {
        private readonly SMPTSettings _smtpSettings;

        public NotificationService(IOptions<SMPTSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }
        public async Task SendBorrowNotificationAsync(UserEntity userEntity, BookEntity bookEntity)
        {
            var title = $"Hello, {userEntity.Name}, you take book {bookEntity.Name}";

            var message = $"You must return book {bookEntity.Name} by {bookEntity.ReturnTime}";

            await SendEmailNotification(userEntity.Email, title, message);
        }

        public async Task SendOverdueNotificationAsync(UserEntity userEntity, BookEntity bookEntity)
        {
            var title = $"Hello, {userEntity.Name}, you are late with the book {bookEntity.Name}";

            var message = $"You must return book {bookEntity.Name} by {bookEntity.ReturnTime}";

            await SendEmailNotification(userEntity.Email, title, message);
        }
        private async Task SendEmailNotification(string email, string subject, string body)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(MailboxAddress.Parse(_smtpSettings.From));
            emailMessage.To.Add(MailboxAddress.Parse(email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text)
            {
                Text = body
            };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_smtpSettings.Host, _smtpSettings.Port, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_smtpSettings.Username, _smtpSettings.Password);
            await smtp.SendAsync(emailMessage);
            await smtp.DisconnectAsync(true);
        }
    }
}
