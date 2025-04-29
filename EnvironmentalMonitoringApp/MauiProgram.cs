using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using EnvironmentalMonitoringApp.ViewModels;
using EnvironmentalMonitoringApp.Data;
using EnvironmentalMonitoringApp.Views;

namespace EnvironmentalMonitoringApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

        var a = Assembly.GetExecutingAssembly();
        using var stream = a.GetManifestResourceStream("EnvironmentalMonitoringApp.appsettings.json");

        var config = new ConfigurationBuilder()
			.AddJsonStream(stream)
			.Build();

        builder.Configuration.AddConfiguration(config);

        var connectionString = builder.Configuration.GetConnectionString("DevelopmentConnection");
        builder.Services.AddDbContext<EnvMonitorDbContext>(options => options.UseSqlServer(connectionString));

        builder.Services.AddTransient<LoginViewModel>();
        builder.Services.AddTransient<AdminDashboardViewModel>();
        builder.Services.AddTransient<ScientistDashboardViewModel>();
        builder.Services.AddTransient<OperationsDashboardViewModel>();
        builder.Services.AddTransient<UserViewModel>();
        builder.Services.AddTransient<SensorConfigViewModel>();

        builder.Services.AddTransient<LoginPage>();
		builder.Services.AddTransient<AdminDashboard>();
		builder.Services.AddTransient<ScientistDashboard>();
		builder.Services.AddTransient<OperationsDashboard>();
		builder.Services.AddTransient<AdminEditUserPage>();
		builder.Services.AddTransient<SensorConfigEditPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
