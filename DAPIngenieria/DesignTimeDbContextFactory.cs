//public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
//{
//    public ApplicationDbContext CreateDbContext(string[] args)
//    {
//        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

//        // Construye el builder de configuración
//        var configuration = new ConfigurationBuilder()
//            .SetBasePath(Directory.GetCurrentDirectory())
//            .AddJsonFile("appsettings.json")
//            .Build();

//        // Configura el DbContext con la cadena de conexión
//        optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

//        return new ApplicationDbContext(optionsBuilder.Options);
//    }
//}

