public static class WeightCalculator
{

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