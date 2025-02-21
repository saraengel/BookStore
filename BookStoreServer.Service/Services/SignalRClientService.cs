using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace BookStoreServer.Service.Services
{
    public class SignalRClientService
    {
        private readonly HubConnection _connection;
        public SignalRClientService()
        {
            _connection = new HubConnectionBuilder()
            .WithUrl("http://localhost:5044/book-hub")
            .Build();

            _connection.On<string>("SendBookNotification", message =>
            {
                Console.WriteLine($"the Client Get a Message: {message}");
            });
         
        }


        public async Task StartAsync()
        {
            try
            {
               await  _connection.StartAsync();
                Console.WriteLine("SignalR Client Connected!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public async Task SendBookNotification(string message)
        {
            await _connection.InvokeAsync("SendBookNotification", message);
        }
    }
}
