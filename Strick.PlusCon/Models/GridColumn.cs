using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strick.PlusCon.Models;

/// <summary>
/// Represents a column in a <see cref="Grid"/> object.
/// </summary>
public class GridColumn
{
	internal GridColumn(Grid grid)
	{
		Grid = grid;
		Header = new(this);
	}

	internal GridColumn(Grid grid, string headerText) : this(grid)
	{
		if (!string.IsNullOrEmpty(headerText))
		{ Header.Content = headerText; }
	}

	internal GridColumn(Grid grid, string headerText, HorizontalAlignment alignment) : this(grid, headerText)
	{
		CellLayout.HorizontalAlignment = alignment;
	}


	/// <summary>
	/// The <see cref="Grid"/> object this column belongs to
	/// </summary>
	public Grid Grid { get; }

	/// <summary>
	/// The zero-based index of this column within the grid's <see cref="Grid.Columns"/> collection.
	/// </summary>
	public int Index => Grid.Columns.IndexOf(this);


	/// <summary>
	/// Returns a sequence containing the <see cref="GridCell"/> objects associated with the column.
	/// Each column will always have exactly <see cref="Grid.RowCount"/> cells.
	/// </summary>
	public IEnumerable<GridCell> Cells
	{
		get
		{
			if (Grid == null || Grid.Rows == null || !Grid.Rows.Any())
			{ return Enumerable.Empty<GridCell>(); }

			int x = Index;
			return Grid.Rows.Select(r => r.Cells[x]);
		}
	}

	/// <summary>
	/// Returns true if the column has at least one cell (i.e. the grid has at least one row), otherwise false. 
	/// If true, the <see cref="Cells"/> property will contain at least one element.
	/// </summary>
	public bool HasCells => Grid.Rows.Any();


	/// <summary>
	/// The header for the column.
	/// </summary>
	public GridHeaderCell Header { get; set; }


	#region STYLE

	/// <summary>
	/// The text styling to be applied to ALL the column's cells. 
	/// <para>Setting this to a value other than null overrides cell styling (<see cref="Grid.CellStyle"/>) inherited from the grid.</para>
	/// </summary>
	public TextStyle? CellStyle { get; set; }

	/// <summary>
	/// The "internal" (to the class) cell style. This property returns the correct
	/// <see cref="TextStyle"/> object for the class to use internally.
	/// </summary>
	internal protected TextStyle CellStyleI => CellStyle ?? Grid.CellStyle;

	/// <summary>
	/// The text styling to be applied to the content of ALL the column's cells. 
	/// <para>Setting this to a value other than null overrides cell content styling (<see cref="Grid.CellContentStyle"/>) inherited from the grid.</para>
	/// </summary>
	public TextStyle? ContentStyle { get; set; }

	/// <summary>
	/// The "internal" (to the class) content style. This property returns the correct
	/// <see cref="TextStyle"/> object for the class to use internally.
	/// </summary>
	internal protected TextStyle ContentStyleI => ContentStyle ?? Grid.CellContentStyle;

	#endregion STYLE


	#region LAYOUT

	/// <summary>
	/// The layout for cells in this column
	/// </summary>
	public GridCellLayout CellLayout = new GridCellLayout();

	#endregion LAYOUT


	/// <summary>
	/// The Max value of the <see cref="GridCellBase.ContentWidth"/> property of all the column's cells . (0 or more)
	/// </summary>
	public int ContentWidth
	{
		get
		{
			int l = HasCells ? Cells.Max(c => c.ContentWidth) : 0;
			if (Header != null)
			{ l = Math.Max(l, Header.ContentWidth); }

			return l;
		}
	}

	/// <summary>
	/// The Max value of the <see cref="GridCellBase.CellWidth"/> property of all the column's cells . (0 or more)
	/// </summary>
	public int CellWidth
	{
		get
		{
			int l = HasCells ? Cells.Max(c => c.CellWidth) : 0;
			if (Header != null)
			{ l = Math.Max(l, Header.CellWidth); }

			return l;
		}
	}

