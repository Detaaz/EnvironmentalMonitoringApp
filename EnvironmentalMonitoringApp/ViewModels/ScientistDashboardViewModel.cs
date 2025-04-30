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
    public ObservableCollection<SensorConfigViewModel> AllSensors { get; }

    public ICommand SelectSensorCommand { get; }

    private EnvMonitorDbContext _context;

    public ScientistDashboardViewModel(EnvMonitorDbContext envMonitorDbContext)
    {
        _context = envMonitorDbContext;

        // Gets quantity ids
        // Has to be a better way of doing this but it works
        AllSensors = new ObservableCollection<SensorConfigViewModel>(
            _context.Sensors.Select(sensor => new
            {
                Sensor = sensor,
                PhysicalQuantity = _context.PhysicalQuantities
                .Where(pq => pq.sensor_id == sensor.sensor_id)
                .Select(pq => new { pq.quantity_name, pq.lower_warning_threshold, pq.lower_emergency_threshold, pq.upper_warning_threshold, pq.upper_emergency_threshold })
                .FirstOrDefault()
            }).ToList()
            .Select(pq => new SensorConfigViewModel(_context, pq.Sensor)
            {
                SensorName = pq.PhysicalQuantity.quantity_name,
                LowerWarning = pq.PhysicalQuantity.lower_warning_threshold,
                UpperWarning = pq.PhysicalQuantity.upper_warning_threshold,
                LowerEmergency = pq.PhysicalQuantity.lower_emergency_threshold,
                UpperEmergency = pq.PhysicalQuantity.upper_emergency_threshold

            }));

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
