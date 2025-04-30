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

public partial class SensorConfigViewModel : ObservableObject, IQueryAttributable
{
    private Models.Sensor _sensor;
    private Models.Measurement _Measurement;
    private Models.PhysicalQuantity _physicalQuantity;
    private EnvMonitorDbContext _context;
    internal object PhysicalQuantities;

    public int SensorID
    {
        get => _sensor.sensor_id;
        set
        {
            if (_sensor.sensor_id != value)
            {
                _sensor.sensor_id = value;
                OnPropertyChanged();
            }
        }
    }
    public string SensorType
    {
        get => _sensor.sensor_type;
        set
        {
            if (_sensor.sensor_type != value)
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

    public string SensorName { get; set; }
    public double LowerWarning { get; set; }
    public double UpperWarning { get; set; }
    public double LowerEmergency { get; set; }
    public double UpperEmergency { get; set; }


    public SensorConfigViewModel(EnvMonitorDbContext envMonitorDbContext)
    {
        _context = envMonitorDbContext;
        _sensor = new Sensor();
    }

    public SensorConfigViewModel(EnvMonitorDbContext envMonitorDbContext, Sensor sensor)
    {
        _context = envMonitorDbContext;
        _sensor = sensor;
    }

    [RelayCommand]
    private async Task Save()
    {
        // needs to be added to the figures that are being updated
        _context.SaveChanges();
        await Shell.Current.GoToAsync(nameof(Views.ScientistDashboard));
    }

    void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("load"))
        {
            _sensor = _context.Sensors.Single(s => s.sensor_id== int.Parse(query["load"].ToString()));
            RefreshProperties();
        }
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
        OnPropertyChanged(nameof(SensorName));
    }
}
