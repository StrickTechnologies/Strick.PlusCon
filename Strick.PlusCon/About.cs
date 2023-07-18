using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace Strick.PlusCon;


/// <summary>
/// Information about this product
/// </summary>
public static class About
{
	/// <summary>
	/// The name of the product
	/// </summary>
	public static string ProductName => Asm.Name!;

	/// <summary>
	/// The current version of the product
	/// </summary>
	public static Version Version => Asm.Version!;

	private static AssemblyName Asm => typeof(About).Assembly.GetName();
}
