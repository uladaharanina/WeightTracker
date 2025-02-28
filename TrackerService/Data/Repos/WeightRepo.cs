using Microsoft.EntityFrameworkCore;
using TrackerService.Interfaces;
using TrackerService.Models;
namespace TrackerService.Data;

public class WeightRepo : IWeightRepo
{

    private readonly WeightContext _context;
    public WeightRepo(WeightContext context)
    {
        _context = context;
    }
    public async Task<List<WeightEntry>?> GetWeightEntries()
    {
        List<WeightEntry>? WeightEntries = await _context.WeightEntries.ToListAsync();
        return WeightEntries;
    }
    public async Task<WeightEntry?> GetWeightById(int id)
    {
        WeightEntry? searchedWeightEntry = await _context.WeightEntries.FirstOrDefaultAsync(entryId => entryId.Equals(id));
        return searchedWeightEntry;
    }

    public async Task<WeightEntry?> AddNewWeightEntry(WeightEntry entry)
    {
        _context.WeightEntries.Add(entry);
        await _context.SaveChangesAsync();
        return entry;
    }
    public async Task<WeightEntry?> UpdateWeightEntry(WeightEntry entry)
    {
        _context.WeightEntries.Update(entry);
        await _context.SaveChangesAsync();
        return entry;
    }
    public async Task<string> DeleteWeightEntry(WeightEntry entry)
    {
        _context.WeightEntries.Remove(entry);
        await _context.SaveChangesAsync();
        return "Removed";
    }

}