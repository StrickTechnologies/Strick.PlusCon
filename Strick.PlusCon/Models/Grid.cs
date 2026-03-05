using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Reflection;

using static Strick.PlusCon.Helpers;


namespace Strick.PlusCon.Models;


/// <summary>
/// A grid that can contain rows and columns of data to be displayed in a tabular format.
/// </summary>
public class Grid
{
	#region CONSTRUCTORS

	/// <summary>
	/// Instantiates a new <seealso cref="Grid"/> object
	/// </summary>
	public Grid()
	{
		Columns = new GridColumns(this);
		Rows = [];
	}

	/// <summary>
	/// Creates a new instance with the <see cref="Title"/> property set to <paramref name="title"/>.
	/// </summary>
	public Grid(string title) : this()
	{
		Title = new StyledText(title);
	}

	/// <summary>
	/// Creates a new instance with the <see cref="Title"/> property set to <paramref name="title"/>, 
	/// and the <see cref="Subtitle"/> property set to <paramref name="subTitle"/>.
	/// </summary>
	public Grid(string title, string subTitle) : this(title)
	{
		Subtitle = new StyledText(subTitle);
	}

	/// <summary>
	/// Creates a new instance with the <see cref="Title"/> property set to <paramref name="title"/>, 
	/// the <see cref="Subtitle"/> property set to <paramref name="subTitle"/>, and 
	/// the <see cref="Footer"/> property set to <paramref name="footer"/>.
	/// </summary>
	public Grid(string title, string subTitle, string footer) : this(title, subTitle)
	{
		Footer = new StyledText(footer);
	}

	#endregion CONSTRUCTORS


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

	/// <summary>
	/// <inheritdoc cref="GenerateColumns{T}"/>
	/// </summary>
	/// <typeparam name="T"><inheritdoc cref="GenerateColumns{T}" path="/typeparam[@name='T']"/></typeparam>
	public void AddColumns<T>() => GenerateColumns<T>();

	/// <summary>
	/// <inheritdoc cref="GenerateColumns{T}"/>
	/// </summary>
	/// <typeparam name="T"><inheritdoc cref="GenerateColumns{T}" path="/typeparam[@name='T']"/></typeparam>
	/// <param name="obj">An object of type <typeparamref name="T"/>. This parameter is not used. 
	/// It is only included to allow for type inference when calling the method.</param>
#pragma warning disable IDE0060
	public void AddColumns<T>(T obj) => AddColumns<T>();
#pragma warning restore IDE0060

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
	/// <para id='summary'>Adds a new row to the grid, and returns the newly created <see cref="GridRow"/> object.</para>
	/// The cells of the row all default to having their <see cref="GridCellBase{T}.Content"/> property set to <c>null</c>. 
	/// </summary>
	/// <returns>The newly created <see cref="GridRow"/> object</returns>
	public GridRow AddRow()
	{
		GridRow r = new(this);
		Rows.Add(r);
		return r;
	}

	/// <summary>
	/// <inheritdoc cref="AddRow()" path="/summary/para[@id='summary']"/>
	/// The <paramref name="cellContent"/> array maps to the <seealso cref="GridRow.Cells"/> of the row by index. 
	/// If the number of elements in <paramref name="cellContent"/> is less than the number of columns in the grid, the remaining cells 
	/// default to having their <see cref="GridCellBase{T}.Content"/> property set to <c>null</c>. 
	/// If the number of elements in <paramref name="cellContent"/> is more than the number of columns in the grid, an exception is thrown.
	/// </summary>
	/// <returns><inheritdoc cref="AddRow()" path="/returns"/></returns>
	public GridRow AddRow(params string?[] cellContent)
	{
		GridRow r = new(this, cellContent);
		Rows.Add(r);
		return r;
	}

