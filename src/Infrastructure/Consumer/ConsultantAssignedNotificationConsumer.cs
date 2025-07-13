using Application.Common.Interfaces;
using Application.Common.Interfaces.UnitOfWork;
using Domain.Entities;
using Domain.Enums;
using MassTransit;
using SharedContracts.Notification;

public class ConsultantAssignedNotificationConsumer : IConsumer<ConsultantAssignedApplicationNotificationEvent>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly INotificationPublisher _publisher;

    public ConsultantAssignedNotificationConsumer(IUnitOfWork unitOfWork, INotificationPublisher publisher)
    {
        _unitOfWork = unitOfWork;
        _publisher = publisher;
    }

    public async Task Consume(ConsumeContext<ConsultantAssignedApplicationNotificationEvent> context)
    {
        var notification = new Notification
        {
            UserId = context.Message.ConsultantId,
            Title = "Hồ sơ xét tuyển mới được phân công",
            Message = $"Bạn vừa được phân công xử lý hồ sơ xét tuyển của người dùng {context.Message.ApplicantName}.",
            Type = NotificationType.Application,
            Status = NotificationStatus.Unread,
            RelatedEntityId = context.Message.ApplicationId,
            RelatedEntityType = "ApplicationBooking",
            SentAt = DateTime.UtcNow
        };

        var notificationRepo = _unitOfWork.GetRepository<Notification>();
        notificationRepo.Add(notification);
        await _unitOfWork.SaveAsync();

        // Send it in real-time (SignalR)
        await _publisher.PublishNotificationAsync(notification.UserId, notification.Title, notification.Message);
    }
}
