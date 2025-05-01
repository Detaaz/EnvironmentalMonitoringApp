using EnvironmentalMonitoringApp.ViewModels;

namespace EnvironmentalMonitoringApp.Views;

public partial class ScientistDashboard : ContentPage
{
	public ScientistDashboard(ScientistDashboardViewModel viewModel)
	{
        this.BindingContext = viewModel;
        InitializeComponent();

        Routing.RegisterRoute(nameof(SensorConfigEditPage), typeof(SensorConfigEditPage));
    }
}