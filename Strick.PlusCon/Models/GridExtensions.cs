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
	{
		if (cells == null)
		{ return Enumerable.Empty<GridCell>(); }

		if (string.IsNullOrEmpty(searchText))
		{ return cells.Where(cell => cell.Content == searchText); }

		switch (searchType)
		{
			case SearchType.Contains:
			{
				return cells.Where(cell => cell.Content != null && cell.Content.Contains(searchText, StringComparison.OrdinalIgnoreCase));
			}

			case SearchType.Equals:
			{
				return cells.Where(cell => searchText.Equals(cell.Content, StringComparison.OrdinalIgnoreCase));
			}

			case SearchType.StartsWith:
			{
				return cells.Where(cell => cell.Content != null && cell.Content.StartsWith(searchText, StringComparison.OrdinalIgnoreCase));
			}

			case SearchType.EndsWith:
			{
				return cells.Where(cell => cell.Content != null && cell.Content.EndsWith(searchText, StringComparison.OrdinalIgnoreCase));
			}
		}

		throw new ArgumentOutOfRangeException(nameof(searchType), "Invalid search type");
	}
}