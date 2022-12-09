using KakarazaZoohop.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<ConnectionOptions>(builder.Configuration.GetSection(nameof(ConnectionOptions)));
builder.Services.AddSingleton<IConnectionOptions>(sp => sp.GetRequiredService<IOptions<ConnectionOptions>>().Value);

builder.Services.AddScoped<IZooShopContext>(serviceProvider => new ZooShopContext(serviceProvider.GetRequiredService<IConnectionOptions>()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

await app.Services.CreateAsyncScope().ServiceProvider.GetRequiredService<IZooShopContext>().Database.MigrateAsync();

app.UseCors(options => options.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
