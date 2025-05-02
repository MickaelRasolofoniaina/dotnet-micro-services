namespace BasketAPI.Basket.DeleteBasket;

public record DeleteBasketRequest(string UserName) : ICommand<DeleteBasketResponse>;
public record DeleteBasketResponse(bool Success);

public class DeleteBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/basket/{userName}", async (string userName, ISender sender) =>
        {
            var request = new DeleteBasketRequest(userName);

            var command = request.Adapt<DeleteBasketCommand>();

            var result = await sender.Send(command);

            var response = result.Adapt<DeleteBasketResponse>();

            return Results.Ok(response);
        })
        .WithName("DeleteBasket")
        .Produces<DeleteBasketResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status500InternalServerError)
        .WithSummary("Delete basket by user name")
        .WithDescription("Delete a basket by user name")
        .WithTags("Basket");
    }
}