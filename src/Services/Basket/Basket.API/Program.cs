using Basket.API.Repositories;
using Basket.API.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//var str = builder.Configuration["ConnectionString"];
//builder.Services.AddStackExchangeRedisCache(options => {
//    options.Configuration = "127.0.0.1:6379";
//});
IConfiguration configuration = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .Build();


builder.Services.AddStackExchangeRedisCache(options => {
    options.Configuration = configuration["CacheSettings:ConnectionString"];
});



builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddControllers();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
