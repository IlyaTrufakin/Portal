namespace Portal.Data.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public Guid ContactPersonId { get; set; }
        public String AccountEmail { get; set; } = null!;
        public String Salt { get; set; } = null!;
        public String DerivedKey { get; set; } = null!;
        public DateTime? UserRegistered { get; set; }
    }
}
