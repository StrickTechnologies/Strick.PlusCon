using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using static Strick.PlusCon.Helpers;


namespace Strick.PlusCon.Models;


/// <summary>
/// A grid that can contain rows and columns of data to be displayed in a tabular format.
/// </summary>
public class Grid
{
	/// <summary>
	/// Instantiates a new <seealso cref="Grid"/> object
	/// </summary>
	public Grid()
	{
		Columns = new GridColumns(this);
		Rows = new List<GridRow>();
	}


	#region COLUMNS

	/// <summary>
	/// The columns of the grid.
	/// </summary>
	public GridColumns Columns { get; }

	/// <summary>
	/// Returns the number of columns contained in the grid
	/// </summary>
	public int ColumnCount => Columns.Count;


	/// <summary>
	/// <inheritdoc cref="GridColumns.Add(string)"/>
	/// </summary>
	public GridColumn AddColumn() => Columns.Add();

	/// <summary>
	/// <inheritdoc cref="GridColumns.Add(string)"/>
	/// </summary>
	/// <param name="headerText"><inheritdoc cref="GridColumns.Add(string, HorizontalAlignment)" path="/param[@name='headerText']"/></param>
	public GridColumn AddColumn(string headerText) => Columns.Add(headerText);

	/// <summary>
	/// <inheritdoc cref="GridColumns.Add(string, HorizontalAlignment)"/>
	/// </summary>
	/// <param name="headerText"><inheritdoc cref="GridColumns.Add(string, HorizontalAlignment)" path="/param[@name='headerText']"/></param>
	/// <param name="alignment"><inheritdoc cref="GridColumns.Add(string, HorizontalAlignment)" path="/param[@name='alignment']"/></param>
	public GridColumn AddColumn(string headerText, HorizontalAlignment alignment) => Columns.Add(headerText, alignment);

	#endregion COLUMNS


	#region ROWS

	/// <summary>
	/// The rows of the grid. Each <see cref="GridRow"/> object in the sequence represents a row displayed in the grid. 
	/// The <see cref="Rows"/> collection is a <see cref="List{T}"/>, and can be manipulated (e.g. Add, Remove, Clear) using the normal methods.
	/// </summary>
	public List<GridRow> Rows { get; }

	/// <summary>
	/// Returns the number of rows contained in the grid.
	/// </summary>
	public int RowCount => Rows.Count;

	/// <summary>
	/// Adds a new row to the grid. The cells of the row all default to having content = null. 
	/// The newly created <see cref="GridRow"/> object is returned.
	/// </summary>
	/// <returns>The newly created <see cref="GridRow"/> object</returns>
	public GridRow AddRow()
	{
		GridRow r = new(this);
		Rows.Add(r);
		return r;
	}

	/// <summary>
	/// Adds a new row to the grid. 
	/// The <paramref name="cellContent"/> array maps to the <seealso cref="GridRow.Cells"/> of the row. 
	/// If the number of elements in <paramref name="cellContent"/> is less than the number of columns in the grid, the remaining cells default to having content = null. 
	/// If the number of elements in <paramref name="cellContent"/> is more than the number of columns in the grid, an exception is thrown.
	/// The newly created <see cref="GridRow"/> object is returned.
	/// </summary>
	/// <returns>The newly created <see cref="GridRow"/> object</returns>
	public GridRow AddRow(params string?[] cellContent)
	{
		GridRow r = new(this, cellContent);
		Rows.Add(r);
		return r;
	}

	/// <summary>
	/// Adds a new row to the grid. 
	/// The <paramref name="cellContent"/> array maps to the <seealso cref="GridRow.Cells"/> of the row. 
	/// Values in <paramref name="cellContent"/> that are not strings will be converted to strings using their <see cref="object.ToString"/> method. 
	/// If the number of elements in <paramref name="cellContent"/> is less than the number of columns in the grid, the remaining cells default to having content = null. 
	/// If the number of elements in <paramref name="cellContent"/> is more than the number of columns in the grid, an exception is thrown.
	/// The newly created <see cref="GridRow"/> object is returned.
	/// </summary>
	/// <returns>The newly created <see cref="GridRow"/> object</returns>
	public GridRow AddRow(params object?[] cellContent)
	{
		GridRow r = new(this, cellContent);
		Rows.Add(r);
		return r;
	}

	/// <summary>
	/// Adds a new "separator" row to the grid. A separator row is just a normal row with:
	/// <list type="bullet">
	/// <item>Each cell's  <see cref="GridCellBase.FillerChar"/> property set to the 
	/// value of the <paramref name="fillerChar"/> argument.</item>
	/// <item>Each cell's <see cref="GridCellBase.Content"/> property set to null. 
	/// Note: the Content property can be set for cells in a separator row, just as in any other row.
	/// </item>
	/// </list>
	/// </summary>
	/// <param name="fillerChar">foo</param>
	public GridRow AddSeparatorRow(char fillerChar = ' ')
	{
		GridRow newRow = new(this);
		Rows.Add(newRow);
		newRow.Cells.SetFillerChar(fillerChar);

		return newRow;
	}

