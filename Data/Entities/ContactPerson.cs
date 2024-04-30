namespace Portal.Data.Entities
{
    public class ContactPerson
    {
        public Guid Id { get; set; }
        public String Name { get; set; } = null!;
        public String SurName { get; set; } = null!;
        public String Phone { get; set; } = null!;
        public String MainEmail { get; set; } = null!;
        public String SecondEmail { get; set; } = null!;
        public String Description { get; set; } = null!;
        public String Role { get; set;} = null!;
        public bool IsChecked { get; set; } = false;
        public bool IsVisible { get; set; } = false;


    }
}
