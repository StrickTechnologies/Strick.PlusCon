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
	/// Creates a new instance with default values for <see cref="Text"/>, 
	/// <see cref="Type"/>, and <see cref="ComparisonType"/>.
	/// </summary>
	public GridSearchExpression() { }

	/// <summary>
	/// Creates a new instance with the <see cref="Text"/> property set to 
	/// the <paramref name="searchText"/> argument, and default values for 
	/// <see cref="Type"/> and <see cref="ComparisonType"/>.
	/// </summary>
	/// <param name="searchText"><inheritdoc cref="Text" path="/summary"/></param>
	public GridSearchExpression(string? searchText)
	{
		Text = searchText;
	}

	/// <summary>
	/// Creates a new instance with the <see cref="Text"/> property set to 
	/// the <paramref name="searchText"/> argument, 
	/// the <see cref="Type"/> property set to the <paramref name="searchType"/> argument 
	/// and the default value for <see cref="ComparisonType"/>.
	/// </summary>
	/// <param name="searchText"><inheritdoc cref="GridSearchExpression(string?)" path="/param[@name='searchText']"/></param>
	/// <param name="searchType"><inheritdoc cref="SearchType" path="/summary"/></param>
	public GridSearchExpression(string? searchText, SearchType searchType) : this(searchText)
	{
		Type = searchType;
	}

	/// <summary>
	/// Creates a new instance with the <see cref="Text"/> property set to 
	/// the <paramref name="searchText"/> argument, 
	/// the <see cref="Type"/> property set to the <paramref name="searchType"/> argument 
	/// and the <see cref="ComparisonType"/> property set to the <paramref name="comparisonType"/> argument.
	/// </summary>
	/// <param name="searchText"><inheritdoc cref="GridSearchExpression(string?)" path="/param[@name='searchText']"/></param>
	/// <param name="searchType"><inheritdoc cref="GridSearchExpression(string?, SearchType)" path="/param[@name='searchType']"/></param>
	/// <param name="comparisonType"><inheritdoc cref="ComparisonType" path="/summary"/></param>
	public GridSearchExpression(string? searchText, SearchType searchType, StringComparison comparisonType) : this(searchText, searchType)
	{
		ComparisonType = comparisonType;
	}


	/// <summary>
	/// The text to search for. Null (the default) or an empty string are acceptable, 
	/// and search for cells containing those values, respectively.
	/// </summary>
	public string? Text { get; set; }


	private SearchType srchType = SearchType.Equals;

	/// <summary>
	/// The type of search to perform. 
	/// The default is <see cref="SearchType.Equals"/>.
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
	/// The default is <see cref="StringComparison.OrdinalIgnoreCase"/>.
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
