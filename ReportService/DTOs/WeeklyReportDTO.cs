public class WeeklyReportDTO
{
    public double WeeklyAverage { get; set; }
    public double HighestWeight { get; set; }
    public double LowestWeight { get; set; }

    public WeeklyReportDTO() { }
    public WeeklyReportDTO(double avg, double highest, double lowest)
    {

        WeeklyAverage = avg;
        HighestWeight = highest;
        LowestWeight = lowest;
    }


}