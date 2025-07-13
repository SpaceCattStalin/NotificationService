namespace SharedContracts.User
{
    public class UserAlreadyExistsEvent
    {
        public Guid UserId { get; set; } = default!;
        public string UserEmail { get; set; } = default!;
    }
}
