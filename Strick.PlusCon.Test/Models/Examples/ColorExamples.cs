using System.Drawing;

using static Strick.PlusCon.Helpers;


namespace Strick.PlusCon.Test.Models.Examples;


internal static class ColorExamples
{
	static ColorExamples()
	{
		Samples = new List<DocSample>()
		{
			new DocSample("colorize1", "Example - Colorize (1)", Ex_colorize_1),
			new DocSample("colorize2", "Example - Colorize (2)", Ex_colorize_2),
			new DocSample("colorize3", "Example - Colorize (3)", Ex_colorize_3),

			new DocSample("colorUtil1", "Example - Color Utilities (1)", Ex_ColorUtil_1),
		};

	}

	internal static List<DocSample> Samples;

	internal static string MenuTitle => "Colors";


	internal static void Ex_colorize_1()
	{
		//Example **COLORIZE 1
		string colorized = "foo".Colorize(Color.Red);
		WL(colorized);
		WL(colorized.Colorize(null, Color.White)); //add background
		WL("Hello World!".Colorize(Color.Red));
		WL("Hello World!".Colorize(Color.Red, Color.White));
	}

	internal static void Ex_colorize_2()
	{
		//Example **COLORIZE 2
		string wrapped = "foo".Colorize(Color.Red, null, "**[", "]**");
		WL(wrapped);
		wrapped = "foo".Colorize(Color.Red, null, "**[", "]**", Color.Lime, Color.White);
		WL(wrapped);
		wrapped = "cruel".Colorize(Color.Red, null, "*", "*", Color.Lime);
		WL($"Hello {wrapped} World!");
	}

	internal static void Ex_colorize_3()
	{
		//Example **COLORIZE 3
		string stick = "Peppermint-stick";
		List<Color> colors = new List<Color>();
		colors.Add(Color.Red);
		colors.Add(Color.White);
		WL(stick.Colorize(colors));

		colors = ColorUtilities.GetGradientColors(Color.Red, Color.White, 5).ToList();
		WL(stick.Colorize(colors));

		colors = ColorUtilities.GetGradientColors(Color.Red, Color.White, stick.Length).ToList();
		WL(stick.Colorize(colors));

		colors.Clear();
		colors.Add(Color.SaddleBrown);
		colors.Add(Color.White);
		colors.Add(Color.Blue);
		WL("Peppermint-Pattie".Colorize(colors).Colorize(null, Color.LightGray));

		colors.Clear();
		colors.Add(Color.FromArgb(171, 96, 0)); //brown
		colors.Add(Color.FromArgb(57, 168, 53)); //green
		colors.Add(Color.FromArgb(2, 85, 166)); //blue
		WL("Peppermint-Patty".Colorize(colors).Colorize(null, Color.LightGray));
	}


	internal static void Ex_ColorUtil_1()
	{
		Color b = Color.FromArgb(255, 0, 0);
		Color d = Color.FromArgb(255, 0, 0);
		int adjustment = 40;

		WL();
		WL();
		WL($"{ShowColor(b)}  {ShowColor(d)}");
		do
		{
			b = b.Brighten(adjustment);
			d = d.Darken(adjustment);
			WL($"{ShowColor(b)}  {ShowColor(d)}");
		} while (d.R > 0 || d.G > 0 || d.B > 0 || b.R < 255 || b.G < 255 || b.B < 255);
	}

	private static string ShowColor(Color color)
	{
		string text;
		text = $"R:{color.R:D3} G:{color.G:D3} B:{color.B:D3}";

		return text.Colorize(null, color);
	}
}
