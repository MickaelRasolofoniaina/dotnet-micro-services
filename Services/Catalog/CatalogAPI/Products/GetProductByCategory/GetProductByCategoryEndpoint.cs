
namespace CatalogAPI.Products.GetProductByCategory;

public record GetProductByCategoryRequest(string Category);
public record GetProductByCategoryResponse(IEnumerable<Product> Products);

public class GetProductByCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/category/{category}", async (string category, ISender sender) =>
        {
            var request = new GetProductByCategoryRequest(category);

            var query = request.Adapt<GetProductByCategoryQuery>();

            var result = await sender.Send(query);

            var response = result.Adapt<GetProductByCategoryResponse>();

            return Results.Ok(response);
        })
        .WithName("GetProductByCategory")
        .Produces<GetProductByCategoryResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status500InternalServerError)
        .WithSummary("Get products by category")
        .WithDescription("Get all products in the catalog by category");
    }
}

