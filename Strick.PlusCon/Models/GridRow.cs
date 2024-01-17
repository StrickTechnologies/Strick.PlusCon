using System;
using System.Collections.Generic;
using System.Linq;


namespace Strick.PlusCon.Models;


/// <summary>
/// Represents a row in a <see cref="Grid"/> object.
/// </summary>
public class GridRow
{
	/// <summary>
	/// Creates a new row for <paramref name="grid"/>.
	/// </summary>
	/// <param name="grid"></param>
	/// <exception cref="ArgumentNullException"></exception>
	public GridRow(Grid grid)
	{
		Grid = grid ?? throw new ArgumentNullException(nameof(grid));

		Cells = new GridRowCells(this);
		CreateDefaultCells();
	}

	/// <summary>
	/// <inheritdoc cref="GridRow(Grid)"/>
	/// The <paramref name="cellContent"/> array maps to the <seealso cref="Cells"/> of the row. 
	/// If the number of elements in <paramref name="cellContent"/> is less than the number of columns in the grid, the remaining cells default to having content = null. 
	/// If the number of elements in <paramref name="cellContent"/> is more than the number of columns in the grid, an exception is thrown.
	/// </summary>
	/// <param name="grid"></param>
	/// <param name="cellContent"></param>
	/// <exception cref="ArgumentException"></exception>
	public GridRow(Grid grid, params string?[] cellContent) : this(grid)
	{
		if (cellContent.Length > grid.Columns.Count)
		{ throw new ArgumentException("Too many cells", nameof(cellContent)); }

		int col = 0;
		foreach (string? val in cellContent)
		{ Cells[col++].Content = val; }
	}

	/// <summary>
	/// <inheritdoc cref="GridRow(Grid)"/>
	/// The <paramref name="cellContent"/> array maps to the <seealso cref="Cells"/> of the row. 
	/// Values in <paramref name="cellContent"/> that are not strings will be converted to strings using their <see cref="object.ToString"/> method. 
	/// If the number of elements in <paramref name="cellContent"/> is less than the number of columns in the grid, the remaining cells default to having content = null. 
	/// If the number of elements in <paramref name="cellContent"/> is more than the number of columns in the grid, an exception is thrown.
	/// </summary>
	/// <param name="grid"></param>
	/// <param name="cellContent"></param>
	/// <exception cref="ArgumentException"></exception>
	public GridRow(Grid grid, params object?[] cellContent) : this(grid)
	{
		if (cellContent.Length > grid.Columns.Count)
		{ throw new ArgumentException("Too many cells", nameof(cellContent)); }

		int col = 0;
		foreach (object? val in cellContent)
		{
			if (val == null)
			{
				//cell content defaults to null, no need to set it again
				col++;
			}
			else if (val is string v)
			{ Cells[col++].Content = v; }
			else
			{ Cells[col++].Content = val.ToString(); }
		}
	}

	//not used. possible future development
	//public GridRow(Grid grid, IEnumerable<GridCell> cells)
	//{
	//	if (grid == null)
	//	{ throw new ArgumentNullException(nameof(grid)); }
	//	Grid = grid;

	//	if (cells == null)
	//	{ CreateDefaultCells(); }
	//	else
	//	{
	//		if (cells.Count() != grid.Columns.Count)
	//		{ throw new ArgumentException("Wrong number of cells", nameof(cells)); }
	//		Cells = cells.ToList();
	//	}
	//}


	/// <summary>
	/// The grid this row belongs to.
	/// </summary>
	public Grid Grid { get; }

	/// <summary>
	/// The index of this row within the grid's <see cref="Grid.Rows"/> collection.
	/// </summary>
	public int Index => Grid.Rows.IndexOf(this);


	/// <summary>
	/// Returns a sequence containing the <see cref="GridCell"/> objects associated with the row. 
	/// Each row will always have exactly <see cref="Grid.ColumnCount"/> cells.
	/// </summary>
	public GridRowCells Cells { get; }

	/// <summary>
	/// Returns true if the row has at least one cell (i.e. the grid has at least one column), otherwise false. 
	/// If true, the <see cref="Cells"/> property will contain at least one element.
	/// </summary>
	public bool HasCells => Cells.Any();


