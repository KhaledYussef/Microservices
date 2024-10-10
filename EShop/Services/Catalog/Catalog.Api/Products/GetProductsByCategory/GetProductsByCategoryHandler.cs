namespace Catalog.Api.Products.GetProductsByCategory
{
    public record GetProductsByCategoryResult(IEnumerable<Product> Products);
    public record GetProductsByCategoryQuery(string category) : IQuery<GetProductsByCategoryResult>;
    public class GetProductsByCategoryHandler(IDocumentSession session)
        : IQueryHandler<GetProductsByCategoryQuery, GetProductsByCategoryResult>
    {
        public async Task<GetProductsByCategoryResult> Handle(GetProductsByCategoryQuery request, CancellationToken cancellationToken)
        {
            var products = await session.Query<Product>()
                .Where(a=> a.Category.Contains(request.category))
                .ToListAsync();

            return new GetProductsByCategoryResult(products);
            
        }
    }
}
