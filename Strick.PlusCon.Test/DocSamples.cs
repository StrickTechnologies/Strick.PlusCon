using System.Drawing;
using System.Security.AccessControl;

using Strick.PlusCon.Models;

using static Strick.PlusCon.Helpers;


namespace Strick.PlusCon.Test;


internal static class DocSamples
{
	static DocSamples()
	{
		Samples = new List<DocSample>()
		{
			new DocSample("wwl1", "Example - W/WL (1)", Ex_wwl_1),
			new DocSample("wwl2", "Example - W/WL (2)", Ex_wwl_2),
			new DocSample("wwl3", "Example - W/WL (3)", Ex_wwl_3),
			new DocSample("wwl4", "Example - W/WL (4)", Ex_wwl_4),
			new DocSample("cls1", "Example - CLS (1)", Ex_cls_1),
			new DocSample("cls2", "Example - CLS (2)", Ex_cls_2),
			new DocSample("colorize1", "Example - Colorize (1)", Ex_colorize_1),
			new DocSample("colorize2", "Example - Colorize (2)", Ex_colorize_2),
			new DocSample("underline1", "Example - Underline", Ex_underline_1),
			new DocSample("reverse1", "Example - Reverse", Ex_reverse_1),
			new DocSample("gradient1", "Example - Gradient (1)", Ex_gradient_1),
			new DocSample("gradient2", "Example - Gradient (2)", Ex_gradient_2),
			new DocSample("gradient3", "Example - Gradient (3)", Ex_gradient_3),
			new DocSample("combo1", "Example - Combinations", Ex_combo_1),
			new DocSample("notes1", "Example - Other Notes", Ex_notes_1),
			new DocSample("textstyle1", "Example - TextStyle", Ex_TextStyle_1),
			new DocSample("styledtext1", "Example - StyledText", Ex_StyledText_1),
			new DocSample("menu1", "Example - Menu (1)", Ex_Menu_1),
			new DocSample("menu2", "Example - Menu (2)", Ex_Menu_2),
		};
	}


	internal static List<DocSample> Samples;

	private static readonly Color ErrColor = Color.Red;

	private static readonly Size SampleSize = new Size(43, 10);

	public static void Show(bool wait = false) => Show((Size?)null, wait);

	public static void Show(Size? conSz = null, bool wait = false)
	{
		Size? sz = null;
		if (conSz.HasValue)
		{
			sz = GetConsoleSize();
			SetConsoleSize(conSz.Value);
		}

		foreach (DocSample sample in Samples)
		{
			sample.Show(wait, wait);
		}

		if (sz != null)
		{ SetConsoleSize(sz.Value); }
	}

