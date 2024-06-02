using BuildingBlocks.Behaviors;
using BuildingBlocks.Exceptions.Handler;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container
builder.Services.AddCarter();
var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(assembly);
    config.AddOpenBehavior(typeof(ValidationBehaviors<,>));
    config.AddOpenBehavior(typeof(LoggingBehaviors<,>));
});

builder.Services.AddValidatorsFromAssemblies([assembly]);

builder.Services.AddMarten(opts =>
{
    opts.Connection(connectionString: builder.Configuration.GetConnectionString("Database"));
    opts.Schema.For<ShoppingCart>().Identity(x=>x.UserName);
})
.UseLightweightSessions();

builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();
// configure the HTTP request pipeline
app.MapCarter();
app.UseExceptionHandler(options => { });
app.Run();
