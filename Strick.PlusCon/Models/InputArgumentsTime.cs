using System;


namespace Strick.PlusCon.Models;


/// <summary>
/// Represents arguments used by the <see cref="Input.Time(InputArgumentsTime)"/> method to collect a time of day (<see cref="TimeOnly"/>) value from the user.
/// </summary>
public class InputArgumentsTime : InputArguments<TimeOnly>
{
	/// <summary>
	/// <inheritdoc cref="InputArgumentsCh.InputArgumentsCh()"/>
	/// </summary>
	public InputArgumentsTime() { }

	/// <summary>
	/// <inheritdoc cref="InputArgumentsCh()" path="/summary/span[@id='summary-init']"/>
	/// with 
	/// <inheritdoc cref="InputArgumentsCh(string?)" path="/summary/span[@id='summary-prompt']"/>.
	/// <inheritdoc cref="InputArgumentsCh(string?)" path="/summary/span[@id='summary-all-other']"/>
	/// </summary>
	/// <param name="prompt"><inheritdoc cref="InputArguments{T}.Prompt" path="/summary"/></param>
	public InputArgumentsTime(string? prompt) : this(prompt, null, null) { }

	/// <summary>
	/// <inheritdoc cref="InputArgumentsCh()" path="/summary/span[@id='summary-init']"/>
	/// with 
	/// <span id='min-max'>the <see cref="Min"/> and <see cref="Max"/> property 
	/// values specified by the <paramref name="min"/> and <paramref name="max"/> arguments.</span>
	/// <inheritdoc cref="InputArgumentsCh(string?)" path="/summary/span[@id='summary-all-other']"/>
	/// </summary>
	/// <param name="min"><inheritdoc cref="Min" path="/summary"/></param>
	/// <param name="max"><inheritdoc cref="Max" path="/summary"/></param>
	public InputArgumentsTime(TimeOnly? min, TimeOnly? max) : this(null, min, max) { }

	/// <summary>
	/// <inheritdoc cref="InputArgumentsCh()" path="/summary/span[@id='summary-init']"/>
	/// with 
	/// <inheritdoc cref="InputArgumentsCh(string?)" path="/summary/span[@id='summary-prompt']"/>
	/// and 
	/// <inheritdoc cref="InputArgumentsTime(TimeOnly?, TimeOnly?)" path="/summary/span[@id='min-max']"/>.
	/// <inheritdoc cref="InputArgumentsCh(string?)" path="/summary/span[@id='summary-all-other']"/>
	/// </summary>
	/// <param name="prompt"><inheritdoc cref="InputArguments{T}.Prompt" path="/summary"/></param>
	/// <param name="min"><inheritdoc cref="Min" path="/summary"/></param>
	/// <param name="max"><inheritdoc cref="Max" path="/summary"/></param>
	public InputArgumentsTime(string? prompt, TimeOnly? min, TimeOnly? max)
	{
		Prompt = prompt;
		Min = min;
		Max = max;
	}


	/// <summary>
	/// <inheritdoc cref="InputArgumentsNumber{T}.Min" path="/summary"/>
	/// </summary>
	public TimeOnly? Min { get; set; }

	/// <summary>
	/// <inheritdoc cref="InputArgumentsNumber{T}.Max" path="/summary"/>
	/// </summary>
	public TimeOnly? Max { get; set; }


	/// <summary>
	/// A delegate that is used to parse the string entered by the user into a <see cref="TimeOnly"/> object. 
	/// The default is the <see cref="TimeOnly.TryParse(string?, out TimeOnly)"/> method. 
	/// This can be set to a custom function to accomodate additional specific parsing requirements.
	/// </summary>
	public InputEntryParseDelegate<TimeOnly> ParseFunction { get; set; } = TimeOnly.TryParse;


	internal override bool Validate(TimeOnly value)
	{
		return ValidateRange(value);
	}

	private bool ValidateRange(TimeOnly value)
	{
		return value.WithinRange(Min, Max);
	}
}
