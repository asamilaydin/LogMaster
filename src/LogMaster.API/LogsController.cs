using Microsoft.AspNetCore.Mvc;
using LogMaster.Domain;
using LogMaster.Application;
using System;
using System.Threading.Tasks;

[ApiController]
[Route("[controller]")]
public class LogsController : ControllerBase
{
    private readonly LogService _logService;

    public LogsController(LogService logService)
    {
        _logService = logService;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] LogEntry log)
    {
        if (log == null) return BadRequest("Log cannot be null.");

        try
        {
            await _logService.SendLogAsync(log);
            return Ok("Log sent.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
