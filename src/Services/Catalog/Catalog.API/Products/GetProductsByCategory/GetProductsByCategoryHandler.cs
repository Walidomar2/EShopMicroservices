
namespace Catalog.API.Products.GetProductsByCategory
{
    public record GetProductsByCategoryQuery(string Category) : IQuery<GetProductsByCategoryResult>;
    public record GetProductsByCategoryResult(IEnumerable<Product> Products);
    public class GetProductsByQueryCategoryHandler
        (IDocumentSession session, ILogger<GetProductsByQueryCategoryHandler> logger)
        : IQueryHandler<GetProductsByCategoryQuery, GetProductsByCategoryResult>
    {
        public async Task<GetProductsByCategoryResult> Handle(GetProductsByCategoryQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductByCategoryQueryHandler.Handle called with {@Query}", query);

            var products = await session.Query<Product>()
                                        .Where(p => p.Category.Contains(query.Category))
                                        .ToListAsync(cancellationToken);

            return new GetProductsByCategoryResult(products);
        }
    }
}
