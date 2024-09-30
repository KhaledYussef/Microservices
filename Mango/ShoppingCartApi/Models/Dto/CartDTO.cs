using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingCartApi.Models.Dto
{
    public class CartDTO
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public string? CouponCode { get; set; }
        public double Discount { get; set; }
        public double TotalPrice { get; set; }

        public List<CartDetailDTO> Items { get; set; } = [];
    }

    public class CartDetailDTO
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
        public ProductDto? Product { get; set; }
    }
}
