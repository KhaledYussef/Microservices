using BuildingBlocks.Behaviors;
using BuildingBlocks.Exceptions.Handler;

using Catalog.Api.Data;

using HealthChecks.UI.Client;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter();
builder.Services.AddMediatR(c =>
{
    c.RegisterServicesFromAssemblies(typeof(Program).Assembly);
    c.AddOpenBehavior(typeof(ValidationBehaviors<,>));
    c.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);


// Marten
builder.Services.AddMarten(a =>
{
    a.Connection(builder.Configuration.GetConnectionString("Database")!);
})
.UseLightweightSessions();
if (builder.Environment.IsDevelopment())
    builder.Services.InitializeMartenWith<CatalogInitialData>();




builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Database")!);



var app = builder.Build();

app.MapCarter();

app.UseExceptionHandler(z => { });

app.UseHealthChecks("/health",
    new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
    });


app.Run();
