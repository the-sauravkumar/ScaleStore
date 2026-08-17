using ScaleStore.Infrastructure.Data;
using ScaleStore.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using ScaleStore.Core.Interfaces;
using FluentValidation;
using ScaleStore.Core.Validators.ProductValidators;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using Serilog;

// Configure Serilog immediately before doing anything else
Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Information()
        .WriteTo.Console()
        .WriteTo.File("Logs/scalestore-log-.txt", rollingInterval: RollingInterval.Day)
        .CreateLogger();

try 
{
    Log.Information("Starting ScaleStore Web API...");

    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog();

    builder.Services.AddControllers(options =>
    {
        options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
    });

    // Disable the built-in .NET model validation filter to allow FluentValidation
    builder.Services.Configure<ApiBehaviorOptions>(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });

    builder.Services.AddSwaggerGen( c =>
    {
        var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

        c.IncludeXmlComments(xmlPath);
    });

    builder.Services.AddDbContext<ScaleStoreDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

    builder.Services.AddScoped<IProductService, ProductService>();
    builder.Services.AddScoped<IOrderService, OrderService>();
    builder.Services.AddScoped<ICustomerService, CustomerService>();

    // Register all validators found in Core assembly (even if they are in different namespaces) for dependency injection
    builder.Services.AddValidatorsFromAssemblyContaining<CreateProductDtoValidator>();

    // Disable built-in model validation and enable FluentValidation auto-validation
    builder.Services.AddFluentValidationAutoValidation(options =>
    {
        options.DisableBuiltInModelValidation = true;
    });

    // -- Global Exception Handling Middleware --
    builder.Services.AddExceptionHandler<ScaleStore.Api.Middleware.GlobalExceptionHandler>();
    builder.Services.AddProblemDetails();

    var app = builder.Build();

    // Add Serilog Request Logging (tracks HTTP request exection times!)
    app.UseSerilogRequestLogging();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseExceptionHandler();

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "ScaleStore API terminated unexpectedly");
}
finally
{
    // ensures all logs are written to file before app closes
    Log.CloseAndFlush();
}
