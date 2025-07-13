namespace SharedContracts.Booking
{
    public class UserCreatedFromBookingEvent
    {
        public Guid UserId { get; set; } = default!;
        public string UserEmail { get; set; } = default!;
    }
}
