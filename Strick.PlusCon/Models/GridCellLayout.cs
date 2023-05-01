using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Strick.PlusCon.Models;


/// <summary>
/// Specifies how content is laid out within a <see cref="GridColumn"/>'s cells
/// </summary>
public class GridCellLayout
{
	/// <summary>
	/// <inheritdoc cref="Models.HorizontalAlignment"/>
	/// </summary>
	public HorizontalAlignment HorizontalAlignment { get; set; } = HorizontalAlignment.Left;

	/// <summary>
	/// Specifies the number of charcters for the left margin of a grid cell. 
	/// <para>Margins are NOT considered part of the cell, and is rendered "outside" the cell.</para>
	/// </summary>
	public int MarginLeft { get; set; } = 1;

	/// <summary>
	/// Specifies the number of charcters for the right margin of a grid cell. 
	/// <para>Margins are NOT considered part of the cell, and is rendered "outside" the cell.</para>
	/// </summary>
	public int MarginRight { get; set; } = 1;

	/// <summary>
	/// Total the number of charcters for the margins (both left and right) of a grid cell. 
	/// <para>Margins are NOT considered part of the cell, and is rendered "outside" the cell.</para>
	/// </summary>
	public int TotalMargin => MarginLeft + MarginRight;

	/// <summary>
	/// Specifies the charcter that will be rendered outside grid cell for the left margin.
	/// </summary>
	public char MarginLeftChar { get; set; } = ' ';

	/// <summary>
	/// Specifies the charcter that will be rendered outside grid cell for the right margin.
	/// </summary>
	public char MarginRightChar { get; set; } = ' ';


	/// <summary>
	/// Specifies the number of charcters for the left padding of a grid cell. 
	/// <para>Padding is considered part of the cell, and is rendered "inside" the cell.</para>
	/// </summary>
	public int PaddingLeft { get; set; }

	/// <summary>
	/// Specifies the number of charcters for the right padding of a grid cell. 
	/// <para>Padding is considered part of the cell, and is rendered "inside" the cell.</para>
	/// </summary>
	public int PaddingRight { get; set; }

	/// <summary>
	/// Total the number of charcters for the padding (both left and right) of a grid cell. 
	/// <para>Padding is considered part of the cell, and is rendered "inside" the cell.</para>
	/// </summary>
	public int TotalPadding => PaddingLeft + PaddingRight;

	/// <summary>
	/// Specifies the charcter that will be rendered inside the grid cell for the left padding.
	/// </summary>
	public char PaddingLeftChar { get; set; } = ' ';

	/// <summary>
	/// Specifies the charcter that will be rendered inside the grid cell for the right padding.
	/// </summary>
	public char PaddingRightChar { get; set; } = ' ';
}


/// <summary>
/// Specifies how content is horizontally aligned
/// </summary>
public enum HorizontalAlignment
{
	/// <summary>
	/// Content is aligned at the left edge of the parent
	/// </summary>
	Left = 0,

	/// <summary>
	/// Content is centered within the parent
	/// </summary>
	Center = 1,

	/// <summary>
	/// Content is aligned at the right edge of the parent
	/// </summary>
	Right = 2,
}
