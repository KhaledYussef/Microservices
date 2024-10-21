using Basket.Api.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter();

builder.Services.AddMediatR(c =>
{
    c.RegisterServicesFromAssemblies(typeof(Program).Assembly);
    c.AddOpenBehavior(typeof(ValidationBehaviors<,>));
    c.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);


builder.Services.AddExceptionHandler<CustomExceptionHandler>();

// Marten
builder.Services.AddMarten(a =>
{
    a.Connection(builder.Configuration.GetConnectionString("Database")!);
    a.Schema.For<ShoppingCart>().Identity(x => x.UserName).UseOptimisticConcurrency(true);
})
.UseLightweightSessions();

builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Database")!);

builder.Services.AddScoped<IBasketRepository, BasketRepository>();


//===========================================================
var app = builder.Build();


app.MapCarter();

app.UseExceptionHandler(z => { });

app.UseHealthChecks("/health",
    new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
    });

app.Run();
