using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;


namespace Strick.PlusCon.Models;


/// <summary>
/// Represents arguments used by the <see cref="Input"/> class to collect a value from the user.
/// </summary>
/// <typeparam name="T">The type of the value to be collected</typeparam>
public abstract class InputArguments<T>
{
	/// <summary>
	/// The prompt to display
	/// </summary>
	public string? Prompt { get; set; }

	/// <summary>
	/// The style to apply to the prompt
	/// </summary>
	public TextStyle? PromptStyle { get; set; }

	internal virtual bool Validate(T value) => true;
}

/// <summary>
/// A delegate that can be used to override the parse method on some InputArguments classes.
/// </summary>
/// <typeparam name="T"></typeparam>
/// <param name="input"></param>
/// <param name="result"></param>
public delegate bool InputEntryParseDelegate<T>(string input, out T result);
