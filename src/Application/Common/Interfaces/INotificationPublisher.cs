namespace Application.Common.Interfaces
{
    public interface INotificationPublisher
    {
        Task PublishNotificationAsync(Guid userId, string title, string message);
    }
}
