using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingCartApi.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public string? CouponCode { get; set; }

        [NotMapped]
        public double Discount { get; set; }

        [NotMapped]
        public double TotalPrice { get; set; }

        public virtual ICollection<CartDetail> Items { get; set; } = [];
    }
}
