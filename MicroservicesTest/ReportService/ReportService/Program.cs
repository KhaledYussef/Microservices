using Plain.RabbitMQ;

using RabbitMQ.Client;

using ReportService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// RabbitMQ Connection
builder.Services.AddSingleton<IConnectionProvider>(new ConnectionProvider("amqp://guest:guest@localhost:5672"));
builder.Services.AddSingleton<ISubscriber>(x =>
{
    return new Subscriber(x.GetRequiredService<IConnectionProvider>(),
        "report.exchange",
        "report_queue",
        "report.*",
        ExchangeType.Topic);
});

builder.Services.AddSingleton<IReportStorage, ReportStorage>();
builder.Services.AddHostedService<ReportCollector>();

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
