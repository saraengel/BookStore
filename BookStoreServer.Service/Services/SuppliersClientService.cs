using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;

namespace BookStoreServer.Service.Services
{
    public class SuppliersClientService
    {
        private readonly HubConnection _connection;
        private readonly ILogger<SuppliersClientService> _logger;

        public SuppliersClientService(ILogger<SuppliersClientService> logger)
        {
            _logger = logger;
            _connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5000/order-hub")
                .Build();

            _connection.On<string>("SendSuppliersNotification", message =>
            {
                _logger.LogInformation($"Client received message: {message}");
            });
        }

        public async Task StartAsync()
        {
            try
            {
                await _connection.StartAsync();
                _logger.LogInformation("SignalR Client Connected!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error starting SignalR connection.");
            }
        }
        public async Task SendMessageToServer(string message)
        {
            await _connection.InvokeAsync("SendMessageToServer", message);
            _logger.LogInformation($"Sending message to server: {message}");
        }
    }
}
