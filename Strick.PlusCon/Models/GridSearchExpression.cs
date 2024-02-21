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
	/// <summary>
	/// Creates a new instance that will search cells for a null value using default 
	/// values for <see cref="SearchType"/> and <see cref="ComparisonType"/>.
	/// </summary>
	public GridSearchExpression() { }

	/// <summary>
	/// Creates a new instance that will search cells for the value specified by the 
	/// <paramref name="searchText"/> argument using default values for 
	/// <see cref="SearchType"/> and <see cref="ComparisonType"/>.
	/// </summary>
	public GridSearchExpression(string? searchText)
	{
		Text = searchText;
	}

	/// <summary>
	/// Creates a new instance that will search cells for the value specified by the 
	/// <paramref name="searchText"/> argument using the <see cref="SearchType"/> specified 
	/// by the <paramref name="searchType"/> argument and the default value for <see cref="ComparisonType"/>.
	/// </summary>
	public GridSearchExpression(string? searchText, SearchType searchType) : this(searchText)
	{
		Type = searchType;
	}

	/// <summary>
	/// Creates a new instance that will search cells for the value specified by the 
	/// <paramref name="searchText"/> argument using the <see cref="SearchType"/> specified 
	/// by the <paramref name="searchType"/> argument and the <see cref="StringComparison"/> 
	/// specified by the <paramref name="comparisonType"/> argument.
	/// </summary>
	public GridSearchExpression(string? searchText, SearchType searchType, StringComparison comparisonType) : this(searchText, searchType)
	{
		ComparisonType = comparisonType;
	}


	/// <summary>
	/// The text to search for. Null or an empty string are acceptable, and search for cells containing those values, respectively.
	/// </summary>
	public string? Text { get; set; }


	private SearchType srchType = SearchType.Equals;

	/// <summary>
	/// The type of search to perform. 
	/// Default is <see cref="SearchType.Equals"/>.
	/// </summary>
	public SearchType Type
	{
		get => srchType;
		set
		{
			if (!value.Exists())
			{ throw new ArgumentOutOfRangeException(nameof(value)); }

			srchType = value;
		}
	}

	private StringComparison compType = StringComparison.OrdinalIgnoreCase;

	/// <summary>
	/// The comparison type used for the search. 
	/// Default is <see cref="StringComparison.OrdinalIgnoreCase"/>.
	/// </summary>
	public StringComparison ComparisonType
	{
		get => compType;
		set
		{
			if (!value.Exists())
			{ throw new ArgumentOutOfRangeException(nameof(value)); }
			
			compType = value;
		}
	}
}
