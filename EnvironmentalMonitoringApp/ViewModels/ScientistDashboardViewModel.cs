using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using EnvironmentalMonitoringApp.Models;
using EnvironmentalMonitoringApp.Data;
using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Linq;


namespace EnvironmentalMonitoringApp.ViewModels;

public partial class ScientistDashboardViewModel : ObservableObject
{
    public ObservableCollection<SensorConfigViewModel> AllSensors { get; set; }

    public ICommand SelectSensorCommand { get; }

    private EnvMonitorDbContext _context;

    public ScientistDashboardViewModel(EnvMonitorDbContext envMonitorDbContext)
    {
        _context = envMonitorDbContext;

        LoadSensors();

        SelectSensorCommand = new AsyncRelayCommand<SensorConfigViewModel>(SelectSensorAsync);
    }

    private void LoadSensors() 
    {
        // Gets quantity ids
        AllSensors = new ObservableCollection<SensorConfigViewModel>(
            _context.PhysicalQuantities.ToList().Select(pq => new SensorConfigViewModel(_context, pq))
            );
    }


    private async Task SelectSensorAsync(ViewModels.SensorConfigViewModel physicalQuantity)
    {
        try
        {
            if (physicalQuantity != null)
                await Shell.Current.GoToAsync($"{nameof(Views.SensorConfigEditPage)}?load={physicalQuantity.SensorID}");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
        }
    }

}
