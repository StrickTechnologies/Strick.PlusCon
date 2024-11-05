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
	public InputArgumentsText() { }

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
		Prompt = prompt;
		MinLength = minLength;
		MaxLength = maxLength;
	}


	private int? minL;

	/// <summary>
	/// The minimum length for the entered string value. 
	/// Must be zero or greater, otherwise an <see cref="ArgumentOutOfRangeException"/> is thrown.
	/// </summary>
	/// <exception cref="ArgumentOutOfRangeException"></exception>
	public int? MinLength
	{
		get => minL;
		set
		{
			if (value < 0)
			{ throw new ArgumentOutOfRangeException(nameof(value)); }

			minL = value;
		}
	}

	private int? maxL;

	/// <summary>
	/// The maximum length for the entered string value. 
	/// Must be greater than zero, otherwise an <see cref="ArgumentOutOfRangeException"/> is thrown.
	/// </summary>
	/// <exception cref="ArgumentOutOfRangeException"></exception>
	public int? MaxLength
	{
		get => maxL;
		set
		{
			if (value <= 0)
			{ throw new ArgumentOutOfRangeException(nameof(value)); }

			maxL = value;
		}
	}


	internal override bool Validate(string value)
	{
		return ValidateLength(value);
	}

	private bool ValidateLength(string value)
	{
		return value.Length.WithinRange(MinLength, MaxLength);
	}
}
