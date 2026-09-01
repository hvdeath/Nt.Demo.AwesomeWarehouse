using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.EntityFrameworkCore;
using Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Features.Product.Shared.Services;
using Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Persistance;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddFastEndpoints();
builder.Services.AddEndpointsApiExplorer();
builder.Services.SwaggerDocument(o =>
{
    o.ShortSchemaNames = true;
    o.DocumentSettings = s =>
    {
        s.DocumentName = "v1"; //must match what's being passed in to the map method below
    };
});

// Swagger (Swashbuckle) UI
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<WarehouseDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("sqldb")
        ?? throw new InvalidOperationException("Connection string 'database' not found.")));


builder.Services.AddHttpClient<ICurrencyExchangeService, CurrencyExchangeService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ExchangeServiceBaseUrl"]);
})
// todo: make it resilient with Polly policies, e.g. retry and circuit breaker..
//.AddPolicyHandler(GetRetryPolicy())
//.AddPolicyHandler(GetCircuitBreakerPolicy())
;

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseFastEndpoints(c =>
{
    c.Endpoints.ShortNames = true;
}).UseOpenApi();
app.UseSwaggerGen();
app.UseSwaggerUi();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<WarehouseDbContext>();
    context.Database.EnsureCreated();
    DbInitializer.Initialize(context);
}

app.MapDefaultEndpoints();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
