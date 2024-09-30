using ShoppingCartApi.Models.Dto;

using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingCartApi.Models
{
    public class CartDetail
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }

        [ForeignKey(nameof(CartId))]
        public virtual Cart? Cart { get; set; }

        [NotMapped]
        public ProductDto? Product { get; set; }


    }
}
