namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductRequest(string Name, List<string> Category, string Description, string ImageFile, decimal Price);
    public record CreateProductResponse(Guid Id);
    public class CreateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/products",
                async (CreateProductRequest request, ISender sender) =>
                {
                    // Map from CreateProductRequest to CreateProductCommand
                    var command = request.Adapt<CreateProductCommand>();

                    // Send the command throught mediator 
                    var result = sender.Send(command);

                    // Map the result from CreateProductResult to  CreateProductResponse
                    var response = result.Adapt<CreateProductResponse>();
                    
                    return Results.Created($"/products/{response.Id}", response);
                })
                .WithName("CreateProduct")
                .Produces<CreateProductResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Create Product")
                .WithDescription("Create Product");
        }
    }
}
