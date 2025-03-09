using Microsoft.AspNetCore.Mvc;

public class ReportController : ControllerBase
{
    private IReportService _service;
    public ReportController(IReportService service)
    {
        _service = service;
    }

    [HttpGet]
    [Route("api/GenerateWeeklyReport")]
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
    [Route("api/GenerateMontlyReport")]
    public IActionResult GetMonthlyReport()
    {
        return StatusCode(200);
    }
}