	/// <summary>
	/// The Max value of the <see cref="GridCellBase.TotalWidth"/> property of all the column's cells . (0 or more)
	/// </summary>
	public int TotalWidth
	{
		get
		{
			int l = HasCells ? Cells.Max(c => c.TotalWidth) : 0;
			if (Header != null)
			{ l = Math.Max(l, Header.TotalWidth); }

			return l;
		}
	}


	#region FIND

	/// <summary>
	/// <span id='searchType'>Searches the Column's cells (<see cref="GridColumn.Cells"/>) and </span>
	/// <inheritdoc cref="GridExtensions.Find(IEnumerable{GridCell}, string?, SearchType)" path="/summary/span[@id='rtype']"/>
	/// <inheritdoc cref="GridExtensions.Find(IEnumerable{GridCell}, string?, SearchType)" path="/summary/span[@id='desc']"/>
	/// </summary>
	/// <param name="searchText"><inheritdoc cref="GridExtensions.Find(IEnumerable{GridCell}, string?, SearchType)" path="/param[@name='searchText']"/></param>
	/// <param name="searchType"><inheritdoc cref="GridExtensions.Find(IEnumerable{GridCell}, string?, SearchType)" path="/param[@name='searchType']"/></param>
	public IEnumerable<GridCell> Find(string? searchText, SearchType searchType = SearchType.Contains) => Cells.Find(searchText, searchType);

	/// <summary>
	/// <inheritdoc cref="GridColumn.Find(string?, SearchType)" path="/summary/span[@id='searchType']"/>
	/// returns the first cell 
	/// <inheritdoc cref="GridExtensions.Find(IEnumerable{GridCell}, string?, SearchType)" path="/summary/span[@id='desc']"/>
	/// </summary>
	/// <param name="searchText"><inheritdoc cref="GridExtensions.Find(IEnumerable{GridCell}, string?, SearchType)" path="/param[@name='searchText']"/></param>
	/// <param name="searchType"><inheritdoc cref="GridExtensions.Find(IEnumerable{GridCell}, string?, SearchType)" path="/param[@name='searchType']"/></param>
	public GridCell? FindFirst(string? searchText, SearchType searchType = SearchType.Contains) => Find(searchText, searchType).FirstOrDefault();

	/// <summary>
	/// <inheritdoc cref="GridColumn.Find(string?, SearchType)" path="/summary/span[@id='searchType']"/>
	/// returns the <see cref="GridRow"/> object for the first cell
	/// <inheritdoc cref="GridExtensions.Find(IEnumerable{GridCell}, string?, SearchType)" path="/summary/span[@id='desc']"/>
	/// </summary>
	/// <param name="searchText"><inheritdoc cref="GridExtensions.Find(IEnumerable{GridCell}, string?, SearchType)" path="/param[@name='searchText']"/></param>
	/// <param name="searchType"><inheritdoc cref="GridExtensions.Find(IEnumerable{GridCell}, string?, SearchType)" path="/param[@name='searchType']"/></param>
	public GridRow? FindFirstRow(string? searchText, SearchType searchType = SearchType.Contains)
	{
		var cell = FindFirst(searchText, searchType);
		if (cell == null)
		{ return null; }

		return cell.Row;
	}

	/// <summary>
	/// <inheritdoc cref="GridColumn.Find(string?, SearchType)" path="/summary/span[@id='searchType']"/>
	/// returns a sequence of <see cref="GridRow"/> objects for the cells 
	/// <inheritdoc cref="GridExtensions.Find(IEnumerable{GridCell}, string?, SearchType)" path="/summary/span[@id='desc']"/>
	/// </summary>
	/// <param name="searchText"><inheritdoc cref="GridExtensions.Find(IEnumerable{GridCell}, string?, SearchType)" path="/param[@name='searchText']"/></param>
	/// <param name="searchType"><inheritdoc cref="GridExtensions.Find(IEnumerable{GridCell}, string?, SearchType)" path="/param[@name='searchType']"/></param>
	public IEnumerable<GridRow> FindRows(string? searchText, SearchType searchType = SearchType.Contains)
	{
		var cells = Find(searchText, searchType);
		if (cells == null)
		{ return Enumerable.Empty<GridRow>(); }

		return cells.Select(cell => cell.Row);
	}

	#endregion FIND
}
