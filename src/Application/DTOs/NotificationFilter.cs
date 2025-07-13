using Domain.Enums;

namespace Application.DTOs
{
    public class NotificationFilter
    {
        public string? Id { get; set; }
        public NotificationStatus? Status { get; set; }
    }
}
