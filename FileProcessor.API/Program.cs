using FileProcessor.API.Middlewares;
using FileProcessor.Application.Extensions;
using FileProcessor.Infrastructure.Extensions;
using Serilog;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<ErrorHandlingMiddle>();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();


// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.UseSerilog((context, configuration) => 
    configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddle>();
app.UseSerilogRequestLogging();

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
