using Microsoft.EntityFrameworkCore;
using MidAssignment.Data;
using MidAssignment.Entities;
using MidAssignment.Interfaces;
using MidAssignment.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<LibraryContext>(options => options.UseSqlServer("name=ConnectionStrings:LibraryConnection"));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddTransient<ILibraryServices<Book>, BookServices>();
builder.Services.AddTransient<ILibraryServices<Category>, CategoryServices>();
builder.Services.AddTransient<ILibraryServices<BookRequest>, BookRequestServices>();
builder.Services.AddTransient<IDetailServices, BookRequestDetailServices>();
builder.Services.AddTransient<IUserServices, UserServices>();

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
