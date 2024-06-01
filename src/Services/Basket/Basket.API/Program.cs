using BuildingBlocks.Behaviors;

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

//builder.Services.AddMarten(opts =>
//{
//    opts.Connection(builder.Configuration.GetConnectionString("Database"));
//})
//    .UseLightweightSessions();

var app = builder.Build();
// configure the HTTP request pipeline
app.MapCarter();
app.Run();
