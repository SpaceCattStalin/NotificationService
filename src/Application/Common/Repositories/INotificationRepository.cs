using Application.DTOs;
using Domain.Entities;
using Domain.Enums;

namespace Application.Common.Interfaces.Repositories
{
    public interface INotificationRepository : IGenericRepository<Notification>
    {
        //Task<List<Booking>> SearchAsync(string? id, string fullname, string email, string phone, string campus, string acafield, string specialization, string location, BookingStatus? status, ContactStatus? contactStatus);
        Task<List<Notification>> SearchAsync(NotificationFilter filter);

    }
}
