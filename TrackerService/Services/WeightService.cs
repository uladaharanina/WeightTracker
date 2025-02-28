using TrackerService.Models;
using TrackerService.Interfaces;

public class WeightService : IWeightService
{
    public Task<List<WeightEntry>> GetWeights()
    {
        throw new NotImplementedException();
    }
    public Task<WeightEntry> GetWeightById(int id)
    {
        throw new NotImplementedException();
    }
    public Task<WeightEntry> AddWeight(WeightEntry weightEntry)
    {
        throw new NotImplementedException();
    }
    public Task<string> UpdateWeight(WeightEntry weightEntry)
    {
        throw new NotImplementedException();
    }
    public Task<string> DeleteWeight(int id)
    {
        throw new NotImplementedException();
    }
}
