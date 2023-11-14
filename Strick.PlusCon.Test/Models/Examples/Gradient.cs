using System.Drawing;

using static Strick.PlusCon.Helpers;


namespace Strick.PlusCon.Test.Models.Examples;


internal static class Gradient
{
	static Gradient()
	{
		Samples = new List<DocSample>()
		{
			new DocSample("gradient1", "Example - Gradient (1)", Ex_gradient_1),
			new DocSample("gradient2", "Example - Gradient (2)", Ex_gradient_2),
			new DocSample("gradient3", "Example - Gradient (3)", Ex_gradient_3),
		};
	}

	internal static List<DocSample> Samples;

	internal static string MenuTitle => "Gradients";


	internal static void Ex_gradient_1()
	{
		//Example **GRADIENT
		string grad = "foo-bar".Gradient(Color.White, Color.BlueViolet);
		WL(grad);
		WL("Hello-World!".Gradient(Color.Red, Color.White));
		WL("--=gradients=--".Gradient(Color.White, Color.BlueViolet, Color.White));
		WL("***fade-out***".Gradient(Color.White, Color.Black));
		WL("***fade-in!***".Gradient(Color.Black, Color.White));
		WL("-- ** down on the beach ** --".Gradient(Color.SandyBrown, Color.FromArgb(3, 240, 165), Color.FromArgb(145, 193, 255)));
	}

	internal static void Ex_gradient_2()
	{
		//Example **GRADIENT 2
		var colors = ColorUtilities.GetGradientColors(Color.SkyBlue, Color.Orange, Console.WindowHeight).ToList();
		string spaces = new(' ', Console.WindowWidth);
		foreach (var color in colors)
		{ W(spaces, Color.White, color); }

		Console.SetCursorPosition(0, 0);
		W("Sunrise", Color.White, colors[0]);
	}

	internal static void Ex_gradient_3()
	{
		//Example **GRADIENT 3
		int top = Console.WindowHeight / 2;
		int bottom = Console.WindowHeight - top;
		var colors = ColorUtilities.GetGradientColors(Color.FromArgb(145, 193, 255), Color.FromArgb(3, 240, 165), top).ToList();
		colors.AddRange(ColorUtilities.GetGradientColors(Color.FromArgb(3, 240, 165), Color.SandyBrown, bottom));
		string spaces = new(' ', Console.WindowWidth);
		foreach (var color in colors)
		{ W(spaces, Color.White, color); }

		Console.SetCursorPosition(0, Console.WindowHeight - 2);
		W("On the beach", Color.White, colors[^2]);
	}
}
