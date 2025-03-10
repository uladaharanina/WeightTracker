
public class ReportService : IReportService
{
    private readonly HttpClient _httpClient;
    public ReportService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<WeeklyReportDTO> GenerateWeeklyReport()
    {
        var response = await _httpClient.GetAsync("http://localhost:8080/api/TrackerService/ListWeights");
        response.EnsureSuccessStatusCode();
        List<WeightEntryDTO>? allENtries = await response.Content.ReadFromJsonAsync<List<WeightEntryDTO>>();
        if (allENtries == null)
        {
            throw new Exception("No entries!");
        }
        //Get current date
        DateTime currentDate = DateTime.Today;
        //Filter by recent week

        List<WeightEntryDTO> weeklyEntries = allENtries.Where(entry => currentDate.Subtract(entry.Date).Days <= 7).ToList();

        //Calculate average weight for the week
        if (weeklyEntries.Count != 0)
        {
            double avg = WeightCalculator.getAvarage(weeklyEntries);
            double highestWeight = weeklyEntries.Max(x => x.Weight);
            double LowestWeight = weeklyEntries.Min(x => x.Weight);

            return new WeeklyReportDTO(Math.Round(avg, 2), highestWeight, LowestWeight);
        }
        else
        {
            throw new Exception("No recent entries!");
        }
    }

    public Task<MonthlyReportDTO> GenerateMonthlyReport()
    {
        throw new NotImplementedException("Currently Working on it!");
    }
}


