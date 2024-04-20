namespace Portal.Data.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public String UserName { get; set; } = null!;
        public String UserSurName { get; set; } = null!;
        public String UserPhoneNumber { get; set; } = null!;
        public String UserEmail { get; set; } = null!;
 /*       public int CountryId { get; set; }
        public UserCountry Country { get; set; } = null!;*/
        public String UserCountry { get; set; } = null!;
  /*      public int RegionId { get; set; }
        public UserRegion Region { get; set; } = null!;*/
        public String UserRegion { get; set; } = null!;
        public String UserLocality { get; set; } = null!;
        public String UserAddress1 { get; set; } = null!;
        public String UserAddress2 { get; set; } = null!;
        public String UserInteractionForm { get; set; } = null!;    
        public String UserCompanyName { get; set; } = null!;

        public String Salt { get; set; } = null!;
        public String DerivedKey { get; set; } = null!;
        public DateTime? UserRegistered { get; set; }
    }
}
