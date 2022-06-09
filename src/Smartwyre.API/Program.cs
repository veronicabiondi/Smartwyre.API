using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Services;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddTransient<IPaymentService, PaymentService>();
builder.Services.AddTransient<IValidationService, ValidationService>();

builder.Services.AddDatabaseContext(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Smartwyre"));
});

builder.Services.AddSwaggerGen(config =>
{

    config.SwaggerDoc("v1.0", new OpenApiInfo
    {
        Title = "Payments API",
        Version = "v1",
    });

    config.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
});

builder.Services.AddHealthChecks();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(config => config.SwaggerEndpoint("/swagger/v1.0/swagger.json", "Payments API v1.0"));
}


app.UseAuthorization();
app.MapControllers();

app.Run();