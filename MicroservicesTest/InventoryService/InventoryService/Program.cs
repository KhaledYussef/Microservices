using InventoryService;

using Microsoft.Extensions.DependencyInjection;

using Plain.RabbitMQ;

using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



// rabbit mq subsicriber
builder.Services.AddSingleton<IConnectionProvider>(new ConnectionProvider("amqp://guest:guest@localhost:5672"));
builder.Services.AddSingleton<ISubscriber>(x =>
    new Subscriber(x.GetService<IConnectionProvider>(),
        "inventory_exchange",
        "inventory_queue",
        "inventory.*",
        ExchangeType.Topic
));

// inventory storage
builder.Services.AddSingleton<IInventoryStorage, InventoryStorage>();
builder.Services.AddHostedService<InventoryTracker>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
