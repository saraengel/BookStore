//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using BookStoreServer.Api.Entities.DTO;
//using BookStoreServer.Repository.Interfaces;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;
//using Microsoft.Extensions.Logging;

//namespace BookStoreServer.CommonServices
//{
//    public class OldBooksCleanupService : BackgroundService
//    {
//        private readonly IServiceProvider _serviceProvider;
//        private readonly ILogger<OldBooksCleanupService> _logger;
//        private readonly SignalRClientService _signalRClientService;

//        public OldBooksCleanupService(IServiceProvider serviceProvider, ILogger<OldBooksCleanupService> logger)
//        {
//            _serviceProvider = serviceProvider;
//            _logger = logger;
//        }

//        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//        {
//            _logger.LogInformation(" OldBooksCleanupService srated");

//            while (!stoppingToken.IsCancellationRequested)
//            {
//                //await Task.Delay(TimeSpan.FromHours(1), stoppingToken); 
//                await Task.Delay(TimeSpan.FromSeconds(13), stoppingToken);
//                await CleanOldBooksAsync();
//            }
//        }

//        private async Task CleanOldBooksAsync()
//        {
//            using (var scope = _serviceProvider.CreateScope())
//            {
//                IBookRepository _bookRepository = scope.ServiceProvider.GetRequiredService<IBookRepository>();
//                var tenYearsAgo = DateTime.Now.AddYears(-10);
//                List<BookDTO> oldBooks = await _bookRepository.GetOldBooksAsync(tenYearsAgo);
//                if (oldBooks.Any())
//                {
//                    _logger.LogInformation($"find {oldBooks.Count} old books");
//                    foreach (BookDTO book in oldBooks)
//                    {

//                        _bookRepository.DeleteBook(book.id);
//                    }
//                }
//                else
//                {
//                    _logger.LogInformation("ther is not old books");
//                }
//            }
//        }
//    }
//}
