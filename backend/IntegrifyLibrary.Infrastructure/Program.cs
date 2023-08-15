using IntegrifyLibrary.Domain;
using IntegrifyLibrary.Business;
using IntegrifyLibrary.Infrastructure;

using Microsoft.EntityFrameworkCore;
using Npgsql;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<DatabaseContext>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services
    .AddScoped<IUserRepo, UserRepo>()
    .AddScoped<IBookRepo, BookRepo>();

builder.Services
    .AddScoped<IUserService, UserService>()
    .AddScoped<IBookService, BookService>();


builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);


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
