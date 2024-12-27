using DotnetCacheStrategies.CacheAside.Data;
using DotnetCacheStrategies.CacheAside.Entities;
using DotnetCacheStrategies.CacheAside.Models;
using DotnetCacheStrategies.CacheAside.Repositories.Abstract;
using DotnetCacheStrategies.CacheAside.Repositories.Concrete.EntityFramework;
using DotnetCacheStrategies.CacheAside.Services.Abstract;
using DotnetCacheStrategies.CacheAside.Services.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ICacheProvider>(sp =>
{
    return new RedisCacheProvider(new RedisConnectionConfiguration(configuration.GetConnectionString("Redis"), 60));
});

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("Application"));
});

builder.Services.AddScoped<IReaderRepository<Person>>(sp =>
{
    return new EFCacheReaderRepository<Person>(
        sp.GetRequiredService<AppDbContext>(),
        sp.GetRequiredService<ICacheProvider>(),
        TimeSpan.FromMinutes(60)
    );
});

builder.Services.AddScoped<IWriterRepository<Person>, EFCacheWriterRepository<Person>>();
builder.Services.AddScoped<IPeopleReaderService, PeopleReaderService>();
builder.Services.AddScoped<IPeopleWriterService, PeopleWriterService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => 
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
