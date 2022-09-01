using System.Drawing;

using static Strick.PlusCon.Helpers;
//using static Strick.PlusCon.ConsoleUtility;


namespace Strick.PlusCon.Test;


internal class Program
{
	static void Main()
	{
		//ConsoleUtilities.EnableVirtualTerminal();

		//Banner(); WL();
		SetConsoleSize(43, 10);
		DocSamples.go(true);
		//DocSamples.go(4, false);

		//WL(); Boxes();
		//WL(); ShowValuesW();
		//WL(); ShowValuesC();
		//WL(); ShowValuesOther();
		//WL(); TryStuff();

		//WL(); gradients();
		//WL(); showGColors();
		//WL(); gradientsV();

		//RkTest();
		//RlTest();

		//var key = RK("press any key...");
		//W(EscapeCodes.GetForeColorSequence(Color.Blue)+EscapeCodes.GetBackColorSequence(Color.White));
		//Console.Clear();
		//W("foo bar");
	}

	private static void Banner() => WL("   S t r i c k . P l u s C o n   ".Gradient(Color.White, Color.Red, Color.White).Colorize(null, Color.DarkSlateGray).Reverse());

	private static void SetConsoleSize(int width, int height)
	{
		Console.SetWindowSize(width, height);
		Console.SetBufferSize(width, height);
	}

	private static void Gradients()
	{
		//odd number of characters
		WL("--=gradients=--".Gradient(Color.White, Color.BlueViolet) + " (white => blue violet)");
		WL("--=gradients=--".Gradient(Color.BlueViolet, Color.White) + " (blue violet => white)");
		WL("--=gradients=--".Gradient(Color.White, Color.BlueViolet, Color.White) + " (white => blue violet => white)");
		WL("--=gradients=--".Gradient(Color.White, Color.BlueViolet, Color.White).Colorize(null, Color.Gray) + " (white => blue violet => white w/gray background)");
		WL("--=gradients=--".Gradient(Color.LightGray, Color.BlueViolet, Color.LightGray).Colorize(null, Color.White) + " (light gray => blue violet => light gray w/white background)");

		//even number of characters
		WL("--=gradients!=--".Gradient(Color.White, Color.BlueViolet) + " (white => blue violet)");
		WL("--=gradients!=--".Gradient(Color.BlueViolet, Color.White) + " (blue violet => white)");
		WL("--=gradients!=--".Gradient(Color.White, Color.BlueViolet, Color.White) + " (white => blue violet => white)");
		WL("--=gradients!=--".Gradient(Color.White, Color.BlueViolet, Color.White).Colorize(null, Color.Gray) + " (white => blue violet => white w/gray background)");
		WL("--=gradients!=--".Gradient(Color.LightGray, Color.BlueViolet, Color.LightGray).Colorize(null, Color.White) + " (light gray => blue violet => light gray w/white background)");

		//reverse
		WL("   --=  r e v e r s e  =--   ".Gradient(Color.White, Color.BlueViolet, Color.White).Reverse());
		WL("--=gradients=--".Gradient(Color.White, Color.BlueViolet, Color.White).Reverse());
		WL("     -----==  g r a d i e n t s  ==-----     ".Gradient(Color.White, Color.BlueViolet, Color.White).Reverse());

		//color schemes
		WL("-- ** stars & stripes ** --".Gradient(Color.Red, Color.White, Color.Blue));
		WL("-- ** stars & stripes ** --".Gradient(Color.Red, Color.White, Color.Blue).Reverse());
		WL("-- ** on the beach ** --".Gradient(Color.SandyBrown, Color.FromArgb(3, 240, 165), Color.FromArgb(145, 193, 255)));
		WL("-- ** on the beach ** --".Gradient(Color.SandyBrown, Color.FromArgb(3, 240, 165), Color.FromArgb(145, 193, 255)).Reverse());
		WL("-- ** arizona ** --".Gradient(Color.Yellow, Color.FromArgb(247, 93, 32), Color.Brown));
		WL("-- ** arizona ** --".Gradient(Color.Yellow, Color.FromArgb(247, 93, 32), Color.Brown).Reverse());
		WL(" ".Gradient(Color.Red, Color.White, Color.Blue).Reverse());
		WL("  ".Gradient(Color.Red, Color.White, Color.Blue).Reverse());
		WL("   ".Gradient(Color.Red, Color.White, Color.Blue).Reverse());
		WL("    ".Gradient(Color.Red, Color.White, Color.Blue).Reverse());
		WL("     ".Gradient(Color.Red, Color.White, Color.Blue).Reverse());
		WL("      ".Gradient(Color.Red, Color.White, Color.Blue).Reverse());

		//"transparency"
		//  note: this creates a proper transparency gradient, but alpha doesn't work in console
		//wl("-- colorize! --".gradient(Color.FromArgb(255, 0, 0, 255), Color.FromArgb(0, 0, 0, 255)).colorize(null, Color.White));
		//...but we can simulate it like this
		WL("***fade-out***".Gradient(Color.White, Color.Black));
		WL("***fade-in!***".Gradient(Color.Black, Color.White));
		WL("***fade-out***".Gradient(Color.Black, Color.White).Colorize(null, Color.White));
		WL("***fade-in!***".Gradient(Color.White, Color.Black).Colorize(null, Color.White));
	}

