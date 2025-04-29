using EnvironmentalMonitoringApp.ViewModels;


namespace EnvironmentalMonitoringApp.Views;

public partial class SensorConfigEditPage : ContentPage
{
	public SensorConfigEditPage(SensorConfigViewModel viewModel)
	{
        this.BindingContext = viewModel;
        InitializeComponent();


	}
}