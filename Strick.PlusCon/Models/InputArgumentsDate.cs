using System;


namespace Strick.PlusCon.Models;


/// <summary>
/// Represents arguments used by the <see cref="Input.Date(InputArgumentsDate)"/> method to collect a date (<see cref="DateOnly"/>) value from the user.
/// </summary>
public class InputArgumentsDate : InputArguments<DateOnly>
{
	/// <summary>
	/// <inheritdoc cref="InputArgumentsCh.InputArgumentsCh()"/>
	/// </summary>
	public InputArgumentsDate() { }

	/// <summary>
	/// <inheritdoc cref="InputArgumentsCh()" path="/summary/span[@id='summary-init']"/>
	/// with 
	/// <inheritdoc cref="InputArgumentsCh(string?)" path="/summary/span[@id='summary-prompt']"/>.
	/// <inheritdoc cref="InputArgumentsCh(string?)" path="/summary/span[@id='summary-all-other']"/>
	/// </summary>
	/// <param name="prompt"><inheritdoc cref="InputArguments{T}.Prompt" path="/summary"/></param>
	public InputArgumentsDate(string? prompt) : this(prompt, null, null) { }

	/// <summary>
	/// <inheritdoc cref="InputArgumentsCh()" path="/summary/span[@id='summary-init']"/>
	/// with 
	/// <span id='min-max'>the <see cref="Min"/> and <see cref="Max"/> property 
	/// values specified by the <paramref name="min"/> and <paramref name="max"/> arguments.</span>
	/// <inheritdoc cref="InputArgumentsCh(string?)" path="/summary/span[@id='summary-all-other']"/>
	/// </summary>
	/// <param name="min"><inheritdoc cref="Min" path="/summary"/></param>
	/// <param name="max"><inheritdoc cref="Max" path="/summary"/></param>
	public InputArgumentsDate(DateOnly? min, DateOnly? max) : this(null, min, max) { }

	/// <summary>
	/// <inheritdoc cref="InputArgumentsCh()" path="/summary/span[@id='summary-init']"/>
	/// with 
	/// <inheritdoc cref="InputArgumentsCh(string?)" path="/summary/span[@id='summary-prompt']"/>.
	/// and 
	/// <inheritdoc cref="InputArgumentsDate(DateOnly?, DateOnly?)" path="/summary/span[@id='min-max']"/>.
	/// <inheritdoc cref="InputArgumentsCh(string?)" path="/summary/span[@id='summary-all-other']"/>
	/// </summary>
	/// <param name="prompt"><inheritdoc cref="InputArguments{T}.Prompt" path="/summary"/></param>
	/// <param name="min"><inheritdoc cref="Min" path="/summary"/></param>
	/// <param name="max"><inheritdoc cref="Max" path="/summary"/></param>
	public InputArgumentsDate(string? prompt, DateOnly? min, DateOnly? max)
	{
		Prompt = prompt;
		Min = min;
		Max = max;
	}


	/// <summary>
	/// <inheritdoc cref="InputArgumentsNumber{T}.Min" path="/summary"/>
	/// </summary>
	public DateOnly? Min { get; set; }

	/// <summary>
	/// <inheritdoc cref="InputArgumentsNumber{T}.Max" path="/summary"/>
	/// </summary>
	public DateOnly? Max { get; set; }


	/// <summary>
	/// A delegate that is used to parse the string entered by the user into a <see cref="DateOnly"/> object. 
	/// The default is the <see cref="DateOnly.TryParse(string?, out DateOnly)"/> method. 
	/// This can be set to a custom function to accomodate additional specific parsing requirements.
	/// </summary>
	public InputEntryParseDelegate<DateOnly> ParseFunction { get; set; } = DateOnly.TryParse;


	internal override bool Validate(DateOnly value)
	{
		return ValidateRange(value);
	}

	/// <summary>
	/// Returns true if the <paramref name="value"/> argument is between (inclusive) 
	/// the <see cref="Min"/> and <see cref="Max"/> values.
	/// </summary>
	/// <param name="value">The value to compare to the <see cref="Min"/> and <see cref="Max"/> values.</param>
	protected virtual bool ValidateRange(DateOnly value)
	{
		return value.WithinRange(Min, Max);
	}
}
