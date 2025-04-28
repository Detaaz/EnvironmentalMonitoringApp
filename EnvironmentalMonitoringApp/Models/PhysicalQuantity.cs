using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnvironmentalMonitoringApp.Models;

[Table("physical_quantity")]
[PrimaryKey(nameof(quantity_id))]


public class PhysicalQuantity
{
    public int quantity_id { get; set; }
    public string quantity_name { get; set; }
    public int sensor_id { get; set; }
    public double lower_warning_threshold { get; set; }
    public double upper_warning_threshold { get; set; }
    public double lower_emergency_threshold { get; set; }
    public double upper_emergency_threshold { get; set; }
}
