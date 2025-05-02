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

public partial class OperationsDashboardViewModel : ObservableObject
{
    public ObservableCollection<SensorViewModel> AllSensors { get; set; } 

    private EnvMonitorDbContext _context;

    public OperationsDashboardViewModel(EnvMonitorDbContext envMonitorDbContext)
    {
        _context = envMonitorDbContext;
        LoadSensors();
    }

    private void LoadSensors()
    {
        var currentTime = DateTime.Now;

        // Get sensors with their latest measurement
        var sensorWithMeasurements = _context.Sensors.Select(sensor => new
        {
            Sensor = sensor,
            LatestMeasurement = _context.PhysicalQuantities
                .Where(pq => pq.sensor_id == sensor.sensor_id)
                .SelectMany(pq => _context.Measurements.Where(m => m.quantity_Id == pq.quantity_id))
                .OrderByDescending(m => m.timestamp)
                .FirstOrDefault()
        }).ToList();

        AllSensors = new ObservableCollection<SensorViewModel>(
        sensorWithMeasurements.Select(swm =>
        {
            var lastTimestamp = swm.LatestMeasurement?.timestamp;
            var expectedFrequency = ParseFrequency(swm.Sensor.measurement_frequency);

            TimeSpan? timeSinceLast = null;
            bool isLate = false;

            if (lastTimestamp.HasValue)
            {
                timeSinceLast = currentTime - lastTimestamp;

                if (expectedFrequency.HasValue)
                {
                    isLate = timeSinceLast > expectedFrequency;
                }
            }

            // update db
            if (isLate)
            {
                swm.Sensor.status = "Not operational";
                _context.SaveChanges();
            }
            else
            {
                swm.Sensor.status = "Operational";
                _context.SaveChanges();
            }

            // Set bindings
            return new SensorViewModel(_context, swm.Sensor)
            {
                LastMeasurement = lastTimestamp,
                TimeSinceLastMeasurement = timeSinceLast,
                IsLate = isLate
            };
        }));
    }

    private TimeSpan? ParseFrequency(string frequencyString)
    {
        if (string.IsNullOrWhiteSpace(frequencyString))
            return null;

        frequencyString = frequencyString.Trim().ToLower();

        if (frequencyString.Contains("hour"))
        {
            if (int.TryParse(frequencyString.Split(' ')[0], out int hours))
                return TimeSpan.FromHours(hours);
        }
        else if (frequencyString.Contains("minute"))
        {
            if (int.TryParse(frequencyString.Split(' ')[0], out int minutes))
                return TimeSpan.FromMinutes(minutes);
        }

        return null;
    }

    [RelayCommand]
    private async Task Refresh()
    {
        LoadSensors();
    }
}