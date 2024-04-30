namespace Portal.Data.Entities
{
    public class ProductionService
    {
        public Guid ProductionServiceImagesId { get; set; }
        public Guid ProductionServiceId { get; set; }
        public String ImageURL { get; set; } = null!;
        public String ImageDescription { get; set; } = null!;
    }
}