	private static void GradientsV()
	{
		//odd number of characters
		ShowGColors("--=gradients=--", Color.White, Color.BlueViolet);
		ShowGColors("--=gradients=--", Color.BlueViolet, Color.White);
		WL("--=gradients=--".Gradient(Color.White, Color.BlueViolet, Color.White) + " (white => blue violet => white)");
		WL("--=gradients=--".Gradient(Color.White, Color.BlueViolet, Color.White).Colorize(null, Color.Gray) + " (white => blue violet => white w/gray background)");
		WL("--=gradients=--".Gradient(Color.LightGray, Color.BlueViolet, Color.LightGray).Colorize(null, Color.White) + " (light gray => blue violet => light gray w/white background)");

		//even number of characters
		WL("--=gradients!=--".Gradient(Color.White, Color.BlueViolet) + " (white => blue violet)");
		WL("--=gradients!=--".Gradient(Color.BlueViolet, Color.White) + " (blue violet => white)");
		WL("--=gradients!=--".Gradient(Color.White, Color.BlueViolet, Color.White) + " (white => blue violet => white)");
		WL("--=gradients!=--".Gradient(Color.White, Color.BlueViolet, Color.White).Colorize(null, Color.Gray) + " (white => blue violet => white w/gray background)");
		WL("--=gradients!=--".Gradient(Color.LightGray, Color.BlueViolet, Color.LightGray).Colorize(null, Color.White) + " (light gray => blue violet => light gray w/white background)");

		WL("-- ** stars & stripes ** --".Gradient(Color.Red, Color.White, Color.Blue));
		WL("-- ** on the beach ** --".Gradient(Color.SandyBrown, Color.FromArgb(3, 240, 165), Color.FromArgb(145, 193, 255)));
		WL("-- ** arizona ** --".Gradient(Color.Yellow, Color.FromArgb(247, 93, 32), Color.Brown));

		//note: alpha doesn't work in console
		//wl("-- colorize! --".gradient(Color.FromArgb(255, 0, 0, 255), Color.FromArgb(0, 0, 0, 255)).colorize(null, Color.White));
		//...but we can simulate it like this
		WL("***fade-out***".Gradient(Color.White, Color.Black));
		WL("***fade-in!***".Gradient(Color.Black, Color.White));
		WL("***fade-out***".Gradient(Color.Black, Color.White).Colorize(null, Color.White));
		WL("***fade-in!***".Gradient(Color.White, Color.Black).Colorize(null, Color.White));
	}

	private static void ShowGColors()
	{
		Color c1 = Color.FromArgb(0, 0, 0);
		Color c2 = Color.FromArgb(0, 255, 0);
		ShowGColors("1", c1, c2);
		WL();
		ShowGColors("12", c1, c2);
		WL();
		ShowGColors("123", c1, c2);
		WL();
		ShowGColors("1234", c1, c2);
		WL();
		ShowGColors("12345", c1, c2);
		WL();
		ShowGColors("123456", c1, c2);
		WL();
		ShowGColors(new string('X', 56), c1, c2);
	}

	private static void ShowGColors(string Text, Color c1, Color c2)
	{
		W(Text.Gradient(c1, c2));
		W(" ");
		WL(Text.Gradient(c1, c2).Colorize(null, Color.White));
		foreach (Color c in ColorUtilities.GetGradientColors(c1, c2, Text.Length))
		{ WL($"    {"  ".Colorize(null, c)} R:{c.R} G:{c.G} B:{c.B}"); }
	}

