using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Fullstack.Core;
using Fullstack.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Fullstack.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FrisbeeController : ControllerBase
{
	private readonly ILogger<FrisbeeController> _logger;
	private readonly IFrisbeeConditionsChecker _frisbeeConditionsChecker;

	public FrisbeeController(ILogger<FrisbeeController> logger, IFrisbeeConditionsChecker frisbeeConditionsChecker)
	{
		_logger = logger;
		_frisbeeConditionsChecker = frisbeeConditionsChecker;
	}

	[HttpGet]
	public async Task<IActionResult> Check([FromQuery] DateTimeOffset date, [FromQuery] double longitude, double latitude)
	{
		var coordinates = new Coordinates(longitude, latitude);
		var conditions = await _frisbeeConditionsChecker.GetConditionsAsync(date, coordinates, Request.HttpContext.RequestAborted);
		return new JsonResult(conditions, new JsonSerializerOptions {
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
			Converters = { new JsonStringEnumConverter() }
		});
	}
}
