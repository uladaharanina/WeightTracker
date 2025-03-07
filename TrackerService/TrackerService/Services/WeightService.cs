using TrackerService.Models;
using TrackerService.Interfaces;

public class WeightService : IWeightService
{
    private readonly IWeightRepo _weightRepo;

    public WeightService(IWeightRepo weightRepo)
    {
        _weightRepo = weightRepo;
    }
    public async Task<List<WeightEntry>?> GetWeights()
    {
        List<WeightEntry>? entries = await _weightRepo.GetWeightEntries();
        if (entries == null)
        {
            return null;
        }
        return entries;
    }
    public Task<WeightEntry> GetWeightById(int id)
    {
        throw new NotImplementedException();
    }
    public async Task<WeightEntry?> AddWeight(WeightEntry weightEntry)
    {
        if (weightEntry.Weight > 0)
        {
            // Save to database
            WeightEntry? entry = await _weightRepo.AddNewWeightEntry(weightEntry);
            if (entry != null)
            {
                return entry;
            }
            return null;
        }
        else
        {
            throw new Exception("Weight is required.");
        }
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
