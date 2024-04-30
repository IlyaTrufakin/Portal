namespace Portal.Data.Entities
{
    public class ServicesImage
    {
        public Guid ProductImagesId { get; set; }
        public Guid ProductionServiceId { get; set; }
        public String ImageURL { get; set; } = null!;
        public String ImageDescription { get; set; } = null!;
    }
}
