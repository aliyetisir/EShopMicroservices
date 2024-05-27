namespace Catalog.API.Products.GetProductById;

public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;
public record GetProductByIdResult(Product Product);

public class GetProductByIdHandler(IDocumentSession session, ILogger<GetProductByIdHandler> logger) 
    : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery qurey, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductByIdHandler.Handler called with {@Query}", qurey);
        var product = await session.LoadAsync<Product>(qurey.Id, cancellationToken);
        
        if (product == null)
        {
            throw new ProductNotFoundException();
        }

        return new GetProductByIdResult(product);
    }
}