	internal void RemoveCellAt(int index)
	{
		if (index < 0 || index > Cells.Count - 1)
		{ throw new ArgumentOutOfRangeException(nameof(index)); }

		Cells.RemoveAt(index);
	}

	internal void AddCell()
	{ Cells.Add(); }


	private void CreateDefaultCells()
	{
		for (int i = 0; i < Grid.ColumnCount; i++)
		{ Cells.Add(CreateDefaultCell()); }
	}

	private GridCell CreateDefaultCell() => new GridCell(this);


	#region STYLE

	/// <summary>
	/// The text styling to be applied to the content of ALL the row's cells. 
	/// <para>Setting this to a value other than null overrides the styling for the row's cell content inherited from the column or grid.</para>
	/// </summary>
	public virtual TextStyle? ContentStyle { get; set; }

	/// <summary>
	/// The text styling to be applied to ALL the row's cells. 
	/// <para>Setting this to a value other than null overrides the styling for the row's cell content inherited from the column or grid.</para>
	/// </summary>
	public virtual TextStyle? CellStyle { get; set; }

	#endregion STYLE


	#region FIND

	/// <summary>
	/// <span id='searchType'>Searches the Row's cells (<see cref="GridRow.Cells"/>) and </span>
	/// <inheritdoc cref="GridExtensions.Find(IEnumerable{GridCell}, GridSearchExpression)" path="/summary/span[@id='rtype']"/>
	/// <inheritdoc cref="GridExtensions.Find(IEnumerable{GridCell}, GridSearchExpression)" path="/summary/span[@id='desc']"/>
	/// </summary>
	/// <param name="searchExpression"><inheritdoc cref="GridExtensions.Find(IEnumerable{GridCell}, GridSearchExpression)" path="/param[@name='searchExpression']"/></param>
	public IEnumerable<GridCell> Find(GridSearchExpression searchExpression) => Cells.Find(searchExpression);

	/// <summary>
	/// <inheritdoc cref="GridRow.Find(GridSearchExpression)" path="/summary/span[@id='searchType']"/>
	/// returns the first cell (or null if no matching cells)
	/// <inheritdoc cref="GridExtensions.Find(IEnumerable{GridCell}, GridSearchExpression)" path="/summary/span[@id='desc']"/>
	/// </summary>
	/// <param name="searchExpression"><inheritdoc cref="GridExtensions.Find(IEnumerable{GridCell}, GridSearchExpression)" path="/param[@name='searchExpression']"/></param>
	public GridCell? FindFirst(GridSearchExpression searchExpression) => Cells.Find(searchExpression).FirstOrDefault();

	/// <summary>
	/// <inheritdoc cref="GridRow.Find(GridSearchExpression)" path="/summary/span[@id='searchType']"/>
	/// returns the <see cref="GridColumn"/> object (or null if no matching cells) for the first cell 
	/// <inheritdoc cref="GridExtensions.Find(IEnumerable{GridCell}, GridSearchExpression)" path="/summary/span[@id='desc']"/>
	/// </summary>
	/// <param name="searchExpression"><inheritdoc cref="GridExtensions.Find(IEnumerable{GridCell}, GridSearchExpression)" path="/param[@name='searchExpression']"/></param>
	public GridColumn? FindFirstColumn(GridSearchExpression searchExpression)
	{
		var cell = FindFirst(searchExpression);
		if (cell == null)
		{ return null; }

		return cell.Column;
	}

	/// <summary>
	/// <inheritdoc cref="GridRow.Find(GridSearchExpression)" path="/summary/span[@id='searchType']"/>
	/// returns a sequence of <see cref="GridColumn"/> objects (or an empty set if no matching cells) for the cells 
	/// <inheritdoc cref="GridExtensions.Find(IEnumerable{GridCell}, GridSearchExpression)" path="/summary/span[@id='desc']"/>
	/// </summary>
	/// <param name="searchExpression"><inheritdoc cref="GridExtensions.Find(IEnumerable{GridCell}, GridSearchExpression)" path="/param[@name='searchExpression']"/></param>
	public IEnumerable<GridColumn> FindColumns(GridSearchExpression searchExpression)
	{
		var cells = Find(searchExpression);
		if (cells == null)
		{ return Enumerable.Empty<GridColumn>(); }

		return cells.Select(cell => cell.Column);
	}

