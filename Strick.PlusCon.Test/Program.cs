using System.Data.Common;
using System.Drawing;
using System.Text;

using Newtonsoft.Json.Linq;

using Strick.PlusCon.Models;

using static Strick.PlusCon.Helpers;


namespace Strick.PlusCon.Test;


internal class Program
{
	static void Main()
	{
		ConsoleUtilities.EnableVirtualTerminal();
		Console.Title = BannerText.Trim();

		//SetConsoleSize(43, 10);
		Menu();

		//Banner(); WL();
		//DocSamples.Show(new Size(43, 10), true);
		//DocSamples.Show("cls1", true);

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

	private static void Menu()
	{
		TextStyle menuTitleStyle = new TextStyle()
		{
			BackColor = Color.DarkSlateGray,
			Reverse = true
		};
		menuTitleStyle.SetGradientColors(Color.White, Color.Red, Color.White);

		Menu samplesMenu = new Menu(BannerText, "Doc Samples");
		samplesMenu.Title!.Style = menuTitleStyle;
		samplesMenu.Subtitle!.Style = menuTitleStyle;
		samplesMenu.Add(new MenuOption("Show All", 'a', () => { DocSamples.Show(new Size(43, 10), true); }));
		samplesMenu.Add(new MenuSeperator("-"));
		char key = 'b';
		foreach (var docSample in DocSamples.Samples)
		{
			samplesMenu.Add(new($"{docSample.Header.Text.Replace("Example - ", "")} ({docSample.Name})", key++, () => { DocSamples.Show(docSample.Name, true); }));
		}

		Menu testMenu = new(BannerText, "Test Menu");
		testMenu.Title!.Style = menuTitleStyle;
		testMenu.Subtitle!.Style = menuTitleStyle;
		testMenu.OptionsStyle = new TextStyle() { ForeColor = Color.White };
		testMenu.Add(new("show greens", 'G', ShowGreens));
		testMenu.Add(new MenuOption("color lighten/darken", 'c', BrightenDarkenColors));
		testMenu.Options[0].Style = new TextStyle() { ForeColor = Color.Lime };
		testMenu.Add(new("TextStyle tests", 'T', TextStyleTests));
		testMenu.Add(new MenuBackOption("back", 'X'));

		Menu gridMenu = new(BannerText, "Grid Menu");
		gridMenu.Title!.Style = menuTitleStyle;
		gridMenu.Subtitle!.Style = menuTitleStyle;
		gridMenu.Options.Add(new("Show test grid", 'G', GridTest));

		Menu mainMenu = new Menu(BannerText, "M a i n  M e n u");
		mainMenu.Title!.Style = menuTitleStyle;
		mainMenu.Subtitle!.Style = menuTitleStyle;
		mainMenu.Prompt!.Style.ForeColor = Color.White;
		mainMenu.OptionsStyle = mainMenu.Prompt.Style;

		mainMenu.Options.Add(new MenuSeperator("-"));
		mainMenu.Options.Add(new MenuOption("Show Doc Samples", 'S', samplesMenu));
		mainMenu.Options.Add(new MenuOption("Test Menu", 'T', testMenu));
		mainMenu.Options.Add(new MenuOption("Grid Menu", 'G', gridMenu));
		mainMenu.Options.Add(new MenuSeperator("-"));
		mainMenu.Show();
	}

	private static void ShowDocSamples()
	{
		DocSamples.Show(new Size(43, 10), true);
	}


	private static void Banner() => WL(BannerText.Gradient(Color.White, Color.Red, Color.White).Colorize(null, Color.DarkSlateGray).Reverse());
	private static string BannerText => "   S t r i c k . P l u s C o n   ";

	private static void SetConsoleSize(int width, int height)
	{
		Console.SetWindowSize(width, height);
		Console.SetBufferSize(width, height);
	}


	private static void TextStyleTests()
	{
		TextStyle style = new TextStyle();
		string text = "foobar";

		CLS();
		WL(style.StyleText(text));

		style.ForeColor = Color.White;
		WL(style.StyleText(text));

		style.BackColor = Color.DarkGray;
		WL(style.StyleText(text));

		style.Underline = true;
		WL(style.StyleText(text));

		style.Underline = false;
		WL(style.StyleText(text));

		style.Reverse = true;
		WL(style.StyleText(text));

		style.Reverse = false;
		WL(style.StyleText(text));

		style.SetGradientColors(Color.Turquoise, Color.BlueViolet);
		WL(style.StyleText(text));

		RK();
	}

	private static void ShowGreens()
	{
		var fc = Color.Blue;
		ShowColors(fc, Color.Green);
		ShowColors(fc, Color.GreenYellow);
		ShowColors(fc, Color.DarkGreen);
		ShowColors(fc, Color.DarkOliveGreen);
		ShowColors(fc, Color.DarkSeaGreen);
		ShowColors(fc, Color.ForestGreen);
		ShowColors(fc, Color.LawnGreen);
		ShowColors(fc, Color.LightGreen);
		ShowColors(fc, Color.LightSeaGreen);
		ShowColors(fc, Color.LimeGreen);
		ShowColors(fc, Color.Lime);
		ShowColors(fc, Color.MediumSeaGreen);
		ShowColors(fc, Color.MediumSpringGreen);
		ShowColors(fc, Color.PaleGreen);
		ShowColors(fc, Color.SeaGreen);
		ShowColors(fc, Color.SpringGreen);
		ShowColors(fc, Color.YellowGreen);
		RK();
	}

	private static void BrightenDarkenColors()
	{
		//Color b = Color.FromArgb(0, 0, 0);
		//Color d = Color.FromArgb(255, 255, 255);
		Color b = Color.FromArgb(255, 0, 0);
		Color d = Color.FromArgb(255, 0, 0);
		int adjustment = 20;

		WL();
		WL($"{ShowColor(b)}  {ShowColor(d)}");
		do
		{
			b = b.Brighten(adjustment);
			d = d.Darken(adjustment);
			WL($"{ShowColor(b)}  {ShowColor(d)}");
		} while ((d.R > 0 || d.G > 0 || d.B > 0) || (b.R < 255 || b.G < 255 || b.B < 255));
		RK();
	}

	private static string ShowColor(Color color)
	{
		string text;
		if (color.IsNamedColor)
		{ text = color.Name; }
		else
		{ text = $"Color R:{color.R:D3} G:{color.G:D3} B:{color.B:D3}"; }

		return text.Colorize(null, color);
	}

	private static void ShowColors(Color fore, Color back) => WL($"Foobar {fore.Name}/{back.Name}", fore, back);

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

	private static void GridTest()
	{
		Grid g = new();
		g.Title = new("A Sample Grid", new(Color.White, Color.Turquoise));
		g.Subtitle = new("this one has a really super long sub-title. It's wider than the whole grid.");
		g.CellStyle.BackColor = Color.FromArgb(48, 48, 48);
		g.CellContentStyle = g.CellStyle;
		//g.CellContentStyle.BackColor = Color.FromArgb(48, 48, 48);
		g.Columns.Add("C-1").Header.HorizontalAlignment = HorizontalAlignment.Right;
		g.Columns.Add("Column-2");
		var col= g.Columns.Add("C-3", HorizontalAlignment.Right);
		//col.Header.HorizontalAlignment = HorizontalAlignment.Center;
		//g.Columns[2].CellLayout.PaddingLeft = 1;
		//g.Columns[2].CellLayout.PaddingLeftChar = '[';
		//g.Columns[2].CellLayout.PaddingRight = 1;
		//g.Columns[2].CellLayout.PaddingRightChar = ']';

		//override style for COLUMN
		g.Columns[1].ContentStyle = new(Color.White, Color.BlueViolet);
		g.Columns[1].CellStyle = new(Color.White, Color.BlueViolet);
		//g.Columns[0].CellLayout.MarginLeft = 0;
		//g.Columns[0].CellLayout.PaddingLeft = 0;
		//g.Columns[0].CellLayout.PaddingRight = 0;
		//g.Columns[1].CellLayout.PaddingLeft = 1;
		//g.Columns[1].CellLayout.PaddingRight = 1;

		g.Rows.Add(new(g, "r1 - c1", "r1-c2", "r1-c3"));
		g.Rows.Add(new(g, "r2-c1", "r2-c2", "row2-column3"));
		g.Rows.Add(new(g, "r3-c1", "r3-c2", "r3-c3"));
		g.AddRow("r4-c1", "r4-c2");//, "r4-c3");
								   //g.AddRow("r5-c1", "r5-c2", "r5-c3", "r5-c4"); too many cells exception
		var r = g.AddRow();
		r.Cells[0].Content = "r5-c1";
		r.Cells[1].Content = "r5-c2";
		r.Cells[2].Content = "r5-c3";

		//override style for ROW
		g.Rows[1].ContentStyle = new() { BackColor = Color.HotPink };
		g.Rows[1].CellStyle = g.Rows[1].ContentStyle;

		//override style for CELL
		g.Rows[1].Cells[2].ContentStyle = new(g.Rows[1].ContentStyle!) { ForeColor = Color.Black };
		g.Rows[4].Cells[2].ContentStyle = new(Color.White, Color.Red, Color.White) { Reverse = true };

		//override layout for CELL
		//...only alignment supported at this time. padding/margins may be implemented later?
		g.Rows[4].Cells[2].HorizontalAlignment = HorizontalAlignment.Left;
		//g.Rows[2].Cells[1].Layout = new() { HorizontalAlignment = HorizontalAlignment.Right };
		//g.Rows[2].Cells[2].Layout = new() { HorizontalAlignment = HorizontalAlignment.Left };
		//g.Rows[3].Cells[0].Layout = new() { PaddingLeft = 2, PaddingRight = 2, PaddingLeftChar = '[', PaddingRightChar = ']' };
		//g.Rows[3].Cells[1].Layout = new() { MarginLeft = 2 };


		//g.Rows[1].Cells[1].ContentStyle = new(Color.Red, Color.White);
		//g.Rows[1].Cells[1].ContentStyle = new(g.Columns[1].ContentStyle);
		//g.Rows[1].Cells[1].ContentStyle.ForeColor = Color.Red;
		//g.Rows[1].Cells[1].Layout.PaddingLeft = 0;
		//g.Rows[1].Cells[1].Layout.PaddingRight = 0;
		//g.Rows[2].Cells[1].Layout = new();
		//g.Rows[2].Cells[1].Layout.HorizontalAlignment = HorizontalAlignment.Right;
		//g.Rows[2].Cells[1].Layout.PaddingLeft = 0;
		//g.Rows[2].Cells[1].Layout.PaddingRight = 0;

		g.Footer = new($"A total of {g.RowCount} fun items", new(Color.White, Color.Purple));

		//g.AddColumn("empty");

		CLS();
		W("     ");
		g.Show();
		RK();
	}
}
