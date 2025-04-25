using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnvironmentalMonitoringApp.Models;

[Table("login")]
[PrimaryKey(nameof(login_id))]

public class Login
{
    
    public int login_id { get; set; }
    [Required]
    public string username { get; set; }
    [Required]
    public string password { get; set; }

}
