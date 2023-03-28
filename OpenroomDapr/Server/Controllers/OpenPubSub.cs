using Dapr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using OpenroomDapr.Shared.Model;

namespace OpenroomDapr.Server.Controllers;

[ApiController]
[Route("[controller]")]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
public class OpenPubSubController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<OpenPubSubController> _logger;

    public OpenPubSubController(ILogger<OpenPubSubController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public ActionResult<MyPerson> Publish()
    {
        _logger.LogInformation("Begin {methodname} in {classname}", nameof(Publish), nameof(WeatherForecastController));
        MyPerson person = new()
        {
            LegalName = "Abraham Lincoln",
            PreferredName = "Abe",
            Id = 16,
            Title = "Mr",
            Alias = "v-abrahamlincoln"
        };
        _logger.LogInformation("End {methodname} in {classname}", nameof(Publish), nameof(WeatherForecastController));
        return Ok(person);
    }

    [HttpGet]
    [Topic("orderpubsub", "orders")]
    public ActionResult<MyPerson> Subscribe()
    {
        _logger.LogInformation("Begin {methodname} in {classname}", nameof(Subscribe), nameof(WeatherForecastController));
        MyPerson person = new()
        {
            LegalName = "Abraham Lincoln",
            PreferredName = "Abe",
            Id = 16,
            Title = "Mr",
            Alias = "v-abrahamlincoln"
        };
        _logger.LogInformation("End {methodname} in {classname}", nameof(Subscribe), nameof(WeatherForecastController));
        return Ok(person);
    }
}
