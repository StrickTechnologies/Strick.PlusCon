using System;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;


namespace Strick.PlusCon;


/// <summary>
/// Methods for formatting values to dipslay in the console.
/// </summary>
public static class Formatting
{

	//see this stack overflow answer: https://stackoverflow.com/a/33206814/1585667
	//and this: https://en.wikipedia.org/wiki/ANSI_escape_code
	//also see: https://github.com/silkfire/Pastel


	/// <summary>
	/// <para>Returns a string containing <paramref name="value"/> wrapped with the escape 
	/// sequences for the foreground color <paramref name="fore"/> and background color <paramref name="back"/>.</para>
	/// </summary>
	/// <param name="value">The value to wrap with escape sequences</param>
	/// <param name="fore">The foreground color. If null, no escape sequence is included in the result for foreground color.</param>
	/// <param name="back">The background color If null, no escape sequence is included in the result for background color.</param>
	public static string Colorize(this string value, Color? fore, Color? back = null)
	{
		if (fore == null && back == null)
		{ return value; }
		else if (back == null)
		{ return $"{EscapeCodes.GetForeColorSequence(fore!.Value)}{value}{EscapeCodes.ColorReset_Fore}"; }
		else if (fore == null)
		{ return $"{EscapeCodes.GetBackColorSequence(back!.Value)}{value}{EscapeCodes.ColorReset_Back}"; }
		else
		{ return $"{EscapeCodes.GetForeColorSequence(fore!.Value)}{EscapeCodes.GetBackColorSequence(back.Value)}{value}{EscapeCodes.ColorReset_Back}{EscapeCodes.ColorReset_Fore}"; }
	}

	/// <summary>
	/// <inheritdoc cref="Colorize(string, Color?, Color?)"/>
	/// Also wraps <c><paramref name="value"/></c> with the colorized values <c><paramref name="pre"/></c> and <c><paramref name="post"/></c>.
	/// </summary>
	/// <param name="value"><inheritdoc cref="Colorize(string, Color?, Color?)" path="/param[@name='value']"/></param>
	/// <param name="fore"><inheritdoc cref="Colorize(string, Color?, Color?)" path="/param[@name='fore']"/></param>
	/// <param name="back"><inheritdoc cref="Colorize(string, Color?, Color?)" path="/param[@name='back']"/></param>
	/// <param name="pre">The value to display <i>before</i> <paramref name="value"/> (null or empty string to display nothing)</param>
	/// <param name="post">The value to display <i>after</i> <paramref name="value"/> (null or empty string to display nothing)</param>
	/// <param name="ppFore">The foreground color to use for <paramref name="pre"/> and <paramref name="post"/>. If null, the default console color is used.</param>
	/// <param name="ppBack">The background color to use for <paramref name="pre"/> and <paramref name="post"/>. If null, the default console color is used.</param>
	public static string Colorize(this string value, Color? fore, Color? back, string pre, string post, Color? ppFore = null, Color? ppBack = null)
	{
		StringBuilder sb = new();
		if (!string.IsNullOrWhiteSpace(pre))
		{
			sb.Append(pre.Colorize(ppFore, ppBack));
		}

		sb.Append(value.Colorize(fore, back));

		if (!string.IsNullOrWhiteSpace(post))
		{
			sb.Append(post.Colorize(ppFore, ppBack));
		}

		return sb.ToString();
	}


	/// <summary>
	/// <para>Returns a string containing <paramref name="value"/> wrapped with the escape 
	/// sequence to enable underlining.</para>
	/// </summary>
	/// <param name="value"><inheritdoc cref="Colorize(string, Color?, Color?)" path="/param[@name='value']"/></param>
	public static string Underline(this string value) => $"{EscapeCodes.Underline}{value}{EscapeCodes.UnderlineReset}";

	/// <summary>
	/// <para>Returns a string containing <paramref name="value"/> wrapped with the escape 
	/// sequence to enable reverse text (the foreground and background colors reversed).</para>
	/// </summary>
	/// <param name="value"><inheritdoc cref="Colorize(string, Color?, Color?)" path="/param[@name='value']"/></param>
	public static string Reverse(this string value) => $"{EscapeCodes.Reverse}{value}{EscapeCodes.ReverseReset}";


