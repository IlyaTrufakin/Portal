namespace Portal.Data.Entities
{
    public class UserCountry
    {
        public int CountryId { get; set; }
        public string Name { get; set; } = null!;
        public string PhoneCode { get; set; } = null!;
        public ICollection<User> Users { get; set; } = null!;
    }
}
