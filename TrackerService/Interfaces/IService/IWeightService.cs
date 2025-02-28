using TrackerService.Models;
public interface IWeightService
{

    public Task<List<WeightEntry>> GetWeights();
    public Task<WeightEntry> GetWeightById(int id);
    public Task<WeightEntry> AddWeight(WeightEntry weightEntry);
    public Task<string> UpdateWeight(WeightEntry weightEntry);
    public Task<string> DeleteWeight(int id);

}