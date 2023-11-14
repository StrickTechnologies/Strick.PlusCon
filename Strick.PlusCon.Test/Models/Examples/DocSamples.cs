using System.Drawing;

using Strick.PlusCon.Models;

using static Strick.PlusCon.Helpers;


namespace Strick.PlusCon.Test.Models.Examples;


internal static class DocSamples
{
	static DocSamples()
	{
		Samples = new List<DocSample>()
		{
			//new DocSample("qs1", "Example - Quick Start (1)", Basics.Ex_qs_1),
			//new DocSample("esc1", "Example - Escape Sequences (1)", Basics.Ex_EscapeSeq_1),
			//new DocSample("wwl1", "Example - W/WL (1)", Basics.Ex_wwl_1),
			//new DocSample("wwl2", "Example - W/WL (2)", Basics.Ex_wwl_2),
			//new DocSample("wwl3", "Example - W/WL (3)", Basics.Ex_wwl_3),
			//new DocSample("wwl4", "Example - W/WL (4)", Basics.Ex_wwl_4),
			//new DocSample("cls1", "Example - CLS (1)", Basics.Ex_cls_1),
			//new DocSample("cls2", "Example - CLS (2)", Basics.Ex_cls_2),

			//new DocSample("colorize1", "Example - Colorize (1)", ColorExamples.Ex_colorize_1),
			//new DocSample("colorize2", "Example - Colorize (2)", ColorExamples.Ex_colorize_2),
			//new DocSample("colorize3", "Example - Colorize (3)", ColorExamples.Ex_colorize_3),
			//new DocSample("underline1", "Example - Underline", Basics.Ex_underline_1),
			//new DocSample("reverse1", "Example - Reverse", Basics.Ex_reverse_1),
			//new DocSample("gradient1", "Example - Gradient (1)", Gradient.Ex_gradient_1),
			//new DocSample("gradient2", "Example - Gradient (2)", Gradient.Ex_gradient_2),
			//new DocSample("gradient3", "Example - Gradient (3)", Gradient.Ex_gradient_3),
			//new DocSample("combo1", "Example - Combinations", Basics.Ex_combo_1),
			//new DocSample("notes1", "Example - Other Notes", Basics.Ex_notes_1),
			//new DocSample("colorUtil1", "Example - Color Utilities (1)", ColorExamples.Ex_ColorUtil_1),

			//new DocSample("formatUtil1", "Example - Format Utilities (1)", Basics.Ex_FormatUtil_1),

			//new DocSample("textstyle1", "Example - TextStyle", TextStyleExamples.Ex_TextStyle_1),
			//new DocSample("styledtext1", "Example - StyledText", TextStyleExamples.Ex_StyledText_1),

			//new DocSample("menu1", "Example - Menu (1)", MenuExamples.Ex_Menu_1),
			//new DocSample("menu2", "Example - Menu (2)", MenuExamples.Ex_Menu_2),
			//new DocSample("menu3", "Example - Menu Events (3)", MenuExamples.Ex_Menu_3),

			//new DocSample("grid1", "Example - Grid (1)", GridExamples.Ex_Grid_1),
			//new DocSample("grid2", "Example - Grid (2)", GridExamples.Ex_Grid_2),

			//new DocSample("ruler1", "Example - Ruler (1)", RulerExamples.Ex_Ruler_1),
		};
	}


	internal static List<DocSample> Samples;

	private static readonly Color ErrColor = Color.Red;

	//note: font s/b Cascadia Mono SemiLight, 18pt
	private static readonly Size SampleSize = new Size(43, 10);

	public static void Show(bool wait = false) => Show((Size?)null, wait);

	public static void Show(Size? conSz = null, bool wait = false)
	{
		ConsoleSize? sz = null;
		if (conSz.HasValue)
		{
			sz = new ConsoleSize(conSz.Value);
		}

		foreach (DocSample sample in Samples)
		{
			sample.Show(wait, wait);
		}

		if (sz != null)
		{ sz.Restore(); }
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
			var sz = new ConsoleSize(SampleSize);
			CLS();
			ex.Show(wait, wait);
			sz.Restore();
		}
		else
		{ WL($"The doc sample [{sampleName}] was not found.", ErrColor, null, ErrColor); }
	}

	public static void Show(DocSample sample)
	{
		if (sample == null)
		{ throw new ArgumentNullException(nameof(sample), "Come on, dude! Give me something to work with here. I need a doc sample."); }

		ConsoleSize sz = new(SampleSize);

		//CLS();
		sample.Show(true, true);
		sz.Restore();
	}


	public static Menu Menu
	{
		get
		{
			Menu samplesMenu = new PCMenu("Doc Samples");
			samplesMenu.Add(new MenuOption(Basics.MenuTitle, 'B', () => { MakeMenu(Basics.Samples, Basics.MenuTitle).Show(); }));
			samplesMenu.Add(new MenuOption(ColorExamples.MenuTitle, 'C', () => { MakeMenu(ColorExamples.Samples, ColorExamples.MenuTitle).Show(); }));
			samplesMenu.Add(new MenuOption(GridExamples.MenuTitle, 'G', () => { MakeMenu(GridExamples.Samples, GridExamples.MenuTitle).Show(); }));
			samplesMenu.Add(new MenuOption(Gradient.MenuTitle, 'X', () => { MakeMenu(Gradient.Samples, Gradient.MenuTitle).Show(); }));
			samplesMenu.Add(new MenuOption(MenuExamples.MenuTitle, 'M', () => { MakeMenu(MenuExamples.Samples, MenuExamples.MenuTitle).Show(); }));
			samplesMenu.Add(new MenuOption(RulerExamples.MenuTitle, 'R', () => { MakeMenu(RulerExamples.Samples, RulerExamples.MenuTitle).Show(); }));
			samplesMenu.Add(new MenuOption(TextStyleExamples.MenuTitle, 'T', () => { MakeMenu(TextStyleExamples.Samples, TextStyleExamples.MenuTitle).Show(); }));
			samplesMenu.Add(new MenuSeperator("-"));
			return samplesMenu;
		}
	}

	public static Menu MakeMenu(IEnumerable<DocSample> samples, string title)
	{
		Menu m = new PCMenu("Doc Samples - " + title);
		char key = 'a';
		foreach (var docSample in samples)
		{
			m.Add(new($"{docSample.Header.Text.Replace("Example - ", "")} ({docSample.Name})", key++, () => { Show(docSample); }));
		}
		return m;
	}
}