	#endregion ROWS


	#region CHROME

	/// <summary>
	/// The title of the grid. Shows at the top of the grid -- above the rows/columns. 
	/// Horizontal alignment of the title's content can be controlled using the <see cref="TitleAlignment"/> property.
	/// <div id='desc'>
	/// If null, the line will not be shown. 
	/// If a single character string is specified (e.g. "-") for the <see cref="StyledText.Text"/> property, 
	/// that character will be repeated for the width of the grid. 
	/// To show a blank line, use a single space as the content.
	/// <para>
	/// Note: if the content is wider than the width of all the grid's columns, 
	/// it will start at the left edge of the grid and flow past the right edge, 
	/// regardless of the horizontal alignment setting.
	/// </para>
	/// </div>
	/// </summary>
	public StyledText? Title { get; set; }

	/// <summary>
	/// The horizontal alignment for the grid's <see cref="Title"/>.
	/// </summary>
	public HorizontalAlignment TitleAlignment { get; set; } = HorizontalAlignment.Center;

	/// <summary>
	/// The length of the <see cref="Title"/> property. If <see cref="Title"/> is null or empty, 0 is returned. 
	/// </summary>
	public int TitleLength => Title == null ? 0 : Title.Text.Length;

	/// <summary>
	/// The subtitle of the grid. Shown at the top of the grid, beneath the <see cref="Title"/>, above the rows/columns.
	/// Horizontal alignment of the subtitle's content can be controlled using the <see cref="SubtitleAlignment"/> property.
	/// <inheritdoc cref="Title" path="/summary/div[@id='desc']"/>
	/// </summary>
	public StyledText? Subtitle { get; set; }

	/// <summary>
	/// The horizontal alignment for the grid's <see cref="Subtitle"/>.
	/// </summary>
	public HorizontalAlignment SubtitleAlignment { get; set; } = HorizontalAlignment.Center;

	/// <summary>
	/// The length of the <see cref="Subtitle"/> property. If <see cref="Subtitle"/> is null or empty, 0 is returned. 
	/// </summary>
	public int SubTitleLength => Subtitle == null ? 0 : Subtitle.Text.Length;

	/// <summary>
	/// The footer of the grid. Shown at the bottom of the grid, beneath the the rows/columns.
	/// Horizontal alignment of the footer's content can be controlled using the <see cref="FooterAlignment"/> property.
	/// <inheritdoc cref="Title" path="/summary/div[@id='desc']"/>
	/// </summary>
	public StyledText? Footer { get; set; }

	/// <summary>
	/// The horizontal alignment for the grid's <see cref="Footer"/>.
	/// </summary>
	public HorizontalAlignment FooterAlignment { get; set; } = HorizontalAlignment.Center;

	/// <summary>
	/// The length of the <see cref="Footer"/> property. If <see cref="Footer"/> is null or empty, 0 is returned. 
	/// </summary>
	public int FooterLength => Footer == null ? 0 : Footer.Text.Length;

	#endregion CHROME


	#region STYLE

	/// <summary>
	/// The text styling to be applied to the column header cells of ALL the grid's columns
	/// </summary>
	public TextStyle ColumnHeaderCellStyle { get; set; } = new(Color.White) { Underline = true };

	/// <summary>
	/// The text styling to be applied to the column header cell content of ALL the grid's columns
	/// </summary>
	public TextStyle ColumnHeaderContentStyle { get; set; } = new(Color.White) { Underline = true };

	/// <summary>
	/// Gets/Sets a value which indicates whether or not to show the column headers. 
	/// The default is true. 
	/// </summary>
	public bool ShowColumnHeaders { get; set; } = true;

	/// <summary>
	/// The text styling to be applied to ALL the grid's cells. 
	/// This includes the area of the cell which is NOT the cell's "content".
	/// <para>Can be overridden at the 
	/// column (<see cref="GridColumn.CellStyle"/>), 
	/// row (<see cref="GridRow.CellStyle"/>), 
	/// or cell (<see cref="GridCellBase.CellStyle"/>) level.
	/// </para>
	/// </summary>
	public TextStyle CellStyle { get; set; } = new(Color.White);

	/// <summary>
	/// The text styling to be applied to the content of ALL the grid's cells. 
	/// This is the cell's "content" (see <see cref="GridCellBase.Content"/>). 
	/// <para>Can be overridden at the 
	/// column (<see cref="GridColumn.ContentStyle"/>), 
	/// row (<see cref="GridRow.ContentStyle"/>), 
	/// or cell (<see cref="GridCellBase.ContentStyle"/>) level.
	/// </para>
	/// </summary>
	public TextStyle CellContentStyle { get; set; } = new(Color.White);

