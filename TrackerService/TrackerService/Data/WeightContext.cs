namespace TrackerService.Data;

using Microsoft.EntityFrameworkCore;
using TrackerService.Models;

public class WeightContext : DbContext
{
    public WeightContext(DbContextOptions<WeightContext> options) : base(options)
    {
    }

    public DbSet<WeightEntry> WeightEntries { get; set; }
}