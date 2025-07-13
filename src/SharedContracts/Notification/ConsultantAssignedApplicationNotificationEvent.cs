namespace SharedContracts.Notification
{
    public class ConsultantAssignedApplicationNotificationEvent
    {
        public Guid ConsultantId { get; set; }
        public Guid ApplicationId { get; set; }
        public string ApplicantName { get; set; } = default!;
    }
}
