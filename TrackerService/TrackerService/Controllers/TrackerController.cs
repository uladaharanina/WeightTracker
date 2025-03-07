
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrackerService.Interfaces;
using TrackerService.Models;

public class TrackerController : ControllerBase
{

    private readonly IWeightService _weightService;

    public TrackerController(IWeightService weightService)
    {
        _weightService = weightService;
    }
    [HttpGet]
    [Route("api/weights")]
    public async Task<ActionResult<List<WeightEntry?>>> GetWeights()
    {
        try
        {
            List<WeightEntry>? weights = await _weightService.GetWeights();
            return Ok(weights);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost]
    [Route("api/Addweights")]
    public async Task<ActionResult<WeightEntry>> AddWeight([FromBody] WeightEntry entry)
    {
        try
        {
            WeightEntry? addedEntry = await _weightService.AddWeight(entry);
            if (addedEntry != null)
            {
                return Ok(addedEntry);
            }
            else
            {
                return StatusCode(400, "Weight entry is null");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}