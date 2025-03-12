
using System.Text.Json;
using Microsoft.AspNetCore.Http.HttpResults;

public class ReportService : IReportService
{
    private readonly HttpClient _httpClient;
    private readonly IMessageHandler _messageHandler;
    public ReportService(HttpClient httpClient, IMessageHandler messageHandler)
    {
        _httpClient = httpClient;
        _messageHandler = messageHandler;

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

    public async Task<MonthlyReportDTO> GenerateMonthlyReport()
    {
        List<WeightEntryDTO>? entries = new();
        var tcs = new TaskCompletionSource<List<WeightEntryDTO>>();

        _messageHandler.SendMessage("Request for MonthlyReport", (response) =>
       {

           List<WeightEntryDTO>? responseEntries = JsonSerializer.Deserialize<List<WeightEntryDTO>>(response);
           // If response entries are valid, set the result on the TaskCompletionSource
           if (responseEntries != null)
           {
               tcs.SetResult(responseEntries);
           }
           else
           {
               tcs.SetException(new Exception("Failed to deserialize response"));
           }
       });
        entries = await tcs.Task;
        return WeightCalculator.GetMonthlyReportDTOs(entries);



    }
}


