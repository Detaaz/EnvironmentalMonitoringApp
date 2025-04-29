using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using EnvironmentalMonitoringApp.Models;
using EnvironmentalMonitoringApp.Data;
using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace EnvironmentalMonitoringApp.ViewModels;

public partial class ScientistDashboardViewModel : ObservableObject
{
    public ObservableCollection<SensorConfigViewModel> AllSensors { get; }



    private EnvMonitorDbContext _context;

    public ScientistDashboardViewModel(EnvMonitorDbContext envMonitorDbContext)
    {
        _context = envMonitorDbContext;
        AllSensors = new ObservableCollection<SensorConfigViewModel>(
            _context.Sensors.ToList().Select(sc => new SensorConfigViewModel(_context, sc))
        );
    }

}
