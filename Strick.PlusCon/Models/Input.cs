using System;
using System.Collections.Generic;
using System.ComponentModel;
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
	/// <param name="prompt"></param>
	public static bool YN(string? prompt)
	{
		var key = Ch(new InputChArguments(prompt, new[] { 'Y', 'y', 'N', 'n' }));
		return char.ToUpper(key.KeyChar, System.Globalization.CultureInfo.InvariantCulture) == 'Y';
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

			//Console.SetCursorPosition(posLeft, posTop);
			ResetCursorPosition(posLeft, posTop, "", 0);
		} while (true);
	}


	public static T? Number<T>(string? prompt) where T : struct, INumber<T>
	{
		return Number(new InputNumberArguments<T>(prompt));
		//var (posLeft, posTop) = Console.GetCursorPosition();
		//do
		//{
		//	string? entry = RL(prompt);
		//	if (string.IsNullOrEmpty(entry))
		//	{ return default; }

		//	if (T.TryParse(entry, null, out T value))
		//	{ return value; }

		//	Console.SetCursorPosition(posLeft, posTop);
		//	W(prompt + new string(' ', entry.Length));
		//	Console.SetCursorPosition(posLeft, posTop);
		//} while (true);
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

			//Console.SetCursorPosition(posLeft, posTop);
			//W(arguments.Prompt + new string(' ', entry.Length));
			//Console.SetCursorPosition(posLeft, posTop);
			ResetCursorPosition(posLeft, posTop, arguments.Prompt, entry.Length);
		} while (true);
	}

	//a version to work in .Net 6.0 (which does not have INumber<T>, IParsable<T>)
	public static T? Number6<T>(string? prompt) where T : struct, IComparable, IComparable<T>, IConvertible, IEquatable<T>, IFormattable
	{
		var (posLeft, posTop) = Console.GetCursorPosition();
		TypeConverter? converter = null;

		do
		{
			string? entry = RL(prompt);
			if (string.IsNullOrEmpty(entry))
			{ return default; }

			//here's the problem...
			//if (T.TryParse(entry, null, out T value))
			//{ return value; }
			if (converter == null)
			{
				converter = TypeDescriptor.GetConverter(typeof(T));
				if (converter == null)
				{ throw new Exception("Cannot create converter"); }
			}
			if (converter.IsValid(entry))
			{ return (T?)converter.ConvertFromString(entry); }

			Console.SetCursorPosition(posLeft, posTop);
			W(prompt + new string(' ', entry.Length));
			Console.SetCursorPosition(posLeft, posTop);
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

		TimeOnly? time = null;
		do
		{
			var strTm = RL(arguments.Prompt);
			if (string.IsNullOrWhiteSpace(strTm))
			{ break; }

			//time = DateTimeUtil.ParseTime2(strTm);
			time = TimeOnly.Parse(strTm);
			if (time != null && arguments.Validate(time.Value))
			{
				ResetCursorPosition(posLeft, posTop, arguments.Prompt, strTm.Length);
				WL(arguments.Prompt + time.Value.ToLongTimeString());
				break;
			}

			time = null;
			ResetCursorPosition(posLeft, posTop, arguments.Prompt, strTm.Length);

		} while (true);

		return time;
	}


	public static DateOnly? Date(string? prompt)
	{
		var (posLeft, posTop) = Console.GetCursorPosition();
		DateOnly? date = null;
		do
		{
			var strDt = RL(prompt);
			if (string.IsNullOrWhiteSpace(strDt))
			{ break; }

			//date = DateTimeUtil.ParseDate2(strDt);
			date = DateOnly.Parse(strDt);
			if (date != null)
			{
				ResetCursorPosition(posLeft, posTop, prompt, strDt.Length);
				WL(prompt + date.Value.ToShortDateString());
				break;
			}

			date = null;
			ResetCursorPosition(posLeft, posTop, prompt, strDt.Length);

		} while (true);

		return date;
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

	internal abstract bool Validate(T value);
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
		//if (Min == null && Max == null)
		//{ return true; }

		//if (Min != null && Max != null)
		//{ return value >= Min && value <= Max; }

		//if (Max != null)
		//{ return value <= Max; }

		//return value >= Min;
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

	internal override bool Validate(TimeOnly value)
	{
		return ValidateRange(value);
	}

	protected bool ValidateRange(TimeOnly value)
	{
		return value.WithinRange(Min, Max);
		//if (Min == null && Max == null)
		//{ return true; }

		//if (Min != null && Max != null)
		//{ return value >= Min && value <= Max; }

		//if (Max != null)
		//{ return value <= Max; }

		//return value >= Min;
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

	public List<char> SelectNextKeys { get; } = new List<char>(new[] { ' ', (char)ConsoleKey.RightArrow, (char)ConsoleKey.DownArrow });

	public List<char> SelectPreviousKeys { get; } = new List<char>(new[] { (char)ConsoleKey.LeftArrow, (char)ConsoleKey.UpArrow });


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


	internal override bool Validate(T value) => true;
}