	#endregion STYLE


	/// <summary>
	/// The total width of the grid. Calculated as the sum of the <see cref="GridColumn.TotalWidth"/> property of all columns.
	/// </summary>
	public int Width => Columns.Sum(c => c.TotalWidth);


	/// <summary>
	/// Displays the grid. 
	/// <para>The grid will be displayed beginning at the console's current cursor position (<see cref="Console.GetCursorPosition"/>). 
	/// The left edge of the grid will line up with the cursor's left/X/column position.</para>
	/// </summary>
	/// <exception cref="InvalidOperationException"></exception>
	public void Show()
	{
		if (Columns == null || !Columns.Any())
		{ throw new InvalidOperationException("Displaying a grid with no columns is somewhat pointless. You should add columns first."); }

		if (Rows == null || !Rows.Any())
		{ throw new InvalidOperationException("Displaying a grid with no rows is somewhat pointless. You should add rows first."); }

		int row = Console.CursorTop;
		int col = Console.CursorLeft;

		//TITLES
		if (Title != null)
		{
			ShowTitle(Title, TitleAlignment);
			MoveToStartOfNextRow(col);
		}
		if (Subtitle != null)
		{
			ShowTitle(Subtitle, SubtitleAlignment);
			MoveToStartOfNextRow(col);
		}

		//COLUMN HEADERS
		if (ShowColumnHeaders && Columns.Any(c => c.Header != null))
		{
			foreach (GridColumn column in Columns)
			{
				if (column.Header != null && column.TotalWidth > 0)
				{
					W(column.Header.RenderedContent);
					//System.Diagnostics.Debug.WriteLine($"col hd {column.Header.Content} r/c:{column.Header.RowIndex}/{column.Header.ColumnIndex}");
				}
				else
				{
					//Console.SetCursorPosition(Console.GetCursorPosition().Left + column.TotalWidth, row);
					//Console.CursorLeft = Console.GetCursorPosition().Left + column.TotalWidth;
					//W($"{EscapeCodes.Escape}[{column.TotalWidth}C"); //does not wrap at the end of line
					W(new string(' ', column.TotalWidth));
				}

			}
			MoveToStartOfNextRow(col);
		}

		//ROWS
		foreach (GridRow gRow in Rows)
		{
			//CELLS
			foreach (GridColumn column in Columns)
			{
				if (column.TotalWidth > 0)
				{
					var cell = gRow.Cells[column.Index];
					W(cell.RenderedContent);
					//System.Diagnostics.Debug.WriteLine($"cell {cell.Content} r/c:{cell.RowIndex}/{cell.ColumnIndex}");
				}
			}
			MoveToStartOfNextRow(col);
		}

		//FOOTER
		if (Footer != null)
		{
			ShowTitle(Footer, FooterAlignment);
			MoveToStartOfNextRow(col);
		}
	}

	private void ShowTitle(StyledText? title, HorizontalAlignment alignment)
	{
		if (title == null)
		{ return; }

		W(RenderTitle(title, alignment));
	}

	internal string RenderTitle(StyledText title, HorizontalAlignment alignment)
	{
		if (title.Text.Length == 1)
		{ return title.StyleText(new string(title.Text[0], Width)); }
		else if (title.Text.Length > Width || alignment == HorizontalAlignment.Left)
		{ return title.StyleText(title.Text.PadRight(Width)); }
		else if (alignment == HorizontalAlignment.Center)
		{ return title.StyleText(title.Text.Center(Width)); }
		else //if (alignment == HorizontalAlignment.Right)
		{ return title.StyleText(title.Text.PadLeft(Width)); }
	}


	/// <summary>
	/// Searches ALL of the Grid's cells in a row, column order and 
	/// <inheritdoc cref="GridExtensions.Find(IEnumerable{GridCell}, GridSearchExpression)" path="/summary/span[@id='rtype']"/>
	/// <inheritdoc cref="GridExtensions.Find(IEnumerable{GridCell}, GridSearchExpression)" path="/summary/span[@id='desc']"/>
	/// </summary>
	/// <param name="searchExpression"><inheritdoc cref="GridExtensions.Find(IEnumerable{GridCell}, GridSearchExpression)" path="/param[@name='searchExpression']"/></param>
	public IEnumerable<GridCell> Find(GridSearchExpression searchExpression)
	{
		var allCells = Rows.SelectMany(r => r.Cells);
		return allCells.Find(searchExpression);
	}


	private static void MoveToStartOfNextRow(int left)
	{
		Cursor.MoveDown();
		Console.CursorLeft = left;
	}
}
