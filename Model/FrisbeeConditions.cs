using System;

namespace Fullstack.Model;

public class FrisbeeConditions
{
	public FrisbeeConditionsType Conditions { get; set; }
	public ReasonType Reasons { get; set; }
}

public enum FrisbeeConditionsType
{
	Terrible,
	Bad,
	Ok,
	Good,
	Perfect,
}

[Flags]
public enum ReasonType
{
	Cold = 1 << 0,
	Rainy = 1 << 1,
	Windy = 1 << 2,
}
