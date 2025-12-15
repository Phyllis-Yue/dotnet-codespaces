using Microsoft.AspNetCore.Mvc;


namespace InterviewProject;

[ApiController]
[Route("api/firmware")]
public class FirmwareUpdateController : ControllerBase
{
    private readonly FirmwareUpdateService service;

    public FirmwareUpdateController(FirmwareUpdateService service)
    {
        this.service = service;
    }

    [HttpPost("progress")]
    public async Task<IActionResult> SetProgress([FromQuery] Guid deviceId, [FromQuery] int progress, [FromBody] string notes)
    {
        string state = await this.service.UpdateProgress(deviceId, progress, notes);
        return Ok(new { deviceId = deviceId, state = state });
    }

    [HttpGet("active")]
    public IActionResult GetActive([FromQuery] Guid deviceId)
    {
        return Ok("TODO");
    }
}
