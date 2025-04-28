using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnvironmentalMonitoringApp.Models;

[Table("sensor")]
[PrimaryKey(nameof(sensor_id))]

public class Sensor
{
    public int sensor_id { get; set; }
    public string sensor_type { get; set; }
    public string status { get; set; }
    public  DateTime deployment_date { get; set; }
    public double latitude { get; set; }
    public double longitude { get; set; }
    public double height { get; set; }
    public int orientation{ get; set; }
    public string measurement_frequency { get; set; }
}

