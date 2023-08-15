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
/// The first nine characters of each segment are specified by the <see cref="Segment"/> property. 
/// The tenth character of each segment is the "tens" counter (1 for 10, 2 for 20, etc.). 
/// The "tens" counter resets to zero at each hundred (100, 200).
/// </summary>
public static class Ruler
{
	static Ruler()
	{
		Colors = ColorUtilities.GetGradientColors(Color.Gray, Color.White, 10).ToList();
	}


	private static string seg = "----┼----";

	/// <summary>
	/// The string used to construct each "segment" of the ruler. A segment consists of 9 characters 
	/// which represent the digits 1-9 in the segment of the ruler.
	/// <para>The segment can be set to any characters, but it must be exactly 9 characters in length 
	/// (otherwise, an exception is thrown).</para>
	/// Setting this property changes the default segment. Some overloads of the <see cref="Get()"/> 
	/// method also include a segment parameter, which will override this default value for only that call.
	/// </summary>
	/// <inheritdoc cref="Get(int, IEnumerable{Color}?, string)" path="/exception"/>
	public static string Segment
	{
		get => seg;

		set
		{
			ThrowIfInvalid(value);

			seg = value;
		}
	}

	/// <summary>
	/// A sequence of colors to use for the ruler. 
	/// A null value or an empty sequence is acceptable, and will result in rulers having no embeded color sequences.
	/// <para>The colors default to a ten-color gradient from <see cref="Color.Gray"/> to <see cref="Color.White"/>.</para>
	/// <para>See <see cref="Formatting.Colorize(string, IEnumerable{Color})"/> for more 
	/// information on how the colors are applied to characters in the ruler.</para>
	/// Setting this property changes the default colors. Some overloads of the <see cref="Get()"/> 
	/// method also include a colors parameter, which will override this default value for only that call.
	/// </summary>
	public static List<Color>? Colors { get; set; }


	/// <summary>
	/// Returns a ruler the length of the console width 
	/// using the default colors and segment
	/// </summary>
	public static string Get() => Get(Console.WindowWidth);

	/// <summary>
	/// Returns a ruler of the length specified by the <paramref name="width"/> argument 
	/// using the default colors and segment
	/// </summary>
	/// <param name="width"><inheritdoc cref="Get(int, IEnumerable{Color}?, string?)" path="/param[@name='width']"/></param>
	public static string Get(int width) => Get(width, Segment);

	/// <summary>
	/// Returns a ruler of the length specified by the <paramref name="width"/> argument 
	/// using the colors specified by the <paramref name="colors"/> argument and the default segment.
	/// </summary>
	/// <param name="width"><inheritdoc cref="Get(int, IEnumerable{Color}?, string?)" path="/param[@name='width']"/></param>
	/// <param name="colors"><inheritdoc cref="Get(int, IEnumerable{Color}?, string?)" path="/param[@name='colors']"/></param>
	public static string Get(int width, IEnumerable<Color>? colors) => Get(width, colors, Segment);

	/// <summary>
	/// Returns a ruler of the length specified by the <paramref name="width"/> argument 
	/// using the segment specified by the <paramref name="segment"/> argument and the default colors.
	/// </summary>
	/// <param name="width"><inheritdoc cref="Get(int, IEnumerable{Color}?, string?)" path="/param[@name='width']"/></param>
	/// <param name="segment"><inheritdoc cref="Get(int, IEnumerable{Color}?, string?)" path="/param[@name='segment']"/></param>
	/// <inheritdoc cref="Get(int, IEnumerable{Color}?, string)" path="/exception"/>
	public static string Get(int width, string segment) => Get(width, Colors!, segment);


	/// <summary>
	/// Returns a ruler of the length specified by the <paramref name="width"/> argument 
	/// using the colors and segment specified by the <paramref name="colors"/> 
	/// and <paramref name="segment"/> arguments, respectively.
	/// </summary>

	/// <param name="width">The width of the ruler to create</param>