	private static void ShowValuesW()
	{
		Color clr = Color.Red;
		Color clrB = Color.Silver;
		string value = "bar!";

		W(EscapeCodes.Underline);
		WL($"Showing values using the [{nameof(W)}] & [{nameof(WL)}] methods.", Color.Lime);
		W(EscapeCodes.UnderlineReset);
		WL(value);

		//no delimited values
		WL(" 1 foo", clr); //no value (whole message will show in color)
		WL($" 2 foo {value}", clr); //value with no delimiters (whole message will show in color)
		WL($" 3 foo [{value}", clr); //value with 1 delimiter (whole message will show in color)
		WL($" 4 foo {value}]", clr); //value with 1 delimiter (whole message will show in color)

		//hardcoded value
		WL(" 5 foo [bar]", clr); //no delimiters
		WL(" 6 foo [bar]", clr, null, true); //show delimiters with default color
		WL(" 7 foo [bar]", clr, clrB, true); //show delimiters with default color
		WL(" 8 foo [bar]", clr, null, Color.Lime); //show delimiters with other colors
		WL(" 9 foo [bar]", clr, null, Color.White, Color.Blue); //show delimiters with other colors
		WL("10 foo [bar]", clr, clrB, Color.White, Color.Blue); //show delimiters with other colors
		WL("11 foo [bar] foo [bat]", clr, clrB, Color.White, Color.Blue); //show delimiters with other colors

		//variable value
		WL($"12 foo [{value}]", clr); //no delimiters
		WL($"13 foo [{value}]", clr, null, true); //show delimiters with default color
		WL($"14 foo [{value}]", clr, clrB, true); //show delimiters with default color
		WL($"15 foo [{value}]", clr, null, Color.Lime); //show delimiters with other colors
		WL($"16 foo [{value}]", clr, null, Color.White, Color.Blue); //show delimiters with other colors
		WL($"17 foo [{value}]", clr, clrB, Color.White, Color.Blue); //show delimiters with other colors
	}

	private static void ShowValuesC()
	{
		Color clr = Color.Red;
		Color clrB = Color.White;
		string dL = "[";
		string dR = "]";
		string value = "bar!";

		WL($"Showing values using {"colorize".Colorize(Color.White, Color.BlueViolet)}");

		//no delimiters
		WL($" 1 foo {value.Colorize(clr)}");
		WL($" 2 foo {value.Colorize(clr, clrB)}");
		WL($" 3 foo {value.Colorize(null, clrB)}");

		//inline delimiters
		WL($" 4 foo [{value.Colorize(clr)}]");
		WL($" 5 foo [{value.Colorize(clr, clrB)}]");

		//delimiters passed to colorize
		WL($" 6 foo {value.Colorize(null, null, dL, dR)}"); //default colors
		WL($" 7 foo {value.Colorize(clr, null, dL, dR)}"); //default colors
		WL($" 8 foo {value.Colorize(clr, clrB, dL, dR)}"); //default colors
		WL($" 9 foo {value.Colorize(null, clrB, dL, dR)}"); //default colors

		WL($"10 foo {value.Colorize(null, null, dL, dR, Color.Lime)}"); //other colors
		WL($"11 foo {value.Colorize(clr, null, dL, dR, Color.Lime)}"); //other colors
		WL($"12 foo {value.Colorize(clr, clrB, dL, dR, Color.Lime)}");
		WL($"13 foo {value.Colorize(null, clrB, dL, dR, Color.Lime)}");

		WL($"14 foo {value.Colorize(null, null, dL, dR, Color.White, Color.Blue)}");
		WL($"15 foo {value.Colorize(clr, null, dL, dR, Color.White, Color.Blue)}");
		WL($"16 foo {value.Colorize(clr, clrB, dL, dR, Color.White, Color.Blue)}");
		WL($"17 foo {value.Colorize(null, clrB, dL, dR, Color.White, Color.Blue)}");

		WL($"18 foo {value.Colorize(null, null, dL, dR, null, Color.Blue)}");
		WL($"19 foo {value.Colorize(clr, null, dL, dR, null, Color.Blue)}");
		WL($"20 foo {value.Colorize(clr, clrB, dL, dR, null, Color.Blue)}");
		WL($"21 foo {value.Colorize(null, clrB, dL, dR, null, Color.Blue)}");


		//"nested" colors don't work consistently
		WL($"22 foo {value.Colorize(null, clrB, dL, dR, Color.BlueViolet, Color.Blue)} still green".Colorize(Color.LawnGreen));
		WL($"23 foo {value.Colorize(Color.BlueViolet, clrB, dL, dR, null, Color.Blue)} still green".Colorize(Color.LawnGreen));
		WL($"24 foo {value.Colorize(null, clrB, dL, dR, null, Color.Blue)} still green".Colorize(Color.LawnGreen));
	}

