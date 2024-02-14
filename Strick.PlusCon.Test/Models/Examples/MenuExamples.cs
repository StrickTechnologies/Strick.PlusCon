using System.Drawing;

using Strick.PlusCon.Models;

using static Strick.PlusCon.Helpers;


namespace Strick.PlusCon.Test.Models.Examples;


internal static class MenuExamples
{
	static MenuExamples()
	{
		Samples = new List<DocSample>()
		{
			new DocSample("menu1", "Example - Menu (1)", Ex_Menu_1),
			new DocSample("menu2", "Example - Menu (2)", Ex_Menu_2),
			new DocSample("menu3", "Example - Menu Events (3)", Ex_Menu_3),
			new DocSample("menu4", "Example - Menu Multi-column (4)", Ex_Menu_4),
		};

	}

	internal static List<DocSample> Samples;

	internal static string MenuTitle => "Menus";


	internal static void Ex_Menu_1()
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

	internal static void Ex_Menu_2()
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


	internal static void Ex_Menu_3()
	{
		Menu myMenu = new("Example Menu - Events", " ");
		myMenu.Add(new MenuOption("Option 2", '2', ExampleMenuOption2));
		myMenu.Add(new MenuSeperator(""));
		//BeforeShow event for menu option
		myMenu.Options.Last().BeforeShow += MenuCount_BeforeShow;

		myMenu.ExitKeys.Remove(' '); //space
		myMenu.Prompt!.Text = $"{myMenu.Prompt!.Text.Trim()}, or space to refresh ";

		//BeforeShow event for menu
		myMenu.BeforeShow += Menu_BeforeShow;

		myMenu.Show();
	}

	//Event handler for menu
	private static void Menu_BeforeShow(object? sender, EventArgs e)
	{
		if (sender is null) return;

		Menu m = (Menu)sender;
		var st = m.Subtitle;
		if (st == null)
		{
			st = new(" ");
			m.Subtitle = st;
		}
		var t = DateTime.Now;
		st.Text = $"Last refreshed {t.ToString("G")}";
		if (t.Second > 29)
		{ st.Style.ForeColor = Color.Red; }
		else
		{ st.Style.ForeColor = Color.Lime; }

		//you can dynamically manipulate the options
		if (m.Options.Count > 2)
		{ m.Options.RemoveAt(0); }
		else
		{ m.Options.Insert(0, new MenuOption("Option 1", '1', ExampleMenuOption1)); }
	}

	//Event handler for menu option
	private static void MenuCount_BeforeShow(object? sender, EventArgs e)
	{
		if (sender is null) return;

		MenuOption opt = (MenuOption)sender;

		int count = 1;
		if (!string.IsNullOrWhiteSpace(opt.Caption))
		{
			count = int.Parse(opt.Caption.Split(' ')[1]) + 1;
		}
		opt.Caption = $"(refreshed {count} times)";
	}

	internal static void Ex_Menu_4()
	{
		TextStyle CatStyle = new TextStyle(Color.White, Color.Blue) { Underline = true };
		Menu menu = new Menu("Example Menu - Multi-column", "-");
		menu.OptionsStyle = new TextStyle(Color.White, Color.Gray);
		menu.ColumnCount = 3;
		menu.GutterWidth = 4;

		menu.Add(new MenuSeperator("Category A"));
		menu.Options[^1].Style = CatStyle;
		menu.Add(new MenuSeperator("Category B"));
		menu.Options[^1].Style = CatStyle;
		menu.Add(new MenuSeperator("Category C"));
		menu.Options[^1].Style = CatStyle;

		menu.Add(new MenuOption("Option 1", '1', ExampleMenuOption1));
		menu.Add(new MenuOption("Option 3", '3', ExampleMenuOption1));
		menu.Add(new MenuOption("Option 6", '6', ExampleMenuOption1));

		menu.Add(new MenuOption("Option 2", '2', ExampleMenuOption1));
		menu.Add(new MenuOption("Option 4", '4', ExampleMenuOption1));
		menu.Add(new MenuSeperator(""));

		menu.Add(new MenuSeperator(""));
		menu.Add(new MenuOption("Option 5", '5', ExampleMenuOption1));
		menu.Add(new MenuSeperator(""));

		menu.Show();
	}
}
