using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Strick.PlusCon.Test;


internal static class Extensions
{
	public static void Clear(this StringWriter sw) => sw.GetStringBuilder().Clear();
}
