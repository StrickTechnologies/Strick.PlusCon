using System.Drawing;

using Strick.PlusCon.Models;

using static Strick.PlusCon.Helpers;


namespace Strick.PlusCon.Test.Models.Examples;


internal static class Basics
{
	static Basics()
	{
		Samples = new List<DocSample>()
		{
			new DocSample("qs1", "Example - Quick Start (1)", Ex_qs_1),

			new DocSample("esc1", "Example - Escape Sequences (1)", Ex_EscapeSeq_1),

			new DocSample("wwl1", "Example - W/WL (1)", Ex_wwl_1),
			new DocSample("wwl2", "Example - W/WL (2)", Ex_wwl_2),
			new DocSample("wwl3", "Example - W/WL (3)", Ex_wwl_3),
			new DocSample("wwl4", "Example - W/WL (4)", Ex_wwl_4),
			new DocSample("cls1", "Example - CLS (1)", Ex_cls_1),
			new DocSample("cls2", "Example - CLS (2)", Ex_cls_2),

			new DocSample("underline1", "Example - Underline", Ex_underline_1),
			new DocSample("reverse1", "Example - Reverse", Ex_reverse_1),

			new DocSample("combo1", "Example - Combinations", Ex_combo_1),
			new DocSample("notes1", "Example - Other Notes", Ex_notes_1),

			new DocSample("formatUtil1", "Example - Format Utilities (1)", Ex_FormatUtil_1)
		};
	}

	internal static List<DocSample> Samples;

	internal static string MenuTitle => "Basics";


	/// <summary>
	/// quick start example 1
	/// </summary>
	internal static void Ex_qs_1()
	{
		//bring WL, RK shortcuts into scope
		//using static Strick.PlusCon.Helpers;
		//...

		WL("Blue In Green", Color.Blue, Color.LimeGreen);

		int currPg = 49;
		int lastPg = 237;
		WL($"Page [{currPg}] of [{lastPg}]", Color.Lime);

		string chillin = " Down On The Beach ".SpaceOut();
		Color sand = Color.SandyBrown;
		Color surf = Color.FromArgb(3, 240, 165);
		Color sky = Color.FromArgb(145, 193, 255);
		TextStyle onTheBeach = new(sand, surf, sky);
		onTheBeach.Reverse = true;
		WL(onTheBeach.StyleText(chillin));
		WL(chillin.Gradient(sand, surf, sky).Reverse());

		Grid g = new();
		g.Columns.Add("Qty", HorizontalAlignment.Right);
		g.Columns.Add("Product");
		g.Columns.Add("Price", HorizontalAlignment.Right);

		g.AddRow(3, "Small Widget", 1.25M);
		g.AddRow(1, "Medium Widget", 2.33M);
		g.AddRow(2, "Large Widget", 3.49M);

		g.Show();
		RK("Press any key ");
	}


	internal static void Ex_EscapeSeq_1()
	{
		Color hot = Color.Red;
		Color grn = Color.LimeGreen;
		string clrES = EscapeCodes.Color;
		string ESHotFore = clrES.Replace("{cs}", ColorSpace.fore.ToString("D"))
			.Replace("{r}", hot.R.ToString())
			.Replace("{g}", hot.G.ToString())
			.Replace("{b}", hot.B.ToString());
		string ESCoolFore = EscapeCodes.GetForeColorSequence(Color.DodgerBlue);
		WL($"{ESHotFore}Red Hot\r\n{ESCoolFore}Cool To The Touch{EscapeCodes.ColorReset_Fore}\r\nBoring");

		string ESGBack = clrES.Replace("{cs}", ColorSpace.back.ToString("D"))
			.Replace("{r}", grn.R.ToString())
			.Replace("{g}", grn.G.ToString())
			.Replace("{b}", grn.B.ToString());
		ESCoolFore = EscapeCodes.GetForeColorSequence(Color.Blue);
		string ESBlueBack = EscapeCodes.GetBackColorSequence(Color.Blue);
		WL($"{ESCoolFore}{ESGBack}Blue In Green{EscapeCodes.ColorReset_Back}{EscapeCodes.ColorReset_Fore}");

		WL($"{EscapeCodes.Reverse}Reverse{EscapeCodes.ReverseReset}\r\n{EscapeCodes.Underline}Underline{EscapeCodes.UnderlineReset}");

		WL($"{EscapeCodes.Underline}{ESHotFore}{ESBlueBack}  Combo  {EscapeCodes.ColorReset_Back}{EscapeCodes.ColorReset_Fore}{EscapeCodes.UnderlineReset}");
	}


