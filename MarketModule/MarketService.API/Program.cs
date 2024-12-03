using MarketService.Data.Context;
using MarketService.Data.Repositories;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq();
});
builder.Services.AddControllers();
builder.Services.AddDbContext<MarketContext>(opt =>
{
    opt.UseSqlServer("server=localhost;database=MarketDb;integrated security=true;trustservercertificate=true");
});
builder.Services.AddScoped<MarketRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
