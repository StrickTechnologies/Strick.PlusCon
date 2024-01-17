using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Strick.PlusCon.Models;


/// <summary>
/// A search expression that can be used to search the cells in a <see cref="Grid"/> object.
/// </summary>
public class GridSearchExpression
{
	public GridSearchExpression() { }

	public GridSearchExpression(string? searchText)
	{
		Text = searchText;
	}

	public GridSearchExpression(string? searchText, SearchType searchType) : this(searchText)
	{
		Type = searchType;
	}

	public GridSearchExpression(string? searchText, SearchType searchType, StringComparison comparisonType) : this(searchText, searchType)
	{
		ComparisonType = comparisonType;
	}


	/// <summary>
	/// The text to search for. Null or an empty string are acceptable, and search for cells containing those values, respectively.
	/// </summary>
	public string? Text { get; set; }

	/// <summary>
	/// The type of search to perform. 
	/// Default is <see cref="SearchType.Equals"/>.
	/// </summary>
	public SearchType Type { get; set; } = SearchType.Equals;

	/// <summary>
	/// The comparison type used for the search. 
	/// Default is <see cref="StringComparison.OrdinalIgnoreCase"/>.
	/// </summary>
	public StringComparison ComparisonType { get; set; } = StringComparison.OrdinalIgnoreCase;
}
