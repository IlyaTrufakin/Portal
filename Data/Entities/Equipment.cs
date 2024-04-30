namespace Portal.Data.Entities
{
    public class Equipment
    {
        public Guid EquipmentId { get; set; }
        public Guid UserId { get; set; }
        public string EquipmentName { get; set; } = null!;
        public String ImageURL { get; set; } = null!;
        public String ImageDescription { get; set; } = null!;
    }
}
