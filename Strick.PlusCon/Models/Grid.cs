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

	#endregion ROWS


	#region CHROME

	/// <summary>
	/// The title of the grid. Centered above the grid rows/columns.
	/// If a single character string is specified (e.g. "-"), that character will be repeated for the width of the grid. 
	/// <para>Note: if the Title is wider than the width of all the grid's columns, 
	/// it will start at the left edge of the grid and flow past the right edge.</para>
	/// </summary>
	public StyledText? Title { get; set; }

	/// <summary>
	/// The length of the <see cref="Title"/> property. If <see cref="Title"/> is null or empty, 0 is returned. 
	/// </summary>
	public int TitleLength => Title == null ? 0 : Title.Text.Length;

	/// <summary>
	/// The subtitle of the grid. Centered at the top of the grid, beneath the <see cref="Title"/> above the grid rows/columns.
	/// If a single character string is specified (e.g. "-"), that character will be repeated for the width of the grid.
	/// <para>Note: if the Subtitle is wider than the width of all the grid's columns, 
	/// it will start at the left edge of the grid and flow past the right edge.</para>
	/// </summary>
	public StyledText? Subtitle { get; set; }

	/// <summary>
	/// The length of the <see cref="Subtitle"/> property. If <see cref="Subtitle"/> is null or empty, 0 is returned. 
	/// </summary>
	public int SubTitleLength => Subtitle == null ? 0 : Subtitle.Text.Length;

	/// <summary>
	/// The footer of the grid. Centered at the bottom of the grid, beneath the the grid rows/columns.
	/// If a single character string is specified (e.g. "-"), that character will be repeated for the width of the grid.
	/// <para>Note: if the Footer is wider than the width of all the grid's columns, 
	/// it will start at the left edge of the grid and flow past the right edge.</para>
	/// </summary>
	public StyledText? Footer { get; set; }

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
			ShowTitle(Title);
			MoveToStartOfNextRow(col);
		}
		if (Subtitle != null)
		{
			ShowTitle(Subtitle);
			MoveToStartOfNextRow(col);
		}

		//COLUMN HEADERS
		if (Columns.Any(c => c.Header != null))
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
			ShowTitle(Footer);
			MoveToStartOfNextRow(col);
		}
	}

	private void ShowTitle(StyledText? title)
	{
		if (title == null)
		{ return; }

		if (title.Text.Length == 1)
		{ WL(title.StyleText(new string(title.Text[0], Width))); }
		else
		{ WL(title.StyleText(title.Text.Center(Width))); }
	}


	private void MoveToStartOfNextRow(int left)
	{
		Cursor.MoveDown();
		Console.CursorLeft = left;
	}
}
