namespace CatalogAPI.Products.DeleteProduct;

public record DeleteProductRequest(Guid Id) : ICommand<DeleteProductResponse>;

public record DeleteProductResponse(bool IsDeleted);

public class DeleteProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/products/{id:guid}", async (Guid id, ISender sender) =>
        {
            var command = new DeleteProductRequest(id).Adapt<DeleteProductCommand>();

            var result = await sender.Send(command);

            var response = result.Adapt<DeleteProductResponse>();

            return Results.Ok(response);
        })
        .WithName("DeleteProduct")
        .Produces<DeleteProductResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status500InternalServerError)
        .WithSummary("Delete a product by id")
        .WithDescription("Delete a product by id from the catalog");
    }
}
