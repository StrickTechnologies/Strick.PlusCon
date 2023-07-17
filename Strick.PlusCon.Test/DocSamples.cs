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
			new DocSample("esc1", "Example - Escape Sequences (1)", Ex_EscapeSeq_1),

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
			new DocSample("colorUtil1", "Example - Color Utilities (1)", Ex_ColorUtil_1),

			new DocSample("formatUtil1", "Example - Format Utilities (1)", Ex_FormatUtil_1),

			new DocSample("textstyle1", "Example - TextStyle", Ex_TextStyle_1),
			new DocSample("styledtext1", "Example - StyledText", Ex_StyledText_1),

			new DocSample("menu1", "Example - Menu (1)", Ex_Menu_1),
			new DocSample("menu2", "Example - Menu (2)", Ex_Menu_2),

			new DocSample("grid1", "Example - Grid (1)", Ex_Grid_1),
			new DocSample("grid2", "Example - Grid (2)", Ex_Grid_2),
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


	private static void Ex_Grid_1()
	{
		Grid g = new();
		g.Title = new("Grid".SpaceOut());
		g.Subtitle = new("Example 1");

		g.Columns.Add("C 1");
		g.Columns.Add("Column 2", HorizontalAlignment.Center);
		g.Columns.Add("Col 3", HorizontalAlignment.Right);

		g.AddRow("r1-c1", "row1-c2", "row1-column3");
		g.AddRow("row2-column1", "row2-col2", "r2-col3");
		g.AddRow("r3-col1", "row3-column2", "r3-c3");

		g.Footer = new($"Total Count {g.RowCount}");
		g.Show();
		RK();
	}

	private static void Ex_Grid_2()
	{
		Grid g = new();
		g.Title = new("Grid".SpaceOut(), new(Color.Silver, Color.Gray, Color.Silver) { BackColor = Color.LimeGreen, Reverse = true });
		g.Subtitle = new("Example 2 (Styling)", new(Color.LimeGreen, Color.Gray));

		//set cell/content styling for entire grid
		g.CellStyle = new(Color.DodgerBlue, Color.Silver);
		g.CellContentStyle = new(Color.Blue, Color.Silver);
		g.ColumnHeaderContentStyle.Underline = false;
		g.ColumnHeaderCellStyle.Underline = false;

		GridColumn col = g.Columns.Add("C 1");
		col.Header.HorizontalAlignment = HorizontalAlignment.Center; //override column header alignment

		col = g.Columns.Add("Column 2", HorizontalAlignment.Center);
		//override styling for column
		col.CellStyle = new() { BackColor = Color.DarkGray };

		g.Columns.Add("Col 3", HorizontalAlignment.Right);

		g.AddRow("r1-c1", "row1-c2", "row1-column3");
		GridRow row = g.AddRow("row2-column1", "row2-column2", "r2-col3");
		//override styling for row
		row.CellStyle = new(Color.White, Color.SlateGray);
		row.ContentStyle = new(Color.White, Color.SlateGray);
		//override styling for specific cell
		GridCell cell = row.Cells[2];
		cell.ContentStyle = new(Color.Red, Color.White);
		cell.CellStyle = new(Color.White, Color.SlateGray);

		g.AddRow("r3-col1", "row3-col2", "r3-c3");

		g.Footer = new("Total Count 3");
		g.Show();
		RK();
	}


	private static void Ex_EscapeSeq_1()
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


	private static void Ex_ColorUtil_1()
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
		} while ((d.R > 0 || d.G > 0 || d.B > 0) || (b.R < 255 || b.G < 255 || b.B < 255));
	}
	private static string ShowColor(Color color)
	{
		string text;
		text = $"R:{color.R:D3} G:{color.G:D3} B:{color.B:D3}";

		return text.Colorize(null, color);
	}

	private static void Ex_FormatUtil_1()
	{
		string nl = "----*----1";
		WL();
		WL(nl);
		WL("A".Center(nl.Length));
		WL("AB".Center(nl.Length));
		WL("ABC".Center(nl.Length, '-'));
		WL("ABCD".Center(nl.Length));
		WL("ABCDEFGHIJ".Center(nl.Length));
		WL("ABCDEFGHIJ-Longer".Center(nl.Length));

		WL("Spaced out".SpaceOut());
		WL("dashed".Intersperse('-'));
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
