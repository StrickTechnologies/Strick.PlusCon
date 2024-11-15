using System;
using System.Collections.Generic;
using System.Linq;


namespace Strick.PlusCon.Models;


/// <summary>
/// Represents arguments used by the <see cref="Input.Select{T}(InputArgumentsSelect{T})"/> method to prompt the user to select a value from a list of options.
/// </summary>
/// <typeparam name="T"><inheritdoc cref="InputArguments{T}"/></typeparam>
public class InputArgumentsSelect<T> : InputArguments<T>
{
	/// <summary>
	/// <inheritdoc cref="InputArgumentsCh()" path="/summary/span[@id='summary-init']"/>
	/// with 
	/// <span id='options'>the <see cref="Options"/> collection set to the <paramref name="options"/> argument</span>
	/// <inheritdoc cref="InputArgumentsCh(string?)" path="/summary/span[@id='summary-all-other']"/>
	/// </summary>
	/// <param name="options">A sequence of objects of type <typeparamref name="T"/> that are displayed, allowing the user to select one.</param>
	public InputArgumentsSelect(IEnumerable<T> options) : this(null, options, default)
	{ }

	/// <summary>
	/// <inheritdoc cref="InputArgumentsCh()" path="/summary/span[@id='summary-init']"/>
	/// <inheritdoc cref="InputArgumentsSelect{T}.InputArgumentsSelect(IEnumerable{T})" path="/summary/span[@id='options']"/>
	/// and 
	/// <span id='sel-option'>the initially selected option set to the <paramref name="selectedOption"/> argument.</span>
	/// <inheritdoc cref="InputArgumentsCh(string?)" path="/summary/span[@id='summary-all-other']"/>
	/// </summary>
	/// <param name="options"><inheritdoc cref="InputArgumentsSelect{T}.InputArgumentsSelect(IEnumerable{T})" path="/param[@name='options']"/></param>
	/// <param name="selectedOption">The initially selected option. An <see cref="ArgumentOutOfRangeException"/> exception 
	/// is thrown if the value is not contained in the <paramref name="options"/> sequence.</param>
	/// <exception cref="ArgumentOutOfRangeException">Thrown if the <paramref name="selectedOption"/> argument 
	/// is not in the <paramref name="options"/> sequence.</exception>
	public InputArgumentsSelect(IEnumerable<T> options, T? selectedOption) : this(null, options, selectedOption)
	{ }

	/// <summary>
	/// <inheritdoc cref="InputArgumentsCh()" path="/summary/span[@id='summary-init']"/>
	/// with 
	/// <inheritdoc cref="InputArgumentsCh.InputArgumentsCh(string?)" path="/summary/span[@id='summary-prompt']"/>
	/// and 
	/// <inheritdoc cref="InputArgumentsSelect{T}.InputArgumentsSelect(IEnumerable{T})" path="/summary/span[@id='options']"/>.
	/// <inheritdoc cref="InputArgumentsCh(string?)" path="/summary/span[@id='summary-all-other']"/>
	/// </summary>
	/// <param name="prompt"><inheritdoc cref="InputArguments{T}.Prompt" path="/summary"/></param>
	/// <param name="options"><inheritdoc cref="InputArgumentsSelect{T}.InputArgumentsSelect(IEnumerable{T})" path="/param[@name='options']"/></param>
	public InputArgumentsSelect(string? prompt, IEnumerable<T> options) : this(prompt, options, default)
	{ }

	/// <summary>
	/// <inheritdoc cref="InputArgumentsCh()" path="/summary/span[@id='summary-init']"/>
	/// with 
	/// <inheritdoc cref="InputArgumentsCh.InputArgumentsCh(string?)" path="/summary/span[@id='summary-prompt']"/>
	/// and 
	/// <inheritdoc cref="InputArgumentsSelect{T}.InputArgumentsSelect(IEnumerable{T})" path="/summary/span[@id='options']"/>
	/// and 
	/// <inheritdoc cref="InputArgumentsSelect(IEnumerable{T}, T?)" path="/summary/span[@id='sel-option']"/>
	/// <inheritdoc cref="InputArgumentsCh(string?)" path="/summary/span[@id='summary-all-other']"/>
	/// </summary>
	/// <param name="prompt"><inheritdoc cref="InputArguments{T}.Prompt" path="/summary"/></param>
	/// <param name="options"><inheritdoc cref="InputArgumentsSelect{T}.Options" path="/summary"/></param>
	/// <param name="selectedOption"><inheritdoc cref="InputArgumentsSelect(IEnumerable{T}, T?)" path="/param[@name='selectedOption']"/></param>
	public InputArgumentsSelect(string? prompt, IEnumerable<T> options, T? selectedOption)
	{
		if (selectedOption != null && !options.Contains(selectedOption))
		{ throw new ArgumentOutOfRangeException(nameof(selectedOption), "Must be in the list of options"); }

		SetOptions(options);

		Prompt.Text = prompt;

		if (selectedOption == null)
		{ SelectedOption = Options[0]; }
		else
		{ SelectedOption = selectedOption; }
	}


