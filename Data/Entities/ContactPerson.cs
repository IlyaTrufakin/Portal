namespace Portal.Data.Entities
{
    public class ContactPerson
    {
        public Guid Id { get; set; }
        public String Name { get; set; } = null!;
        public String SurName { get; set; } = null!;
        public String Phone { get; set; } = null!;
        public String WorkEmail { get; set; } = null!;
        public String Description { get; set; } = null!;
        public bool IsChecked { get; set; } = false;
        public bool IsVisible { get; set; } = false;
        public String? AvatarUrl { get; set; } = null!;

    }
}
