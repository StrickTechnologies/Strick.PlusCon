using System;


namespace Strick.PlusCon.Models;


/// <summary>
/// Represents arguments used by the <see cref="Input.Text(InputArgumentsText)"/> method to collect a <see cref="string"/> value from the user.
/// </summary>
public class InputArgumentsText : InputArguments<string>
{
	/// <summary>
	/// <inheritdoc cref="InputArgumentsCh.InputArgumentsCh()"/>
	/// </summary>
	public InputArgumentsText() : this(null, null, null) { }

	/// <summary>
	/// <inheritdoc cref="InputArgumentsCh()" path="/summary/span[@id='summary-init']"/>
	/// with 
	/// <inheritdoc cref="InputArgumentsCh(string?)" path="/summary/span[@id='summary-prompt']"/>.
	/// <inheritdoc cref="InputArgumentsCh(string?)" path="/summary/span[@id='summary-all-other']"/>
	/// </summary>
	/// <param name="prompt"><inheritdoc cref="InputArguments{T}.Prompt" path="/summary"/></param>
	public InputArgumentsText(string? prompt) : this(prompt, null, null) { }

	/// <summary>
	/// <inheritdoc cref="InputArgumentsCh()" path="/summary/span[@id='summary-init']"/>
	/// with 
	/// <span id='min-max'>the <see cref="MinLength"/> and <see cref="MaxLength"/> property 
	/// values specified by the <paramref name="minLength"/> and <paramref name="maxLength"/> arguments.</span>
	/// <inheritdoc cref="InputArgumentsCh(string?)" path="/summary/span[@id='summary-all-other']"/>
	/// </summary>
	/// <param name="minLength"><inheritdoc cref="MinLength" path="/summary"/></param>
	/// <param name="maxLength"><inheritdoc cref="MaxLength" path="/summary"/></param>
	public InputArgumentsText(int? minLength, int? maxLength) : this(null, minLength, maxLength) { }

	/// <summary>
	/// <inheritdoc cref="InputArgumentsCh()" path="/summary/span[@id='summary-init']"/>
	/// with 
	/// <inheritdoc cref="InputArgumentsCh(string?)" path="/summary/span[@id='summary-prompt']"/>
	/// and 
	/// <inheritdoc cref="InputArgumentsText(int?, int?)" path="/summary/span[@id='min-max']"/>
	/// <inheritdoc cref="InputArgumentsCh(string?)" path="/summary/span[@id='summary-all-other']"/>
	/// </summary>
	/// <param name="prompt"><inheritdoc cref="InputArguments{T}.Prompt" path="/summary"/></param>
	/// <param name="minLength"><inheritdoc cref="MinLength" path="/summary"/></param>
	/// <param name="maxLength"><inheritdoc cref="MaxLength" path="/summary"/></param>
	public InputArgumentsText(string? prompt, int? minLength, int? maxLength)
	{
		if (minLength < 0)
		{ throw new ArgumentOutOfRangeException(nameof(minLength)); }

		if (maxLength <= 0)
		{ throw new ArgumentOutOfRangeException(nameof(maxLength)); }

		Prompt.Text = prompt;
		LengthRange = new Range<int>(minLength, maxLength);
	}


	private Range<int> LengthRange { get; }

	/// <summary>
	/// If non-null, represents the minimum length for the entered string value. 
	/// Must be zero or greater, otherwise an <see cref="ArgumentOutOfRangeException"/> is thrown. 
	/// If null, the minimum length is not checked.
	/// </summary>
	/// <exception cref="ArgumentOutOfRangeException"></exception>
	public int? MinLength => LengthRange.Min;

	/// <summary>
	/// If non-null, represents the maximum length for the entered string value. 
	/// Must be greater than zero, otherwise an <see cref="ArgumentOutOfRangeException"/> is thrown.
	/// If null, the maximum length is not checked.
	/// </summary>
	/// <exception cref="ArgumentOutOfRangeException"></exception>
	public int? MaxLength => LengthRange.Max;


	internal override bool Validate(string value)
	{
		return ValidateLength(value);
	}

	/// <summary>
	/// Returns true if the length of the <paramref name="value"/> argument is between (inclusive) 
	/// the <see cref="MinLength"/> and <see cref="MaxLength"/> values.
	/// </summary>
	/// <param name="value">The value whose length will be compared to the <see cref="MinLength"/> and <see cref="MaxLength"/> values.</param>
	protected virtual bool ValidateLength(string value)
	{
		return LengthRange.InRange(value.Length);
	}
}
