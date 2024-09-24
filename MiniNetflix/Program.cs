using Application.IRepository;
using Application.IServices;
using Application.Repository;
using Application.Services;
using Data.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Agrega DbContext al contenedor de servicios
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ISeriesService, SeriesService>();
// Registro del repositorio y del servicio
builder.Services.AddScoped<ISeriesRepository, SeriesRepository>();
builder.Services.AddScoped<IProducerRepository, ProducerRepository>();
builder.Services.AddScoped<IProducerService, ProducerService>();
builder.Services.AddScoped<IGenresRepository, GenresRepository>();
builder.Services.AddScoped<IGenresService, GenreService>();


// Agrega los servicios al contenedor
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configura el pipeline de solicitud HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();