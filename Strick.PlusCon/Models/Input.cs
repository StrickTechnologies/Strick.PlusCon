using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

using static Strick.PlusCon.Helpers;


namespace Strick.PlusCon.Models;


public static class Input
{
	/// <summary>
	/// Prompts for input of a single char. The entered char must be one of <b>YyNn</b>. 
	/// If Y or y, true is returned. If N or n, false is returned.
	/// </summary>
	/// <param name="prompt">The prompt that is displayed</param>
	public static bool YN(string? prompt)
	{
		var key = Ch(new InputChArguments(prompt, ['Y', 'y', 'N', 'n']));
		return char.ToUpperInvariant(key.KeyChar) == 'Y';
	}


	/// <summary>
	/// <inheritdoc cref="Any(string?)"/>
	/// <para>The default prompt is displayed. See <see cref="Any(string?)"/> for more on the prompt.</para>
	/// </summary>
	public static ConsoleKeyInfo Any() => Any(null);

	/// <summary>
	/// Prompts for input of a single char, 
	/// and returns a <see cref="ConsoleKeyInfo"/> structure containing the key that was pressed. 
	/// Any key is accepted. 
	/// </summary>
	/// <param name="prompt">The prompt to display. Omit, or pass null to show the default prompt of "Press Any Key ". 
	/// To display no prompt, pass an empty string for the <paramref name="prompt"/> argument.</param>
	public static ConsoleKeyInfo Any(string? prompt)
	{
		InputChArguments args = new();
		if (prompt != null)
		{ args.Prompt = prompt; }
		else
		{ args.Prompt = "Press Any Key "; }

		return Ch(args);
	}


	/// <summary>
	/// Prompts for input of a single char
	/// </summary>
	/// <param name="arguments">The arguments that contol how the input is collected.</param>
	public static ConsoleKeyInfo Ch(InputChArguments arguments)
	{
		var (posLeft, posTop) = Console.GetCursorPosition();
		do
		{
			var key = RK(arguments.Prompt);
			if (arguments.Validate(key.KeyChar))
			{ return key; }

			ResetCursorPosition(posLeft, posTop, "", 0);
		} while (true);
	}


	public static T? Number<T>(string? prompt) where T : struct, INumber<T>
	{
		return Number(new InputNumberArguments<T>(prompt));
	}

	public static T? Number<T>(InputNumberArguments<T> arguments) where T : struct, INumber<T>
	{
		var (posLeft, posTop) = Console.GetCursorPosition();
		do
		{
			string? entry = RL(arguments.Prompt);
			if (string.IsNullOrEmpty(entry))
			{ return default; }

			if (T.TryParse(entry, null, out T value))
			{
				if (arguments.Validate(value))
				{ return value; }
			}

			ResetCursorPosition(posLeft, posTop, arguments.Prompt, entry.Length);
		} while (true);
	}


	public static string? Text(string? prompt) => Text(new InputTextArguments(prompt));

	public static string? Text(InputTextArguments arguments)
	{
		var (posLeft, posTop) = Console.GetCursorPosition();

		do
		{
			var txt = RL(arguments.Prompt);

			if (txt != null && arguments.Validate(txt))
			{ return txt; }

			ResetCursorPosition(posLeft, posTop, arguments.Prompt, txt == null ? 0 : txt.Length);
		} while (true);
	}


	public static TimeOnly? Time(string? prompt) => Time(new InputTimeArguments() { Prompt = prompt });

	public static TimeOnly? Time(InputTimeArguments arguments)
	{
		var (posLeft, posTop) = Console.GetCursorPosition();

		do
		{
			var strTm = RL(arguments.Prompt);
			if (string.IsNullOrWhiteSpace(strTm))
			{ break; }

			if (arguments.ParseFunction(strTm, out TimeOnly time))
			{
				if (arguments.Validate(time))
				{
					ResetCursorPosition(posLeft, posTop, arguments.Prompt, strTm.Length);
					WL(arguments.Prompt + time.ToLongTimeString());
					return time;
				}
			}

			ResetCursorPosition(posLeft, posTop, arguments.Prompt, strTm.Length);

		} while (true);

		return null;
	}


