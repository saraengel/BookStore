using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace BookStoreServer.Service.Services
{
    public interface IStoreHub
    {
        public Task SendSuppliersNotification(string title);
    }
    public sealed class StoreHub : Hub<IStoreHub>
    {
        private readonly ILogger<StoreHub> _logger;

        public StoreHub(ILogger<StoreHub> logger)
        {
            _logger = logger;
        }

        public override async Task OnConnectedAsync()
        {
            _logger.LogInformation("Client connected.");
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            _logger.LogInformation("Client disconnected.");
            await base.OnDisconnectedAsync(exception);
        }
        public async Task SendSuppliersNotification(string bookTitle)
        {
            _logger.LogInformation($"Sending notification for book: {bookTitle}");
            await Clients.All.SendSuppliersNotification(bookTitle);
        }
    }
    public class StoreHubNotifier : IStoreHub
    {
        private readonly IHubContext<StoreHub, IStoreHub> _hubContext;
        private readonly ILogger<StoreHubNotifier> _logger;

        public StoreHubNotifier(IHubContext<StoreHub, IStoreHub> hubContext, ILogger<StoreHubNotifier> logger)
        {
            _hubContext = hubContext;
            _logger = logger;
        }

        public async Task SendSuppliersNotification(string title)
        {
            _logger.LogInformation($"Notifying suppliers about book: {title}");
            await _hubContext.Clients.All.SendSuppliersNotification(title);
        }
    }
}
