using System;
using System.Drawing;
using System.Text.RegularExpressions;


namespace Strick.PlusCon;


/// <summary>
/// Shortcuts and helper methods for <see cref="Console"/>.
/// </summary>
public static class Helpers
{
	/// <summary>
	/// <para>A simple shortcut for <see cref="Console.Write(string)"/>.</para>
	/// </summary>
	/// <param name="message">The value to write.</param>
	public static void W(string message) => Console.Write(message);

	/// <summary>
	/// <para>A simple shortcut for <see cref="Console.WriteLine()"/></para>
	/// </summary>
	public static void WL() => Console.WriteLine();

	/// <summary>
	/// <para>A simple shortcut for <see cref="Console.WriteLine(string)"/></para>
	/// </summary>
	/// <param name="message"><inheritdoc cref="W(string)" path="/param[@name='message']"/></param>
	public static void WL(string message) => Console.WriteLine(message);


	//Original version inspired from https://stackoverflow.com/a/60492990/1585667

	/// <summary>
	/// <inheritdoc cref="W(string)"/>
	/// Writes any portions of <paramref name="message"/> enclosed in "[" and "]" using the colors <paramref name="fore"/> and <paramref name="back"/>. 
	/// The delimiters ("[" and "]") are NOT displayed.
	/// <para>If no portion of <paramref name="message"/> is enclosed in "[" and "]", 
	/// the entire message is written using the colors <paramref name="fore"/> and <paramref name="back"/>.</para>
	/// </summary>
	/// <param name="message"><inheritdoc cref="W(string)" path="/param[@name='message']"/></param>
	/// <param name="fore">The foreground color to use when displaying values enclosed in "[" and "]"</param>
	/// <param name="back">The background color to use when displaying values enclosed in "[" and "]"</param>
	public static void W(string message, Color fore, Color? back = null) => W(message, fore, back, null);

	/// <summary>
	/// <inheritdoc cref="W(string)"/>
	/// Writes any portions of <paramref name="message"/> enclosed in "[" and "]" using the colors <paramref name="fore"/> and <paramref name="back"/>. 
	/// The delimiters ("[" and "]") are displayed based on the value of the <paramref name="showDelimiters"/> argument.
	/// <para>If no portion of <paramref name="message"/> is enclosed in "[" and "]", 
	/// the entire message is written using the colors <paramref name="fore"/> and <paramref name="back"/>.</para>
	/// </summary>
	/// <param name="message"><inheritdoc cref="W(string, Color, Color?)" path="/param[@name='message']"/></param>
	/// <param name="fore"><inheritdoc cref="W(string, Color, Color?)" path="/param[@name='fore']"/></param>
	/// <param name="back"><inheritdoc cref="W(string, Color, Color?)" path="/param[@name='back']"/></param>
	/// <param name="showDelimiters">A <see cref="bool"/> indicating whether or not to show the delimeters ("[" and "]"). 
	/// The delimiters are display using the default console colors.</param>
	public static void W(string message, Color fore, Color? back, bool showDelimiters) => W(message, fore, back, showDelimiters ? Color.Transparent : null);

