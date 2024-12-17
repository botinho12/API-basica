using Microsoft.EntityFrameworkCore;
using API_basica.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbContext")));

builder.Services.AddControllers();


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();  // Usando o Swagger UI

app.UseStaticFiles();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
