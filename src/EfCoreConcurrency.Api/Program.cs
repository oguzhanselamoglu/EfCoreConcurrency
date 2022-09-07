using EfCoreConcurrency.Api.DataAccess.Entities;
using EfCoreConcurrency.Api.DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConcurrencyDb"));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateTime.Now.AddDays(index),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.MapGet("/products", async (AppDbContext _db) =>
{
    var products = await _db.Products.ToListAsync();
    return Results.Ok(products);
}).WithName("GetProducts");

app.MapGet("/products/{id}", async (int id, AppDbContext _db) =>
{
    var product = await _db.Products.FindAsync(id);
    if (product == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(product);
}).WithName("GetProductById");

app.MapDelete("/products/{id}", async (int id, AppDbContext _db) =>
{
    var product = await _db.Products.FindAsync(id);
    if (product == null)
    {
        return Results.NotFound();
    }
    _db.Products.Remove(product);
    await _db.SaveChangesAsync();
    return Results.Ok();
}).WithName("DeleteProductById");

app.MapPost("/products/create", async (ProductDto product, AppDbContext _db) =>
{
    _db.Products.Add(new Product
    {
        Name = product.Name,
        Code = product.Code,
        Price = product.Price
    });
    await _db.SaveChangesAsync();
    return Results.Ok();
});

app.MapPost("/products/update", async (Product product, AppDbContext _db) =>
{
    _db.Products.Update(product);
    await _db.SaveChangesAsync();
    return Results.Ok();
});

app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}