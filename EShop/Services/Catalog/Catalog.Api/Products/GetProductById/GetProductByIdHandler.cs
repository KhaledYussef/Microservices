

namespace Catalog.Api.Products.GetProductById
{
    public record GetProductByIdResult(Product? Product);

    public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;


    internal class GetProductByIdHandler(IDocumentSession session)
        : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            //var product = await session.Query<Product>().FirstOrDefaultAsync(a => a.Id == query.Id);
            var product = await session.LoadAsync<Product>(query.Id);
            return new GetProductByIdResult(product);
            
        }
    }
}
