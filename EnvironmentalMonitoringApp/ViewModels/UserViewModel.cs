using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using EnvironmentalMonitoringApp.Models;
using EnvironmentalMonitoringApp.Data;
using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Collections.ObjectModel;


namespace EnvironmentalMonitoringApp.ViewModels;

public partial class UserViewModel : ObservableObject, IQueryAttributable
{
    private Models.Users _users;

    public int User_id => _users.user_id;
    public string First_name
    {
        get => _users.first_name;
        set
        {
            if (_users.first_name != value)
            {
                _users.first_name = value;
                OnPropertyChanged();
            }
        }
    }
    public string Last_name
    {
        get => _users.last_name;
        set
        {
            if (_users.last_name != value)
            {
                _users.last_name = value;
                OnPropertyChanged();
            }
        }
    }
    public string Email => _users.email;
    public string Role
    {
        get => _users.role;
        set
        {
            if (_users.role != value)
            {
                _users.role = value;
                OnPropertyChanged();
            }
        }
    }
    public int Login_id => _users.login_id;

    public string SelectedRole
    {
        get => _users.role;
        set
        {
            if (_users.role != value)
            {
                _users.role = value;
                OnPropertyChanged();
            }
        }
    }

    // Combine names for presentation
    public string FullName => _users.first_name + " " + _users.last_name;

    private EnvMonitorDbContext _context;

    public ObservableCollection<string> Roles { get; } = new ObservableCollection<string>
    {
        "Administrator",
        "Operations Manager",
        "Environmental Scientist"
    };

    public UserViewModel(EnvMonitorDbContext envMonitorDbContext)
    {
        _context = envMonitorDbContext;
        _users = new Users();
    }
    public UserViewModel(EnvMonitorDbContext envMonitorDbContext, Users user)
    {
        _context = envMonitorDbContext;
        _users = user;
    }

    [RelayCommand]
    private async Task Save()
    {
        _users.role = SelectedRole;
        _context.SaveChanges();
        await Shell.Current.GoToAsync(nameof(Views.AdminDashboard));
    }

    void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("load"))
        {
            _users = _context.Users.Single(u => u.user_id == int.Parse(query["load"].ToString()));
            RefreshProperties();
        }
    }

    public void Reload()
    {
        _context.Entry(_users).Reload();
        RefreshProperties();
    }

    private void RefreshProperties()
    {
        OnPropertyChanged(nameof(Role));
        OnPropertyChanged(nameof(SelectedRole));
        OnPropertyChanged(nameof(First_name));
        OnPropertyChanged(nameof(Last_name));
        OnPropertyChanged(nameof(FullName));
    }
}
