using Lib.Core.Entities;

namespace Lib.Core.Abstractions.Services
{
    public interface INotificationService
    {
        Task SendBorrowNotificationAsync(UserEntity userEntity, BookEntity bookEntity);
        Task SendOverdueNotificationAsync(UserEntity userEntity, BookEntity bookEntity);
    }
}