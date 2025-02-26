using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using BookStoreServer.Entities.AppSettings;
using BookStoreServer.Services.Interfaces;
using BookStoreServer.CommonServices;
using BookStoreServer.Entities.Events;

namespace BookStoreServer.Service.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;
        private readonly EventAggregator _eventAggregator;

        public EmailService(IOptions<EmailSettings> emailSettings, EventAggregator eventAggregator)
        {
            _emailSettings = emailSettings.Value;
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe<OrderCreatedEvent>(CraeteOrderSendEmail);
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            using (var client = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.Port))
            {
                client.Credentials = new NetworkCredential(_emailSettings.SenderEmail, _emailSettings.Password);
                client.EnableSsl = true; // יש לבדוק אם השרת שלך דורש SSL

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_emailSettings.SenderEmail, _emailSettings.SenderName),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(to);

                await client.SendMailAsync(mailMessage);
            }
        }
        public void CraeteOrderSendEmail(OrderCreatedEvent orderCreatedEvent)
        {
            string to = orderCreatedEvent.CustomerEmail;
            string subject = $"Thank You for Your Order – Order {orderCreatedEvent.OrderId}";
            string body = $"Dear {orderCreatedEvent.CustomerName},We have received your order {orderCreatedEvent.OrderId} and are processing it.";
            SendEmailAsync(to, subject, body);
        }
    }
}
