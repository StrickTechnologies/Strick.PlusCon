using System;
using System.Drawing;
using System.Text.RegularExpressions;

using Strick.PlusCon.Models;


namespace Strick.PlusCon;


/// <summary>
/// Shortcuts and helper methods for <see cref="Console"/>.
/// </summary>
public static class Helpers
{
	/// <summary>
	/// <para>A simple shortcut for <see cref="Console.Write(string)"/>.</para>
	/// </summary>
	/// <param name="message">The value to write to the console. 
	/// A null or empty value results in nothing being written to the console.
	/// </param>
	public static void W(string? message) => Console.Write(message);

	/// <summary>
	/// <para>A simple shortcut for <see cref="Console.WriteLine()"/></para>
	/// </summary>
	public static void WL() => Console.WriteLine();

	/// <summary>
	/// <para>A simple shortcut for <see cref="Console.WriteLine(string)"/></para>
	/// </summary>
	/// <param name="message">The value to write to the console. 
	/// A null or empty value results in only the current line terminator being written to the console.
	/// </param>
	public static void WL(string? message) => Console.WriteLine(message);


	//Original version inspired from https://stackoverflow.com/a/60492990/1585667

	/// <summary>
	/// <inheritdoc cref="W(string?)"/>
	/// <span id="delimBase">Writes any portions of <paramref name="message"/> enclosed in "[" and "]" using the colors <paramref name="fore"/> and <paramref name="back"/>.</span>
	/// <span id="delimNoDisplay"> The delimiters ("[" and "]") are NOT displayed.</span>
	/// <para id="noDelim"> If no portion of <paramref name="message"/> is enclosed in "[" and "]", 
	/// the entire message is written using the colors <paramref name="fore"/> and <paramref name="back"/>.</para>
	/// </summary>
	/// <param name="message"><inheritdoc cref="W(string?)" path="/param[@name='message']"/></param>
	/// <param name="fore">The foreground color used for displaying the <paramref name="message"/> argument.</param>
	/// <param name="back">The background color used for displaying the <paramref name="message"/> argument.</param>
	public static void W(string? message, Color fore, Color? back = null) => W(message, fore, back, null);

	/// <summary>
	/// <inheritdoc cref="W(string?)"/>
	/// <inheritdoc cref="W(string?, Color, Color?)" path="/summary/span[@id='delimBase']"/>
	/// <span id="delimBool">The delimiters ("[" and "]") are displayed or not based on the value of the <paramref name="showDelimiters"/> argument.</span>
	/// <inheritdoc cref="W(string?, Color, Color?)" path="/summary/para[@id='noDelim']"/>
	/// </summary>
	/// <param name="message"><inheritdoc cref="W(string, Color, Color?)" path="/param[@name='message']"/></param>
	/// <param name="fore"><inheritdoc cref="W(string, Color, Color?)" path="/param[@name='fore']"/></param>
	/// <param name="back"><inheritdoc cref="W(string, Color, Color?)" path="/param[@name='back']"/></param>
	/// <param name="showDelimiters">A bool indicating whether or not to show the delimeters ("[" and "]"), 
	/// if any are included in the <paramref name="message"/> argument. 
	/// The delimiters are displayed using the default console colors.</param>
	public static void W(string? message, Color fore, Color? back, bool showDelimiters) => W(message, fore, back, showDelimiters ? Color.Transparent : null);

