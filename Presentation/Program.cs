using Application.Extensions;
using Infrastructure.Extensions;
using Infrastructure.Migrations;
using Presentation.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRepositoryDependencies();
builder.Services.AddServiceDependencies();

builder.Services.AddScoped<ExceptionHandlingMiddleware>();
    
var app = builder.Build();

app.Migrate<Program>();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