	/// <summary>
	/// <para>Returns <paramref name="value"/> formatted with the escape sequences to create a color gradient
	/// for the foreground color. The color varies for each character in <paramref name="value"/></para>
	/// <para>If the length of <paramref name="value"/> is 1, its color will be <paramref name="start"/>. 
	/// If the length of <paramref name="value"/> is 2, the color of the two charcters will be <paramref name="start"/> and <paramref name="end"/> respectively. 
	/// </para>
	/// <para>If the length of <paramref name="value"/> is >2, the beginning/ending characters will be colored 
	/// with <paramref name="start"/> and <paramref name="end"/> respectively.
	/// </para>
	/// </summary>
	/// <param name="value">The value to format as a gradient</param>
	/// <param name="start">The starting color of the gradient.</param>
	/// <param name="end">The ending color of the gradient.</param>
	public static string Gradient(this string value, Color start, Color end)
	{
		if (string.IsNullOrEmpty(value))
		{ return ""; }

		if (value.Length == 1)
		{ return value.Colorize(start); }


		var sb = new StringBuilder();
		var colors = ColorUtilities.GetGradientColors(start, end, value.Length);

		for (int i = 0; i < value.Length; i++)
		{ sb.Append(value[i].ToString().Colorize(colors.ElementAt(i))); }

		return sb.ToString();
	}

	/// <summary>
	/// <para>Returns <paramref name="value"/> formatted with the escape sequences to create a three-color gradient
	/// for the foreground color. The color varies for each character in <paramref name="value"/></para>
	/// <para>If the length of <paramref name="value"/> is 1, its color will be <paramref name="start"/>. 
	/// If the length of <paramref name="value"/> is 2, the color of the two charcters will be <paramref name="start"/> and <paramref name="end"/> respectively. 
	/// If the length of <paramref name="value"/> is 3, the color of the three charcters will be <paramref name="start"/>, <paramref name="middle"/> and <paramref name="end"/> respectively. 
	/// </para>
	/// <para>If the length of <paramref name="value"/> is >3, <paramref name="value"/> is split in half, 
	/// and the first half is a gradient from <paramref name="start"/> to <paramref name="middle"/> and 
	/// the second half a gradient from <paramref name="middle"/> to <paramref name="end"/>.
	/// </para>
	/// </summary>
	/// <param name="value"><inheritdoc cref="Gradient(string, Color, Color)" path="/param[@name='value']"/></param>
	/// <param name="start"><inheritdoc cref="Gradient(string, Color, Color)" path="/param[@name='start']"/></param>
	/// <param name="middle">The central color of the gradient.</param>
	/// <param name="end"><inheritdoc cref="Gradient(string, Color, Color)" path="/param[@name='end']"/></param>
	/// <returns></returns>
	public static string Gradient(this string value, Color start, Color middle, Color end)
	{
		if (string.IsNullOrEmpty(value))
		{ return ""; }

		if (value.Length <= 2)
		{ return value.Gradient(start, end); }


		int l = value.Length;
		//string v1 = value.Substring(0, l / 2);
		string v1 = value[..(l / 2)];
		string v2 = value[v1.Length..];
		return v1.Gradient(start, middle) + v2.Gradient(middle, end);
	}


	/// <summary>
	/// Returns a string of <paramref name="width"/> characters, with <paramref name="value"/> "Centered" (padded on the left and right with <paramref name="fillChar"/>) within the string. 
	/// <para>If <paramref name="value"/> cannot be centered evenly, the extraneous <paramref name="fillChar"/> is on the right of the result.</para>
	/// <para>If <paramref name="value"/> is null, or an empty string, a string consisting of <paramref name="width"/> <paramref name="fillChar"/> characters is returned.</para>
	/// <para><b>Note:</b> if the length of <paramref name="value"/> is greater than, or equal to <paramref name="width"/>, <paramref name="value"/> is returned unchanged.</para>
	/// <para><b>If <paramref name="width"/> is less than 1, an <see cref="ArgumentOutOfRangeException"/> is thrown.</b></para>
	/// </summary>
	/// <param name="value"></param>
	/// <param name="width"></param>
	/// <param name="fillChar"></param>
	/// <exception cref="ArgumentOutOfRangeException"></exception>
	public static string Center(this string value, int width, char fillChar = ' ')
	{
		if (width <= 0)
		{ throw new ArgumentOutOfRangeException(nameof(width), "Must be > 0"); }

		if (string.IsNullOrEmpty(value))
		{ return new string(fillChar, width); }

		if (value.Length >= width)
		{ return value; }

		var l = (width - value.Length) / 2;
		//return new string(fillChar, l) + value + new string(fillChar, width - value.Length - l);
		return value.PadLeft(l + value.Length, fillChar).PadRight(width, fillChar);
	}
}
