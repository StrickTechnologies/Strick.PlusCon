using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Strick.PlusCon;

public static class Extensions
{
	internal static string AsString(this ColorSpace cs)
	{
		if (!Enum.IsDefined(typeof(ColorSpace), cs))
		{ throw new ArgumentException("Invalid value", nameof(cs)); }

		return cs.ToString("D");
	}
}
