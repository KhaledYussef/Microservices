using BuildingBlocks.Exceptions;

namespace Catalog.Api.Exceptions
{
    public class ProductNotFound : NotFoundException
    {
        public ProductNotFound(Guid id) : base("Product", id)
        {
        }
    }
}
