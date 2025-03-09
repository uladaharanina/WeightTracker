using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace TrackerService.Models;

public class WeightEntry
{
    [Key]
    public int Id { get; set; }
    public DateTime Date { get; set; }

    [Required]
    public double Weight { get; set; }
    public string? Comment { get; set; }
}