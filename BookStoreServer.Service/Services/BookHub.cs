using Microsoft.AspNetCore.SignalR;

namespace BookStoreServer.Service.Services
{
    public interface IBookHub
    {
        public Task SendBookNotification(string title);
    }
    public sealed class BookHub : Hub<IBookHub>
    {
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendBookNotification(string bookTitle)
        {
            await Clients.All.SendBookNotification(bookTitle);
        }
    }
    public class BookHubNotifier : IBookHub
    {
        private readonly IHubContext<BookHub, IBookHub> _hubContext;
        public BookHubNotifier(IHubContext<BookHub, IBookHub> hubContext)
        {
            _hubContext = hubContext;
        }
        public async Task SendBookNotification(string title)
        {
            await _hubContext.Clients.All.SendBookNotification(title);
        }
    }
}
