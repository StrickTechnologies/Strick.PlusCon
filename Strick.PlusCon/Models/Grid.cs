using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

	///// <summary>
	///// Backer field for the <see cref="Columns"/> property.
	///// </summary>
	//protected List<GridColumn> Cols;

	///// <summary>
	///// The columns of the grid. Readonly.
	///// <para>Use <see cref="AddColumn()"/> and <see cref="RemoveColumn(int)"/> to add or remove columns from the grid.</para>
	///// </summary>
	//public IReadOnlyList<GridColumn> Columns2 => Cols;

	///// <summary>
	///// Adds a column to the grid's <see cref="Columns"/> collection and returns the newly added <see cref="GridColumn"/> object.
	///// </summary>
	///// <returns>The newly added <see cref="GridColumn"/> object.</returns>
	//public GridColumn AddColumn() => Columns.Add();

	///// <summary>
	///// <inheritdoc cref="AddColumn()"/>
	///// </summary>
	///// <param name="headerText">The text to display in the column header.</param>
	///// <returns><inheritdoc cref="AddColumn()"/></returns>
	//public GridColumn AddColumn(string headerText) => Columns.Add(headerText);

	///// <summary>
	///// <inheritdoc cref="AddColumn()"/>
	///// </summary>
	///// <param name="headerText">The text to display in the column header.</param>
	///// <param name="alignment">The <see cref="HorizontalAlignment"/> for the column. Applies to both the cell content and the column header.</param>
	///// <returns><inheritdoc cref="AddColumn()"/></returns>
	//public GridColumn AddColumn(string headerText, HorizontalAlignment alignment) => Columns.Add(headerText, alignment);
	//{
	//	if (Rows != null && Rows.Count > 0)
	//	{ throw new InvalidOperationException($"Cannot add columns when the grid has rows. Add columns before rows, or clear the {nameof(Rows)} collection before adding columns."); }

	//	var c = new GridColumn(this, headerText, alignment);
	//	Cols.Add(c);
	//	return c;
	//}

	///// <summary>
	///// Removes the specified column from the <see cref="Columns"/> collection. 
	///// If <paramref name="col"/> is null, or not found in the <see cref="Columns"/> collection, an exception is thrown.
	///// </summary>
	///// <param name="col"></param>
	///// <exception cref="ArgumentNullException"></exception>
	///// <exception cref="ArgumentException"></exception>
	//public bool RemoveColumn(GridColumn col) => Columns.Remove(col);
	//{
	//	if (col == null)
	//	{ throw new ArgumentNullException(nameof(col)); }

	//	var x = Cols.IndexOf(col);
	//	if (x == -1)
	//	{ throw new ArgumentException("Column is not found in this grid.", nameof(col)); }

	//	RemoveColumn(x);
	//}

	///// <summary>
	///// Removes the column at the index specified by <paramref name="index"/> from the <see cref="Columns"/> collection. 
	///// <para>An exception is thrown if the grid has any rows.</para>
	///// </summary>
	///// <param name="index">The zero based index of the column to be removed.</param>
	///// <exception cref="InvalidOperationException"></exception>
	///// <exception cref="ArgumentOutOfRangeException"></exception>
	//public void RemoveColumn(int index) => Columns.RemoveAt(index);
	//{
	//	if (Rows != null && Rows.Count > 0)
	//	{ throw new InvalidOperationException("Cannot remove columns when the grid has rows."); }

	//	if (index < 0 || index > (Columns.Count - 1))
	//	{ throw new ArgumentOutOfRangeException(); }

	//	Cols.RemoveAt(index);
	//}

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
			Console.SetCursorPosition(col, ++row);
		}
		if (Subtitle != null)
		{
			ShowTitle(Subtitle);
			Console.SetCursorPosition(col, ++row);
		}

		//COLUMN HEADERS
		if (Columns.Any(c => c.Header != null))
		{
			foreach (GridColumn column in Columns)
			{
				if (column.Header != null)
				{
					W(column.Header.RenderedContent);
					//System.Diagnostics.Debug.WriteLine($"col hd {column.Header.Content} r/c:{column.Header.RowIndex}/{column.Header.ColumnIndex}");
				}
				else
				{ Console.SetCursorPosition(Console.GetCursorPosition().Left + column.TotalWidth, row); }

			}
			Console.SetCursorPosition(col, ++row);
		}

		//ROWS
		foreach (GridRow gRow in Rows)
		{
			//CELLS
			foreach (GridColumn column in Columns)
			{
				var cell = gRow.Cells[column.Index];
				W(cell.RenderedContent);
				//System.Diagnostics.Debug.WriteLine($"cell {cell.Content} r/c:{cell.RowIndex}/{cell.ColumnIndex}");
			}
			Console.SetCursorPosition(col, ++row);
		}

		//FOOTER
		if (Footer != null)
		{
			ShowTitle(Footer);
			Console.SetCursorPosition(col, ++row);
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
}
