using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Numerics;

using static Strick.PlusCon.Helpers;


namespace Strick.PlusCon.Models;


/// <summary>
/// Methods to collect various types of keyboard input.
/// </summary>
public static class Input
{
	/// <summary>
	/// Prompts for input of a single char.
	/// </summary>
	/// <param name="prompt">The prompt that is displayed</param>
	public static ConsoleKeyInfo Ch(string? prompt) => Ch(new InputArgumentsCh(prompt));

	/// <summary>
	/// <inheritdoc cref="Ch(string?)"/>
	/// </summary>
	/// <param name="arguments">The arguments that contol how the input is collected and validated.</param>
	public static ConsoleKeyInfo Ch(InputArgumentsCh arguments)
	{
		var pos = GetCursorPosition();

		do
		{
			var key = RK(arguments.Prompt, arguments.PromptStyle);
			if (arguments.Validate(key.KeyChar))
			{ return key; }

			if (pos != null)
			{ ResetCursorPosition(pos.Value.X, pos.Value.Y, "", 0); }
		} while (true);
	}


	/// <summary>
	/// <inheritdoc cref="Ch(string?)"/> 
	/// The entered char must be one of <b>YyNn</b>. 
	/// If Y or y, true is returned. If N or n, false is returned.
	/// </summary>
	/// <param name="prompt"><inheritdoc cref="Ch(string?)" path="/param[@name='prompt']"/></param>
	public static bool YN(string? prompt) => YN(prompt, null);

	/// <summary>
	/// <inheritdoc cref="YN(string?)"/> 
	/// </summary>
	/// <param name="prompt"><inheritdoc cref="Ch(string?)" path="/param[@name='prompt']"/></param>
	/// <param name="promptStyle"><inheritdoc cref="InputArguments{T}.PromptStyle" path="/summary"/></param>
	public static bool YN(string? prompt, TextStyle? promptStyle)
	{
		InputArgumentsCh args = new InputArgumentsCh(prompt, ['Y', 'y', 'N', 'n'])
		{
			PromptStyle = promptStyle
		};

		var key = Ch(args);
		return char.ToUpperInvariant(key.KeyChar) == 'Y';
	}


	/// <summary>
	/// <inheritdoc cref="Any(string?)"/>
	/// <para>The default prompt is displayed. See <see cref="Any(string?)"/> for more on the prompt.</para>
	/// </summary>
	public static ConsoleKeyInfo Any() => Any(null);

	/// <summary>
	/// Prompts for input of a single char. 
	/// Returns a <see cref="ConsoleKeyInfo"/> structure containing the key that was pressed. 
	/// Any key is accepted. 
	/// </summary>
	/// <param name="prompt"><inheritdoc cref="Ch(string?)" path="/param[@name='prompt']"/>. 
	/// Omit, or pass null to show the default prompt of "Press Any Key ". 
	/// To display no prompt, pass an empty string for the <paramref name="prompt"/> argument.</param>
	public static ConsoleKeyInfo Any(string? prompt) => Any(prompt, null);

	/// <summary>
	/// <inheritdoc cref="Any(string?)"/>
	/// </summary>
	/// <param name="prompt"><inheritdoc cref="Ch(string?)" path="/param[@name='prompt']"/>. 
	/// Omit, or pass null to show the default prompt of "Press Any Key ". 
	/// To display no prompt, pass an empty string for the <paramref name="prompt"/> argument.</param>
	/// <param name="promptStyle"><inheritdoc cref="InputArguments{T}.PromptStyle" path="/summary"/></param>
	public static ConsoleKeyInfo Any(string? prompt, TextStyle? promptStyle)
	{
		InputArgumentsCh args = new()
		{
			PromptStyle = promptStyle
		};

		if (prompt != null)
		{ args.Prompt = prompt; }
		else
		{ args.Prompt = "Press Any Key "; }

		return Ch(args);
	}


	/// <summary>
	/// Prompts for the entry of a numeric value.
	/// </summary>
	/// <typeparam name="T">The numeric type of the value to be returned. 
	/// The type must be a <c>struct</c> and implement the <see cref="INumber{TSelf}"/> interface.
	/// This includes all numeric value types</typeparam>
	/// <param name="prompt"><inheritdoc cref="Ch(string?)" path="/param[@name='prompt']"/></param>
	public static T? Number<T>(string? prompt) where T : struct, INumber<T>
	{
		return Number(new InputArgumentsNumber<T>(prompt));
	}

