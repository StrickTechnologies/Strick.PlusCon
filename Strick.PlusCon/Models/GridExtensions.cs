using System;
using System.Collections.Generic;
using System.Linq;


namespace Strick.PlusCon.Models;


/// <summary>
/// Contains extension methods for <see cref="Grid"/> and related classes.
/// </summary>
public static class GridExtensions
{
	/// <summary>
	/// Searches the <paramref name="cells"/> argument and 
	/// <span id='rtype'>returns a sequence of <see cref="GridCell"/> objects (or an empty set if no matching cells)</span>
	/// <span id='desc'>whose <see cref="GridCellBase.Content"/> property 
	/// matches the <see cref="GridSearchExpression.Text"/> property of the <paramref name="searchExpression"/> argument. 
	/// The various properties of the <paramref name="searchExpression"/> argument (<see cref="GridSearchExpression"/>) specify the type of search that is performed.
	/// </span>
	/// </summary>
	/// <param name="cells">The <see cref="GridCell"/> objects to search.</param>
	/// <param name="searchExpression">The text to search for. See <see cref="GridSearchExpression"/> for information regarding specific properties and functionality.</param>
	/// <exception cref="ArgumentOutOfRangeException"></exception>
	public static IEnumerable<GridCell> Find(this IEnumerable<GridCell> cells, GridSearchExpression searchExpression)
	{
		if (cells == null)
		{ return Enumerable.Empty<GridCell>(); }

		if(searchExpression == null)
		{ return cells.Where(cell => cell.Content == null); }

		if (string.IsNullOrEmpty(searchExpression.Text))
		{ return cells.Where(cell => cell.Content == searchExpression.Text); }

		switch (searchExpression.Type)
		{
			case SearchType.Contains:
			{
				return cells.Where(cell => cell.Content != null && cell.Content.Contains(searchExpression.Text, searchExpression.ComparisonType));
			}

			case SearchType.Equals:
			{
				return cells.Where(cell => searchExpression.Text.Equals(cell.Content, searchExpression.ComparisonType));
			}

			case SearchType.StartsWith:
			{
				return cells.Where(cell => cell.Content != null && cell.Content.StartsWith(searchExpression.Text, searchExpression.ComparisonType));
			}

			case SearchType.EndsWith:
			{
				return cells.Where(cell => cell.Content != null && cell.Content.EndsWith(searchExpression.Text, searchExpression.ComparisonType));
			}
		}

		throw new ArgumentOutOfRangeException(nameof(searchExpression.Type), "Invalid search type");
	}

	/// <summary>
	/// Sets the <see cref="GridCellBase.FillerChar"/> property of each object in the <paramref name="cells"/> sequence 
	/// to the value of the <paramref name="fillerChar"/> argument.
	/// </summary>
	/// <param name="cells">A sequence of <see cref="GridCell"/> objects.</param>
	/// <param name="fillerChar">A char value.</param>
	public static void SetFillerChar(this IEnumerable<GridCell> cells, char fillerChar)
	{
		if (!cells.HasAny())
		{ return; }

		foreach (GridCell cell in cells)
		{ cell.FillerChar = fillerChar; }
	}
}
