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
	/// Creates a <see cref="Menu"/> instance with no options, 
	/// and the <see cref="Title"/> property set to <paramref name="title"/>.
	/// </summary>
	public Menu(string title) : this()
	{
		Title = new(title);
	}

	/// <summary>
	/// Creates a <see cref="Menu"/> instance with no options, 
	/// the <see cref="Title"/> property set to <paramref name="title"/>, 
	/// and the <see cref="Subtitle"/> property set to <paramref name="subTitle"/>.
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
	/// The horizontal alignment for the menu's <see cref="Title"/>.
	/// </summary>
	public HorizontalAlignment TitleAlignment { get; set; } = HorizontalAlignment.Center;

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
	/// The horizontal alignment for the menu's <see cref="Subtitle"/>.
	/// </summary>
	public HorizontalAlignment SubtitleAlignment { get; set; } = HorizontalAlignment.Center;

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
	/// The number of columns. If >1, the options will show in a row, column order in that number of columns.
	/// </summary>
	public int ColumnCount { get; set; } = 1;

	/// <summary>
	/// Indicates the number of spaces between columns when 
	/// the <see cref="ColumnCount"/> property is >1.
	/// </summary>
	public int GutterWidth { get; set; } = 3;

	/// <summary>
	/// 0=render using classic, 1=render using grid
	/// <para>Note: <see cref="ColumnCount"/> and <see cref="GutterWidth"/> are only 
	/// applicable when rendering is set to grid</para>
	/// </summary>
	internal int RenderEngine = 1;

	/// <summary>
	/// TESTING! show margins, padding and filler
	/// </summary>
	internal bool ShowSpecial = false;


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
		if (!Options.HasAny())
		{ throw new InvalidOperationException("Cannot display a menu with no options. Add options to the menu before displaying..."); }

		do
		{
			//raise OnBeforeShow events for menu and each option
			//  this is done before the width is calculated, so that any
			//  mods to the options are included in the width calc
			OnBeforeShow(new());
			foreach (MenuOption opt in Options)
			{ opt.OnBeforeShow(new()); }

			if (RenderEngine == 0)
			{ Render(); }
			else
			{ Render_G(); }

			//user input
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

	private void Render()
	{
		var width = Width;

		CLS();

		//show titles
		ShowTitle(Title, width);
		ShowTitle(Subtitle, width);

		//show options
		foreach (MenuOption opt in Options)
		{ ShowOption(opt, width); }
	}
	private void Render_G()
	{
		int colCount = ColumnCount;

		var width = Width;
		Grid grid = new Grid();
		grid.ShowColumnHeaders = false;

		for (int i = 0; i < colCount; i++)
		{
			var col = grid.AddColumn();
			col.CellLayout.MarginLeft = 0;
			col.CellLayout.MarginRight = GutterWidth;
			if (ShowSpecial)
			{
				col.CellLayout.MarginLeftChar = 'm';
				col.CellLayout.MarginRightChar = 'm';
				col.CellLayout.PaddingLeftChar = 'p';
				col.CellLayout.PaddingRightChar = 'p';
			}
			col.ContentStyle = OptionsStyle;
			col.CellStyle = OptionsStyle;
		}
		grid.Columns[^1].CellLayout.MarginRight = 0;

		if (Title != null)
		{
			grid.Title = Title;
			grid.TitleAlignment = TitleAlignment;
		}
		if (Subtitle != null)
		{
			grid.Subtitle = Subtitle;
			grid.SubtitleAlignment = SubtitleAlignment;
		}

		int colIndex = colCount;
		GridRow row = null!;
		foreach (MenuOption opt in Options)
		{
			colIndex++;
			if (colIndex >= colCount)
			{
				colIndex = 0;
				row = grid.AddRow();
			}
			if (opt is MenuSeperator && opt.Caption.Length == 1)
			{ row.Cells[colIndex].FillerChar = opt.Caption[0]; }
			else
			{
				row.Cells[colIndex].Content = opt.GetText(width);
				if (ShowSpecial)
				{ row.Cells[colIndex].FillerChar = 'f'; }
			}
			if (opt.Style != null)
			{
				row.Cells[colIndex].ContentStyle = opt.Style;
				row.Cells[colIndex].CellStyle = opt.Style;
			}
		}
		for(int i = colIndex+1; i<colCount; i++)
		{
			row.Cells[i].ContentStyle = new TextStyle();
			row.Cells[i].CellStyle = new TextStyle();
		}

		//some experimental schemes to pad out the columns so the menu options match the width of the wider of the title/subtitle.
		int colWidth = grid.Columns.Sum(col => col.TotalWidth);
		if (colWidth < Math.Max(grid.TitleLength, grid.SubTitleLength))
		{
			//grid.Columns[^1].CellLayout.MarginRight += Math.Max(grid.TitleLength, grid.SubTitleLength) - colWidth;
		}
		if (false && colWidth < Math.Max(grid.TitleLength, grid.SubTitleLength))
		{
			width = Math.Max(grid.TitleLength, grid.SubTitleLength);

			//var col2 = grid.AddColumn();
			//col2.CellLayout.MarginLeft = 0;
			//col2.CellLayout.MarginRight = 0;
			//grid.Rows[0].Cells[1].Content = new string(' ', width - col.ContentWidth);
			//grid.Columns[0].CellStyle = new(Color.White, System.Drawing.Color.HotPink);

			if (false && colCount <= 2)
			{
				grid.Columns[0].CellLayout.PaddingRight = width - colWidth;
				//grid.Columns[0].CellLayout.MarginRight += width - colWidth;
				//grid.Columns[0].CellLayout.PaddingRightChar = 'p';
				//grid.Columns[0].Cells.SetFillerChar('f');
				//grid.Columns[0].CellLayout.MarginRightChar = 'm';
			}
			else
			{
				//int x = (width - colWidth) / (colCount - 1);
				int x = (width - colWidth) / colCount;
				int x2 = (width - colWidth) % colCount;
				//int x2 = (width - colWidth) - x * colCount;
				for (int i = 0; i < colCount; i++)
				{
					//grid.Columns[i].CellLayout.MarginRight += x;
					grid.Columns[i].CellLayout.PaddingRight = x;
				}
				grid.Columns[^1].CellLayout.PaddingRight += x2;
			}
		}

		CLS();
		grid.Show();
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
	/// Calculated based on the max width of <see cref="Title"/>, <see cref="Subtitle"/>, <see cref="Prompt"/>, 
	/// and all the <see cref="Options"/> (+3 for the Key, a period,  and space; e.g. "x. ").
	/// </summary>
	public int Width
	{
		get
		{
			var l = Options.Max(o => o.Width);

			if (Prompt != null)
			{ l = Math.Max(l, Prompt.Text.Length); }

			return Math.Max(l, Math.Max(TitleLength, SubTitleLength));
		}
	}


	#region EVENTS

	/// <summary>
	/// Occurs <b>before</b> the menu is shown.
	/// </summary>
	public event EventHandler? BeforeShow;

	/// <summary>
	/// Invokes the <see cref="BeforeShow"/> event
	/// </summary>
	/// <param name="e"></param>
	internal protected virtual void OnBeforeShow(EventArgs e)
	{
		BeforeShow?.Invoke(this, e);
	}

	#endregion EVENTS
}
