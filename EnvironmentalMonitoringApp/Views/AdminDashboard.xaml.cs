using EnvironmentalMonitoringApp.ViewModels;

namespace EnvironmentalMonitoringApp.Views;

public partial class AdminDashboard : ContentPage
{
    public AdminDashboard(AdminDashboardViewModel viewModel)
    {
        this.BindingContext = viewModel;
        InitializeComponent();

        Routing.RegisterRoute(nameof(AdminEditUserPage), typeof(AdminEditUserPage));
    }
}