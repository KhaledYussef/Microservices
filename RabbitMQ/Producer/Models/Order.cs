namespace Producer.Models
{
    public class Order
    {
        public int Id { get; set; }
        public required string ProductName { get; set; }
        public int Quantity { get; set; }
        public string? CustomerName { get; set; }
        public string? Address { get; set; }
    }
}
