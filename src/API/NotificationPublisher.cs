using API.Hubs;
using Application.Common.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace API
{
    public class NotificationPublisher : INotificationPublisher
    {
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly ILogger<NotificationPublisher> _logger;

        public NotificationPublisher(
              IHubContext<NotificationHub> hubContext,
              ILogger<NotificationPublisher> logger)
        {
            _hubContext = hubContext;
            _logger = logger;
        }
        public async Task PublishNotificationAsync(Guid userId, string title, string message)
        {
            //await _hubContext.Clients
            //      //.User(userId.ToString())
            //      .User(userId.ToString("N"))
            //      .SendAsync("ReceiveNotification", new
            //      {
            //          Title = title,
            //          Message = message,
            //          Time = DateTime.UtcNow.ToString("o")
            //      });
            //await _hubContext.Clients.All.SendAsync("ReceiveNotification", new
            //{
            //    title,
            //    message
            //});

            var formattedUserId = userId.ToString();
            _logger.LogInformation("📢 Publishing notification to user {UserId} | Title: {Title} | Message: {Message}", formattedUserId, title, message);

            await _hubContext.Clients
                .User(formattedUserId)
                .SendAsync("ReceiveNotification", new
                {
                    Title = title,
                    Message = message,
                    Time = DateTime.UtcNow.ToString("o")
                });
        }
    }
}
