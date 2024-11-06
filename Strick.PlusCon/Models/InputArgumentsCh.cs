using System.Collections.Generic;


namespace Strick.PlusCon.Models;


/// <summary>
/// Represents arguments used by the <see cref="Input.Ch(InputArgumentsCh)"/> method to collect a <see cref="char"/> value from the user.
/// </summary>
public class InputArgumentsCh : InputArguments<char>
{
	/// <summary>
	/// <span id='summary-init'>Initializes a new instance</span>
	/// with default values for all properties.
	/// </summary>
	public InputArgumentsCh() { }

	/// <summary>
	/// <inheritdoc cref="InputArgumentsCh.InputArgumentsCh()" path="/summary/span[@id='summary-init']"/>
	/// with 
	/// <span id='summary-prompt'>the <see cref="InputArguments{T}.Prompt"/> 
	/// property set to the value of the <paramref name="prompt"/> argument</span>.
	/// <span id='summary-all-other'>All other properties are initialized to default values.</span>
	/// </summary>
	/// <param name="prompt"><inheritdoc cref="InputArguments{T}.Prompt" path="/summary"/></param>
	public InputArgumentsCh(string? prompt) : this()
	{
		Prompt = prompt;
	}

	/// <summary>
	/// <inheritdoc cref="InputArgumentsCh()" path="/summary/span[@id='summary-init']"/>
	/// with 
	/// <span id='allowed'>the <see cref="Allowed"/> values specified by the <paramref name="allowed"/> argument.</span> 
	/// <inheritdoc cref="InputArgumentsCh(string?)" path="/summary/span[@id='summary-all-other']"/>
	/// </summary>
	/// <param name="allowed"><inheritdoc cref="Allowed" path="/summary"/></param>
	public InputArgumentsCh(IEnumerable<char> allowed) : this(null, allowed) { }

	/// <summary>
	/// <inheritdoc cref="InputArgumentsCh()" path="/summary/span[@id='summary-init']"/>
	/// <inheritdoc cref="InputArgumentsCh(string?)" path="/summary/span[@id='summary-prompt']"/>
	/// and
	/// <inheritdoc cref="InputArgumentsCh(IEnumerable{char})" path="/summary/span[@id='allowed']"/>
	/// <inheritdoc cref="InputArgumentsCh(string?)" path="/summary/span[@id='summary-all-other']"/>
	/// </summary>
	/// <param name="prompt"><inheritdoc cref="InputArguments{T}.Prompt" path="/summary"/></param>
	/// <param name="allowed"><inheritdoc cref="Allowed" path="/summary"/></param>
	public InputArgumentsCh(string? prompt, IEnumerable<char> allowed) : this(prompt)
	{
		if (allowed.HasAny())
		{ Allowed.AddRange(allowed); }
	}


	/// <summary>
	/// The acceptable values - only values in the collection are accepted.
	/// If the collection is empty (the default), any value will be accepted.
	/// </summary>
	public List<char> Allowed { get; } = [];

	internal override bool Validate(char value)
	{
		return ValidateAllowed(value);
	}

	/// <summary>
	/// Returns true if the <see cref="Allowed"/> sequence contains 
	/// the <paramref name="value"/> argument, otherwise, returns false. Returns false if the <see cref="Allowed"/> sequence is null or empty.
	/// </summary>
	/// <param name="value">The char value to locate in the <see cref="Allowed"/> sequence.</param>
	protected virtual bool ValidateAllowed(char value)
	{
		if (!Allowed.HasAny())
		{ return true; }

		return Allowed.Contains(value);
	}
}
