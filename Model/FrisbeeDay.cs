using System;

namespace Fullstack.Model;

public class FrisbeeDay
{
	public int Id { get; set; }
	public DateTimeOffset Date { get; set; }
	public FrisbeeConditions Conditions { get; set; }
}
