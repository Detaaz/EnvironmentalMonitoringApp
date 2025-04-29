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

public partial class ScientistDashboardViewModel : ObservableObject
{
    public ObservableCollection<SensorConfigViewModel> AllSensors { get; }

    public ICommand SelectSensorCommand { get; }

    private EnvMonitorDbContext _context;

    public ScientistDashboardViewModel(EnvMonitorDbContext envMonitorDbContext)
    {
        _context = envMonitorDbContext;
        AllSensors = new ObservableCollection<SensorConfigViewModel>(
            _context.Sensors.ToList().Select(sc => new SensorConfigViewModel(_context, sc))
        );
        SelectSensorCommand = new AsyncRelayCommand<SensorConfigViewModel>(SelectSensorAsync);
    }

    private async Task SelectSensorAsync(ViewModels.SensorConfigViewModel sensor)
    {
        try
        {
            if (sensor != null)
                await Shell.Current.GoToAsync($"{nameof(Views.SensorConfigEditPage)}?load={sensor.SensorID}");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
        }
    }

}
