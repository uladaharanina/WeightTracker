using TrackerService.Models;

public class IWeightRepo
{
    public Task<List<WeightEntry>> GetWeightEntries();
    public Task<WeightEntry> GetWeightById(int id);
    public Task<WeightEntry> DeleteWeightEntry();

}