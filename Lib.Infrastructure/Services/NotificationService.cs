using Lib.Core.Abstractions;
using Lib.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Infrastructure.Services
{
    public class NotificationService : INotificationService
    {
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
        private async Task SendEmailNotification(string email, string title, string message)
        {
            //
        }
    }
}
