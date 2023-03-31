using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static Strick.PlusCon.Helpers;


namespace Strick.PlusCon.Models;


public class Menu
{
	public Menu()
	{
		Options = new List<MenuOption>();
	}

	public Menu(string title) : this()
	{
		Title = title;
	}

	public Menu(string title, string subTitle) : this(title)
	{
		Subtitle = subTitle;
	}

	public Menu(IEnumerable<MenuOption> options) : this()
	{
		Options.AddRange(options);
	}

	public Menu(string title, IEnumerable<MenuOption> options) : this(options)
	{
		Title = title;
	}

	public Menu(string title, string subTitle, IEnumerable<MenuOption> options) : this(title, options)
	{
		Subtitle = subTitle;
	}


	public string? Title { get; set; }

	public string? Subtitle { get; set; }

	public string? Prompt { get; set; }


	public List<MenuOption> Options { get; set; }

	/// <summary>
	/// Any key included in this sequence will close the menu
	/// </summary>
	public List<ConsoleKey> ExitKeys { get; } = new(new[] { ConsoleKey.NumPad0, ConsoleKey.D0, ConsoleKey.Escape, ConsoleKey.Spacebar, ConsoleKey.Enter, ConsoleKey.Backspace });


	public void Show()
	{
		if (Options == null || !Options.Any())
		{ throw new InvalidOperationException("Cannot display a menu with no options"); }

		do
		{
			Console.Clear();

			if (!string.IsNullOrEmpty(Title))
			{ WL(Title); }

			if (!string.IsNullOrEmpty(Subtitle))
			{ WL(Subtitle); }

			foreach (MenuOption opt in Options)
			{ WL($"{opt.Key}. {opt.Caption}"); }

			var k = RK();

			//if (char.IsWhiteSpace(k.KeyChar) || k.KeyChar == '0' || k.Key== ConsoleKey.Escape)
			//if (k.Key.In(ExitKeys.ToArray()))
			if (ExitKeys.Contains(k.Key))
			{
				break;
			}

			var selOpt = GetOption(k);
			if (selOpt != null)
			{
				if (selOpt.Function != null)
				{ selOpt.Function(); }
				else if (selOpt.Submenu != null)
				{ selOpt.Submenu.Show(); }
				else
				{ break; }
			}

		} while (true);
	}

	private MenuOption? GetOption(ConsoleKeyInfo k)
	{ return Options.FirstOrDefault(o => o.Key == k.KeyChar); }
}
