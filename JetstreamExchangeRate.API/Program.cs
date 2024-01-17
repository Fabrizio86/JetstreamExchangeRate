using JetstreamExchangeRate.Core;
using JetstreamExchangeRate.Core.Interfaces;
using JetstreamExchangeRate.Interfaces;
using JetstreamExchangeRate.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json")
    .Build();

builder.Services.AddSingleton<IConfiguration>(configuration);
builder.Services.AddHttpClient<FixerExchangeRateService>();
builder.Services.AddTransient<IExchangeRateService, FixerExchangeRateService>();
builder.Services.AddTransient<IInvoiceCalculator, InvoiceCalculator>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