	public static DateOnly? Date(string? prompt) => Date(new InputDateArguments() { Prompt = prompt });

	public static DateOnly? Date(InputDateArguments arguments)
	{
		var (posLeft, posTop) = Console.GetCursorPosition();
		do
		{
			var strDt = RL(arguments.Prompt);
			if (string.IsNullOrWhiteSpace(strDt))
			{ break; }

			if (arguments.ParseFunction(strDt, out DateOnly date))
			{
				if (arguments.Validate(date))
				{
					ResetCursorPosition(posLeft, posTop, arguments.Prompt, strDt.Length);
					WL(arguments.Prompt + date.ToShortDateString());
					return date;
				}
			}

			ResetCursorPosition(posLeft, posTop, arguments.Prompt, strDt.Length);

		} while (true);

		return null;
	}


	public static bool? SelectYesNo(string? prompt) => SelectYesNo(prompt, false);

	public static bool? SelectYesNo(string? prompt, bool? currentSelection) => SelectYesNo(prompt, currentSelection?.YesNo());

	public static bool? SelectYesNo(string? prompt, string? currentSelection)
	{
		InputSelectArguments<string> args = new(prompt, new[] { "Yes", "No" }, currentSelection);
		string? selected = Select(args);

		if (selected == null)
		{ return null; }

		return selected == "Yes";
	}

	public static T? Select<T>(InputSelectArguments<T> arguments)
	{
		if (arguments == null)
		{ throw new ArgumentNullException(nameof(arguments)); }

		var (posLeft, posTop) = Console.GetCursorPosition();
		do
		{
			var x = arguments.SelectedOption;
			W(arguments.Prompt + arguments.SelectionOptionStyle.StyleText(arguments.SelectedOption!.ToString()!));
			var k = Console.ReadKey(true);

			if (k.Key == ConsoleKey.Enter)
			{ break; }

			if (k.Key == ConsoleKey.Escape)
			{
				arguments.ClearSelection();
				break;
			}

			ResetCursorPosition(posLeft, posTop, arguments.Prompt, arguments.SelectedOption!.ToString()!.Length);
			if (arguments.SelectNextKeys.Contains((char)k.Key))
			{ arguments.SelectNext(); }
			if (arguments.SelectPreviousKeys.Contains((char)k.Key))
			{ arguments.SelectPrevious(); }

		} while (true);

		return arguments.SelectedOption;
	}


	private static void ResetCursorPosition(int posLeft, int posTop, string? prompt, int entryLength)
	{
		Console.SetCursorPosition(posLeft, posTop);
		W(prompt + new string(' ', entryLength));
		Console.SetCursorPosition(posLeft, posTop);
	}


	public static bool WithinRange<T>(this T value, T? min, T? max) where T : struct, IComparable<T>
	{
		if (min == null && max == null)
		{ return true; }

		if (min != null && value.CompareTo(min.Value) < 0)
		{ return false; }

		if (max != null && value.CompareTo(max.Value) > 0)
		{ return false; }

		return true;
	}
}


/// <summary>
/// Arguments used by the <see cref="Input"/> class to collect a value from the user.
/// </summary>
/// <typeparam name="T">The type of the value to be collected</typeparam>
public abstract class InputArguments<T>
{
	/// <summary>
	/// The prompt to display
	/// </summary>
	public string? Prompt { get; set; }

	internal virtual bool Validate(T value) => true;
}

public interface IInputArguments<T>
{
	public string? Prompt { get; set; }
	public abstract bool Validate(T value);
}


