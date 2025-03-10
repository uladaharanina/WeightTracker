
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrackerService.Interfaces;
using TrackerService.Models;

[Route("api/TrackerService")]
public class TrackerController : ControllerBase
{

    private readonly IWeightService _weightService;

    public TrackerController(IWeightService weightService)
    {
        _weightService = weightService;
    }
    [HttpGet]
    [Route("ListWeights")]
    public async Task<ActionResult<List<WeightEntryDTO?>>> GetWeights()
    {
        try
        {
            List<WeightEntryDTO>? weights = await _weightService.GetWeights();
            return Ok(weights);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost]
    [Route("AddWeights")]
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