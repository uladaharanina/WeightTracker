namespace TrackerService.Models;

public class WeightEntry
{

    public int Id { get; set; }
    public DateTime Date { get; set; }
    public decimal Weight { get; set; }
    public string? Comment { get; set; }
}