	/// <summary>
	/// <inheritdoc cref="W(string?)"/>
	/// <inheritdoc cref="W(string?, Color, Color?)" path="/summary/span[@id='delimBase']"/>
	/// <span id="delimColors">The delimiters ("[" and "]") are displayed based on the value of the <paramref name="delimFore"/> and <paramref name="delimBack"/> 
	/// arguments -- if either is NOT null, the delimiters are displayed using those colors.</span>
	/// <inheritdoc cref="W(string?, Color, Color?)" path="/summary/para[@id='noDelim']"/>
	/// </summary>
	/// <param name="message"><inheritdoc cref="W(string, Color, Color?)" path="/param[@name='message']"/></param>
	/// <param name="fore"><inheritdoc cref="W(string, Color, Color?)" path="/param[@name='fore']"/></param>
	/// <param name="back"><inheritdoc cref="W(string, Color, Color?)" path="/param[@name='back']"/></param>
	/// <param name="delimFore">The foreground color to use for the delimiters "[" and "]", 
	/// if any are included in the <paramref name="message"/> argument. 
	/// </param>
	/// <param name="delimBack">The background color to use for the delimiters "[" and "]", 
	/// if any are included in the <paramref name="message"/> argument. 
	/// </param>
	public static void W(string? message, Color fore, Color? back, Color? delimFore = null, Color? delimBack = null)
	{
		if(string.IsNullOrEmpty( message))
		{ return; }


		char delimL = '[';
		char delimR = ']';
		//var segments = Regex.Split(message, @"(\[[^\]]*\])");
		var segments = Regex.Split(message, @$"(\{delimL}[^\]]*\{delimR})");

		//no values enclosed within delimiters, so just colorize the whole message...
		if (segments != null && segments.Length == 1 && !(segments[0].StartsWith(delimL) && segments[0].EndsWith(delimR)))
		{
			W(message.Colorize(fore, back));
			//*********
			return;
			//*********
		}

		for (int x = 0; x < segments!.Length; x++)
		{
			string segment = segments[x];

			if (segment.StartsWith(delimL) && segment.EndsWith(delimR))
			{
				segment = segment[1..^1];

				if (delimFore == null && delimBack == null)
				{ W(segment.Colorize(fore, back)); }

				else
				{ W(segment.Colorize(fore, back, delimL.ToString(), delimR.ToString(), delimFore == Color.Transparent ? null : delimFore, delimBack)); }
			}

			else
			{ W(segment); }
		}
	}

	/// <summary>
	/// <inheritdoc cref="WL(string)"/>
	/// <inheritdoc cref="W(string?, Color, Color?)" path="/summary/span[@id='delimBase']"/>
	/// <inheritdoc cref="W(string?, Color, Color?)" path="/summary/span[@id='delimNoDisplay']"/>
	/// <inheritdoc cref="W(string?, Color, Color?)" path="/summary/para[@id='noDelim']"/>
	/// </summary>
	/// <param name="message"><inheritdoc cref="WL(string)" path="/param[@name='message']"/></param>
	/// <param name="fore"><inheritdoc cref="W(string, Color, Color?)" path="/param[@name='fore']"/></param>
	/// <param name="back"><inheritdoc cref="W(string, Color, Color?)" path="/param[@name='back']"/></param>
	public static void WL(string? message, Color fore, Color? back = null) => WL(message, fore, back, null, null);

	/// <summary>
	/// <inheritdoc cref="WL(string)"/>
	/// <inheritdoc cref="W(string?, Color, Color?, bool)" path="/summary/span[@id='delimBase']"/>
	/// <inheritdoc cref="W(string?, Color, Color?, bool)" path="/summary/span[@id='delimBool']"/>
	/// <inheritdoc cref="W(string?, Color, Color?)" path="/summary/para[@id='noDelim']"/>
	/// </summary>
	/// <param name="message"><inheritdoc cref="WL(string, Color, Color?)" path="/param[@name='message']"/></param>
	/// <param name="fore"><inheritdoc cref="WL(string, Color, Color?)" path="/param[@name='fore']"/></param>
	/// <param name="back"><inheritdoc cref="WL(string, Color, Color?)" path="/param[@name='back']"/></param>
	/// <param name="showDelimiters"><inheritdoc cref="W(string, Color, Color?, bool)" path="/param[@name='showDelimiters']"/></param>
	public static void WL(string? message, Color fore, Color? back, bool showDelimiters) => WL(message, fore, back, showDelimiters ? Color.Transparent : null);

	/// <summary>
	/// <inheritdoc cref="WL(string)"/>
	/// <inheritdoc cref="W(string?, Color, Color?, bool)" path="/summary/span[@id='delimBase']"/>
	/// <inheritdoc cref="W(string?, Color, Color?, Color?, Color?)" path="/summary/span[@id='delimColors']"/>
	/// <inheritdoc cref="W(string?, Color, Color?)" path="/summary/para[@id='noDelim']"/>
	/// </summary>
	/// <param name="message"><inheritdoc cref="WL(string, Color, Color?, bool)" path="/param[@name='message']"/></param>
	/// <param name="fore"><inheritdoc cref="WL(string, Color, Color?, bool)" path="/param[@name='fore']"/></param>
	/// <param name="back"><inheritdoc cref="WL(string, Color, Color?, bool)" path="/param[@name='back']"/></param>
	/// <param name="delimFore"><inheritdoc cref="W(string, Color, Color?, Color?, Color?)" path="/param[@name='delimFore']"/></param>
	/// <param name="delimBack"><inheritdoc cref="W(string, Color, Color?, Color?, Color?)" path="/param[@name='delimBack']"/></param>
	public static void WL(string? message, Color fore, Color? back, Color? delimFore = null, Color? delimBack = null)
	{
		W(message, fore, back, delimFore, delimBack);
		WL();
	}


