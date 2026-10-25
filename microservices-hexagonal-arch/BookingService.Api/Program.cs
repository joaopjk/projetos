using Adapters.SQL;
using Adapters.SQL.Repositories;
using Application.Ports;
using Application.Services;
using Domain.Ports;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<HotelDbContext>(opt =>
{
    opt.UseSqlServer(
        "Server=127.0.0.1,1433;Database=master;User Id=sa;Password=SuaSenhaForte123!;TrustServerCertificate=True;");
});
builder.Services.AddScoped<IGuestManager, GuestManager>();
builder.Services.AddScoped<IGuestRepository, GuestRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
        