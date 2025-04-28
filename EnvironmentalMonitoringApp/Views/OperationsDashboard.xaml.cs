using EnvironmentalMonitoringApp.ViewModels;

namespace EnvironmentalMonitoringApp.Views;

public partial class OperationsDashboard : ContentPage
{
	public OperationsDashboard(OperationsDashboardViewModel viewModel)
	{
        this.BindingContext = viewModel;
        InitializeComponent();
    }
}