using Microsoft.EntityFrameworkCore;
using WebApplication2;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//builder.Services.AddEntityFrameworkNpgsql().AddDbContext<ApiContext>(options =>
//            options.UseNpgsql(@"Server=localhost;Port=5432;Database=PostgresDemo;User Id=postgres;Password=tusar@1234"));
//builder.Services.AddEntityFrameworkNpgsql().AddDbContext<ApiContext>(options =>
//            options.UseNpgsql(@"Server=localhost;Port=5432;Database=Auditdb;User Id=postgres;Password=tusar@1234"));
//builder.Services.AddDbContext<ApiContext>(options =>
//            options.UseNpgsql());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