	/// <summary>
	/// <inheritdoc cref="AddRow(string?[])" path="/summary"/>
	/// <para>
	/// Values in <paramref name="cellContent"/> that are not strings will be converted to strings using their <see cref="object.ToString"/> method. 
	/// </para>
	/// </summary>
	/// <returns><inheritdoc cref="AddRow()" path="/returns"/></returns>
	public GridRow AddRow(params object?[] cellContent)
	{
		GridRow r = new(this, cellContent);
		Rows.Add(r);
		return r;
	}

	/// <summary>
	/// <para id='summary'>Adds a new <i>"separator"</i> row to the grid, and returns the newly created <see cref="GridRow"/> object.</para>
	/// A separator row is just a normal row with:
	/// <list type="bullet">
	/// <item>Each cell's  <see cref="GridCellBase{T}.FillerChar"/> property set to the 
	/// value of the <paramref name="fillerChar"/> argument.</item>
	/// <item>Each cell's <see cref="GridCellBase{T}.Content"/> property set to null. 
	/// Note: the Content property can be set for cells in a separator row, just as in any other row.
	/// </item>
	/// </list>
	/// </summary>
	/// <returns><inheritdoc cref="AddRow()" path="/returns"/></returns>
	/// <param name="fillerChar">The character to use as the filler in each of the row's cells. See <see cref="GridCellBase{T}.FillerChar"/> for more details.</param>
	public GridRow AddSeparatorRow(char fillerChar = ' ')
	{
		GridRow newRow = new(this);
		Rows.Add(newRow);
		newRow.Cells.SetFillerChar(fillerChar);

		return newRow;
	}


	/// <summary>
	/// <inheritdoc cref="AddRow()" path="/summary/para[@id='summary']"/>
	/// The row's cells are populated with data from the <paramref name="cellContent"/> argument 
	/// by matching the names of the public properties of the <paramref name="cellContent"/> object to the column's <see cref="GridColumn.Name"/> property. 
	/// The value of each property in the <paramref name="cellContent"/> object is converted to a string using its <see cref="object.ToString"/> method, 
	/// and that string is used as the content for the corresponding cell in the new row. 
	/// Properties of the <paramref name="cellContent"/> object that do not have a corresponding column with a matching <see cref="GridColumn.Name"/> are ignored. 
	/// Columns that do not have a corresponding property in the <paramref name="cellContent"/> object will have their 
	/// cell's <see cref="GridCellBase{T}.Content"/> property set to <c>null</c>.
	/// <para>
	/// If the <see cref="Grid"/> has no columns, columns are generated 
	/// automatically (see <see cref="AddColumns{T}()"/>).
	/// </para>
	/// </summary>
	/// <returns><inheritdoc cref="AddRow()" path="/returns"/></returns>
	/// 
	/// <typeparam name="T">The type of the object that provides the data for the new row. Each public property of this type is mapped to a
	/// corresponding column in the grid by name.</typeparam>
	/// <param name="cellContent">An object containing the data to populate the new row.</param>
	public GridRow AddRow<T>(T cellContent)
	{
		if (ColumnCount == 0)
		{
			AddColumns<T>();
		}


		var row = AddRow();

		if (cellContent != null)
		{
			var type = typeof(T);
			foreach (GridColumn col in Columns.Where(c => !string.IsNullOrEmpty(c.Name)))
			{
				var val = type.GetProperty(col.Name!, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public)?.GetValue(cellContent);
				if (val != null)
				{
					row.Cells[col.Index].Content = val.ToString();
				}
			}
		}

		return row;
	}

