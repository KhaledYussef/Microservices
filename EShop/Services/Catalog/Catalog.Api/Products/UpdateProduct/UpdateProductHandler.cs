

using Catalog.Api.Exceptions;

namespace Catalog.Api.Products.UpdateProduct
{
    public record UpdateProductResult(Product Product);
    public record UpdateProductCommand(Guid Id, string Name, List<string> Category, string? Description, string? ImageFile, decimal Price)
        : ICommand<UpdateProductResult>;



    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(c => c.Id).NotEmpty();
            RuleFor(c => c.Name).NotEmpty();
        }
    }


    public class UpdateProductHandler(IDocumentSession session)
        : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var product = await session.LoadAsync<Product>(command.Id, cancellationToken);

            if (product == null) 
                throw new ProductNotFound(command.Id);
            

            product.Name = command.Name;
            product.Description = command.Description;
            product.Category = command.Category;
            product.ImageFile = command.ImageFile;
            product.Price = command.Price;

            await session.SaveChangesAsync(cancellationToken);

            return new UpdateProductResult(product);
        }

        
    }

 
}


