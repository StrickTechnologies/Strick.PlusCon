using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Strick.PlusCon.Models;


/// <summary>
/// A sequence containing the cells (<see cref="GridCell"/>) for a given row <see cref="GridRow"/>. 
/// Implements <see cref="IReadOnlyList{T}"/>, along with some other methods that allow the list to be manipulated.
/// </summary>
public class GridRowCells : IReadOnlyList<GridCell>
{
	internal GridRowCells(GridRow row)
	{
		Row = row;
	}


	private readonly List<GridCell> cells = new List<GridCell>();

	/// <summary>
	/// The row (<see cref="GridRow"/>) the cells belong to.
	/// </summary>
	public GridRow Row { get; }


	#region IREADONLYLIST

	/// <inheritdoc/>
	public GridCell this[int index] => ((IReadOnlyList<GridCell>)cells)[index];

	/// <inheritdoc/>
	public int Count => ((IReadOnlyCollection<GridCell>)cells).Count;

	/// <inheritdoc/>
	public IEnumerator<GridCell> GetEnumerator() => ((IEnumerable<GridCell>)cells).GetEnumerator();

	/// <inheritdoc/>
	IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)cells).GetEnumerator();

	#endregion IREADONLYLIST


	/// <summary>
	/// The zero-based index of <paramref name="cell"/> within the sequence. 
	/// Returns -1 if <paramref name="cell"/> is not found in the sequence.
	/// </summary>
	/// <param name="cell"></param>
	public int IndexOf(GridCell cell) => cells.IndexOf(cell);

	internal GridCell Add()
	{
		var c = new GridCell(Row);
		Add(c);
		return c;
	}

	internal void Add(GridCell cell)
	{ cells.Add(cell); }

	internal bool Remove(GridCell cell)
	{
		if (cell == null)
		{ throw new ArgumentNullException(nameof(cell)); }

		var x = cells.IndexOf(cell);
		if (x == -1)
		{ return false; }

		RemoveAt(x);
		return true;
	}

	internal void RemoveAt(int index)
	{
		if (index < 0 || index > (Count - 1))
		{ throw new ArgumentOutOfRangeException(nameof(index)); }

		cells.RemoveAt(index);
	}
}