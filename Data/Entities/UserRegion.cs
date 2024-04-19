namespace Portal.Data.Entities
{
    public class UserRegion
    {
        public int UserRegionId { get; set; }
        public string Name { get; set; } = null!;
        public string PhoneCode { get; set; } = null!;
        public ICollection<User> Users { get; set; } = null!;
    }
}
