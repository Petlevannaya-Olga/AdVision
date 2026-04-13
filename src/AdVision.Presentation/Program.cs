using System.Globalization;
using AdVision.Application;
using AdVision.Infrastructure;
using AdVision.Presentation.Notifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace AdVision.Presentation;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Console(formatProvider: CultureInfo.InvariantCulture)
            .CreateBootstrapLogger();

        try
        {
            Log.Information("Приложение запускается");

            // Загружаем конфигурацию
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Создаём контейнер DI
            var services = new ServiceCollection();

            // Регистрируем зависимости
            ConfigureServices(services, configuration);

            // Собираем ServiceProvider
            var serviceProvider = services.BuildServiceProvider();

            using (var scope = serviceProvider.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                db.Database.Migrate();
            }

            ApplicationConfiguration.Initialize();

            // Получаем Form через DI
            var form = serviceProvider.GetRequiredService<MainForm>();

            System.Windows.Forms.Application.Run(form);
        }
        catch (Exception e)
        {
            Log.Fatal(e, "Не удалось запустить приложение ");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        // Регистрируем формы
        services.AddTransient<MainForm>();
        services.AddTransient<VenueForm>();
        services.AddTransient<VenueTypeForm>();

        // Регистрируем уведомления
        services.AddSingleton<INotificationService, NotificationService>();
        
        // Регистрируем зависимости из инфраструктуры
        services.AddInfrastructure(configuration);

        // Регистрируем зависимости из Application
        services.AddApplication();

        // Регистрируем логирование
        services.AddSerilogLogging(configuration);
        services.AddLogging();
    }
}