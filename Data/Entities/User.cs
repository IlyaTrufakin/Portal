namespace Portal.Data.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public String Name { get; set; } = null!;
        public String SurName { get; set; } = null!;
        public String PhoneNumber { get; set; } = null!;
        public String Email { get; set; } = null!;
 /*       public int CountryId { get; set; }
        public UserCountry Country { get; set; } = null!;*/
        public String Country { get; set; } = null!;
  /*      public int RegionId { get; set; }
        public UserRegion Region { get; set; } = null!;*/
        public String Region { get; set; } = null!;
        public String UserLocality { get; set; } = null!;
        public String UserAddress1 { get; set; } = null!;
        public String UserAddress2 { get; set; } = null!;
        public String UserInteractionForm { get; set; } = null!;    
        public String UserCompanyName { get; set; } = null!;

        public String Salt { get; set; } = null!;
        public String DerivedKey { get; set; } = null!;
        public String AvatarUrl { get; set; } = null!;
        public DateTime? Registered { get; set; }
    }
}
