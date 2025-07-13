using Application.DTOs;
using Domain.Enums;
using MediatR;

namespace Application.UseCases.Commands
{
    public class CreateNotificationCommand : IRequest<ResponseNotification>
    {
        public Guid UserId { get; set; }
        public string Title { get; set; } = default!;
        public string Message { get; set; } = default!;
        public NotificationType Type { get; set; }
        public Guid? RelatedEntityId { get; set; }
        public string? RelatedEntityType { get; set; }
    }
}
