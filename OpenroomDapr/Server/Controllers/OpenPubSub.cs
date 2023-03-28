using Dapr;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using OpenroomDapr.Shared.Model;
using System.Text.Json;

namespace OpenroomDapr.Server.Controllers;

public class OpenPubSubController : ControllerBase
{
    private readonly ILogger<OpenPubSubController> _logger;

    public OpenPubSubController(ILogger<OpenPubSubController> logger)
    {
        _logger = logger;
    }

    [HttpGet("daprgreet")]
    public async Task<ActionResult<MyPerson>> Greet([FromServices] DaprClient daprClient)
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
        await daprClient.PublishEventAsync("pubsub", "mypersons", person);
        _logger.LogInformation("End {methodname} in {classname}", nameof(Publish), nameof(WeatherForecastController));
        return Ok(person);
    }


    [HttpGet("publish")]
    public async Task<ActionResult<MyPerson>> Publish([FromServices] DaprClient daprClient)
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
        await daprClient.PublishEventAsync("pubsub", "mypersons", person);
        _logger.LogInformation("End {methodname} in {classname}", nameof(Publish), nameof(WeatherForecastController));
        return Ok(person);
    }

    [HttpGet("subscribe")]
    [Topic("pubsub", "mypersons")]
    public ActionResult<MyPerson> Subscribe([FromServices] DaprClient daprClient)
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
