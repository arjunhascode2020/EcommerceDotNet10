using Basket.Application.GrpcServices;
using Basket.Application.Handlers;
using Basket.Application.Settings;
using Basket.Core.Repositories;
using Basket.Core.Settings;
using Basket.Infrastructure.Repositories;
using Discount.Grpc.Protos;
using Microsoft.Extensions.Options;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.Configure<CacheSettings>(builder.Configuration.GetSection("CacheSettings"));
builder.Services.Configure<GrpcSettings>(builder.Configuration.GetSection("GrpcSettings"));
//Register gRPC client for Discount service
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>((sp, cfg) =>
{
    var grpcSettings = sp.GetRequiredService<IOptions<GrpcSettings>>().Value;
    cfg.Address = new Uri(grpcSettings.DiscountUrl);
});
// Register GrpcService
builder.Services.AddScoped<DiscountGrpcService>();

builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddSwaggerGen();
var assemblies = new Assembly[]
{
    Assembly.GetExecutingAssembly(),
    typeof(CreateShoppingCartHandler).Assembly,
};

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));
builder.Services.AddStackExchangeRedisCache(options =>
{

    options.Configuration = builder.Configuration.GetSection("CacheSettings").GetValue<string>("ConnectionString");

});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
