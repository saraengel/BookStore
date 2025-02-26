using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BookStoreServer.Api.Entities.DTO;
using BookStoreServer.Repository.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BookStoreServer.Service.Services
{
    public class HandleLowStockBooks : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<HandleLowStockBooks> _logger;
        private readonly ConcurrentBag<BookDTO> _lowStockBooks = new ConcurrentBag<BookDTO>();
        private readonly IStoreHub _storeHub;
        private readonly SuppliersClientService _suppliersClientService;
        private readonly EmailService _emailService;

        public HandleLowStockBooks(IServiceProvider serviceProvider, SuppliersClientService suppliersClientService, ILogger<HandleLowStockBooks> logger, IStoreHub storeHub, EmailService emailService)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _suppliersClientService = suppliersClientService;
            _storeHub = storeHub;
            _emailService = emailService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("HandleLowStockBooks service is starting.");

            await _suppliersClientService.StartAsync();

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Checking for low stock books.");
                //todo
                //await Task.Delay(TimeSpan.FromDays(7), stoppingToken);
                await Task.Delay(TimeSpan.FromSeconds(6), stoppingToken);

                var checkLowStockBooksTask = CheckLowStockBooks(stoppingToken);
                var notifySuppliersTask = NotifySuppliers(stoppingToken);

                await Task.WhenAll(checkLowStockBooksTask, notifySuppliersTask);

                _logger.LogInformation("Treatment ended, low of stock.");
            }

            _logger.LogInformation("HandleLowStockBooks service is stopping.");
        }

        private async Task CheckLowStockBooks(CancellationToken stoppingToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var bookRepository = scope.ServiceProvider.GetRequiredService<IBookRepository>();
                var bookDTOs = await bookRepository.GetLowStockBooksAsync(stoppingToken);

                foreach (var item in bookDTOs)
                {
                    _lowStockBooks.Add(item);
                }

                _logger.LogInformation("Checked low stock books and added to the list.");
            }
        }

        private async Task NotifySuppliers(CancellationToken stoppingToken)
        {
            while (!_lowStockBooks.IsEmpty && !stoppingToken.IsCancellationRequested)
            {
                if (_lowStockBooks.TryTake(out var lowStockBook))
                {
                    await _storeHub.SendSuppliersNotification($"The Book: {lowStockBook.Title} Is Lowed In Stock");
                    _logger.LogInformation($"Notified suppliers about low stock book: {lowStockBook.Title}");
                }
            }
        }
    }
}