	public static void Show(string sampleName, bool wait = false)
	{
		if (string.IsNullOrWhiteSpace(sampleName))
		{
			WL("Come on, dude! Give me something to work with here. I need the name of a doc sample.", ErrColor);
			return;
		}

		var ex = Samples.Where(s => s.Name.Equals(sampleName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
		if (ex != null)
		{
			var sz = GetConsoleSize();
			SetConsoleSize(SampleSize);
			ex.Show(wait, wait);
			SetConsoleSize(sz);
		}
		else
		{ WL($"The doc sample [{sampleName}] was not found.", ErrColor, null, ErrColor); }
	}

	private static void SetConsoleSize(Size conSz)
	{
		Console.SetWindowSize(conSz.Width, conSz.Height);
		Console.SetBufferSize(conSz.Width, conSz.Height);
	}

	private static Size GetConsoleSize() => new Size(Console.WindowWidth, Console.WindowHeight);


	#region EXAMPLE FUNCTIONS

	private static void Ex_wwl_1()
	{
		//Example ** W/WL 1
		WL("Hello World!", Color.Red);
		WL("Hello World!", Color.Red, Color.White);
	}

	private static void Ex_wwl_2()
	{
		//Example ** W/WL 2
		WL("Hello [World]!", Color.Red);
		WL("Hello [World]!", Color.Red, Color.White);

	}

	private static void Ex_wwl_3()
	{
		//Example ** W/WL 3
		WL("Hello [World]!", Color.Red, null, true);
		WL("Hello [World]!", Color.Red, Color.White, true);
	}

	private static void Ex_wwl_4()
	{
		//Example ** W/WL 4
		WL("Hello [World]!", Color.Red, null, Color.Red);
		WL("Hello [World]!", Color.Red, Color.White, Color.Blue, Color.White);
	}

	private static void Ex_cls_1()
	{
		CLS(Color.LimeGreen, Color.Blue);
		WL("Blue in Green");
	}

	private static void Ex_cls_2()
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

	private static void Ex_colorize_1()
	{
		//Example **COLORIZE 1
		string colorized = "foo".Colorize(Color.Red);
		WL(colorized);
		WL(colorized.Colorize(null, Color.White)); //add background
		WL("Hello World!".Colorize(Color.Red));
		WL("Hello World!".Colorize(Color.Red, Color.White));
	}

	private static void Ex_colorize_2()
	{
		//Example **COLORIZE 2
		string wrapped = "foo".Colorize(Color.Red, null, "**[", "]**");
		WL(wrapped);
		wrapped = "foo".Colorize(Color.Red, null, "**[", "]**", Color.Lime, Color.White);
		WL(wrapped);
		wrapped = "cruel".Colorize(Color.Red, null, "*", "*", Color.Lime);
		WL($"Hello {wrapped} World!");
	}


	private static void Ex_underline_1()
	{
		//Example **UNDERLINE
		string underlined = "foo".Underline();
		WL(underlined);
		WL("Hello World!".Underline());
	}

	private static void Ex_reverse_1()
	{
		//Example **REVERSE
		string reversed = "foo".Reverse();
		WL(reversed);
		WL("Hello World!".Reverse());
		WL(reversed, Color.LimeGreen, Color.White);
		WL(reversed, Color.White, Color.LimeGreen);
	}

	private static void Ex_gradient_1()
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

	private static void Ex_gradient_2()
	{
		//Example **GRADIENT 2
		var colors = ColorUtilities.GetGradientColors(Color.SkyBlue, Color.Orange, Console.WindowHeight).ToList();
		string spaces = new(' ', Console.WindowWidth);
		foreach (var color in colors)
		{ W(spaces, Color.White, color); }

		Console.SetCursorPosition(0, 0);
		W("Sunrise", Color.White, colors[0]);
	}

	private static void Ex_gradient_3()
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

	private static void Ex_combo_1()
	{
		//Example **COMBINATIONS
		WL($"Hello World!".Underline(), Color.Red);
		WL($"Hello {"cruel".Underline()} World!", Color.Red);
		WL("***fade-out***".Gradient(Color.Black, Color.White).Colorize(null, Color.White));
		WL("***fade-in!***".Gradient(Color.White, Color.Black).Colorize(null, Color.White));
		WL("-- ** down on the beach ** --".Gradient(Color.SandyBrown, Color.FromArgb(3, 240, 165), Color.FromArgb(145, 193, 255)).Reverse());
		WL("-- ** down on the beach ** --".Gradient(Color.SandyBrown, Color.FromArgb(3, 240, 165), Color.FromArgb(145, 193, 255)).Underline());
	}

	private static void Ex_notes_1()
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


	private static void Ex_TextStyle_1()
	{
		TextStyle ts = new(Color.White, Color.DodgerBlue, Color.White);
		//same style, different content
		foreach (string s in new[] { "content 1", "level 2" })
		{ WL(ts.StyleText(s)); }

		ts.ForeColor = Color.Red;
		ts.Underline = true;
		ts.ClearGradient();
		WL(ts.StyleText("Hello World!"));

		ts.Underline = false;
		ts.ForeColor = null;
		ts.BackColor = Color.White;
		ts.SetGradientColors(Color.Black, Color.White);
		WL(ts.StyleText("***fade-out***"));

		ts.SetGradientColors(Color.White, Color.Black);
		WL(ts.StyleText("***fade-in!***"));

		ts.BackColor = null;
		ts.Reverse = true;
		ts.SetGradientColors(Color.SandyBrown, Color.FromArgb(3, 240, 165), Color.FromArgb(145, 193, 255));
		WL(ts.StyleText("-- ** down on the beach ** --"));

		ts.Reverse = false;
		ts.Underline = true;
		W(ts.StyleText("-- ** down on the beach ** --"));
	}

	private static void Ex_StyledText_1()
	{
		StyledText st = new("Hello World!", new(Color.Blue));

		//same content, different styling
		foreach (Color c in ColorUtilities.GetGradientColors(Color.FromArgb(0, 255, 0), Color.FromArgb(0, 128, 0), 4))
		{
			st.Style.BackColor = c;
			WL(st.TextStyled);
		}
		//same styling, alternate content
		WL(st.StyleText("Blue in Green"));

		//back to default
		st.Style = new();
		st.Text = "Default styling";
		WL(st.TextStyled);

		//different content, different styling
		st.Style.BackColor = Color.DarkGray;
		st.Style.ForeColor = Color.White;
		st.Text = "(not) " + st.Text;
		W(st.TextStyled);
	}


	private static void Ex_Menu_1()
	{
		Menu subMenu = new("Example Submenu");
		//lambda
		subMenu.Add(new MenuOption("Option 1", '1', () =>
		{
			CLS();
			WL("This is Example Submenu Option 1", Color.Red);
			RK("press a key to return to the menu...");
		}));

		//lambda
		subMenu.Add(new MenuOption("Option 2", '2', () =>
		{
			CLS();
			WL("This is Example Submenu Option 2", Color.LimeGreen);
			RK("press a key to return to the menu...");
		}));

		subMenu.Add(new MenuSeperator("-"));
		subMenu.Add(new MenuBackOption("Return to Example Menu", 'X'));


		Menu myMenu = new("Example Menu");
		myMenu.Add(new MenuOption("Option 1", '1', ExampleMenuOption1));
		myMenu.Add(new MenuOption("Option 2", '2', ExampleMenuOption2));
		myMenu.Add(new MenuOption("Submenu", 'S', subMenu));

		myMenu.Show();
	}

	private static void Ex_Menu_2()
	{
		Menu subMenu = new("Example Submenu", "-");
		subMenu.Title!.Style.SetGradientColors(Color.Silver, Color.SlateGray, Color.Silver);
		subMenu.Title!.Style.BackColor = Color.White;
		subMenu.Title!.Style.Reverse = true;
		subMenu.Subtitle!.Style = subMenu.Title.Style;
		subMenu.OptionsStyle = new(Color.DodgerBlue);
		subMenu.Prompt!.Style.ForeColor = Color.White;

		//lambda
		subMenu.Add(new MenuOption("Option 1", '1', () =>
		{
			CLS();
			WL("This is Example Submenu Option 1", Color.Red);
			RK("press a key to return to the menu...");
		}));

		//lambda
		subMenu.Add(new MenuOption("Option 2", '2', () =>
		{
			CLS();
			WL("This is Example Submenu Option 2", Color.LimeGreen);
			RK("press a key to return to the menu...");
		}));

		subMenu.Add(new MenuSeperator("-"));
		subMenu.Add(new MenuBackOption("Return to Example Menu", 'X'));
		subMenu.Options[subMenu.Options.Count - 1].Style = new(Color.Silver);


		Menu myMenu = new("Example Menu", " ");
		myMenu.Title!.Style.ForeColor = Color.LimeGreen;
		myMenu.OptionsStyle = new(Color.BlueViolet);
		myMenu.Add(new MenuOption("Option 1", '1', ExampleMenuOption1));
		myMenu.Add(new MenuOption("Option 2", '2', ExampleMenuOption2));
		myMenu.Add(new MenuSeperator(""));
		myMenu.Add(new MenuOption("Submenu", 'S', subMenu));
		myMenu.Options[myMenu.Options.Count - 1].Style = new(Color.White);
		myMenu.Add(new MenuSeperator(""));

		myMenu.Show();
	}

	private static void ExampleMenuOption1()
	{
		CLS();
		WL("This is Example Menu Option 1", Color.Red);
		RK("press a key to return to the menu...");
	}

	private static void ExampleMenuOption2()
	{
		CLS();
		WL("This is Example Menu Option 2", Color.LimeGreen);
		RK("press a key to return to the menu...");
	}

	#endregion EXAMPLE FUNCTIONS


	internal class DocSample
	{
		public DocSample(string name, string header, Action action) : this(name, header, null, action) { }

		public DocSample(string name, string header, TextStyle? headerStyle, Action action)
		{
			Name = name;
			Action = action;

			if (headerStyle == null)
			{ headerStyle = new TextStyle(docHdFore, docHdBack); }
			Header = new StyledText(header, headerStyle);
		}


		private static readonly Color docHdFore = Color.White;
		private static readonly Color docHdBack = Color.Blue;


		public string Name { get; }

		public StyledText Header { get; }

		public Action Action { get; }


		public void Show(bool wait = false, bool clear = false)
		{
			if (clear)
			{
				WL(EscapeCodes.ResetAll);
				CLS();
			}

			Action();

			ShowHd();

			if (wait)
			{ RK(); }
		}

		private void ShowHd()
		{
			Console.SetCursorPosition(Console.WindowWidth - Header.Text.Length, 0);
			W(Header.TextStyled);
		}
	}
}
