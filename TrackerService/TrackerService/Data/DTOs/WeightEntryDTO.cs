public class WeightEntryDTO
{

    public WeightEntryDTO(double Weight, DateTime date, string Comment)
    {

        this.Weight = Weight;
        this.Date = date;
        this.Comment = Comment;
    }

    public DateTime Date { get; set; }
    public double Weight { get; set; }
    public string? Comment { get; set; }
}