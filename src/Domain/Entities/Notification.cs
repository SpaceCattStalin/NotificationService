using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class Notification : BaseEntity<Guid>
    {
        public Guid UserId { get; set; }

        public string Title { get; set; } = default!;
        public string Message { get; set; } = default!;

        public NotificationType Type { get; set; }
        public NotificationStatus Status { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? SentAt { get; set; }
        public DateTime? ReadAt { get; set; }

        public Guid? RelatedEntityId { get; set; }
        public string? RelatedEntityType { get; set; }
    }
}
