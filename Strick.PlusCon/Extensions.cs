using System;


namespace Strick.PlusCon;


internal static class Extensions
{
	internal static string AsString(this ColorSpace cs)
	{
		if (!Enum.IsDefined(typeof(ColorSpace), cs))
		{ throw new ArgumentException("Invalid value", nameof(cs)); }

		return cs.ToString("D");
	}
}
