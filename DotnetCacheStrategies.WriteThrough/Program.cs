using DotnetCacheStrategies.WriteThrough;
using DotnetCacheStrategies.WriteThrough.Data;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    var cfg = ConfigurationOptions.Parse(configuration.GetConnectionString("Redis"), true);
    cfg.AbortOnConnectFail = false;
    return ConnectionMultiplexer.Connect(cfg);
});

builder.Services.AddScoped<ICache, RedisCacheService>();
builder.Services.AddScoped<IDataStore, DataStore>();
builder.Services.AddScoped<WriteThroughCacheService>();

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
