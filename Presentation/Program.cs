using Application.Extensions;
using Infrastructure.Extensions;
using Infrastructure.Migrations.Extensions;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRepositoryDependencies();
builder.Services.AddServiceDependencies();
    
var app = builder.Build();

app.Migrate<Program>();

/*if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}*/

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
