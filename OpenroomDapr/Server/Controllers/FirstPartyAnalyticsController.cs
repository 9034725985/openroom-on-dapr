using Man.Dapr.Sidekick;
using Microsoft.AspNetCore.Mvc;
using OpenroomDapr.Server.Services;

namespace OpenroomDapr.Server.Controllers;

public class FirstPartyAnalyticsController : Controller
{
    private readonly ILogger<FirstPartyAnalyticsController> _logger;
    private readonly FirstPartyAnalyticsService _service;
    public FirstPartyAnalyticsController(FirstPartyAnalyticsService service, ILogger<FirstPartyAnalyticsController> logger)
    {
        _logger = logger;
        _service = service;
    }

    [Route("[controller]")]
    public async Task<bool> Index(string key, string value)
    {
        _logger.LogInformation("Begin {methodname} in {classname}", nameof(Index), nameof(FirstPartyAnalyticsController));
        Stopwatch stopwatch = Stopwatch.StartNew();
        bool result = await _service.PostAsync(key, value);
        stopwatch.Stop();
        _logger.LogInformation("End {methodname} in {classname}", nameof(Index), nameof(FirstPartyAnalyticsController));
        _logger.LogInformation("PerfMatters: {methodname} in {classname} returned in {stopwatchmilliseconds} milliseconds",
            nameof(Index), nameof(FirstPartyAnalyticsController), stopwatch.ElapsedMilliseconds);
        return result;
    }

    [HttpGet("status")]
    public ActionResult GetStatus(
        [FromServices] IDaprSidecarHost daprSidecarHost) => Ok(new
        {
            process = daprSidecarHost.GetProcessInfo(), // Information about the sidecar process such as if it is running
            options = daprSidecarHost.GetProcessOptions() // the sidecar options if running, including ports and locations 
        });

    [HttpGet("greet")]
    public ActionResult GetGreeting() => Ok("hello, world!");
}
