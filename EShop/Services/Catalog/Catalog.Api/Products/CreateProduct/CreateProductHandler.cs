namespace Catalog.Api.Products.CreateProduct
{
    /// <summary>
    /// Command to create a new product.
    /// </summary>
    /// <param name="Name">The name of the product.</param>
    /// <param name="Category">The categories of the product.</param>
    /// <param name="Description">The description of the product.</param>
    /// <param name="ImageFile">The image file of the product.</param>
    /// <param name="Price">The price of the product.</param>
    public record CreateProductCommand(string Name, List<string> Category, string? Description, string? ImageFile, decimal Price)
        : ICommand<CreateProductResult>;

    /// <summary>
    /// Result of the create product command.
    /// </summary>
    /// <param name="Id">The unique identifier of the created product.</param>
    public record CreateProductResult(Guid Id);

    /// <summary>
    /// Validator for the <see cref="CreateProductCommand"/>.
    /// </summary>
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateProductCommandValidator"/> class.
        /// </summary>
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Price is required.");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0.");
        }
    }


    /// <summary>
    /// Handler for the <see cref="CreateProductCommand"/>.
    /// </summary>
    internal class CreateProductCommandHandler(IDocumentSession session)
        : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        /// <summary>
        /// Handles the create product command.
        /// </summary>
        /// <param name="command">The create product command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The result of the create product command.</returns>
        /// <exception cref="ValidationException">Thrown when the command validation fails.</exception>
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price,
            };

            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);

            return new CreateProductResult(product.Id);
        }
    }





}
