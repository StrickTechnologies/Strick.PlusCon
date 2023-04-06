using System;
using System.Collections.Generic;
using System.Linq;

using static Strick.PlusCon.Helpers;


namespace Strick.PlusCon.Models;


/// <summary>
/// Represents a menu of choices that is displayed and awaits user input. 
/// The choices are made up of a sequence of <see cref="MenuOption"/> (or derived) objects (see <see cref="Options"/>).
/// </summary>
public class Menu
{
	/// <summary>
	/// Creates a <see cref="Menu"/> instance with no titles and no options.
	/// </summary>
	public Menu()
	{
		Options = new List<MenuOption>();
	}

	/// <summary>
	/// Creates a <see cref="Menu"/> instance with no options, and the <see cref="Title"/> property set to <paramref name="title"/>.
	/// </summary>
	public Menu(string title) : this()
	{
		Title = new(title);
	}

	/// <summary>
	/// Creates a <see cref="Menu"/> instance with no options, the <see cref="Title"/> property set to <paramref name="title"/>, and the <see cref="Subtitle"/> property set to <paramref name="subTitle"/>.
	/// </summary>
	public Menu(string title, string subTitle) : this(title)
	{
		Subtitle = new(subTitle);
	}

	/// <summary>
	/// Creates a <see cref="Menu"/> instance with no titles and <see cref="Options"/> set to <paramref name="options"/>.
	/// </summary>
	public Menu(IEnumerable<MenuOption> options) : this()
	{
		Options.AddRange(options);
	}

	/// <summary>
	/// Creates a <see cref="Menu"/> instance with <see cref="Options"/> set to <paramref name="options"/>, 
	/// and the <see cref="Title"/> property set to <paramref name="title"/>.
	/// </summary>
	public Menu(string title, IEnumerable<MenuOption> options) : this(options)
	{
		Title = new(title);
	}

	/// <summary>
	/// Creates a <see cref="Menu"/> instance with <see cref="Options"/> set to <paramref name="options"/>, 
	/// the <see cref="Title"/> property set to <paramref name="title"/>, and the <see cref="Subtitle"/> property set to <paramref name="subTitle"/>.
	/// </summary>
	public Menu(string title, string subTitle, IEnumerable<MenuOption> options) : this(title, options)
	{
		Subtitle = new(subTitle);
	}


	/// <summary>
	/// The title of the menu. Centered at the top of the menu.
	/// If a single character string is specified (e.g. "-"), that character will be repeated for the width of the menu.
	/// </summary>
	public StyledText? Title { get; set; }

	/// <summary>
	/// The length of the <see cref="Title"/> property. If <see cref="Title"/> is null or empty, 0 is returned. 
	/// </summary>
	public int TitleLength => Title == null ? 0 : Title.Text.Length;


	/// <summary>
	/// The subtitle of the menu. Centered at the top of the menu, beneath the <see cref="Title"/>.
	/// If a single character string is specified (e.g. "-"), that character will be repeated for the width of the menu.
	/// </summary>
	public StyledText? Subtitle { get; set; }

	/// <summary>
	/// The length of the <see cref="Subtitle"/> property. If <see cref="Subtitle"/> is null or empty, 0 is returned. 
	/// </summary>
	public int SubTitleLength => Subtitle == null ? 0 : Subtitle.Text.Length;

	/// <summary>
	/// The prompt displayed beneath the menu options when awaiting user input
	/// </summary>
	public StyledText? Prompt { get; set; } = new StyledText("Select an option ");


	/// <summary>
	/// A collection of <see cref="MenuOption"/> objects. 
	/// These are the options that are displayed when the menu is rendered.
	/// <para>If multiple <see cref="MenuOption"/> objects in the <see cref="Options"/> collection contain the same value 
	/// in their <see cref="MenuOption.Keys"/> collection, only the first option will be executed when that key is pressed.</para>
	/// </summary>
	public List<MenuOption> Options { get; }

	/// <summary>
	/// Adds <paramref name="option"/> to the end of the menu's <see cref="Options"/> collection.
	/// </summary>
	/// <param name="option"></param>
	public void Add(MenuOption option) => Options.Add(option);

	/// <summary>
	/// The style to apply to all menu options. Set to null to show menu options with NO style applied.
	/// <para>Note: if any option has its <see cref="MenuOption.Style"/> property set, that style will override this one when the option is displayed.</para>
	/// </summary>
	public TextStyle? OptionsStyle { get; set; }


