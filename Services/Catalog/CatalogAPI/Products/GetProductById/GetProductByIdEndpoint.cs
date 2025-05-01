
namespace CatalogAPI.Products.GetProductById;

public record GetProductByIdRequest(Guid Id);
public record GetProductByIdResponse(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price);

public class GetProductByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id:guid}", async (Guid id, ISender sender) =>
        {
            try 
            {
                var request = new GetProductByIdRequest(id);

                var query = request.Adapt<GetProductByIdQuery>();

                var result = await sender.Send(query);

                var response = result.Adapt<GetProductByIdResponse>();

                return Results.Ok(response);
            }
            catch(ProductNotFoundException ex) 
            {                
                return Results.NotFound(new { ex.Message });
            }
        })
        .WithName("GetProductById")
        .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get a product by id")
        .WithDescription("Get a product by id from the catalog");
    }
}