	/// <summary>
	/// <inheritdoc cref="Number{T}(string?)"/>
	/// </summary>
	/// <typeparam name="T"><inheritdoc cref="Number{T}(string?)" path="/typeparam[@name='T']"/></typeparam>
	/// <param name="arguments"><inheritdoc cref="Ch(InputArgumentsCh)" path="/param[@name='arguments']"/></param>
	public static T? Number<T>(InputArgumentsNumber<T> arguments) where T : struct, INumber<T>
	{
		var pos = GetCursorPosition();

		do
		{
			string? entry = RL(arguments.Prompt, arguments.PromptStyle);
			if (string.IsNullOrEmpty(entry))
			{ return default; }

			if (T.TryParse(entry, null, out T value))
			{
				if (arguments.Validate(value))
				{ return value; }
			}

			if (pos != null)
			{ ResetCursorPosition(pos.Value.X, pos.Value.Y, arguments.Prompt, entry.Length); }
		} while (true);
	}


	/// <summary>
	/// Prompts for the entry of a text value.
	/// Returns a <see cref="string"/> containing the entered text, 
	/// or null if the user presses only the enter key (without entering anything).
	/// </summary>
	/// <param name="prompt"><inheritdoc cref="Ch(string?)" path="/param[@name='prompt']"/></param>
	public static string? Text(string? prompt) => Text(new InputArgumentsText(prompt));

	/// <summary>
	/// <inheritdoc cref="Text(string?)"/>
	/// </summary>
	/// <param name="arguments"><inheritdoc cref="Ch(InputArgumentsCh)" path="/param[@name='arguments']"/></param>
	public static string? Text(InputArgumentsText arguments)
	{
		var pos = GetCursorPosition();

		do
		{
			var txt = RL(arguments.Prompt, arguments.PromptStyle);

			if (txt != null && arguments.Validate(txt))
			{ return txt; }

			if (pos != null)
			{ ResetCursorPosition(pos.Value.X, pos.Value.Y, arguments.Prompt, txt == null ? 0 : txt.Length); }
		} while (true);
	}


	/// <summary>
	/// Prompts for the entry of a time (time of day) value.
	/// Returns a <see cref="Nullable{TimeOnly}"/> containing the time value entered, 
	/// or null if the enter key is pressed (without entering a time).
	/// </summary>
	/// <param name="prompt"><inheritdoc cref="Ch(string?)" path="/param[@name='prompt']"/></param>
	public static TimeOnly? Time(string? prompt) => Time(new InputArgumentsTime() { Prompt = prompt });

	/// <summary>
	/// <inheritdoc cref="Time(string?)"/>
	/// </summary>
	/// <param name="arguments"><inheritdoc cref="Ch(InputArgumentsCh)" path="/param[@name='arguments']"/></param>
	public static TimeOnly? Time(InputArgumentsTime arguments)
	{
		var pos = GetCursorPosition();

		do
		{
			var strTm = RL(arguments.Prompt, arguments.PromptStyle);
			if (string.IsNullOrWhiteSpace(strTm))
			{ break; }

			if (arguments.ParseFunction(strTm, out TimeOnly time))
			{
				if (arguments.Validate(time))
				{
					if (pos != null)
					{ ResetCursorPosition(pos.Value.X, pos.Value.Y, arguments.Prompt, strTm.Length); }
					WL(arguments.Prompt + time.ToLongTimeString());
					return time;
				}
			}

			if (pos != null)
			{ ResetCursorPosition(pos.Value.X, pos.Value.Y, arguments.Prompt, strTm.Length); }

		} while (true);

		return null;
	}


	/// <summary>
	/// Prompts for the entry of a date value. 
	/// Returns a <see cref="Nullable{DateOnly}"/> containing the date value entered, 
	/// or null if the enter key is pressed (without entering a date).
	/// </summary>
	/// <param name="prompt"><inheritdoc cref="Ch(string?)" path="/param[@name='prompt']"/></param>
	public static DateOnly? Date(string? prompt) => Date(new InputArgumentsDate() { Prompt = prompt });

