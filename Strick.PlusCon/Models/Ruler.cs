using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Text;


namespace Strick.PlusCon.Models;


/// <summary>
/// To create Rulers...
/// A ruler is made up of "segments" of ten characters. 
/// The first nine characters of each segment are specified by the <see cref="HorizontalSegment"/> property. 
/// The tenth character of each segment is the "tens" counter (1 for 10, 2 for 20, etc.). 
/// The "tens" counter resets to zero at each hundred (100, 200).
/// </summary>
public static class Ruler
{
	static Ruler()
	{
		Colors = ColorUtilities.GetGradientColors(Color.Gray, Color.White, 10).ToList();
	}


	private static string segH = "----┼----";

	private static string segV = "||||┼||||";


	/// <summary>
	/// The string used to construct each "segment" of a horizontal ruler. A segment consists of 9 characters 
	/// which represent the digits 1-9 in the segment of the ruler.
	/// <para>The segment can be set to any characters, but it must be exactly 9 characters in length 
	/// (otherwise, an exception is thrown).</para>
	/// Setting this property changes the default horizontal segment. Some overloads of the <see cref="GetH()"/> 
	/// method also include a segment parameter, which will override this default value for only that call.
	/// </summary>
	/// <inheritdoc cref="Get(int, IEnumerable{Color}?, string)" path="/exception"/>
	public static string HorizontalSegment
	{
		get => segH;

		set
		{
			ThrowIfInvalid(value);

			segH = value;
		}
	}

	/// <summary>
	/// The string used to construct each "segment" of a vertical ruler. A segment consists of 9 characters 
	/// which represent the digits 1-9 in the segment of the ruler.
	/// <para>The segment can be set to any characters, but it must be exactly 9 characters in length 
	/// (otherwise, an exception is thrown).</para>
	/// Setting this property changes the default vertical segment. Some overloads of the <see cref="GetV()"/> 
	/// method also include a segment parameter, which will override this default value for only that call.
	/// </summary>
	/// <inheritdoc cref="Get(int, IEnumerable{Color}?, string)" path="/exception"/>
	public static string VerticalSegment
	{
		get => segV;

		set
		{
			ThrowIfInvalid(value);

			segV = value;
		}
	}

	/// <summary>
	/// A sequence of colors to use for the ruler. 
	/// A null value or an empty sequence is acceptable, and will result in rulers having no embeded color sequences.
	/// <para>The colors default to a ten-color gradient from <see cref="Color.Gray"/> to <see cref="Color.White"/>.</para>
	/// <para>See <see cref="Formatting.Colorize(string, IEnumerable{Color})"/> for more 
	/// information on how the colors are applied to characters in the ruler.</para>
	/// Setting this property changes the default colors. Some overloads of the <see cref="GetH()"/> 
	/// method also include a colors parameter, which will override this default value for only that call.
	/// </summary>
	public static List<Color>? Colors { get; set; }


	/// <summary>
	/// Returns a horizontal ruler the length of the console width 
	/// using the default colors and segment
	/// </summary>
	public static string GetH() => GetH(Console.WindowWidth);

	/// <summary>
	/// Returns a horizontal ruler of the length specified by the <paramref name="width"/> argument 
	/// using the default colors and segment
	/// </summary>
	/// <param name="width"><inheritdoc cref="Get(int, IEnumerable{Color}?, string?)" path="/param[@name='width']"/></param>
	/// <exception cref="ArgumentOutOfRangeException"></exception>
	public static string GetH(int width) => GetH(width, HorizontalSegment);

	/// <summary>
	/// Returns a horizontal ruler of the length specified by the <paramref name="width"/> argument 
	/// using the colors specified by the <paramref name="colors"/> argument and the default segment.
	/// </summary>
	/// <param name="width"><inheritdoc cref="Get(int, IEnumerable{Color}?, string?)" path="/param[@name='width']"/></param>
	/// <param name="colors"><inheritdoc cref="Get(int, IEnumerable{Color}?, string?)" path="/param[@name='colors']"/></param>
	/// <exception cref="ArgumentOutOfRangeException"></exception>
	public static string GetH(int width, IEnumerable<Color>? colors) => GetH(width, colors, HorizontalSegment);

