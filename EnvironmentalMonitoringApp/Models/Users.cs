using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnvironmentalMonitoringApp.Models;

[Table("users")]
[PrimaryKey(nameof(user_id))]

public class Users
    {
    public int user_id { get; set; }
    public string first_name { get; set; }
    public string last_name { get; set; }
    public string email { get; set; }
    public string role { get; set; }
    public int login_id { get; set; }
}