	/// <summary>
	/// <inheritdoc cref="W(string)"/>
	/// Writes any portions of <paramref name="message"/> enclosed in "[" and "]" using the colors <paramref name="fore"/> and <paramref name="back"/>. 
	/// The delimiters ("[" and "]") are displayed based on the value of the <paramref name="delimFore"/> and <paramref name="delimBack"/> 
	/// arguments -- if either is NOT null, the delimiters are displayed using those colors.
	/// <para>If no portion of <paramref name="message"/> is enclosed in "[" and "]", 
	/// the entire message is written using the colors <paramref name="fore"/> and <paramref name="back"/>.</para>
	/// </summary>
	/// <param name="message"><inheritdoc cref="W(string, Color, Color?)" path="/param[@name='message']"/></param>
	/// <param name="fore"><inheritdoc cref="W(string, Color, Color?)" path="/param[@name='fore']"/></param>
	/// <param name="back"><inheritdoc cref="W(string, Color, Color?)" path="/param[@name='back']"/></param>
	/// <param name="delimFore">The foreground color to use for the delimiters "[" and "]"</param>
	/// <param name="delimBack">The background color to use for the delimiters "[" and "]"</param>
	public static void W(string message, Color fore, Color? back, Color? delimFore = null, Color? delimBack = null)
	{
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
	/// Writes any portions of <paramref name="message"/> enclosed in "[" and "]" using the colors <paramref name="fore"/> and <paramref name="back"/>. 
	/// The delimiters ("[" and "]") are NOT displayed.
	/// <para>If no portion of <paramref name="message"/> is enclosed in "[" and "]", 
	/// the entire message is written using the colors <paramref name="fore"/> and <paramref name="back"/>.</para>
	/// </summary>
	/// <param name="message"><inheritdoc cref="W(string, Color, Color?)" path="/param[@name='message']"/></param>
	/// <param name="fore"><inheritdoc cref="W(string, Color, Color?)" path="/param[@name='fore']"/></param>
	/// <param name="back"><inheritdoc cref="W(string, Color, Color?)" path="/param[@name='back']"/></param>
	public static void WL(string message, Color fore, Color? back = null) => WL(message, fore, back, null, null);

	/// <summary>
	/// <inheritdoc cref="WL(string)"/>
	/// Writes any portions of <paramref name="message"/> enclosed in "[" and "]" using the colors <paramref name="fore"/> and <paramref name="back"/>. 
	/// The delimiters ("[" and "]") are displayed based on the value of the <paramref name="showDelimiters"/> argument.
	/// <para>If no portion of <paramref name="message"/> is enclosed in "[" and "]", 
	/// the entire message is written using the colors <paramref name="fore"/> and <paramref name="back"/>.</para>
	/// </summary>
	/// <param name="message"><inheritdoc cref="W(string, Color, Color?, bool)" path="/param[@name='message']"/></param>
	/// <param name="fore"><inheritdoc cref="W(string, Color, Color?, bool)" path="/param[@name='fore']"/></param>
	/// <param name="back"><inheritdoc cref="W(string, Color, Color?, bool)" path="/param[@name='back']"/></param>
	/// <param name="showDelimiters"><inheritdoc cref="W(string, Color, Color?, bool)" path="/param[@name='showDelimiters']"/></param>
	public static void WL(string message, Color fore, Color? back, bool showDelimiters) => WL(message, fore, back, showDelimiters ? Color.Transparent : null);

	/// <summary>
	/// <inheritdoc cref="WL(string)"/>
	/// Writes any portions of <paramref name="message"/> enclosed in "[" and "]" using the colors <paramref name="fore"/> and <paramref name="back"/>. 
	/// The delimiters ("[" and "]") are displayed based on the value of the <paramref name="delimFore"/> and <paramref name="delimBack"/> 
	/// arguments -- if either is NOT null, the delimiters are displayed using those colors.
	/// <para>If no portion of <paramref name="message"/> is enclosed in "[" and "]", 
	/// the entire message is written using the colors <paramref name="fore"/> and <paramref name="back"/>.</para>
	/// </summary>
	/// <param name="message"><inheritdoc cref="WL(string, Color, Color?, bool)" path="/param[@name='message']"/></param>
	/// <param name="fore"><inheritdoc cref="WL(string, Color, Color?, bool)" path="/param[@name='fore']"/></param>
	/// <param name="back"><inheritdoc cref="WL(string, Color, Color?, bool)" path="/param[@name='back']"/></param>
	/// <param name="delimFore"><inheritdoc cref="W(string, Color, Color?, Color?, Color?)" path="/param[@name='delimFore']"/></param>
	/// <param name="delimBack"><inheritdoc cref="W(string, Color, Color?, Color?, Color?)" path="/param[@name='delimBack']"/></param>
	public static void WL(string message, Color fore, Color? back, Color? delimFore = null, Color? delimBack = null)
	{
		W(message, fore, back, delimFore, delimBack);
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
		if (!string.IsNullOrWhiteSpace(prompt))
		{ W(prompt); }

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
		if (!string.IsNullOrWhiteSpace(prompt))
		{ W(prompt); }

		return Console.ReadLine();
	}


	//public static void w(string message, ConsoleColor color, bool stripDelimiters = true)
	//{
	//	var segments = Regex.Split(message, @"(\[[^\]]*\])");
	//	for (int x = 0; x < segments.Length; x++)
	//	{
	//		string segment = segments[x];

	//		if (segment.StartsWith("[") && segment.EndsWith("]"))
	//		{
	//			Console.ForegroundColor = color;

	//			if (stripDelimiters)
	//			{ segment = segment.Substring(1, segment.Length - 2); }
	//		}

	//		w(segment);
	//		Console.ResetColor();
	//	}
	//}
	//public static void wl(string message, ConsoleColor color, bool stripDelimiters = true)
	//{
	//	w(message, color, stripDelimiters);
	//	wl();
	//}
}
