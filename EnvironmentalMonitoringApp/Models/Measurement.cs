using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnvironmentalMonitoringApp.Models;

[Table("measurement")]
[PrimaryKey(nameof(measurement_id))]

public class Measurement
{
    public int measurement_id { get; set; }
    public DateTime timestamp { get; set; }
    public int quantity_Id { get; set; }
    public string value { get; set; }
    public string measurement_unit { get; set; }

}
