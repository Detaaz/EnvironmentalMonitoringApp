using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using EnvironmentalMonitoringApp.Models;
using EnvironmentalMonitoringApp.Data;
using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.EntityFrameworkCore;



namespace EnvironmentalMonitoringApp.ViewModels;

public partial class LoginViewModel : ObservableObject
{
    private Models.Login _login;
    private readonly Models.Users _user;

    public int login_id => _login.login_id;
    public string Username
    {
        get => _login.username;
        set
        {
            if (_login.username != value)
            {
                _login.username = value;
                OnPropertyChanged();
            }
        }
    }
    public string Password
    {
        get => _login.password;
        set
        {
            if (_login.password != value)
            {
                _login.password = value;
                OnPropertyChanged();
            }
        }
    }

    private EnvMonitorDbContext _context;

    public LoginViewModel(EnvMonitorDbContext envMonitorDbContext)
    {
        _context = envMonitorDbContext;
        _login = new Login();
        _user = new Users();
    }

    [RelayCommand]
    private async Task Login()
    {
        try
        {

            var login = await _context.Logins
                .AsNoTracking()
                .FirstOrDefaultAsync(l => l.username == Username);

            if (login == null)
            {
                loginFailed();
                return;
            }
            if (login.username == Username)
            {
                if (login.password == Password)
                {
                    await Shell.Current.DisplayAlert("Success", "Logged in!", "OK");
                    // passes the id of the account thats logged in to direct the user to the correct dashboard
                    int logged_Id = login.login_id;
                    loginSuccess(logged_Id);
                }
                else
                {
                    loginFailed();
                    return;
                }
            }
            else
            {
                loginFailed();
                return;
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
        }
    }

    async void loginSuccess(int logged_Id)
    {
        try
        {
            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.login_id == logged_Id);

            if (user == null)
            {
                return;
            }
            if (user.role == "Administrator")
            {
                await Shell.Current.GoToAsync(nameof(Views.AdminDashboard));
            }
            else if (user.role == "Operations Manager")
            {
                await Shell.Current.GoToAsync(nameof(Views.OperationsDashboard));
            }
            else if (user.role == "Environmental Scientist")
            {
                await Shell.Current.GoToAsync(nameof(Views.ScientistDashboard));
            }

        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
        }
    }

    async void loginFailed()
    {
        await Shell.Current.DisplayAlert("Login Failed", "Please try again", "OK");
        return;
    }

    public void Reload()
    {
        _context.Entry(_login).Reload();
        RefreshProperties();
    }

    private void RefreshProperties()
    {
        OnPropertyChanged(nameof(Username));
        OnPropertyChanged(nameof(Password));
    }
}