	private static void ShowValuesOther()
	{
		string value = "bar!";

		WL($"Showing values using {"reverse".Reverse()} and {"underline".Underline()}");

		WL($" 1 foo {value.Underline()} and more");
		WL($" 1 foo bar in color and underline".Colorize(Color.Blue).Underline());
		WL($" 2 foo {value.Reverse()} and more");
		WL($" 4 foo bar in color and reverse".Colorize(Color.Blue).Reverse());
		WL($" 5 foo {value.Reverse().Underline()} and more".Colorize(Color.White));
		WL($" 6 foo bar in color, reverse and underline".Colorize(Color.Blue, Color.White).Reverse().Underline());
		WL($" 7 foo bar in {"color".Colorize(Color.Green)}, underline".Underline());
		WL();
	}

	private static void TryStuff()
	{
		WL("\x1b[38;2;255;0;0mred\x1b[0m");
		WL("\x1b[38;2;0;255;0mgreen\x1b[0m");
		WL("\x1b[38;2;0;0;255mblue\x1b[0m");
		WL("\x1b[38;2;255;192;203mpink\x1b[0m");
		WL("\x1b[38;2;255;0;0mred\x1b[39m default color");
		WL("\x1b[38;2;0;255;0mgreen\x1b[38;2;255;0;0mred\x1b[39m default color\x1b[0m");
		WL();

		WL("\x1b[1mbold\x1b[0m");
		WL("\x1b[2mfaint\x1b[0m");
		WL("\x1b[3mitalic or inverse\x1b[0m");
		WL("\x1b[4munderline\x1b[24m and now off");
		WL("\x1b[5mslow blink\x1b[0m");
		WL("\x1b[6mrapid blink\x1b[0m");
		WL("\x1b[7mreverse\x1b[0m");
		WL("\x1b[9mx-out\x1b[0m");
		WL("\x1b[11mAlt font 1 (ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789)\x1b[0m");
		WL("\x1b[12mAlt font 2 (ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789)\x1b[0m");
		WL("\x1b[13mAlt font 3 (ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789)\x1b[0m");
		WL("\x1b[14mAlt font 4 (ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789)\x1b[0m");
		WL("\x1b[15mAlt font 5 (ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789)\x1b[0m");
		WL("\x1b[16mAlt font 6 (ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789)\x1b[0m");
		WL("\x1b[17mAlt font 7 (ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789)\x1b[0m");
		WL("\x1b[18mAlt font 8 (ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789)\x1b[0m");
		WL("\x1b[19mAlt font 9 (ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789)\x1b[0m");

		WL($"foo bar {"foo bar (in pink)".Colorize(Color.Pink)}");
		WL("Hot Pink".Colorize(Color.HotPink, Color.White));

		WL("123".Gradient(Color.Red, Color.Blue));
	}

	private static void Boxes()
	{
		//https://en.wikipedia.org/wiki/Code_page_437
		//WL("╡╢║│─┐└─┘┌┌╔═╗╚╝█▄▌▐▀■");
		WL("╔═╗");
		WL("║X║");
		WL("╠═╣");
		WL("║W║");
		WL("╚═╝");
		WL("┌─┐");
		WL("│X│");
		WL("├─┤");
		WL("│W│");
		WL("└─┘");
		WL("▄▄▄");
		WL("▌X▐");
		WL("███");
		WL("▌W▐");
		WL("▀▀▀");
	}

	private static void RkTest()
	{
		ConsoleKeyInfo cki;
		// Prevent example from ending if CTL+C is pressed.
		Console.TreatControlCAsInput = true;

		W("Press any combination of CTL, ALT, and SHIFT, and a console key.");
		W("Press the Escape (Esc) key to quit: \n");
		do
		{
			cki = RK();
			W(" --- You pressed ");
			if ((cki.Modifiers & ConsoleModifiers.Alt) != 0) W("ALT+");
			if ((cki.Modifiers & ConsoleModifiers.Shift) != 0) W("SHIFT+");
			if ((cki.Modifiers & ConsoleModifiers.Control) != 0) W("CTL+");
			WL(cki.Key.ToString());
		} while (cki.Key != ConsoleKey.Escape);
	}

	private static void RlTest()
	{
		string? input;

		do
		{
			input = RL("Enter something and press <enter> to submit (press only <enter> to exit)\n  ");
			WL($"  You entered '[{input}]'", Color.Red);
		} while (input != null && !string.IsNullOrEmpty(input));
	}
}