	/// <summary>
	/// Adds rows to the grid, and returns a sequence containing the newly created <see cref="GridRow"/> objects. 
	/// One row is added for each element in the <paramref name="rowContent"/> argument. 
	/// <para>See <see cref="AddRow{T}(T)"/> for details on how each row is generated from the elements in <paramref name="rowContent"/>.</para>
	/// </summary>
	/// <typeparam name="T"><inheritdoc cref="AddRow{T}(T)" path="/typeparam[@name='T']"/></typeparam>
	/// <param name="rowContent">A seqence consisting of the objects containing the data to populate the new rows.</param>
	public IEnumerable<GridRow> AddRows<T>(IEnumerable<T> rowContent)
	{
		foreach (T obj in rowContent)
		{ yield return AddRow(obj); }
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
	public int TitleLength => Title != null && Title.Text != null ? Title.Text.Length : 0;

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
	public int SubTitleLength => Subtitle != null && Subtitle.Text != null ? Subtitle.Text.Length : 0;

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
	public int FooterLength => Footer != null && Footer.Text != null ? Footer.Text.Length : 0;

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
	/// or cell (<see cref="GridCellBase{T}.CellStyle"/>) level.
	/// </para>
	/// </summary>
	public TextStyle CellStyle { get; set; } = new(Color.White);

	/// <summary>
	/// The text styling to be applied to the content of ALL the grid's cells. 
	/// This is the cell's "content" (see <see cref="GridCellBase{T}.Content"/>). 
	/// <para>Can be overridden at the 
	/// column (<see cref="GridColumn.ContentStyle"/>), 
	/// row (<see cref="GridRow.ContentStyle"/>), 
	/// or cell (<see cref="GridCellBase{T}.ContentStyle"/>) level.
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
		if (Columns == null || Columns.Count == 0)
		{ throw new InvalidOperationException("Displaying a grid with no columns is somewhat pointless. You should add columns first."); }

		if (Rows == null || Rows.Count == 0)
		{ throw new InvalidOperationException("Displaying a grid with no rows is somewhat pointless. You should add rows first."); }

		int row = Console.CursorTop;
		int col = Console.CursorLeft;

		//TITLES
		ShowChromeElement(Title, TitleAlignment, col);
		ShowChromeElement(Subtitle, SubtitleAlignment, col);

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
		ShowChromeElement(Footer, FooterAlignment, col);
	}

	private void ShowChromeElement(StyledText? element, HorizontalAlignment alignment, int left)
	{
		if (element == null || element.Text == null)
		{ return; }

		W(RenderChromeElement(element, alignment));
		MoveToStartOfNextRow(left);
	}

	internal string RenderChromeElement(StyledText element, HorizontalAlignment alignment)
	{
		if (element.Text == null)
		{ throw new ArgumentException("title text cannot be null"); }

		if (element.Text.Length == 1)
		{ return element.StyleText(new string(element.Text[0], Width)); }
		else if (element.Text.Length > Width || alignment == HorizontalAlignment.Left)
		{ return element.StyleText(element.Text.PadRight(Width)); }
		else if (alignment == HorizontalAlignment.Center)
		{ return element.StyleText(element.Text.Center(Width)); }
		else //if (alignment == HorizontalAlignment.Right)
		{ return element.StyleText(element.Text.PadLeft(Width)); }
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

	/// <summary>
	/// Generates and adds columns to the <see cref="Columns"/> collection based on the public properties of the specified type.
	/// <para>Creates a column for each public property of the specified type. Columns corresponding
	/// to numeric properties are right-aligned.</para>
	/// <para>New columns are appended to any existing columns in the <see cref="Columns"/> collection.</para>
	/// </summary>
	/// <typeparam name="T">The type whose public properties are used to generate columns.</typeparam>
	internal void GenerateColumns<T>()
	{
		foreach (var prop in GetPropertyInfos<T>())
		{
			var col = Columns.Add(MakeHeaderText(prop.Name));
			col.Name = prop.Name;
			if (IsNumeric(prop.PropertyType))
			{
				col.CellLayout.HorizontalAlignment = HorizontalAlignment.Right;
			}
		}
	}

	internal static IEnumerable<PropertyInfo> GetPropertyInfos<T>()
	{
		return typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public);
	}

	internal static bool IsNumeric(Type type)
	{
		var numType = typeof(INumber<>);
		return type.GetInterfaces().Any(i => i.IsGenericType && (i.GetGenericTypeDefinition() == numType));
	}

	internal static string MakeHeaderText(string propName) => propName.Replace("_", " ");
}
