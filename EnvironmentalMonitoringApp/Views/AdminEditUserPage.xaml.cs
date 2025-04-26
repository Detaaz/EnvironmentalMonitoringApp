using EnvironmentalMonitoringApp.ViewModels;

namespace EnvironmentalMonitoringApp.Views;

public partial class AdminEditUserPage : ContentPage
{
	public AdminEditUserPage(UserViewModel viewModel)
	{
        this.BindingContext = viewModel;
        InitializeComponent();
	}
}