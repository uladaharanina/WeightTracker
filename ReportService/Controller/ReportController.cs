using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

[Route("api/ReportService")]
public class ReportController : ControllerBase
{
    private IReportService _service;
    public ReportController(IReportService service)
    {
        _service = service;
    }

    [HttpGet("GenerateWeeklyReport")]
    public async Task<IActionResult> GetWeeklyReport()
    {
        //Request data
        try
        {
            WeeklyReportDTO report = await _service.GenerateWeeklyReport();
            return Ok(report);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    [Route("GenerateMontlyReport")]
    public async Task<IActionResult> GetMonthlyReport()
    {
        try
        {
            MonthlyReportDTO report = await _service.GenerateMonthlyReport();
            return Ok(report);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}