namespace CatalogAPI.Products.DeleteProduct;

public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;

public record DeleteProductResult(bool IsDeleted);

internal class DeleteProductCommandHandler(IDocumentSession documentSession) : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        documentSession.Delete<Product>(command.Id);
        
        try
        {
            await documentSession.SaveChangesAsync(cancellationToken);

            return new DeleteProductResult(true);
        }
        catch
        {
            // Log the exception if needed
            return new DeleteProductResult(false);
        }
    }
}
