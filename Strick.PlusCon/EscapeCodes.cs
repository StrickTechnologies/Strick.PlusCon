using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Strick.PlusCon;


/// <summary>
/// Escape codes used for formatting console output
/// </summary>
public static class EscapeCodes
{
	/// <summary>
	/// Returns a <see cref="char"/> representing the escape character
	/// </summary>
	public static char Escape => '\x1b'; //27


	/// <summary>
	/// Returns a string containing the escape sequence to reset all formatting
	/// </summary>
	public static string ResetAll => $"{Escape}[0m";


	/// <summary>
	/// Returns a string containing a template for a color escape sequence. The template contains the following placeholders:
	/// <list type="bullet">
	/// <item><c>{cs}</c>. The color space. Replace with a value from the <see cref="ColorSpace"/> enum.</item>
	/// <item><c>{r}</c>. The Red component of the color (a value between 0 and 255, inclusive).</item>
	/// <item><c>{g}</c>. The Green component of the color (a value between 0 and 255, inclusive).</item>
	/// <item><c>{b}</c>. The Blue component of the color (a value between 0 and 255, inclusive).</item>
	/// </list>
	/// </summary>
	public static string Color => Escape + "[{cs};2;{r};{g};{b}m";

	/// <summary>
	/// Returns a string containing a template for a foreground color escape sequence. The template contains the following placeholders:
	/// <list type="bullet">
	/// <item><c>{r}</c>. The Red component of the color (a value between 0 and 255, inclusive).</item>
	/// <item><c>{g}</c>. The Green component of the color (a value between 0 and 255, inclusive).</item>
	/// <item><c>{b}</c>. The Blue component of the color (a value between 0 and 255, inclusive).</item>
	/// </list>
	/// </summary>
	public static string Color_Fore => Color.Replace("{cs}", ColorSpace.fore.AsString());

	/// <summary>
	/// Returns a string containing a template for a background color escape sequence. The template contains the following placeholders:
	/// <list type="bullet">
	/// <item><c>{r}</c>. The Red component of the color (a value between 0 and 255, inclusive).</item>
	/// <item><c>{g}</c>. The Green component of the color (a value between 0 and 255, inclusive).</item>
	/// <item><c>{b}</c>. The Blue component of the color (a value between 0 and 255, inclusive).</item>
	/// </list>
	/// </summary>
	public static string Color_Back => Color.Replace("{cs}", ColorSpace.back.AsString());


	/// <summary>
	/// Returns a string containing an escape sequence to reset foreground color.
	/// </summary>
	public static string ColorReset_Fore => $"{Escape}[{ColorSpace.foreReset.AsString()}m";

	/// <summary>
	/// Returns a string containing an escape sequence to reset background color.
	/// </summary>
	public static string ColorReset_Back => $"{Escape}[{ColorSpace.backReset.AsString()}m";


	/// <summary>
	/// Returns a string containing an escape sequence to start underlining.
	/// </summary>
	public static string Underline => $"{Escape}[4m";

	/// <summary>
	/// Returns a string containing an escape sequence to reset underlining.
	/// </summary>
	public static string UnderlineReset => $"{Escape}[24m";


	/// <summary>
	/// Returns a string containing an escape sequence to start reverse text (the foreground and background colors reversed).
	/// </summary>
	public static string Reverse => $"{Escape}[7m";

	/// <summary>
	/// Returns a string containing an escape sequence to reset reverse text.
	/// </summary>
	public static string ReverseReset => $"{Escape}[27m";


	/// <summary>
	/// Returns a string containing an escape sequence to begin a foreground color.
	/// </summary>
	/// <param name="color">The color to set</param>
	public static string GetForeColorSequence(Color color) => Color_Fore
		.Replace("{r}", color.R.ToString())
		.Replace("{g}", color.G.ToString())
		.Replace("{b}", color.B.ToString());

	/// <summary>
	/// Returns a string containing an escape sequence to begin a background color.
	/// </summary>
	/// <param name="color">The color to set</param>
	public static string GetBackColorSequence(Color color) => Color_Back
		.Replace("{r}", color.R.ToString())
		.Replace("{g}", color.G.ToString())
		.Replace("{b}", color.B.ToString());
}


/// <summary>
/// Values used to create escape sequences for foreground and background colors.
/// </summary>
public enum ColorSpace
{
	/// <summary>
	/// The value that designates to start a foreground color.
	/// </summary>
	fore = 38,

	/// <summary>
	/// The value that designates to reset the foreground color.
	/// </summary>
	foreReset = 39,


	/// <summary>
	/// The value that designates to start a background color.
	/// </summary>
	back = 48,

	/// <summary>
	/// The value that designates to reset the background color.
	/// </summary>
	backReset = 49
}
