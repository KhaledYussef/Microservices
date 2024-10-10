namespace Catalog.Api.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<string> Category { get; set; } = new List<string>();
        public string? Description { get; set; }
        public string? ImageFile { get; set; }
        public decimal Price { get; set; }

    }
}
