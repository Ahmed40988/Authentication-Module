using System.Reflection;
using Application.Commands.AuthModules;
using Application.Handlers.AuthModules;
using Application.Interfaces;
using Application.Interfaces.Auth;
using Domain.Entities.AuthModules;
using Infrastructure;
using Infrastructure.Services.AuthModules;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDatabaseConfig(builder.Configuration);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(LoginCommandHandler).Assembly)
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapGet("/", () => Results.Redirect("/swagger"));
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
