namespace BasketAPI.Basket.GetBasket;

public record GetBasketRequest(string UserName) : IQuery<GetBasketResponse>;
public record GetBasketResponse(ShoppingCart ShoppingCart);

public class GetBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/basket/{userName}", async (string userName, ISender sender) =>
        {
            var request = new GetBasketRequest(userName);

            var query = request.Adapt<GetBasketQuery>();

            var result = await sender.Send(query);

            var response = result.Adapt<GetBasketResponse>();

            return Results.Ok(response);
        })
        .WithName("GetBasket")
        .Produces<GetBasketResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .ProducesProblem(StatusCodes.Status500InternalServerError)
        .WithSummary("Get basket by user name")
        .WithDescription("Get basket by user name from the basket service")
        .WithTags("Basket");
    }
}
