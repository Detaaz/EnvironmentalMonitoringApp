using EnvironmentalMonitoringApp.ViewModels;

namespace EnvironmentalMonitoringApp.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage(LoginViewModel viewModel)
	{
        this.BindingContext = viewModel;
        InitializeComponent();

        // routing to dashboards after log in
        Routing.RegisterRoute(nameof(AdminDashboard), typeof(AdminDashboard));
        Routing.RegisterRoute(nameof(OperationsDashboard), typeof(OperationsDashboard));
        Routing.RegisterRoute(nameof(ScientistDashboard), typeof(ScientistDashboard));

    }
}