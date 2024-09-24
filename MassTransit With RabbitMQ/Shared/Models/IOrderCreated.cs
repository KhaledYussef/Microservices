namespace Shared.Models
{
    public record OrderCreated
    {
        public int Id { get; set; }
        public string? ProductName { get; set; }
        public int Quantity { get; set; }
        public string? CustomerName { get; set; }
        public string? Address { get; set; }
    }
}
