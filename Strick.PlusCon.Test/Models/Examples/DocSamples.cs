using System.Drawing;

using Strick.PlusCon.Models;

using static Strick.PlusCon.Helpers;


namespace Strick.PlusCon.Test.Models.Examples;


internal static class DocSamples
{
	static DocSamples()
	{
		Samples = new List<DocSample>();
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
