using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Strick.PlusCon.Models;


/// <summary>
/// Represents a cell within a grid
/// </summary>
public abstract class GridCellBase
{
	/// <summary>
	/// The grid the cell belongs to
	/// </summary>
	public abstract Grid Grid { get; }

	/// <summary>
	/// The column the cell belongs to
	/// </summary>
	public abstract GridColumn Column { get; }

	/// <summary>
	/// The zero-based index of the cell's <see cref="GridColumn"/> within the grid's <see cref="Grid.Columns"/> collection.
	/// </summary>
	public abstract int ColumnIndex { get; }


	/// <summary>
	/// The content of the cell
	/// </summary>
	public string? Content { get; set; }

	/// <summary>
	/// Returns true if the cell's content is not null/empty.
	/// </summary>
	[MemberNotNullWhen(true, nameof(Content))]
	public bool HasContent => !string.IsNullOrEmpty(Content);


	#region STYLE

	/// <summary>
	/// The style applied to the cell content. The default is null.
	/// <para>If null, the cell will get its content style from the row, the column or the grid. 
	/// If NOT null, this style will override any cell conent styles set at the row, column or grid levels.</para>
	/// </summary>
	public virtual TextStyle? ContentStyle { get; set; }

	/// <summary>
	/// The "internal" (to the class) content style. This property returns the correct
	/// <see cref="TextStyle"/> object for the class to use internally.
	/// </summary>
	internal protected abstract TextStyle ContentStyleI { get; }


	/// <summary>
	/// The style applied to the cell itself (NOT the content, see also <see cref="ContentStyle"/>. The default is null.
	/// <para>If null, the cell will get its content style from the row, the column or the grid. 
	/// If NOT null, this style will override any cell styles set at the row, column or grid levels.</para>
	/// </summary>
	public virtual TextStyle? CellStyle { get; set; }

	/// <summary>
	/// The "internal" (to the class) cell style. This property returns the correct
	/// <see cref="TextStyle"/> object for the class to use internally.
	/// </summary>
	internal protected abstract TextStyle CellStyleI { get; }

	#endregion STYLE


	#region LAYOUT

	//possibly for later implementation. there are complexities involved with overriding padding and margins at the cell level, and it's not needed at this point
	//public virtual TextLayout? Layout { get; set; }

	private HorizontalAlignment? ha = null;

	/// <summary>
	/// Gets/Sets a value for the cell content's horizontal alignment. 
	/// If set to a value other than null, this will override the value for the <see cref="GridCellLayout"/> for the column.
	/// </summary>
	public HorizontalAlignment HorizontalAlignment
	{
		get => ha ?? LayoutI.HorizontalAlignment;
		set => ha = value;
	}

	/// <summary>
	/// The "internal" (to the class) cell layout. This property returns the correct
	/// <see cref="GridCellLayout"/> object for the class to use internally.
	/// </summary>
	internal protected virtual GridCellLayout LayoutI => Column.CellLayout;

	#endregion LAYOUT


	/// <summary>
	/// Width (Length) of <see cref="Content"/> property. (0 or more)
	/// </summary>
	public int ContentWidth => HasContent ? Content.Length : 0;

	/// <summary>
	/// The width of the cell. Calculated as <see cref="ContentWidth"/> + left/right padding (<see cref="GridColumn.CellLayout"/>).
	/// Padding is included "inside" the cell, but is not considered part of the content.
	/// </summary>
	public int CellWidth => ContentWidth + LayoutI.PaddingLeft + LayoutI.PaddingRight;

	/// <summary>
	/// The width of the cell, including margins. 
	/// <para>Calculated as <see cref="CellWidth"/> + left/right margins (<see cref="GridColumn.CellLayout"/>).
	/// Margins are "outside" the cell, but are included in the total width of the column (<see cref="GridColumn"/>).</para>
	/// </summary>
	public int TotalWidth => CellWidth + LayoutI.MarginLeft + LayoutI.MarginRight;


	/// <summary>
	/// Returns the cell's content rendered for display within the grid (all text styling, cell styling, padding and margins applied)
	/// </summary>
	internal string RenderedContent
	{
		get
		{
			if (HasContent)
			{
				string rc;

				rc = ContentStyleI.StyleText(Content);
				return new string(LayoutI.MarginLeftChar, LayoutI.MarginLeft) + AlignContent(rc) + new string(LayoutI.MarginRightChar, LayoutI.MarginRight);
			}

			return new string(LayoutI.MarginLeftChar, LayoutI.MarginLeft) + CellStyleI.StyleText(new string(' ', Column.CellWidth)) + new string(LayoutI.MarginRightChar, LayoutI.MarginRight);
		}
	}

	/// <summary>
	/// Aligns the content within the cell
	/// </summary>
	/// <param name="content"></param>
	/// <returns></returns>
	protected string AlignContent(string content)
	{
		char fillChar = ' ';
		string pl = "";
		string pr = "";

		int cellW = Column.CellWidth;

		//**** LEFT ****
		if (HorizontalAlignment == Models.HorizontalAlignment.Left)
		{ pr = new string(fillChar, cellW - ContentWidth - LayoutI.TotalPadding); }

		//**** CENTER ****
		else if (HorizontalAlignment == HorizontalAlignment.Center)
		{
			var l = (cellW - ContentWidth - LayoutI.TotalPadding) / 2;
			pl = new string(fillChar, l);
			pr = new string(fillChar, cellW - ContentWidth - LayoutI.TotalPadding - l);
		}

		//**** RIGHT ****
		else //if (HorizontalAlignment == HorizontalAlignment.Right) 
		{
			pl = new string(fillChar, cellW - ContentWidth - LayoutI.TotalPadding);
		}

		if (LayoutI.PaddingLeft > 0)
		{ pl += new string(LayoutI.PaddingLeftChar, LayoutI.PaddingLeft); }
		if (LayoutI.PaddingRight > 0)
		{ pr = new string(LayoutI.PaddingRightChar, LayoutI.PaddingRight) + pr; }

		if (!string.IsNullOrEmpty(pl))
		{ pl = CellStyleI.StyleText(pl); }

		if (!string.IsNullOrEmpty(pr))
		{ pr = CellStyleI.StyleText(pr); }

		return pl + content + pr;
	}
}
