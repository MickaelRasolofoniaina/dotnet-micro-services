namespace CatalogAPI.Products.GetProductByCategory;

public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;
public record GetProductByCategoryResult(IEnumerable<Product> Products);

internal class GetProductByCategoryQueryHandler(IDocumentSession documentSession) : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
    {
        var products = await documentSession.Query<Product>()
            .Where(x => x.Category.Any(c => string.Equals(c, query.Category, StringComparison.OrdinalIgnoreCase)))
            .ToListAsync(cancellationToken);

        return new GetProductByCategoryResult(products);
    }
}