	/// <summary>
	/// <inheritdoc cref="W(string?)"/>
	/// </summary>
	/// <param name="message"><inheritdoc cref="W(string?)" path="/param[@name='message']"/></param>
	/// <param name="style">The <see cref="TextStyle"/> object used to style the <paramref name="message"/> argument. 
	/// If null, the <paramref name="message"/> argument is written without styling.</param>
	public static void W(string? message, TextStyle style)
	{
		if (string.IsNullOrEmpty(message))
		{ return; }

		if (style == null)
		{
			W(message);
			return;
		}

		W(style.StyleText(message));
	}

	/// <summary>
	/// <inheritdoc cref="WL(string?)"/>
	/// </summary>
	/// <param name="message"><inheritdoc cref="WL(string?)" path="/param[@name='message']"/></param>
	/// <param name="style"><inheritdoc cref="W(string?, TextStyle)" path="/param[@name='style']"/></param>
	public static void WL(string? message, TextStyle style)
	{
		W(message, style);
		WL();
	}

	/// <summary>
	/// <inheritdoc cref="W(string?)"/>
	/// </summary>
	/// <param name="message">A <see cref="StyledText"/> object that contains 
	/// the value to write (<see cref="StyledText.Text"/>) to the console 
	/// and the style (<see cref="StyledText.Style"/>) used to style the value. 
	/// If null, nothing is written to the console.</param>
	public static void W(StyledText? message)
	{
		if (message == null)
		{ return; }

		W(message.TextStyled);
	}

	/// <summary>
	/// <inheritdoc cref="WL(string?)"/>
	/// </summary>
	/// <param name="message">A <see cref="StyledText"/> object that contains 
	/// the value to write (<see cref="StyledText.Text"/>) to the console
	/// and the style (<see cref="StyledText.Style"/>) used to style the value. 
	/// If null, only the current line terminator is written to the console.</param>
	public static void WL(StyledText? message)
	{
		W(message);
		WL();
	}


	/// <summary>
	/// Displays <paramref name="prompt"/> if not null or white space, then waits for a key press. 
	/// If <paramref name="prompt"/> is null or white space, simply waits for a key press.
	/// </summary>
	/// <param name="prompt">Optional value to display</param>
	/// <returns>A <see cref="ConsoleKeyInfo"/> object containing information about the key pressed.</returns>
	public static ConsoleKeyInfo RK(string? prompt = null)
	{
		W(prompt);
		return Console.ReadKey();
	}

	/// <summary>
	/// <inheritdoc cref="RK(string?)"/>. 
	/// Uses <see cref="W(string?, Color, Color?, Color?, Color?)"/> to display the prompt.
	/// </summary>
	/// <param name="prompt"><inheritdoc cref="W(string?, Color, Color?, Color?, Color?)" path="/param[@name='message']"/></param>
	/// <param name="fore"><inheritdoc cref="W(string?, Color, Color?, Color?, Color?)" path="/param[@name='fore']"/></param>
	/// <param name="back"><inheritdoc cref="W(string?, Color, Color?, Color?, Color?)" path="/param[@name='back']"/></param>
	/// <param name="delimFore"><inheritdoc cref="W(string?, Color, Color?, Color?, Color?)" path="/param[@name='delimFore']"/></param>
	/// <param name="delimBack"><inheritdoc cref="W(string?, Color, Color?, Color?, Color?)" path="/param[@name='delimBack']"/></param>
	/// <returns><inheritdoc cref="RK(string?)" path="/returns"/></returns>
	public static ConsoleKeyInfo RK(string? prompt, Color fore, Color? back, Color? delimFore = null, Color? delimBack = null)
	{
		W(prompt, fore, back, delimFore, delimBack);
		return Console.ReadKey();
	}

	/// <summary>
	/// <inheritdoc cref="RK(string?)"/>. 
	/// Uses <see cref="W(string?, TextStyle?)"/> to display the prompt.
	/// </summary>
	/// <param name="prompt"><inheritdoc cref="W(string?, TextStyle?)" path="/param[@name='message']"/></param>
	/// <param name="style"><inheritdoc cref="W(string?, TextStyle?)" path="/param[@name='style']"/></param>
	/// <returns><inheritdoc cref="RK(string?)" path="/returns"/></returns>
	public static ConsoleKeyInfo RK(string? prompt, TextStyle style)
	{
		W(prompt, style);
		return Console.ReadKey();
	}

