namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string name, List<string> Category, string Description, string ImageFile, decimal Price) 
         : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid id);

    internal class CreateProductCommandHandler 
        : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            // Create new Product entity from command object
            var product = new Product
            {
                Name = command.name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price
            };

            return new CreateProductResult(Guid.NewGuid());
        }
    }
}
