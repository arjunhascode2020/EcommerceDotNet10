using Ordering.API.Extensions;
using Ordering.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// add ordering services
builder.Services.AddOrderingServices(builder.Configuration);
var app = builder.Build();
app.MigrateDatabase<OrderContext>(async (context, services) =>
{
    var logger = services.GetRequiredService<ILogger<OrderContextSeed>>();
    var cancellationToken = new CancellationToken(); // or CancellationToken.None
    await OrderContextSeed.SeedAsync(context, logger, cancellationToken);
});
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
