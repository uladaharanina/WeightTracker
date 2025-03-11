public static class WeightCalculator
{
    public static MonthlyReportDTO GetMonthlyReportDTOs(List<WeightEntryDTO> allEntries)
    {

        List<WeightEntryDTO> monthlyWeights = new List<WeightEntryDTO>();

        //Get all entries that is less than 30 days old

        DateTime today = DateTime.Today;
        monthlyWeights = allEntries.Where(entry => today.Subtract(entry.Date).Days <= 30).ToList();

        if (monthlyWeights.Count == 0)
        {
            throw new Exception("No entries within the last 30 days!");
        }

        double avg = getAvarage(monthlyWeights);
        double highestWeight = monthlyWeights.Max(x => x.Weight);
        double lowestWeight = monthlyWeights.Min(x => x.Weight);

        MonthlyReportDTO monthlyReport = new MonthlyReportDTO(avg, highestWeight, lowestWeight);

        return monthlyReport;
    }
    public static double getAvarage(List<WeightEntryDTO> entries)
    {

        if (entries.Count == 0)
        {
            return 0;
        }
        double sum = 0;
        foreach (var entry in entries)
        {
            sum += entry.Weight;
        }
        return sum / entries.Count;
    }
}