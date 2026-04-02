using System.Globalization;
using AdVision.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace AdVision.Presentation;

static class Program
{
    [STAThread]
    static void Main()
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

            ApplicationConfiguration.Initialize();

            // Получаем Form через DI
            var form = serviceProvider.GetRequiredService<Form1>();

            Application.Run(form);
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
        services.AddTransient<Form1>();

        // Регистрируем зависимости из инфраструктуры
        services.AddInfrastructure(configuration);

        // Регистрируем логирование
        services.AddSerilogLogging(configuration);
    }
}