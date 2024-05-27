namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand
    (
        string Name,
        List<string> Category,
        string Description,
        string ImageFile,
        decimal Price
    ) : ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id);

internal class CreteProductHandler(IDocumentSession session) 
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        // Business locgic to create Product
        // Save to Database
        // Return CreateProductResult result

        var product = new Product
        {
            Name = command.Name,
            Category = command.Category,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Price = command.Price
        };

        session.Store( product );
        await session.SaveChangesAsync(cancellationToken);

        return new CreateProductResult(product.Id);
    }
}
