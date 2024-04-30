namespace Portal.Data.Entities
{
    public class CompanyRegion
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string PhoneCode { get; set; } = null!;
        public ICollection<Company> Companies { get; set; } = null!;
    }
}
