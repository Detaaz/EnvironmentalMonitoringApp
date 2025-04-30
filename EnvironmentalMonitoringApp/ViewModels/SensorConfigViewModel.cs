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

public partial class SensorConfigViewModel : ObservableObject, IQueryAttributable
{
    private Models.Sensor _sensor;
    private Models.PhysicalQuantity _physicalQuantity;
    private EnvMonitorDbContext _context;
    internal object PhysicalQuantities;

    public int SensorID
    {
        get => _physicalQuantity.sensor_id;
        set
        {
            if (_physicalQuantity.sensor_id != value)
            {
                _physicalQuantity.sensor_id = value;
                OnPropertyChanged();
            }
        }
    }
    public string SensorName
    {
        get => _physicalQuantity.quantity_name;
        set
        {
            if (_physicalQuantity.quantity_name != value)
            {
                _physicalQuantity.quantity_name = value;
                OnPropertyChanged();

            }
        }
    }

    public double LowerWarning
    {
        get => _physicalQuantity.lower_warning_threshold;
        set
        {
            if (_physicalQuantity.lower_warning_threshold != value)
            {
                _physicalQuantity.lower_warning_threshold = value;
                OnPropertyChanged();

            }
        }
    }
    
    public double UpperWarning
    {
        get => _physicalQuantity.upper_warning_threshold;
        set
        {
            if (_physicalQuantity.upper_warning_threshold != value)
            {
                _physicalQuantity.upper_warning_threshold = value;
                OnPropertyChanged();

            }
        }
    }
        
    public double LowerEmergency
    {
        get => _physicalQuantity.lower_emergency_threshold;
        set
        {
            if (_physicalQuantity.lower_emergency_threshold != value)
            {
                _physicalQuantity.lower_emergency_threshold = value;
                OnPropertyChanged();

            }
        }
    }        

    public double UpperEmergency
    {
        get => _physicalQuantity.upper_emergency_threshold;
        set
        {
            if (_physicalQuantity.upper_emergency_threshold != value)
            {
                _physicalQuantity.upper_emergency_threshold = value;
                OnPropertyChanged();

            }
        }
    }

    public SensorConfigViewModel(EnvMonitorDbContext envMonitorDbContext)
    {
        _context = envMonitorDbContext;
        _physicalQuantity = new PhysicalQuantity();
    }

    public SensorConfigViewModel(EnvMonitorDbContext envMonitorDbContext, PhysicalQuantity phyiscalQuantity)
    {
        _context = envMonitorDbContext;
        _physicalQuantity = phyiscalQuantity;
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
            _physicalQuantity = _context.PhysicalQuantities.Single(pq => pq.quantity_id == int.Parse(query["load"].ToString()));
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
        OnPropertyChanged(nameof(SensorName));
        OnPropertyChanged(nameof(LowerWarning));
        OnPropertyChanged(nameof(UpperWarning));
        OnPropertyChanged(nameof(LowerEmergency));
        OnPropertyChanged(nameof(UpperEmergency));
    }
}
