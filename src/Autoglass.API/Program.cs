using Autoglass.API.Extensions;
using Autoglass.Application.Extensions;
using Autoglass.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCors()
    .AddProductContext(builder.Configuration)
    .AddServices()
    .AddMappers();

builder.WebHost.ConfigureKestrel(options => options.AllowSynchronousIO = true);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.AddPathExtensions();

app.UseRouting();

app.UseHttpsRedirection();
app.UseCors(
    builder => builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
);

app.Run();

public partial class Program { }