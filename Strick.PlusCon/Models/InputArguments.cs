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
	public StyledText Prompt { get; } = new(null);

	/// <summary>
	/// The style to apply to the prompt. 
	/// If null, no styling is applied.
	/// </summary>
	public TextStyle? PromptStyle
	{
		get => Prompt.Style;

		set
		{
			if (value != null)
			{ Prompt.Style = value; }
			else
			{ Prompt.Style = new TextStyle(); }
		}
	}

	internal virtual bool Validate(T value) => true;
}

/// <summary>
/// A delegate that can be used to override the parse method on some InputArguments classes.
/// </summary>
/// <typeparam name="T"></typeparam>
/// <param name="input">A string containing the characters representing the value to convert.</param>
/// <param name="result">When the method returns, contains the <typeparamref name="T"/> value equivalent 
/// to the value contained in <paramref name="input"/>, if the conversion succeeded.</param>
public delegate bool InputEntryParseDelegate<T>(string input, out T result);