	/// <summary>
	/// The keys used to close the menu -- when the user presses any key included in this sequence, the menu will be exited.
	/// <para>Note: Any <see cref="MenuOption"/> in the <see cref="Options"/> collection having a value in its <see cref="MenuOption.Keys"/> 
	/// collection will override the same character in this collection.</para>
	/// </summary>
	public List<char> ExitKeys { get; } = new(new[] { '0', ' ', (char)ConsoleKey.Escape, (char)ConsoleKey.Enter, (char)ConsoleKey.Backspace });


	/// <summary>
	/// Show the menu and wait for user input (a key press). 
	/// On user input:
	/// <list type="bullet">
	/// <item>Looks at each option in the menu's <see cref="Options"/> collection. 
	/// If any value in any option's <see cref="MenuOption.Keys"/> collection matches the key pressed, that option is executed.
	/// <br />If the option is a <see cref="MenuBackOption"/>, the menu is exited.
	/// <br />If the option is a <see cref="MenuOption"/>, the method designated by the <see cref="MenuOption.Function"/> property is executed, 
	/// or the <see cref=" Menu"/> designated by the <see cref="MenuOption.Submenu"/> property is displayed.
	/// </item>
	/// <item>If no option's <see cref="MenuOption.Keys"/> collection matches the key pressed, the <see cref="Menu.ExitKeys"/> collection is checked. 
	/// If any value in the collection matches the key pressed, the menu is exited.</item>
	/// <item>If neither of the above two conditions is met, the menu is refreshed.</item>
	/// </list>
	/// <para>If <see cref="Show"/> is called and there are no elements in 
	/// the <see cref="Options"/> collection, an <see cref="InvalidOperationException"/> exception is thrown.</para>
	/// </summary>
	/// <exception cref="InvalidOperationException">Thrown if <see cref="Show"/> is called and there are no elements in the <see cref="Options"/> collection</exception>
	public void Show()
	{
		if (Options == null || !Options.Any())
		{ throw new InvalidOperationException("Cannot display a menu with no options. Add options to the menu before displaying..."); }

		var w = Width;
		do
		{
			CLS();

			//show titles
			ShowTitle(Title, w);
			ShowTitle(Subtitle, w);

			//show options
			foreach (MenuOption opt in Options)
			{ ShowOption(opt, w); }


			//user input
			//var k = RK(Prompt != null ? Prompt.TextStyled : null);
			var k = RK(Prompt?.TextStyled);


			//find selected option and execute it
			var selOpt = FindOption(k);
			if (selOpt != null)
			{
				if (selOpt.Function != null)
				{ selOpt.Function(); }
				else if (selOpt.Submenu != null)
				{ selOpt.Submenu.Show(); }
				else
				{ break; }
			}
			//key pressed not an option, check exit keys
			else if (ExitKeys.Contains(k.KeyChar))
			{
				break;
			}

		} while (true);
	}

	private void ShowOption(MenuOption opt, int width)
	{
		if (string.IsNullOrEmpty(opt.Caption))
		{ WL(opt.GetText(width)); }

		else if (opt.Style != null)
		{ WL(opt.Style.StyleText(opt.GetText(width))); }

		else if (OptionsStyle != null)
		{ WL(OptionsStyle.StyleText(opt.GetText(width))); }

		else
		{ WL(opt.GetText(width)); }
	}

	private static void ShowTitle(StyledText? title, int width)
	{
		if (title == null)
		{ return; }

		if (title.Text.Length == 1)
		{ WL(title.StyleText(new string(title.Text[0], width))); }
		else
		{ WL(title.StyleText(title.Text.Center(width))); }
	}

	private MenuOption? FindOption(ConsoleKeyInfo k)
	{ return Options.FirstOrDefault(o => o.Keys.Contains(k.KeyChar)); }


	/// <summary>
	/// Returns an int value which represents the maximum width of the menu when displayed. 
	/// Calculated based on the max width of <see cref="Title"/>, <see cref="Subtitle"/>, and all the <see cref="Options"/> (+3 for the Key, a period,  and space; e.g. "x. ").
	/// </summary>
	public int Width
	{
		get
		{
			var l = Options.Max(o => o.Caption.Length + 3);

			return Math.Max(l, Math.Max(TitleLength, SubTitleLength));
		}
	}
}
