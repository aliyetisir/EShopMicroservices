namespace Catalog.API.Products.GetProductById;

public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;
public record GetProductByIdResult(Product Product);

public class GetProductByIdHandler(IDocumentSession session) 
    : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery qurey, CancellationToken cancellationToken)
    {        
        var product = await session.LoadAsync<Product>(qurey.Id, cancellationToken);
        
        if (product == null)
        {
            throw new ProductNotFoundException(qurey.Id);
        }

        return new GetProductByIdResult(product);
    }
}
