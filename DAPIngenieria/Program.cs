using DAPIngenieria.Data;
using Microsoft.EntityFrameworkCore; // Para EF Core

public class Program
{
    public static void Main(string[] args)
    {
        // Crear el builder de la aplicación
        var builder = WebApplication.CreateBuilder(args);

        // Forzar el entorno como "Development" para depuración
        builder.Environment.EnvironmentName = "Development";
        Console.WriteLine($"Environment: {builder.Environment.EnvironmentName}");

        // Configuración de servicios
        builder.Services.AddControllersWithViews();

        // Configurar DbContext para usar SQL Server
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        // Configurar servicios de sesión
        builder.Services.AddDistributedMemoryCache();
        builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(30); // Tiempo de expiración
            options.Cookie.HttpOnly = true; // Seguridad adicional
            options.Cookie.IsEssential = true; // Cookies esenciales
        });

        // Construir la aplicación
        var app = builder.Build();

        // Configurar el pipeline de la aplicación
        if (app.Environment.IsDevelopment())
        {
            // Mostrar página de excepciones para desarrollo
            app.UseDeveloperExceptionPage();
        }
        else
        {
            // Configuración de errores para producción
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();
        app.UseSession(); // Habilitar sesiones
        app.UseAuthorization();

        // Configuración de la ruta predeterminada
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Account}/{action=Login}/{id?}");

        // Ejecutar la aplicación
        app.Run();
    }
}
