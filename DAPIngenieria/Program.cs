using DAPIngenieria.Data;
using Microsoft.EntityFrameworkCore; // Para EF Core

public class Program
{
    public static void Main(string[] args)
    {
        // Crear el builder de la aplicaci�n
        var builder = WebApplication.CreateBuilder(args);

        // Forzar el entorno como "Development" para depuraci�n
        builder.Environment.EnvironmentName = "Development";
        Console.WriteLine($"Environment: {builder.Environment.EnvironmentName}");

        // Configuraci�n de servicios
        builder.Services.AddControllersWithViews();

        // Configurar DbContext para usar SQL Server
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        // Configurar servicios de sesi�n
        builder.Services.AddDistributedMemoryCache();
        builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(30); // Tiempo de expiraci�n
            options.Cookie.HttpOnly = true; // Seguridad adicional
            options.Cookie.IsEssential = true; // Cookies esenciales
        });

        // Construir la aplicaci�n
        var app = builder.Build();

        // Configurar el pipeline de la aplicaci�n
        if (app.Environment.IsDevelopment())
        {
            // Mostrar p�gina de excepciones para desarrollo
            app.UseDeveloperExceptionPage();
        }
        else
        {
            // Configuraci�n de errores para producci�n
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();
        app.UseSession(); // Habilitar sesiones
        app.UseAuthorization();

        // Configuraci�n de la ruta predeterminada
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Account}/{action=Login}/{id?}");

        // Ejecutar la aplicaci�n
        app.Run();
    }
}
