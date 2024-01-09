namespace Strick.PlusCon.Models;


/// <summary>
/// Specifies the type of search to perform
/// </summary>
public enum SearchType
{
	/// <summary>
	/// Indicates that a search should determine whether the search string matches a string anywhere in a field
	/// </summary>
	Contains = 0,

	/// <summary>
	/// Indicates that a search should determine whether the search string matches a string at the beginning of a field
	/// </summary>
	StartsWith = 1,

	/// <summary>
	/// Indicates that a search should determine whether the search string matches a string at the end of a field
	/// </summary>
	EndsWith = 2,

	/// <summary>
	/// Indicates that a search should determine whether the search string matches a string
	/// </summary>
	Equals = 3
}