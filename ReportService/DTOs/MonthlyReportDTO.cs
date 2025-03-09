public class MonthlyReportDTO
{

    public MonthlyReportDTO(double avg, double highest, double lowest)
    {

        MonthlyAverage = avg;
        HighestWeight = highest;
        LowestWeight = lowest;
    }
    public double MonthlyAverage { get; set; }
    public double HighestWeight { get; set; }
    public double LowestWeight { get; set; }
}
