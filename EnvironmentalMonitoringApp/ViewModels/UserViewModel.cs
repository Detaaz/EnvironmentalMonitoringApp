using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using EnvironmentalMonitoringApp.Models;
using EnvironmentalMonitoringApp.Data;
using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace EnvironmentalMonitoringApp.ViewModels;

public partial class UserViewModel : ObservableObject, IQueryAttributable
{
    private Models.Users _users;

    public int User_id => _users.user_id;
    public string First_name => _users.first_name;
    public string Last_name => _users.last_name;
    public string Email => _users.email;
    public string Role => _users.role;
    public int Login_id => _users.login_id;

    public string FullName => _users.first_name + " " + _users.last_name;

    private EnvMonitorDbContext _context;
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
    }
}