	/// <param name="colors">A sequence of colors to use for the ruler. This overrides the default
	/// colors specified in the <see cref="Colors"/> property. 
	/// A null value or an empty sequence is acceptable, and will result in the ruler having no color sequences.</param>

	/// <param name="segment">The string used to construct each segment of the ruler. 
	/// This overrides the default segment string specified in the <see cref="Segment"/> property.
	/// <para>Note: the same rules apply to this argument as the property.</para></param>

	/// <exception cref="ArgumentNullException"></exception>
	/// <exception cref="ArgumentException"></exception>
	public static string Get(int width, IEnumerable<Color>? colors, string segment)
	{
		ThrowIfInvalid(segment);


		StringBuilder result = new(width);
		int ten = 1;
		int tens = width / 10;
		for (int i = 1; i <= tens; i++)
		{
			result.Append(GetSegment(segment, 10));
			result.Append(ten);
			if (ten > 8)
			{ ten = 0; }
			else
			{ ten++; }
		}
		result.Append(GetSegment(segment, width - tens * 10));

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
		{ throw new ArgumentException($"{nameof(Segment)} value must be exactly 9 characters in length"); }
	}


	/* UNUSED STUFF
	//public static char[] chars = { '-', '-', '-', '-', '*', '-', '-', '-', '-' };
	//public static char[] chars;

	private static string Segx2(int width, int value)
	{
		string seg;
		if (width < 10)
		{ seg = new string(chars, 0, width); }
		else
		{ seg = new string(chars) + value.ToString(); }

		//if (Colors.HasAny())
		//{ return seg.Colorize(Colors); }

		return seg;
	}

	private static IEnumerable<Color> GetColors()
	{
		//List<Color> colors = ColorUtilities.GetGradientColors(Color.Gray, Color.White, 9).ToList();
		//colors.Insert(4, Color.White);
		//colors.Add(Color.White);
		////colors = ColorUtilities.GetGradientColors(Color.White, Color.Gray, 10).ToList();
		//colors = new();// ColorUtilities.GetGradientColors(Color.Gray, Color.White, 5).ToList();
		//colors.AddRange(ColorUtilities.GetGradientColors(Color.White, Color.Gray, 6).ToList().GetRange(1, 4));
		//colors.Add(Color.White);
		//colors.AddRange(ColorUtilities.GetGradientColors(Color.White, Color.Gray, 6).ToList().GetRange(1, 4));
		//colors.Add(Color.White);
		//colors[0] = Color.Silver;
		//colors[1] = Color.Gray;
		//colors[2] = Color.Gray;
		//colors[3] = Color.Silver;
		//colors[4] = Color.White;
		//colors[5] = Color.Silver;
		//colors[6] = Color.Gray;
		//colors[7] = Color.Gray;
		//colors[8] = Color.Silver;
		//colors[9] = Color.White;
		return Colors;
	}

	//inter style
	public static StyledText inter14 = new("----", new TextStyle(Color.White, null, Color.Gray));
	public static StyledText inter69 = new("----", new TextStyle(Color.Gray, null, Color.White));
	//5 style
	public static StyledText five = new("*", new TextStyle(Color.White));
	//10 style
	public static TextStyle ten = new(Color.Lime);

	private static string Segx(int width, int value)
	{
		//* get a list of the 10 colors
		//* build the string
		//* trim the string, if necessary
		//* call the colorize method with the string and list of colors

		if (width == 10)
		{ return $"{inter14.TextStyled}{five.TextStyled}{inter69.TextStyled}{ten.StyleText(value.ToString())}"; }


		return $"{inter14.Text}{five.Text}{inter69.Text}{value.ToString()}".Substring(0, width);
	}

	public static string Get1(int width)
	{
		StringBuilder nl = new(new string('-', width));

		for (int i = 4; i < width; i += 10)
		{ nl[i] = five.Text[0]; }
		int ten = 1;
		for (int i = 9; i < width; i += 10)
		{
			nl[i] = (char)(ten + 48);
			if (ten > 8)
			{ ten = 0; }
			else
			{ ten++; }
		}

		return nl.ToString();
	}
	*/
}
