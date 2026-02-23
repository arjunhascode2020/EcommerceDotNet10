using Catalog.Application.Extensions;
using Catalog.Core.Settings;
using Catalog.Infrastructure.Data.SeedData;
using Catalog.Infrastructure.Extensions;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);
//Register Custom Serlizers
BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationServicec();
builder.Services.AddInfraServices();
//Register MongoDbClient As Single
builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
    return new MongoClient(settings.ConnectionString);

});

var app = builder.Build();
//Seed MongoDb Data
using (var scop = app.Services.CreateScope())
{
    var options = scop.ServiceProvider.GetRequiredService<IOptions<DatabaseSettings>>();
    await SeedData.SeedDatabaseAsync(options);
}
;
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
