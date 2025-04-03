using Lib.Core.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Lib.Infrastructure.Services
{
    public class BooksOverdueService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<BooksOverdueService> _logger;
        public BooksOverdueService(IServiceProvider service,
            ILogger<BooksOverdueService> logger)
        {
            _serviceProvider = service;
            _logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                using var scope = _serviceProvider.CreateScope();
                var booksRepository = scope.ServiceProvider.GetRequiredService<IBooksRepository>();
                var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();

                var books = await booksRepository.GetOverdueBooksAsync(cancellationToken);

                if (books?.Any() == true)
                {
                    foreach (var book in books)
                    {
                        try
                        {
                            _logger.LogInformation($"Sending overdue(book {book.Name}) notification to user {book.User.Email}");
                            await notificationService.SendOverdueNotificationAsync(book.User, book);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, $"Failed to send notification for book {book.Id}");
                        }
                    }
                }

                await Task.Delay(TimeSpan.FromMinutes(1), cancellationToken);
            }
        }
    }
}