	/// <summary>
	/// <inheritdoc cref="RK(string?)"/>. 
	/// Uses <see cref="W(StyledText?)"/> to display the prompt.
	/// </summary>
	/// <param name="prompt"><inheritdoc cref="W(StyledText?)" path="/param[@name='message']"/></param>
	/// <returns><inheritdoc cref="RK(string?)" path="/returns"/></returns>
	public static ConsoleKeyInfo RK(StyledText? prompt)
	{
		W(prompt);
		return Console.ReadKey();
	}


	/// <summary>
	/// Displays <paramref name="prompt"/> if not null or white space, then waits for user input. 
	/// If <paramref name="prompt"/> is null or white space, simply waits for user input.
	/// <para>Allows the user to enter characters until the <c>&lt;enter&gt;</c> key is pressed.</para>
	/// </summary>
	/// <param name="prompt">Optional value to display</param>
	/// <returns>The characters entered. If only <c>&lt;enter&gt;</c> an empty string is returned.</returns>
	public static string? RL(string? prompt = null)
	{
		W(prompt);
		return Console.ReadLine();
	}

	/// <summary>
	/// <inheritdoc cref="RL(string?)"/> 
	/// Uses <see cref="W(string?, Color, Color?, Color?, Color?)"/> to display the prompt.
	/// </summary>
	/// <param name="prompt"><inheritdoc cref="W(string?, Color, Color?, Color?, Color?)" path="/param[@name='message']"/></param>
	/// <param name="fore"><inheritdoc cref="W(string?, Color, Color?, Color?, Color?)" path="/param[@name='fore']"/></param>
	/// <param name="back"><inheritdoc cref="W(string?, Color, Color?, Color?, Color?)" path="/param[@name='back']"/></param>
	/// <param name="delimFore"><inheritdoc cref="W(string?, Color, Color?, Color?, Color?)" path="/param[@name='delimFore']"/></param>
	/// <param name="delimBack"><inheritdoc cref="W(string?, Color, Color?, Color?, Color?)" path="/param[@name='delimBack']"/></param>
	/// <returns><inheritdoc cref="RL(string?)" path="/returns"/></returns>
	public static string? RL(string? prompt, Color fore, Color? back, Color? delimFore = null, Color? delimBack = null)
	{
		W(prompt, fore, back, delimFore, delimBack);
		return Console.ReadLine();
	}

	/// <summary>
	/// <inheritdoc cref="RL(string?)"/> 
	/// Uses <see cref="W(string?, TextStyle?)"/> to display the prompt.
	/// </summary>
	/// <param name="prompt"><inheritdoc cref="W(string?, TextStyle?)" path="/param[@name='message']"/></param>
	/// <param name="style"><inheritdoc cref="W(string?, TextStyle?)" path="/param[@name='style']"/></param>
	/// <returns><inheritdoc cref="RL(string?)" path="/returns"/></returns>
	public static string? RL(string? prompt, TextStyle style)
	{
		W(prompt, style);
		return Console.ReadLine();
	}

	/// <summary>
	/// <inheritdoc cref="RL(string?)"/> 
	/// Uses <see cref="W(StyledText?)"/> to display the prompt.
	/// </summary>
	/// <param name="prompt"><inheritdoc cref="W(StyledText?)" path="/param[@name='message']"/></param>
	/// <returns><inheritdoc cref="RL(string?)" path="/returns"/></returns>
	public static string? RL(StyledText? prompt)
	{
		W(prompt);
		return Console.ReadLine();
	}


	/// <summary>
	/// <para>A simple shortcut for <see cref="Console.Clear()"/></para>
	/// </summary>
	public static void CLS()
	{
		//just to catch an io exception if console output is redirected
		try
		{
			Console.Clear();
		}
		catch { }
	}

	/// <summary>
	/// <inheritdoc cref="CLS()"/>
	/// Applies the colors <paramref name="back"/> and/or <paramref name="fore"/> (if non-null) to the screen before clearing. 
	/// <br />Note: the color(s) will remain in effect until a reset sequence (see <see cref="EscapeCodes.ColorReset_Back"/>, <see cref="EscapeCodes.ColorReset_Fore"/>) is sent to the console. 
	/// This would happen, for example, by using the <see cref="Formatting.Colorize(string, Color?, Color?)"/> function (and others).
	/// </summary>
	/// <param name="back">The background color to use for the console window</param>
	/// <param name="fore">The foreground color to use for content displayed in the console window</param>
	public static void CLS(Color? back = null, Color? fore = null)
	{
		if (back != null)
		{ W(EscapeCodes.GetBackColorSequence(back.Value)); }

		if (fore != null)
		{ W(EscapeCodes.GetForeColorSequence(fore.Value)); }

		CLS();
	}
}
