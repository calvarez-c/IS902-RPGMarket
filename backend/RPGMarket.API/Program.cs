using DotNetEnv;
using RPGMarket.Application;
using RPGMarket.Infrastructure;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
Env.Load();

var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__PostgresConnection") 
                       ?? throw new InvalidOperationException("Falta la cadena de conexión.");


builder.Services.AddInfrastructure(connectionString);
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddApplication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(); //URL: http://localhost:5000/scalar/v1
}

app.MapControllers();
app.Run();
