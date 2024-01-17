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
				return cells.Where(cell => cell.Content != null && cell.Content.Contains(searchExpression.Text, StringComparison.OrdinalIgnoreCase));
			}

			case SearchType.Equals:
			{
				return cells.Where(cell => searchExpression.Text.Equals(cell.Content, StringComparison.OrdinalIgnoreCase));
			}

			case SearchType.StartsWith:
			{
				return cells.Where(cell => cell.Content != null && cell.Content.StartsWith(searchExpression.Text, StringComparison.OrdinalIgnoreCase));
			}

			case SearchType.EndsWith:
			{
				return cells.Where(cell => cell.Content != null && cell.Content.EndsWith(searchExpression.Text, StringComparison.OrdinalIgnoreCase));
			}
		}

		throw new ArgumentOutOfRangeException(nameof(searchExpression.Type), "Invalid search type");
	}

	/// <summary>
	/// Searches the <paramref name="cells"/> argument and 
	/// <span id='rtype'>returns a sequence of <see cref="GridCell"/> objects </span>
	/// <span id='desc'>whose <see cref="GridCellBase.Content"/> property 
	/// matches the <paramref name="searchText"/> argument, as specified by the <paramref name="searchType"/> argument.
	/// The search is case-insensitive.</span>
	/// </summary>
	/// <param name="cells">The <see cref="GridCell"/> objects to search.</param>
	/// <param name="searchText">The text for search for. Null or an empty string are acceptable, and search for cells containing those values, respectively.</param>
	/// <param name="searchType">The type of search to perform</param>
	/// <exception cref="ArgumentOutOfRangeException"></exception>
	public static IEnumerable<GridCell> Find(this IEnumerable<GridCell> cells, string? searchText, SearchType searchType = SearchType.Contains)
	{ return cells.Find(new GridSearchExpression(searchText, searchType)); }
}