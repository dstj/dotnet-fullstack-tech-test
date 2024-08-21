using System;

namespace Fullstack.Model;

public class DayWeather
{
	public DateTimeOffset Date { get; set; }
	public double MaxTemp { get; set; }
	public double MinTemp { get; set; }
	public double AvgTemp { get; set; }
	public double MaxWind { get; set; }
	public double MinWind { get; set; }
	public double AvgWind { get; set; }
	public double QtyRain { get; set; }
	public double NbHoursRains { get; set; }
}
