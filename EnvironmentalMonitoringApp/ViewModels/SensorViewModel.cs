using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using EnvironmentalMonitoringApp.Models;
using EnvironmentalMonitoringApp.Data;
using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Collections.ObjectModel;
using System.Data;


namespace EnvironmentalMonitoringApp.ViewModels;

public partial class SensorViewModel : ObservableObject
{
    private Models.Sensor _sensor;
    private Models.Measurement _Measurement;
    private Models.PhysicalQuantity _physicalQuantity;
    private EnvMonitorDbContext _context;

    public string SensorType
    {
        get => _sensor.sensor_type;
        set
        {
            if(_sensor.sensor_type != value)
            {
                _sensor.sensor_type = value;
                OnPropertyChanged();

            }
        }
    }
    public string Status
    {
        get => _sensor.status;
        set
        {
            if (_sensor.status != value)
            {
                _sensor.status = value;
                OnPropertyChanged();
            }
        }
    }
    public string MeasurementFrequency
    {
        get => _sensor.measurement_frequency;
        set
        {
            if (_sensor.measurement_frequency != value)
            {
                _sensor.measurement_frequency = value;
                OnPropertyChanged();
            }
        }
    }
    public DateTime? LastMeasurement { get; set; }
    public TimeSpan? TimeSinceLastMeasurement { get; set; }
    public bool IsLate { get; set; }


    public SensorViewModel(EnvMonitorDbContext envMonitorDbContext, Sensor sensor)
    {
        _context = envMonitorDbContext;
        _sensor = sensor;
    }

    public void Reload()
    {
        _context.Entry(_sensor).Reload();
        RefreshProperties();
    }

    private void RefreshProperties()
    {
        OnPropertyChanged(nameof(SensorType));
        OnPropertyChanged(nameof(Status));
        OnPropertyChanged(nameof(MeasurementFrequency));
        OnPropertyChanged(nameof(LastMeasurement));
        OnPropertyChanged(nameof(TimeSinceLastMeasurement));
        OnPropertyChanged(nameof(IsLate));
    }
}
