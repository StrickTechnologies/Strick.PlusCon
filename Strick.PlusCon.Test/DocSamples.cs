using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static Strick.PlusCon.Helpers;


namespace Strick.PlusCon.Test;


internal static class DocSamples
{
	private static Color docHdFore = Color.White;
	private static Color docHdBack = Color.Blue;

	public static void go(bool wait = false)
	{
		//WL("Hello World!");

		go(1, wait, wait);
		go(2, wait, wait);
		go(3, wait, wait);
		go(4, wait, wait);
		go(5, wait, wait);
		go(6, wait, wait);
		go(7, wait, wait);
		go(8, wait, wait);
		go(9, wait, wait);
		go(10, wait, wait);
		go(11, wait, wait);
	}

	public static void go(int sample, bool wait = false, bool clearAfter = false)
	{
		switch (sample)
		{
			case 1:
				s1();
				break;
			case 2:
				s2();
				break;
			case 3:
				s3();
				break;
			case 4:
				s4();
				break;
			case 5:
				s5();
				break;
			case 6:
				s6();
				break;
			case 7:
				s7();
				break;
			case 8:
				s8();
				break;
			case 9:
				s9();
				break;
			case 10:
				s10();
				break;
			case 11:
				s11();
				break;

			default:
				throw new ArgumentOutOfRangeException(nameof(sample));
		}
		WL();

		if (wait)
		{ RK(); }

		if (clearAfter)
		{ Console.Clear(); }
	}

	private static void showDocHd(string text)
	{
		WL($"* {text} *".Colorize(docHdFore, docHdBack));
		WL();
	}


	#region SAMPLES

	private static void s1()
	{
		//sample 1
		showDocHd("Sample 1");
		WL("Hello World!", Color.Red);
		WL("Hello World!", Color.Red, Color.White);
	}

	private static void s2()
	{
		//sample 2
		showDocHd("Sample 2");
		WL("Hello [World]!", Color.Red);
		WL("Hello [World]!", Color.Red, Color.White);

	}

	private static void s3()
	{
		//sample 3
		showDocHd("Sample 3");
		WL("Hello [World]!", Color.Red, null, true);
		WL("Hello [World]!", Color.Red, Color.White, true);
	}

	private static void s4()
	{
		//sample 4
		showDocHd("Sample 4");
		WL("Hello [World]!", Color.Red, null, Color.Red);
		WL("Hello [World]!", Color.Red, Color.White, Color.Blue, Color.White);
	}

	private static void s5()
	{
		//sample 5 **COLORIZE
		showDocHd("Sample 5 - Colorize");
		string colorized = "foo".Colorize(Color.Red);
		WL(colorized);
		WL(colorized.Colorize(null, Color.White)); //add background
		WL("Hello World!".Colorize(Color.Red));
		WL("Hello World!".Colorize(Color.Red, Color.White));
	}

	private static void s6()
	{
		//sample 6 **COLORIZE II
		showDocHd("Sample 6 - Colorize II");
		string wrapped = "foo".Colorize(Color.Red, null, "**[", "]**");
		WL(wrapped);
		wrapped = "foo".Colorize(Color.Red, null, "**[", "]**", Color.Lime, Color.White);
		WL(wrapped);
		wrapped = "cruel".Colorize(Color.Red, null, "*", "*", Color.Lime);
		WL($"Hello {wrapped} World!");
	}

	private static void s7()
	{
		//sample 7 **UNDERLINE
		showDocHd("Sample 7 - Underline");
		string underlined = "foo".Underline();
		WL(underlined);
		WL("Hello World!".Underline());
	}

	private static void s8()
	{
		//sample 8 **REVERSE
		showDocHd("Sample 8 - Reverse");
		string reversed = "foo".Reverse();
		WL(reversed);
		WL("Hello World!".Reverse());
	}

	private static void s9()
	{
		//sample 9 **GRADIENT
		showDocHd("Sample 9 - Gradient");
		string grad = "foo-bar".Gradient(Color.White, Color.BlueViolet);
		WL(grad);
		WL("Hello-World!".Gradient(Color.Red, Color.White));
		WL("--=gradients=--".Gradient(Color.White, Color.BlueViolet, Color.White));
		WL("***fade-out***".Gradient(Color.White, Color.Black));
		WL("***fade-in!***".Gradient(Color.Black, Color.White));
		WL("-- ** on the beach ** --".Gradient(Color.SandyBrown, Color.FromArgb(3, 240, 165), Color.FromArgb(145, 193, 255)));
	}

	private static void s10()
	{
		//sample 10 **COMBINATIONS
		showDocHd("Sample 10 - Combinations");
		WL($"Hello World!".Underline(), Color.Red);
		WL($"Hello {"cruel".Underline()} World!", Color.Red);
		WL("***fade-out***".Gradient(Color.Black, Color.White).Colorize(null, Color.White));
		WL("***fade-in!***".Gradient(Color.White, Color.Black).Colorize(null, Color.White));
		WL("-- ** on the beach ** --".Gradient(Color.SandyBrown, Color.FromArgb(3, 240, 165), Color.FromArgb(145, 193, 255)).Reverse());
		WL("-- ** on the beach ** --".Gradient(Color.SandyBrown, Color.FromArgb(3, 240, 165), Color.FromArgb(145, 193, 255)).Underline());
	}

	private static void s11()
	{
		//sample 11 **NOTES
		showDocHd("Sample 11 - Notes");
		Console.ForegroundColor = ConsoleColor.Red;
		WL($"Hello {"cruel".Colorize(Color.Lime).Underline()} World!");
		Console.ResetColor();
		WL($"Hello {"cruel".Colorize(Color.Lime).Underline()} World!", Color.Red);
		WL($"Hello [cruel] World!".Colorize(Color.Red), Color.Lime);
		//You have to use something more like this...
		WL($"{"Hello".Colorize(Color.Red)} {"cruel".Colorize(Color.Lime).Underline()} {"World!".Colorize(Color.Red)}");
	}

	#endregion SAMPLES
}
