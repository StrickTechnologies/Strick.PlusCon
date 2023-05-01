using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Strick.PlusCon.Models;


/// <summary>
/// <inheritdoc cref="GridCellBase"/>
/// </summary>
public class GridCell : GridCellBase
{
	/// <summary>
	/// Creates a new cell in the row specified by <paramref name="row"/>.
	/// </summary>
	/// <param name="row"></param>
	internal GridCell(GridRow row)
	{
		Row = row;
	}


	/// <summary>
	/// <inheritdoc cref="GridCellBase.Grid"/>
	/// </summary>
	public override Grid Grid => Row.Grid;

	/// <summary>
	/// The row the cell belongs to
	/// </summary>
	public virtual GridRow Row { get; }

	/// <summary>
	/// The zero-based index of the cell's <see cref="Row"/> within the grid's <see cref="Grid.Rows"/> collection.
	/// </summary>
	public virtual int RowIndex => Grid.Rows.IndexOf(Row);

	/// <summary>
	/// <inheritdoc cref="GridCellBase.Column"/>
	/// </summary>
	public override GridColumn Column => Row.Grid.Columns[ColumnIndex];

	/// <summary>
	/// <inheritdoc cref="GridCellBase.ColumnIndex"/>
	/// </summary>
	public override int ColumnIndex => Row.Cells.IndexOf(this);


	/// <summary>
	/// <inheritdoc cref="GridCellBase.ContentStyleI"/>
	/// </summary>
	internal protected override TextStyle ContentStyleI
	{
		get
		{
			//first, if we have a custom style for the cell, return that
			if (ContentStyle != null)
			{ return ContentStyle; }

			//next, if the row has a style, return that
			if (Row.ContentStyle != null)
			{ return Row.ContentStyle; }

			return Column.ContentStyleI;
		}
	}

	/// <summary>
	/// <inheritdoc cref="GridCellBase.CellStyleI"/>
	/// </summary>
	internal protected override TextStyle CellStyleI
	{
		get
		{
			if (CellStyle != null)
			{ return CellStyle; }

			if (Row.CellStyle != null)
			{ return Row.CellStyle; }

			return Column.CellStyleI;
		}
	}
}
