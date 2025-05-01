

using Marten.Pagination;

namespace CatalogAPI.Products.GetProducts;

public record GetProductsQuery(int PageNumber = 1, int PageSize = 10) : IQuery<GetProductsResult>;
public record GetProductsResult(IEnumerable<Product> Products, Pagination Pagination);

internal class GetProductsQueryHandler(IDocumentSession documentSession) : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        var productsPagedList = await documentSession.Query<Product>().ToPagedListAsync(query.PageNumber, query.PageSize, cancellationToken);

        var pagination = productsPagedList.Adapt<Pagination>();

        var result = new GetProductsResult(productsPagedList ?? Enumerable.Empty<Product>(), pagination);

        return result;
    }
}