	///// <summary>
	///// <span id='searchType'>Searches the Row's cells (<see cref="GridRow.Cells"/>) and </span>
	///// <inheritdoc cref="GridExtensions.Find(IEnumerable{GridCell}, string?, SearchType)" path="/summary/span[@id='rtype']"/>
	///// <inheritdoc cref="GridExtensions.Find(IEnumerable{GridCell}, string?, SearchType)" path="/summary/span[@id='desc']"/>
	///// </summary>
	///// <param name="searchText"><inheritdoc cref="GridExtensions.Find(IEnumerable{GridCell}, string?, SearchType)" path="/param[@name='searchText']"/></param>
	///// <param name="searchType"><inheritdoc cref="GridExtensions.Find(IEnumerable{GridCell}, string?, SearchType)" path="/param[@name='searchType']"/></param>
	//public IEnumerable<GridCell> Find(string? searchText, SearchType searchType = SearchType.Contains) => Cells.Find(searchText, searchType);

	///// <summary>
	///// <inheritdoc cref="GridRow.Find(string?, SearchType)" path="/summary/span[@id='searchType']"/>
	///// returns the first cell
	///// <inheritdoc cref="GridExtensions.Find(IEnumerable{GridCell}, string?, SearchType)" path="/summary/span[@id='desc']"/>
	///// </summary>
	///// <param name="searchText"><inheritdoc cref="GridExtensions.Find(IEnumerable{GridCell}, string?, SearchType)" path="/param[@name='searchText']"/></param>
	///// <param name="searchType"><inheritdoc cref="GridExtensions.Find(IEnumerable{GridCell}, string?, SearchType)" path="/param[@name='searchType']"/></param>
	//public GridCell? FindFirst(string? searchText, SearchType searchType = SearchType.Contains) => Cells.Find(searchText, searchType).FirstOrDefault();

	///// <summary>
	///// <inheritdoc cref="GridRow.Find(string?, SearchType)" path="/summary/span[@id='searchType']"/>
	///// returns the <see cref="GridColumn"/> object for the first cell 
	///// <inheritdoc cref="GridExtensions.Find(IEnumerable{GridCell}, string?, SearchType)" path="/summary/span[@id='desc']"/>
	///// </summary>
	///// <param name="searchText"><inheritdoc cref="GridExtensions.Find(IEnumerable{GridCell}, string?, SearchType)" path="/param[@name='searchText']"/></param>
	///// <param name="searchType"><inheritdoc cref="GridExtensions.Find(IEnumerable{GridCell}, string?, SearchType)" path="/param[@name='searchType']"/></param>
	//public GridColumn? FindFirstColumn(string? searchText, SearchType searchType = SearchType.Contains)
	//{
	//	var cell = FindFirst(searchText, searchType);
	//	if (cell == null)
	//	{ return null; }

	//	return cell.Column;
	//}

	///// <summary>
	///// <inheritdoc cref="GridRow.Find(string?, SearchType)" path="/summary/span[@id='searchType']"/>
	///// returns a sequence of <see cref="GridColumn"/> objects for the cells 
	///// <inheritdoc cref="GridExtensions.Find(IEnumerable{GridCell}, string?, SearchType)" path="/summary/span[@id='desc']"/>
	///// </summary>
	///// <param name="searchText"><inheritdoc cref="GridExtensions.Find(IEnumerable{GridCell}, string?, SearchType)" path="/param[@name='searchText']"/></param>
	///// <param name="searchType"><inheritdoc cref="GridExtensions.Find(IEnumerable{GridCell}, string?, SearchType)" path="/param[@name='searchType']"/></param>
	//public IEnumerable<GridColumn> FindColumns(string? searchText, SearchType searchType = SearchType.Contains)
	//{
	//	var cells = Find(searchText, searchType);
	//	if (cells == null)
	//	{ return Enumerable.Empty<GridColumn>(); }

	//	return cells.Select(cell => cell.Column);
	//}

	#endregion FIND
}
