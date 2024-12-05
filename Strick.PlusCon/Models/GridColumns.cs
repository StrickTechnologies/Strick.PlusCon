using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


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

	private readonly List<GridColumn> cols = [];

	/// <summary>
	/// The <see cref="Models.Grid"/> object these columns belong to.
	/// </summary>
	public Grid Grid { get; }


	#region IREADONLYLIST

	/// <inheritdoc/>
	public GridColumn this[int index] => cols[index];

	/// <inheritdoc/>
	public int Count => cols.Count;

	/// <inheritdoc/>
	public IEnumerator<GridColumn> GetEnumerator() => cols.GetEnumerator();

	/// <inheritdoc/>
	IEnumerator IEnumerable.GetEnumerator() => cols.GetEnumerator();

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
		ArgumentNullException.ThrowIfNull(col, nameof(col));

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


	/// <summary>
	/// <inheritdoc cref="ByName(string)"/>
	/// </summary>
	/// <param name="name"><inheritdoc cref="ByName(string)" path="/param[@name='name']"/></param>
	public GridColumn? this[string name] => ByName(name);

	/// <summary>
	/// Returns the <see cref="GridColumn"/> object whose <see cref="GridColumn.Name"/> property 
	/// matches the <paramref name="name"/> argument (case insensitive).
	/// If the <paramref name="name"/> argument is null (or empty) or no column with a 
	/// matching name is found, null is returned.
	/// </summary>
	/// <param name="name">The name of the column to retrieve</param>
	public GridColumn? ByName(string name)
	{
		if (string.IsNullOrEmpty(name))
		{ return null; }

		return cols.FirstOrDefault(c => !string.IsNullOrEmpty(c.Name) && c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
	}
}
