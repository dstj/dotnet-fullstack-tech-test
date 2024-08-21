using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Fullstack.Model;

namespace Fullstack.Core;

public interface IFrisbeeConditionsChecker
{
  Task<FrisbeeConditions> GetConditionsAsync(DateTimeOffset date, Coordinates coordinates, CancellationToken cancellationToken);
}

public class FrisbeeConditionsChecker : IFrisbeeConditionsChecker
{
	private readonly IOpenWeatherApiClient _openWeatherApiClient;

	public FrisbeeConditionsChecker(IOpenWeatherApiClient openWeatherApiClient)
	{
		_openWeatherApiClient = openWeatherApiClient;
	}

	public async Task<FrisbeeConditions> GetConditionsAsync(DateTimeOffset date, Coordinates coordinates, CancellationToken cancellationToken)
	{
		var weatherResponse = await _openWeatherApiClient.GetWeatherAsync(date, coordinates, cancellationToken);

		var sunrise = ParseDateTime(weatherResponse.Daily.Sunrise.Single());
		var sunset = ParseDateTime(weatherResponse.Daily.Sunset.Single());

		var timeIndexesToKeep = new HashSet<int>();
		for (var i = 0; i < weatherResponse.Hourly.Time.Count; ++i) {
			var time = ParseDateTime(weatherResponse.Hourly.Time[i]);
			if (time > sunrise && time < sunset) {
				timeIndexesToKeep.Add(i);
			}
		}

		var tempEntries = weatherResponse.Hourly.ApparentTemperature.Where((_, i) => timeIndexesToKeep.Contains(i)).ToArray();
		var rainEntries = weatherResponse.Hourly.Rain.Where((_, i) => timeIndexesToKeep.Contains(i)).ToArray();
		var windEntries = weatherResponse.Hourly.WindGusts10m.Where((_, i) => timeIndexesToKeep.Contains(i)).ToArray();

		const double negligibleRain = 0.3;
		var weather = new DayWeather {
			Date = date,
			MaxTemp = tempEntries.Max(),
			MinTemp = tempEntries.Min(),
			AvgTemp = tempEntries.Average(),
			MaxWind = windEntries.Max(),
			MinWind = windEntries.Min(),
			AvgWind = windEntries.Average(),
			QtyRain = rainEntries.Sum(),
			NbHoursRains = rainEntries.Count(x => x > negligibleRain),
		};

		var tempConditions = weather.AvgTemp switch {
			< 6 => FrisbeeConditionsType.Terrible,
			< 12 => FrisbeeConditionsType.Bad,
			< 20 => FrisbeeConditionsType.Good,
			< 25 => FrisbeeConditionsType.Perfect,
			< 30 => FrisbeeConditionsType.Good,
			< 35 => FrisbeeConditionsType.Ok,
			_ => FrisbeeConditionsType.Terrible
		};

		FrisbeeConditionsType rainQtyConditions;
		if (weather.NbHoursRains < 2) {
			rainQtyConditions = weather.QtyRain switch {
				< 1 => FrisbeeConditionsType.Perfect,
				< 5 => FrisbeeConditionsType.Good,
				< 10 => FrisbeeConditionsType.Bad,
				_ => FrisbeeConditionsType.Terrible
			};
		}
		else {
			rainQtyConditions = weather.QtyRain switch {
				< 1 => FrisbeeConditionsType.Good,
				< 3 => FrisbeeConditionsType.Ok,
				< 4 => FrisbeeConditionsType.Bad,
				_ => FrisbeeConditionsType.Terrible
			};
		}

		var windConditions = weather.AvgWind switch {
			< 5 => FrisbeeConditionsType.Perfect,
			< 10 => FrisbeeConditionsType.Good,
			< 21 => FrisbeeConditionsType.Ok,
			< 33 => FrisbeeConditionsType.Bad,
			_ => FrisbeeConditionsType.Terrible
		};

		var minConditions = new[] { (int)tempConditions, (int)rainQtyConditions, (int)windConditions }.Min();
		ReasonType reasons = 0;
		if (tempConditions < FrisbeeConditionsType.Ok) reasons |= ReasonType.Cold;
		if (rainQtyConditions < FrisbeeConditionsType.Ok) reasons |= ReasonType.Rainy;
		if (windConditions < FrisbeeConditionsType.Ok) reasons |= ReasonType.Windy;
		var conditions = new FrisbeeConditions {
			Conditions = (FrisbeeConditionsType)minConditions,
			Reasons = reasons
		};
		return conditions;
	}

	private static DateTimeOffset ParseDateTime(string str)
	{
		return DateTimeOffset.ParseExact(str, "yyyy-MM-ddTHH:mm", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
	}
}
