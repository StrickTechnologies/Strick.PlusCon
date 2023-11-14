using System.Drawing;

using Strick.PlusCon.Models;

using static Strick.PlusCon.Helpers;


namespace Strick.PlusCon.Test.Models.Examples;


internal static class RulerExamples
{
	static RulerExamples()
	{
		Samples = new List<DocSample>()
		{
			new DocSample("hruler1", "Example - Horizontal Ruler (1)", Ex_HRuler_1),
			new DocSample("vruler1", "Example - Vertical Ruler (1)", Ex_VRuler_1),
		};
	}

	internal static List<DocSample> Samples;


	internal static string MenuTitle => "Rulers";

	internal static void Ex_HRuler_1()
	{
		WL(Ruler.GetH(10));
		WL("Default");

		W(Ruler.GetH());
		WL("console width");

		WL(Ruler.GetH(30).Reverse());
		WL("reverse color");

		WL(Ruler.GetH(30, "----*----"));
		WL("change characters");

		WL(Ruler.GetH(30, new[] { Color.Red, Color.White }));
		W("change colors");
	}

	internal static void Ex_VRuler_1()
	{
		W(Ruler.GetV());
		Console.SetCursorPosition(1, 0);
		W("default".Vertical());

		Console.SetCursorPosition(4, 0);
		W(Ruler.GetV().Reverse());
		Console.SetCursorPosition(5, 0);
		W("reverse".Vertical());

		Console.SetCursorPosition(8, 0);
		W(Ruler.GetV(Console.WindowHeight, @"/\/\*/\/\"));
		Console.SetCursorPosition(9, 0);
		W("characters".Vertical());

		Console.SetCursorPosition(12, 0);
		W(Ruler.GetV(Console.WindowHeight, new[] { Color.Red, Color.White }));
		Console.SetCursorPosition(13, 0);
		W("colors".Vertical());
	}
}
