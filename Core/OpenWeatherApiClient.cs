using System;
using System.Threading;
using System.Threading.Tasks;
using Fullstack.Model;

namespace Fullstack.Core;

public interface IOpenWeatherApiClient
{
	Task<OpenWeatherApiResponse> GetWeatherAsync(DateTimeOffset date, Coordinates coordinates, CancellationToken cancellationToken);
}

public sealed class OpenWeatherApiClient : IOpenWeatherApiClient
{
	private static readonly Uri DefaultUri = new("https://archive-api.open-meteo.com/");

	public OpenWeatherApiClient()
		: this(DefaultUri)
	{
	}

	public OpenWeatherApiClient(Uri baseUrl)
	{
	}

	public Task<OpenWeatherApiResponse> GetWeatherAsync(DateTimeOffset date, Coordinates coordinates, CancellationToken cancellationToken)
	{
		// Call the real API at (example): https://archive-api.open-meteo.com/v1/archive?latitude=45.508888&longitude=-73.561668&start_date=2024-08-01&end_date=2024-08-01&daily=sunrise,sunset&hourly=apparent_temperature,rain,wind_gusts_10m

		var toReplaceByARealApiCall = new OpenWeatherApiResponse {
			Latitude = 45.51845,
			Longitude = -73.61069,
			GenerationtimeMs = 0.15294551849365234,
			UtcOffsetSeconds = 0,
			Timezone = "GMT",
			TimezoneAbbreviation = "GMT",
			Elevation = 29.0,
			HourlyUnits = new() {
				Time = "iso8601",
				ApparentTemperature = "°C",
				Rain = "mm",
				WindGusts10m = "km/h"
			},
			Hourly = new() {
				Time = [
					"2024-08-01T00:00",
					"2024-08-01T01:00",
					"2024-08-01T02:00",
					"2024-08-01T03:00",
					"2024-08-01T04:00",
					"2024-08-01T05:00",
					"2024-08-01T06:00",
					"2024-08-01T07:00",
					"2024-08-01T08:00",
					"2024-08-01T09:00",
					"2024-08-01T10:00",
					"2024-08-01T11:00",
					"2024-08-01T12:00",
					"2024-08-01T13:00",
					"2024-08-01T14:00",
					"2024-08-01T15:00",
					"2024-08-01T16:00",
					"2024-08-01T17:00",
					"2024-08-01T18:00",
					"2024-08-01T19:00",
					"2024-08-01T20:00",
					"2024-08-01T21:00",
					"2024-08-01T22:00",
					"2024-08-01T23:00"
				],
				ApparentTemperature = [
					21.3,
					18.9,
					19.0,
					19.3,
					19.2,
					19.0,
					18.8,
					18.8,
					19.0,
					19.3,
					18.8,
					17.8,
					18.8,
					19.7,
					20.4,
					20.8,
					20.7,
					20.1,
					20.8,
					22.0,
					22.7,
					23.3,
					22.0,
					21.4
				],
				Rain = [
					0.00,
					0.00,
					0.00,
					0.00,
					0.00,
					0.00,
					0.00,
					0.00,
					0.00,
					4.40,
					8.10,
					3.90,
					2.10,
					5.20,
					2.70,
					3.20,
					3.90,
					5.50,
					6.50,
					3.70,
					3.30,
					5.10,
					7.40,
					20.80
				],
				WindGusts10m = [
					25.6,
					38.9,
					38.9,
					35.3,
					33.5,
					32.0,
					33.5,
					33.1,
					31.3,
					28.8,
					24.8,
					42.5,
					46.4,
					30.2,
					29.9,
					29.5,
					25.9,
					29.2,
					31.0,
					28.1,
					28.1,
					22.0,
					34.9,
					50.4
				]
			},
			DailyUnits = new() {
				Time = "iso8601",
				Sunrise = "iso8601",
				Sunset = "iso8601"
			},
			Daily = new() {
				Time = [
					"2024-08-01"
				],
				Sunrise = [
					"2024-08-01T09:39"
				],
				Sunset = [
					"2024-08-02T00:22"
				]
			}
		};

		return Task.FromResult(toReplaceByARealApiCall);
	}
}