/// <summary>
/// <inheritdoc cref="InputArguments{T}"/> 
/// Used to retrieve a <see cref="char"/> value.
/// </summary>
public class InputChArguments : InputArguments<char>
{
	/// <summary>
	/// Creates an instance with the default <see cref="InputArguments{T}.Prompt"/> 
	/// and the default <see cref="Allowed"/> values.
	/// </summary>
	public InputChArguments() { }

	/// <summary>
	/// Creates an instance with the <see cref="InputArguments{T}.Prompt"/> specified by 
	/// the <paramref name="prompt"/> argument 
	/// and the default <see cref="Allowed"/> values.
	/// </summary>
	public InputChArguments(string? prompt) : this()
	{
		Prompt = prompt;
	}

	/// <summary>
	/// Creates an instance with the <see cref="InputArguments{T}.Prompt"/> specified by 
	/// the <paramref name="prompt"/> argument 
	/// and the <see cref="Allowed"/> values specified by the <paramref name="allowed"/> argument.
	/// </summary>
	public InputChArguments(string? prompt, IEnumerable<char> allowed) : this(prompt)
	{
		if (allowed.HasAny())
		{ Allowed.AddRange(allowed); }
	}


	/// <summary>
	/// The allowed values - only values in the collection are accepted.
	/// If the collection is empty (the default), any value will be accepted.
	/// </summary>
	public List<char> Allowed { get; } = new List<char>();

	internal override bool Validate(char value)
	{
		return ValidateAllowed(value);
	}

	protected bool ValidateAllowed(char value)
	{
		if (!Allowed.HasAny())
		{ return true; }

		return Allowed.Contains(value);
	}
}

/// <summary>
/// <inheritdoc cref="InputArguments{T}"/> 
/// Used to retrieve a numeric value.
/// </summary>
/// <typeparam name="T">Any struct type that implements the <see cref="INumber{TSelf}"/> interface.</typeparam>
public class InputNumberArguments<T> : InputArguments<T> where T : struct, INumber<T>
{
	/// <summary>
	/// Creates a default instance.
	/// </summary>
	public InputNumberArguments() { }

	/// <summary>
	/// Creates an instance 
	/// with the <see cref="InputArguments{T}.Prompt"/> property set to the value of the <paramref name="prompt"/> argument.
	/// </summary>
	/// <param name="prompt"><inheritdoc cref="InputArguments{T}.Prompt" path="/summary"/></param>
	public InputNumberArguments(string? prompt) : this(prompt, null, null) { }

	public InputNumberArguments(T? min, T? max) : this(null, min, max) { }

	public InputNumberArguments(string? prompt, T? min, T? max)
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

	protected bool ValidateRange(T value)
	{
		return value.WithinRange(Min, Max);
	}
}

/// <summary>
/// <inheritdoc cref="InputArguments{T}"/> 
/// Used to retrieve a string/text value.
/// </summary>
public class InputTextArguments : InputArguments<string>
{
	public InputTextArguments() { }

	public InputTextArguments(string? prompt) : this(prompt, null, null) { }

	public InputTextArguments(int? minLength, int? maxLength) : this(null, minLength, maxLength) { }

	public InputTextArguments(string? prompt, int? minLength, int? maxLength)
	{
		Prompt = prompt;
		MinLength = minLength;
		MaxLength = maxLength;
	}


	public int? MinLength { get; set; }

	public int? MaxLength { get; set; }


	internal override bool Validate(string value)
	{
		return ValidateLength(value);
	}

	protected bool ValidateLength(string value)
	{
		return value.Length.WithinRange(MinLength, MaxLength);
	}
}

/// <summary>
/// <inheritdoc cref="InputArguments{T}"/> 
/// Used to retrieve a <see cref="DateOnly"/> (date) value.
/// </summary>
public class InputDateArguments : InputArguments<DateOnly>
{
	public InputDateArguments() { }

	public InputDateArguments(string? prompt) : this(prompt, null, null) { }

	public InputDateArguments(DateOnly? min, DateOnly? max) : this(null, min, max) { }

