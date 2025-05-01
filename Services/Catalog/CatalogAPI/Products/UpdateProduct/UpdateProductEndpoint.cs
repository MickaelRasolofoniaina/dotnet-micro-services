namespace CatalogAPI.Products.UpdateProduct;

public record UpdateProductRequest(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price) : ICommand<UpdateProductResponse>;
public record UpdateProductResponse();

public class UpdateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/products", async (UpdateProductRequest request, ISender sender) =>
        {
            try 
            {
                var command = request.Adapt<UpdateProductCommand>();
                
                var result = await sender.Send(command);

                var response = result.Adapt<UpdateProductResponse>();
                
                return Results.NoContent();
            }
            catch(ProductNotFoundException ex)
            {
                return Results.NotFound(new { ex.Message });
            }

        })
        .WithName("UpdateProduct")
        .Produces<UpdateProductResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Update a product by id")
        .WithDescription("Update a product by id from the catalog");;
    }
}