	private readonly List<T> theOptions = [];

	/// <summary>
	/// The options that are presented to the user. The collection must be non-null and contain at least <b>two</b> elements.
	/// </summary>
	public IReadOnlyList<T> Options => theOptions;

	/// <summary>
	/// <inheritdoc cref="Options" path="/summary"/>
	/// </summary>
	/// <param name="options"></param>
	/// <exception cref="ArgumentNullException"></exception>
	/// <exception cref="ArgumentException"></exception>
	internal void SetOptions(IEnumerable<T> options)
	{
		ArgumentNullException.ThrowIfNull(options);

		if (options.Count() < 2)
		{ throw new ArgumentException(nameof(options), "Must have at least two options"); }

		theOptions.Clear();
		theOptions.AddRange(options);
	}


	/// <summary>
	/// The option selected by the user.
	/// </summary>
	public T? SelectedOption { get; internal set; }

	/// <summary>
	/// Clears the current selection.
	/// </summary>
	internal void ClearSelection() => SelectedOption = default;


	/// <summary>
	/// A <see cref="TextStyle"/> object used to format the options when displayed for selection.
	/// </summary>
	public TextStyle SelectionOptionStyle { get; set; } = new TextStyle() { Reverse = true };


	/// <summary>
	/// Indicates whether or not to "wrap" the displayed option. 
	/// If true, pressing any key in the <see cref="SelectNextKeys"/> collection when the last option is displayed will "wrap" to the first option, 
	/// and pressing any key in the <see cref="SelectPreviousKeys"/> collection when the first option is displayed  will "wrap" to the last option.
	/// If false, these keypresses are ignored.
	/// </summary>
	public bool Wrap { get; set; } = true;

	/// <summary>
	/// A collection of char values that can be used to move to the next option when the user presses the key.
	/// Default vales are <see cref="ConsoleKey.Spacebar"/>, <see cref="ConsoleKey.RightArrow"/>, <see cref="ConsoleKey.DownArrow"/>
	/// </summary>
	public List<char> SelectNextKeys { get; } = new List<char>([' ', (char)ConsoleKey.RightArrow, (char)ConsoleKey.DownArrow]);

	/// <summary>
	/// A collection of char values that can be used to move to the previous option when the user presses the key. 
	/// Default values are <see cref="ConsoleKey.LeftArrow"/>, <see cref="ConsoleKey.UpArrow"/>
	/// </summary>
	public List<char> SelectPreviousKeys { get; } = new List<char>([(char)ConsoleKey.LeftArrow, (char)ConsoleKey.UpArrow]);


	/// <summary>
	/// Selects the next element in the <see cref="Options"/> sequence. 
	/// If <see cref="SelectedOption"/> is the last element in the <see cref="Options"/> sequence, the <see cref="Wrap"/> property dictates whether or not 
	/// the <see cref="SelectedOption"/> is changed when this method is called.
	/// </summary>
	public void SelectNext()
	{
		int index = theOptions.IndexOf(SelectedOption!);

		if (!Wrap && index == Options.Count - 1)
		{ return; }

		if (SelectedOption == null || index == -1 || index == Options.Count - 1)
		{ SelectedOption = Options[0]; }
		else
		{ SelectedOption = Options[index + 1]; }
	}

	/// <summary>
	/// Selects the previous element in the <see cref="Options"/> sequence. 
	/// If <see cref="SelectedOption"/> is the first element in the <see cref="Options"/> sequence, the <see cref="Wrap"/> property dictates whether or not 
	/// the <see cref="SelectedOption"/> is changed when this method is called.
	/// </summary>
	public void SelectPrevious()
	{
		int index = theOptions.IndexOf(SelectedOption!);

		if (!Wrap && index == 0)
		{ return; }

		if (SelectedOption == null || index == -1 || index == 0)
		{ SelectedOption = Options[^1]; }
		else
		{ SelectedOption = Options[index - 1]; }
	}
}
