using EnvironmentalMonitoringApp.ViewModels;

namespace EnvironmentalMonitoringApp.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage(LoginViewModel viewModel)
	{
        this.BindingContext = viewModel;
        InitializeComponent();
	}
}