	public InputDateArguments(string? prompt, DateOnly? min, DateOnly? max)
	{
		Prompt = prompt;
		Min = min;
		Max = max;
	}


	public DateOnly? Min { get; set; }

	public DateOnly? Max { get; set; }


	public InputEntryParseDelegate<DateOnly> ParseFunction { get; set; } = DateOnly.TryParse; //Input.ParseDate2;


	internal override bool Validate(DateOnly value)
	{
		return ValidateRange(value);
	}

	protected bool ValidateRange(DateOnly value)
	{
		return value.WithinRange(Min, Max);
	}
}

/// <summary>
/// <inheritdoc cref="InputArguments{T}"/> 
/// Used to retrieve a <see cref="TimeOnly"/> (time of day) value.
/// </summary>
public class InputTimeArguments : InputArguments<TimeOnly>
{
	public InputTimeArguments() { }

	public InputTimeArguments(string? prompt) : this(prompt, null, null) { }

	public InputTimeArguments(TimeOnly? min, TimeOnly? max) : this(null, min, max) { }

	public InputTimeArguments(string? prompt, TimeOnly? min, TimeOnly? max)
	{
		Prompt = prompt;
		Min = min;
		Max = max;
	}


	public TimeOnly? Min { get; set; }

	public TimeOnly? Max { get; set; }


	public InputEntryParseDelegate<TimeOnly> ParseFunction { get; set; } = TimeOnly.TryParse;


	internal override bool Validate(TimeOnly value)
	{
		return ValidateRange(value);
	}

	protected bool ValidateRange(TimeOnly value)
	{
		return value.WithinRange(Min, Max);
	}
}

/// <summary>
/// <inheritdoc cref="InputArguments{T}"/> 
/// Used to retrieve a value from a list of options.
/// </summary>
/// <typeparam name="T"><inheritdoc cref="InputArguments{T}"/></typeparam>
public class InputSelectArguments<T> : InputArguments<T>
{
	public InputSelectArguments(IEnumerable<T> options) : this(null, options, default)
	{ }

	public InputSelectArguments(IEnumerable<T> options, T? selectedOption) : this(null, options, selectedOption)
	{ }

	public InputSelectArguments(string? prompt, IEnumerable<T> options) : this(prompt, options, default)
	{ }

	public InputSelectArguments(string? prompt, IEnumerable<T> options, T? selectedOption)
	{
		if (options == null || options.Count() < 2)
		{ throw new ArgumentNullException(nameof(options), "Must have at least two options"); }

		if (selectedOption != null && !options.Contains(selectedOption))
		{ throw new ArgumentOutOfRangeException(nameof(selectedOption), "Must be in the list of options"); }

		theOptions = options.ToList();

		Prompt = prompt;

		if (selectedOption == null)
		{ SelectedOption = Options.First(); }
		else
		{ SelectedOption = selectedOption; }
	}


	public T? SelectedOption { get; protected set; }

	public void ClearSelection() => SelectedOption = default;


	protected List<T> theOptions;

	public IReadOnlyList<T> Options => theOptions;


	public TextStyle SelectionOptionStyle { get; set; } = new TextStyle() { Reverse = true };

	//public bool SelectionRequired { get; set; } = false;

	public bool Wrap { get; set; } = true;

	public List<char> SelectNextKeys { get; } = new List<char>([' ', (char)ConsoleKey.RightArrow, (char)ConsoleKey.DownArrow]);

	public List<char> SelectPreviousKeys { get; } = new List<char>([(char)ConsoleKey.LeftArrow, (char)ConsoleKey.UpArrow]);


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

	public void SelectPrevious()
	{
		int index = theOptions.IndexOf(SelectedOption!);

		if (!Wrap && index == 0)
		{ return; }

		if (SelectedOption == null || index == -1 || index == 0)
		{ SelectedOption = Options.Last(); }
		else
		{ SelectedOption = Options[index - 1]; }
	}
}


public delegate bool InputEntryParseDelegate<T>(string input, out T result);
