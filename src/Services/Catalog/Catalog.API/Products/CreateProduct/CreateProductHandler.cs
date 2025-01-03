namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string name, List<string> Category, string Description, string ImageFile, decimal Price) 
         : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);

    internal class CreateProductCommandHandler(IDocumentSession session, IValidator<CreateProductCommand> validator)
        : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            // Validation for the incoming model
            var result = await validator.ValidateAsync(command, cancellationToken);
            var errors = result.Errors.Select(e => e.ErrorMessage).ToList();

            if (errors.Any())
            {
                throw new ValidationException(errors.FirstOrDefault());
            }

            // Create new Product entity from command object
            var product = new Product
            {
                Name = command.name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price
            };

            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);

            return new CreateProductResult(product.Id);
        }
    }
}
