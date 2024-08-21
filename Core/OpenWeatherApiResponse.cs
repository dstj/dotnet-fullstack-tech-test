using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Fullstack.Core;

public sealed class OpenWeatherApiResponse
{
	public double Latitude { get; set; }
	public double Longitude { get; set; }
	public double GenerationtimeMs { get; set; }
	public int UtcOffsetSeconds { get; set; }
	public string Timezone { get; set; }
	public string TimezoneAbbreviation { get; set; }
	public double Elevation { get; set; }
	public HourlyUnits HourlyUnits { get; set; }
	public Hourly Hourly { get; set; }
	public DailyUnits DailyUnits { get; set; }
	public Daily Daily { get; set; }
}

public sealed class DailyUnits
{
	public string Time { get; set; }
	public string Sunrise { get; set; }
	public string Sunset { get; set; }
}

public sealed class Daily
{
	public List<string> Time { get; set; }
	public List<string> Sunrise { get; set; }
	public List<string> Sunset { get; set; }
}

public sealed class HourlyUnits
{
	public string Time { get; set; }
	public string ApparentTemperature { get; set; }
	public string Rain { get; set; }
	public string WindGusts10m { get; set; }
}

public sealed class Hourly
{
	public List<string> Time { get; set; }
	public List<double> ApparentTemperature { get; set; }
	public List<double> Rain { get; set; }
	[JsonPropertyName("wind_gusts_10m")]
	public List<double> WindGusts10m { get; set; }
}
