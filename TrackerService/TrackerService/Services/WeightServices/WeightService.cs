using TrackerService.Models;
using TrackerService.Interfaces;

public class WeightService : IWeightService
{
    private readonly IWeightRepo _weightRepo;
    //private readonly IMessageProducer _messageProducer;

    public WeightService(IWeightRepo weightRepo)
    {
        _weightRepo = weightRepo;
    }
    public async Task<List<WeightEntryDTO>?> GetWeights()
    {
        List<WeightEntry>? entries = await _weightRepo.GetWeightEntries();
        List<WeightEntryDTO> entriesDTOs = new List<WeightEntryDTO>();
        if (entries == null)
        {
            return null;
        }
        //Convert to DTOs
        foreach (WeightEntry entry in entries)
        {
            entriesDTOs.Add(new WeightEntryDTO(entry.Weight, entry.Date, entry.Comment));
        }
        return entriesDTOs;
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
