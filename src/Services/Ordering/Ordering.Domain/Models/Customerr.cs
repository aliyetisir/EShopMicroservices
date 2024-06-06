namespace Ordering.Domain.Models;

public class Customerr : Entity<CustomerId>
{
    public string Name { get; private set; } = default!;
    public string Email { get; private set; } = default!;
}
