using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using EnvironmentalMonitoringApp.Models;
using EnvironmentalMonitoringApp.Data;
using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace EnvironmentalMonitoringApp.ViewModels;

public partial class AdminDashboardViewModel : ObservableObject
{
    public ObservableCollection<UserViewModel> AllUsers { get; }

    public ICommand SelectUserCommand { get; }

    private EnvMonitorDbContext _context;

    public AdminDashboardViewModel(EnvMonitorDbContext envMonitorDbContext)
    {
        _context = envMonitorDbContext;
        AllUsers = new ObservableCollection<UserViewModel>(
            _context.Users.ToList().Select(u => new UserViewModel(_context, u))
        );
        SelectUserCommand = new AsyncRelayCommand<UserViewModel>(SelectUserAsync);
    }

    private async Task SelectUserAsync(ViewModels.UserViewModel user)
    {
        try
        {
            if (user != null)
                await Shell.Current.GoToAsync($"{nameof(Views.AdminEditUserPage)}?load={user.User_id}");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
        }
    }
}
