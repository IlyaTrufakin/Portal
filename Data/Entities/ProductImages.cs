namespace Portal.Data.Entities
{
    public class ProductImages
    {
        public Guid ProductImagesId { get; set; }
        public Guid ProductId { get; set; } 
        public String ImageURL { get; set; } = null!;
        public String ImageDescription { get; set; } = null!;
    }
}
