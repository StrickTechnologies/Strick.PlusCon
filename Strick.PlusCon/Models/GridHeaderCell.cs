using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Strick.PlusCon.Models;


/// <summary>
/// Represents a header cell within a grid column
/// </summary>
public class GridHeaderCell : GridCellBase
{
	/// <summary>
	/// Creates a new header cell in the column specified by <paramref name="column"/>.
	/// </summary>
	/// <param name="column"></param>
	public GridHeaderCell(GridColumn column)
	{
		col = column;
	}

	/// <summary>
	/// <inheritdoc cref="GridCellBase.Grid"/>
	/// </summary>
	public override Grid Grid => Column.Grid;


	private readonly GridColumn col;

	/// <summary>
	/// <inheritdoc cref="GridCellBase.Column"/>
	/// </summary>
	public override GridColumn Column => col;

	/// <summary>
	/// <inheritdoc cref="GridCellBase.ColumnIndex"/>
	/// </summary>
	public override int ColumnIndex => Grid.Columns.IndexOf(Column);


	/// <summary>
	/// <inheritdoc cref="GridCellBase.ContentStyleI"/>
	/// </summary>
	internal protected override TextStyle ContentStyleI => ContentStyle ?? Grid.ColumnHeaderContentStyle;

	/// <summary>
	/// <inheritdoc cref="GridCellBase.CellStyleI"/>
	/// </summary>
	internal protected override TextStyle CellStyleI => CellStyle ?? Grid.ColumnHeaderCellStyle;
}
