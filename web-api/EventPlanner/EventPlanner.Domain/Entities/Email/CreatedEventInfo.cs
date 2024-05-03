namespace EventPlanner.Domain.Entities.Email
{
    public class CreatedEventInfo
    {
        public string EventName { get; set; }
        public string LocationName { get; set; }
        public string Creator { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
    }
}
