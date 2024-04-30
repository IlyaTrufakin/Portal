namespace Portal.Data.Entities
{
    public class Company
    {
        public Guid CompanyId { get; set; }
        public String CompanyForm { get; set; } = null!;
        public String CompanyName { get; set; } = null!;
        public String CompanyWebsite { get; set; } = null!;
        public String CompanyEDRPOU { get; set; } = null!;
        public Guid CompanyCountry  { get; set; }
        public Guid CompanyRegion { get; set; }
        public String CompanyLocality { get; set; } = null!;
        public String CompanyAddress1 { get; set; } = null!;
        public String CompanyAddress2 { get; set; } = null!;
    }
}