	/// <summary>
	/// Returns a horizontal ruler of the length specified by the <paramref name="width"/> argument 
	/// using the segment specified by the <paramref name="segment"/> argument and the default colors.
	/// </summary>
	/// <param name="width"><inheritdoc cref="Get(int, IEnumerable{Color}?, string?)" path="/param[@name='width']"/></param>
	/// <param name="segment"><inheritdoc cref="Get(int, IEnumerable{Color}?, string?)" path="/param[@name='segment']"/></param>
	/// <inheritdoc cref="Get(int, IEnumerable{Color}?, string)" path="/exception"/>
	public static string GetH(int width, string segment) => GetH(width, Colors, segment);

	/// <summary>
	/// Returns a horizontal ruler of the length specified by the <paramref name="width"/> argument 
	/// using the colors and segment specified by the <paramref name="colors"/> 
	/// and <paramref name="segment"/> arguments, respectively.
	/// </summary>
	/// <param name="width">The width of the ruler to create. Must be a positive value, otherwise an exception is thrown.</param>
	/// <param name="colors">A sequence of colors to use for the ruler. This overrides the default
	/// colors specified in the <see cref="Colors"/> property. 
	/// A null value or an empty sequence is acceptable, and will result in the ruler having no color sequences.</param>
	/// <param name="segment">The string used to construct each segment of the ruler. 
	/// This overrides the default segment string specified in the <see cref="HorizontalSegment"/> property.
	/// <para>Note: the same rules apply to this argument as the property.</para></param>
	/// <inheritdoc cref="Get(int, IEnumerable{Color}?, string)" path="/exception"/>
	public static string GetH(int width, IEnumerable<Color>? colors, string segment) => Get(width, colors, segment);


	public static string GetV() => GetV(Console.WindowHeight);

	public static string GetV(int height) => GetV(height, VerticalSegment);

	public static string GetV(int height, IEnumerable<Color>? colors) => GetV(height, colors, VerticalSegment);

	public static string GetV(int height, string segment) => GetV(height, Colors, segment);

	public static string GetV(int height, IEnumerable<Color>? colors, string segment)
	{
		if (!colors.HasAny() || colors.Count() < 2)
		{ return Get(height, null, segment).Intersperse(EscapeCodes.Down1Left1).Colorize(colors); }

		var ruler = Get(height, null, segment).Colorize(colors).Split(EscapeCodes.ColorReset_Fore, StringSplitOptions.RemoveEmptyEntries);

		return string.Join(EscapeCodes.ColorReset_Fore + EscapeCodes.Down1Left1, ruler) + EscapeCodes.ColorReset_Fore;
	}


	/// <summary>
	/// Returns a ruler of the length specified by the <paramref name="length"/> argument 
	/// using the colors and segment specified by the <paramref name="colors"/> 
	/// and <paramref name="segment"/> arguments, respectively.
	/// </summary>
	/// <param name="length">The width (horizontal) or height (vertical) of the ruler to create. Must be a positive value, otherwise an exception is thrown.</param>
	/// <param name="colors">A sequence of colors to use for the ruler. This overrides the default
	/// colors specified in the <see cref="Colors"/> property. 
	/// A null value or an empty sequence is acceptable, and will result in the ruler having no color sequences.</param>
	/// <param name="segment">The string used to construct each segment of the ruler. 
	/// This overrides the default segment string specified in either the <see cref="HorizontalSegment"/> or <see cref="VerticalSegment"/> property.
	/// <para>Note: the same rules apply to this argument as the property.</para></param>
	/// <exception cref="ArgumentOutOfRangeException"></exception>
	/// <exception cref="ArgumentNullException"></exception>
	/// <exception cref="ArgumentException"></exception>
	internal static string Get(int length, IEnumerable<Color>? colors, string segment)
	{
		if (length < 1)
		{ throw new ArgumentOutOfRangeException(nameof(length), "must be > 0"); }

		ThrowIfInvalid(segment);


		StringBuilder result = new(length);
		int ten = 1;
		int tens = length / 10;
		for (int i = 1; i <= tens; i++)
		{
			result.Append(GetSegment(segment, 10));
			result.Append(ten);
			if (ten > 8)
			{ ten = 0; }
			else
			{ ten++; }
		}
		result.Append(GetSegment(segment, length - tens * 10));

		return result.ToString().Colorize(colors);
	}

	private static string GetSegment(string segment, int width)
	{
		if (width < 10)
		{ return segment[..width]; }

		return segment;
	}

	private static void ThrowIfInvalid([NotNull] string? segment)
	{
		if (segment == null)
		{ throw new ArgumentNullException(); }

		if (segment.Length != 9)
		{ throw new ArgumentException($"{nameof(segment)} value must be exactly 9 characters in length"); }
	}
}