	/// <summary>
	/// <inheritdoc cref="Date(string?)"/>
	/// </summary>
	/// <param name="arguments"><inheritdoc cref="Ch(InputArgumentsCh)" path="/param[@name='arguments']"/></param>
	public static DateOnly? Date(InputArgumentsDate arguments)
	{
		var pos = GetCursorPosition();

		do
		{
			var strDt = RL(arguments.Prompt, arguments.PromptStyle);
			if (string.IsNullOrWhiteSpace(strDt))
			{ break; }

			if (arguments.ParseFunction(strDt, out DateOnly date))
			{
				if (arguments.Validate(date))
				{
					if (pos != null)
					{ ResetCursorPosition(pos.Value.X, pos.Value.Y, arguments.Prompt, strDt.Length); }
					WL(arguments.Prompt + date.ToShortDateString());
					return date;
				}
			}

			if (pos != null)
			{ ResetCursorPosition(pos.Value.X, pos.Value.Y, arguments.Prompt, strDt.Length); }

		} while (true);

		return null;
	}


	/// <summary>
	/// Allows the user to select either Yes or No. 
	/// If Yes is selected, true is returned. if No is selected, false is returned.
	/// <para>See also <see cref="Extensions.YesNo(bool)"/></para>
	/// </summary>
	/// <param name="prompt"><inheritdoc cref="Ch(string?)" path="/param[@name='prompt']"/></param>
	public static bool? SelectYesNo(string? prompt) => SelectYesNo(prompt, false);

	/// <summary>
	/// <inheritdoc cref="SelectYesNo(string?)"/>
	/// </summary>
	/// <param name="prompt"><inheritdoc cref="SelectYesNo(string?)" path="/param[@name='prompt']"/></param>
	/// <param name="initialSelection">If true, Yes will be initially selected. 
	/// If false, No will be initially selected.</param>
	public static bool? SelectYesNo(string? prompt, bool? initialSelection) => SelectYesNo(prompt, initialSelection?.YesNo());

	/// <summary>
	/// <inheritdoc cref="SelectYesNo(string?)"/>
	/// </summary>
	/// <param name="prompt"><inheritdoc cref="SelectYesNo(string?)" path="/param[@name='prompt']"/></param>
	/// <param name="initialSelection">The option (Yes or No, case insensitive) that will be initially selected.</param>
	public static bool? SelectYesNo(string? prompt, string? initialSelection)
	{
		IEnumerable<string> options = [true.YesNo(), false.YesNo()];

		//correct lower case
		if (initialSelection != null && !options.Contains(initialSelection))
		{
			initialSelection = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(initialSelection);
			if (!options.Contains(initialSelection))
			{
				initialSelection = null;
			}
		}

		InputArgumentsSelect<string> args = new(prompt, options, initialSelection);
		string? selected = Select(args);

		if (selected == null)
		{ return null; }

		return selected == "Yes";
	}

	/// <summary>
	/// Presents a list of options and allows one to be selected. Returns the selected option, or null if escape is pressed. 
	/// The options are of the type specified by the type parameter <typeparamref name="T"/>.
	/// </summary>
	/// <typeparam name="T">The type for the options. The <see cref="object.ToString"/> method is used to display each option.</typeparam>
	/// <param name="arguments"><inheritdoc cref="Ch(InputArgumentsCh)" path="/param[@name='arguments']"/></param>
	/// <exception cref="ArgumentNullException"></exception>
	public static T? Select<T>(InputArgumentsSelect<T> arguments)
	{
		ArgumentNullException.ThrowIfNull(arguments);

		var pos = GetCursorPosition();

		do
		{
			W(arguments.Prompt + arguments.SelectionOptionStyle.StyleText(arguments.SelectedOption!.ToString()!));
			var k = Console.ReadKey(true);

			if (k.Key == ConsoleKey.Enter)
			{ break; }

			if (k.Key == ConsoleKey.Escape)
			{
				arguments.ClearSelection();
				break;
			}

			if (pos != null)
			{ ResetCursorPosition(pos.Value.X, pos.Value.Y, arguments.Prompt, arguments.SelectedOption!.ToString()!.Length); }
			if (arguments.SelectNextKeys.Contains((char)k.Key))
			{ arguments.SelectNext(); }
			if (arguments.SelectPreviousKeys.Contains((char)k.Key))
			{ arguments.SelectPrevious(); }

		} while (true);

		return arguments.SelectedOption;
	}


	private static Point? GetCursorPosition()
	{
		//if the console input stream is redirected, Console.GetCursorPosition will throw an exception
		try
		{
			var (posLeft, posTop) = Console.GetCursorPosition();
			return new Point(posLeft, posTop);
		}
		catch
		{ }

		return null;
	}

	private static void ResetCursorPosition(int posLeft, int posTop, string? prompt, int entryLength)
	{
		Console.SetCursorPosition(posLeft, posTop);
		W(prompt + new string(' ', entryLength));
		Console.SetCursorPosition(posLeft, posTop);
	}
}