	internal static void Ex_wwl_1()
	{
		//Example ** W/WL 1
		WL("Hello World!", Color.Red);
		WL("Hello World!", Color.Red, Color.White);
	}

	internal static void Ex_wwl_2()
	{
		//Example ** W/WL 2
		WL("Hello [World]!", Color.Red);
		WL("Hello [World]!", Color.Red, Color.White);

	}

	internal static void Ex_wwl_3()
	{
		//Example ** W/WL 3
		WL("Hello [World]!", Color.Red, null, true);
		WL("Hello [World]!", Color.Red, Color.White, true);
	}

	internal static void Ex_wwl_4()
	{
		//Example ** W/WL 4
		WL("Hello [World]!", Color.Red, null, Color.Red);
		WL("Hello [World]!", Color.Red, Color.White, Color.Blue, Color.White);
	}


	internal static void Ex_cls_1()
	{
		CLS(Color.LimeGreen, Color.Blue);
		WL("Blue in Green");
	}

	internal static void Ex_cls_2()
	{
		CLS(Color.LimeGreen, Color.Blue);
		WL("Blue in Green");
		//colors reset by the line below
		WL("Kind Of Blue", Color.White, Color.Blue);
		//back to default console colors here
		WL("No longer blue");
		//set to desired colors again
		W(EscapeCodes.GetBackColorSequence(Color.LimeGreen) + EscapeCodes.GetForeColorSequence(Color.Blue));
		WL("Blue once more");
		WL("All Blues");
	}


	internal static void Ex_underline_1()
	{
		//Example **UNDERLINE
		string underlined = "foo".Underline();
		WL(underlined);
		WL("Hello World!".Underline());
	}

	internal static void Ex_reverse_1()
	{
		//Example **REVERSE
		string reversed = "foo".Reverse();
		WL(reversed);
		WL("Hello World!".Reverse());
		WL(reversed, Color.LimeGreen, Color.White);
		WL(reversed, Color.White, Color.LimeGreen);
	}


	internal static void Ex_combo_1()
	{
		//Example **COMBINATIONS
		WL($"Hello World!".Underline(), Color.Red);
		WL($"Hello {"cruel".Underline()} World!", Color.Red);
		WL("***fade-out***".Gradient(Color.Black, Color.White).Colorize(null, Color.White));
		WL("***fade-in!***".Gradient(Color.White, Color.Black).Colorize(null, Color.White));
		WL("-- ** down on the beach ** --".Gradient(Color.SandyBrown, Color.FromArgb(3, 240, 165), Color.FromArgb(145, 193, 255)).Reverse());
		WL("-- ** down on the beach ** --".Gradient(Color.SandyBrown, Color.FromArgb(3, 240, 165), Color.FromArgb(145, 193, 255)).Underline());
	}

	internal static void Ex_notes_1()
	{
		//Example **OTHER NOTES
		Console.ForegroundColor = ConsoleColor.Red;
		WL($"Hello {"cruel".Colorize(Color.Lime).Underline()} World!");
		Console.ResetColor();
		WL($"Hello {"cruel".Colorize(Color.Lime).Underline()} World!", Color.Red);
		WL($"Hello [cruel] World!".Colorize(Color.Red), Color.Lime);
		//You have to use something more like this...
		WL($"{"Hello".Colorize(Color.Red)} {"cruel".Colorize(Color.Lime).Underline()} {"World!".Colorize(Color.Red)}");
	}


	internal static void Ex_FormatUtil_1()
	{
		int len = 10;
		string ruler = Ruler.GetH(len);
		WL();
		WL(ruler);
		WL("A".Center(len));
		WL("AB".Center(len));
		WL("ABC".Center(len, '-'));
		WL("ABCD".Center(len));
		WL("ABCDEFGHIJ".Center(len));
		WL("ABCDEFGHIJ-Longer".Center(len));

		WL("Spaced out".SpaceOut());
		WL("dashed".Intersperse('-'));

		Console.SetCursorPosition(25, 1);
		W("Vertical".Vertical());
	}
}
