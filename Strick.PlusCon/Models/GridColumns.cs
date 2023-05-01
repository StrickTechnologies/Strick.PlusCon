using System;
using System.Collections;
using System.Collections.Generic;


namespace Strick.PlusCon.Models;


/// <summary>
/// A collection of <see cref="GridColumn"/> objects that belong to a grid.
/// </summary>
public class GridColumns : IReadOnlyList<GridColumn>
{
	internal GridColumns(Grid g)
	{
		Grid = g;
	}

	private readonly List<GridColumn> cols = new List<GridColumn>();
	
	/// <summary>
	/// The <see cref="Grid"/> object these columns belong to.
	/// </summary>
	public Grid Grid { get; }

	#region IREADONLYLIST

	/// <inheritdoc/>
	public GridColumn this[int index] => ((IReadOnlyList<GridColumn>)cols)[index];

	/// <inheritdoc/>
	public int Count => ((IReadOnlyCollection<GridColumn>)cols).Count;

	/// <inheritdoc/>
	public IEnumerator<GridColumn> GetEnumerator() => ((IEnumerable<GridColumn>)cols).GetEnumerator();

	/// <inheritdoc/>
	IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)cols).GetEnumerator();

	#endregion IREADONLYLIST


	/// <summary>
	/// Returns the zero-based index of <paramref name="col"/> within the collection of columns. 
	/// If <paramref name="col"/> is not found in the collection, -1 is returned.
	/// </summary>
	/// <param name="col"></param>
	/// <returns></returns>
	public int IndexOf(GridColumn col) => cols.IndexOf(col);

	/// <summary>
	/// Adds a new <see cref="GridColumn"/> to the end of the collection and 
	/// returns the newly added <see cref="GridColumn"/> object.
	/// </summary>
	/// <returns>The newly added <see cref="GridColumn"/> object.</returns>
	public GridColumn Add() => Add("");

	/// <summary>
	/// <inheritdoc cref="Add()"/>
	/// </summary>
	/// <param name="headerText">The text to be displayed in the column header.</param>
	/// <returns><inheritdoc cref="Add()"/></returns>
	public GridColumn Add(string headerText) => Add(headerText, HorizontalAlignment.Left);

	/// <summary>
	/// <inheritdoc cref="Add()"/>
	/// </summary>
	/// <param name="headerText"><inheritdoc cref="Add(string)" path="/param[@name='headerText']" /></param>
	/// <param name="alignment">The horizontal alignment for the column's cells</param>
	/// <returns><inheritdoc cref="Add()"/></returns>
	public GridColumn Add(string headerText, HorizontalAlignment alignment)
	{
		var c = new GridColumn(Grid, headerText, alignment);
		cols.Add(c);

		if (Grid.Rows.Count > 0)
		{
			//add new cell in each row...
			foreach (var row in Grid.Rows)
			{ row.AddCell(); }
		}

		return c;
	}


	/// <summary>
	/// Removes <paramref name="col"/> from the grid. Returns true if the column is removed, or false if the column is not found in the grid.
	/// If the grid contains any rows, the corresponding cell is removed from each row.
	/// </summary>
	/// <param name="col"></param>
	/// <returns>true if the column is removed, false if the column is not found in the grid.</returns>
	/// <exception cref="ArgumentNullException"></exception>
	public bool Remove(GridColumn col)
	{
		if (col == null)
		{ throw new ArgumentNullException(nameof(col)); }

		var x = cols.IndexOf(col);
		if (x == -1)
		{ return false; }

		RemoveAt(x);
		return true;
	}

	/// <summary>
	/// Removes the column at the specified index of the collection of columns.
	/// </summary>
	/// <param name="index"></param>
	/// <exception cref="ArgumentOutOfRangeException"></exception>
	public void RemoveAt(int index)
	{
		if (index < 0 || index > (Count - 1))
		{ throw new ArgumentOutOfRangeException(nameof(index)); }

		cols.RemoveAt(index);
		if (Grid.Rows.Count > 0)
		{
			//remove the corresponding cell from each row...
			foreach (var row in Grid.Rows)
			{ row.RemoveCellAt(index); }
		}
	}
}
