using TrackerService.Models;
namespace TrackerService.Interfaces;
public interface IWeightRepo
{
    public Task<List<WeightEntry>?> GetWeightEntries();
    public Task<WeightEntry?> GetWeightById(int id);
    public Task<WeightEntry?> AddNewWeightEntry(WeightEntry entry);
    public Task<WeightEntry?> UpdateWeightEntry(WeightEntry entry);
    public Task<string> DeleteWeightEntry(WeightEntry entry);

}