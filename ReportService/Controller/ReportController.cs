using Microsoft.AspNetCore.Mvc;

public class ReportController : ControllerBase
{

    [HttpGet]
    [Route("api/GenerateReport")]
    public IActionResult GetReport()
    {
        return StatusCode(200);
    }
}