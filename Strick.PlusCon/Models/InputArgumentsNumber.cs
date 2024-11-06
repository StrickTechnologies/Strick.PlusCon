using System.Numerics;


namespace Strick.PlusCon.Models;


/// <summary>
/// Represents arguments used by the <see cref="Input.Number{T}(InputArgumentsNumber{T})"/> method to collect a numeric value from the user.
/// </summary>
/// <typeparam name="T">Any struct type that implements the <see cref="INumber{TSelf}"/> interface.</typeparam>
public class InputArgumentsNumber<T> : InputArguments<T> where T : struct, INumber<T>
{
	/// <summary>
	/// <inheritdoc cref="InputArgumentsCh()"/>
	/// </summary>
	public InputArgumentsNumber() { }

	/// <summary>
	/// <inheritdoc cref="InputArgumentsCh()" path="/summary/span[@id='summary-init']"/>
	/// with 
	/// <inheritdoc cref="InputArgumentsCh(string?)" path="/summary/span[@id='summary-prompt']"/>.
	/// <inheritdoc cref="InputArgumentsCh(string?)" path="/summary/span[@id='summary-all-other']"/>
	/// </summary>
	/// <param name="prompt"><inheritdoc cref="InputArguments{T}.Prompt" path="/summary"/></param>
	public InputArgumentsNumber(string? prompt) : this(prompt, null, null) { }

	/// <summary>
	/// <inheritdoc cref="InputArgumentsCh()" path="/summary/span[@id='summary-init']"/>
	/// with <span id='min-max'>the <see cref="Min"/> and <see cref="Max"/> property 
	/// values specified by the <paramref name="min"/> and <paramref name="max"/> arguments.</span>
	/// <inheritdoc cref="InputArgumentsCh(string?)" path="/summary/span[@id='summary-all-other']"/>
	/// </summary>
	/// <param name="min"><inheritdoc cref="Min" path="/summary"/></param>
	/// <param name="max"><inheritdoc cref="Max" path="/summary"/></param>
	public InputArgumentsNumber(T? min, T? max) : this(null, min, max) { }

	/// <summary>
	/// <inheritdoc cref="InputArgumentsCh()" path="/summary/span[@id='summary-init']"/>
	/// with 
	/// <inheritdoc cref="InputArgumentsCh.InputArgumentsCh(string?)" path="/summary/span[@id='summary-prompt']"/>
	/// and 
	/// <inheritdoc cref="InputArgumentsNumber{T}.InputArgumentsNumber(T?, T?)" path="/summary/span[@id='min-max']"/>
	/// <inheritdoc cref="InputArgumentsCh(string?)" path="/summary/span[@id='summary-all-other']"/>
	/// </summary>
	/// <param name="prompt"><inheritdoc cref="InputArguments{T}.Prompt" path="/summary"/></param>
	/// <param name="min"><inheritdoc cref="InputArgumentsNumber{T}.InputArgumentsNumber(T?, T?)" path="/param[@name='min']"/></param>
	/// <param name="max"><inheritdoc cref="InputArgumentsNumber{T}.InputArgumentsNumber(T?, T?)" path="/param[@name='max']"/></param>
	public InputArgumentsNumber(string? prompt, T? min, T? max)
	{
		Prompt = prompt;
		Min = min;
		Max = max;
	}


	/// <summary>
	/// The minimum acceptable value. If null (the default), no minimum is checked. 
	/// </summary>
	public T? Min { get; set; }

	/// <summary>
	/// The maximum acceptable value. If null (the default), no maximum is checked.
	/// </summary>
	public T? Max { get; set; }

	internal override bool Validate(T value)
	{
		return ValidateRange(value);
	}

	/// <summary>
	/// Returns true if the <paramref name="value"/> argument is between (inclusive) 
	/// the <see cref="Min"/> and <see cref="Max"/> values.
	/// </summary>
	/// <param name="value">The value to compare to the <see cref="Min"/> and <see cref="Max"/> values.</param>
	protected virtual bool ValidateRange(T value)
	{
		return value.WithinRange(Min, Max);
	}
}
