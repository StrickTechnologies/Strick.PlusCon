using System;
using System.Drawing;
using System.Text;


namespace Strick.PlusCon.Models;


/// <summary>
/// A class that can be used to quickly and easily add 
/// styling to any text.
/// </summary>
public class TextStyle
{
	/// <summary>
	/// Creates an instance with all default values
	/// </summary>
	public TextStyle() { }

	/// <summary>
	/// Creates an instance and sets the <see cref="ForeColor"/> property to <paramref name="foreColor"/>.
	/// </summary>
	/// <param name="foreColor"></param>
	public TextStyle(Color foreColor) : this()
	{
		ForeColor = foreColor;
	}

	/// <summary>
	/// Creates an instance and sets the <see cref="ForeColor"/>, and <see cref="BackColor"/> 
	/// property values to the corresponding arguments.
	/// </summary>
	/// <param name="foreColor"></param>
	/// <param name="backColor"></param>
	public TextStyle(Color foreColor, Color backColor) : this(foreColor)
	{
		BackColor = backColor;
	}

	/// <summary>
	/// Creates an instance and sets the <see cref="GradientStart"/>, <paramref name="gradientMiddle"/>, and <paramref name="gradientEnd"/> 
	/// property values to the corresponding arguments.
	/// <para>Note: for a two-color gradient pass <paramref name="gradientMiddle"/> as null. 
	/// Alternatively, use the <see cref="TextStyle"/> constructor and the <see cref="SetGradientColors(Color, Color)"/> method.</para>
	/// </summary>
	/// <param name="gradientStart"><inheritdoc cref="Formatting.Gradient(string, Color, Color)" path="/param[@name='start']"/></param>
	/// <param name="gradientMiddle"><inheritdoc cref="Formatting.Gradient(string, Color, Color, Color)" path="/param[@name='middle']"/></param>
	/// <param name="gradientEnd"><inheritdoc cref="Formatting.Gradient(string, Color, Color)" path="/param[@name='end']"/></param>
	public TextStyle(Color gradientStart, Color? gradientMiddle, Color gradientEnd) : this()
	{
		GradientStart = gradientStart;
		GradientMiddle = gradientMiddle;
		GradientEnd = gradientEnd;
	}

	/// <summary>
	/// Creates a new instance with the same property values as <paramref name="sourceStyle"/>. 
	/// <para>If <paramref name="sourceStyle"/> is null, a new instance is created with default property values.</para>
	/// </summary>
	/// <param name="sourceStyle"></param>
	public TextStyle(TextStyle sourceStyle)
	{
		if (sourceStyle != null)
		{
			ForeColor = sourceStyle.ForeColor;
			BackColor = sourceStyle.BackColor;
			GradientStart = sourceStyle.GradientStart;
			GradientMiddle = sourceStyle.GradientMiddle;
			GradientEnd = sourceStyle.GradientEnd;
			Reverse = sourceStyle.Reverse;
			Underline = sourceStyle.Underline;
		}
	}


	/// <summary>
	/// The foreground color to apply to the text
	/// </summary>
	public Color? ForeColor { get; set; }

	/// <summary>
	/// The background color to apply to the text
	/// </summary>
	public Color? BackColor { get; set; }


	/// <summary>
	/// <inheritdoc cref="Formatting.Gradient(string, Color, Color)" path="/param[@name='start']"/>
	/// <para>Readonly. Use <seealso cref="SetGradientColors(Color, Color)"/> or <seealso cref="SetGradientColors(Color, Color, Color)"/> to set colors.</para>
	/// </summary>
	public Color? GradientStart { get; protected set; }

	/// <summary>
	/// <inheritdoc cref="Formatting.Gradient(string, Color, Color, Color)" path="/param[@name='middle']"/>
	/// <para>Readonly. Use <seealso cref="SetGradientColors(Color, Color)"/> or <seealso cref="SetGradientColors(Color, Color, Color)"/> to set colors.</para>
	/// </summary>
	public Color? GradientMiddle { get; protected set; }

	/// <summary>
	/// <inheritdoc cref="Formatting.Gradient(string, Color, Color)" path="/param[@name='end']"/>
	/// <para>Readonly. Use <seealso cref="SetGradientColors(Color, Color)"/> or <seealso cref="SetGradientColors(Color, Color, Color)"/> to set colors.</para>
	/// </summary>
	public Color? GradientEnd { get; protected set; }

	/// <summary>
	/// Sets the colors to apply a two-color gradient.
	/// <para>For a three-color gradient, use <see cref="SetGradientColors(Color, Color, Color)"/></para>
	/// <para>
	/// See also: <seealso cref="Formatting.Gradient(string, Color, Color)"/>, 
	/// <seealso cref="Formatting.Gradient(string, Color, Color, Color)"/>
	/// </para>
	/// </summary>
	/// <param name="start"><inheritdoc cref="Formatting.Gradient(string, Color, Color)" path="/param[@name='start']"/></param>
	/// <param name="end"><inheritdoc cref="Formatting.Gradient(string, Color, Color)" path="/param[@name='end']"/></param>
	public void SetGradientColors(Color start, Color end)
	{
		GradientStart = start;
		GradientMiddle = null;
		GradientEnd = end;
	}

	/// <summary>
	/// Sets the colors to apply a three-color gradient.
	/// <para>For a two-color gradient, use <see cref="SetGradientColors(Color, Color)"/></para>
	/// <para>
	/// See also: <seealso cref="Formatting.Gradient(string, Color, Color)"/>, 
	/// <seealso cref="Formatting.Gradient(string, Color, Color, Color)"/>
	/// </para>
	/// </summary>
	/// <param name="start"><inheritdoc cref="Formatting.Gradient(string, Color, Color)" path="/param[@name='start']"/></param>
	/// <param name="middle"><inheritdoc cref="Formatting.Gradient(string, Color, Color, Color)" path="/param[@name='middle']"/></param>
	/// <param name="end"><inheritdoc cref="Formatting.Gradient(string, Color, Color)" path="/param[@name='end']"/></param>
	public void SetGradientColors(Color start, Color middle, Color end)
	{
		GradientStart = start;
		GradientMiddle = middle;
		GradientEnd = end;
	}

	/// <summary>
	/// Sets <see cref="GradientStart"/>, <see cref="GradientMiddle"/>, and <see cref="GradientEnd"/> to null.
	/// </summary>
	public void ClearGradient()
	{
		GradientStart = null;
		GradientMiddle = null;
		GradientEnd = null;
	}


	/// <summary>
	/// Gets/Sets a value indicating whether the styled text will include the <see cref="Formatting.Reverse(string)"/> escape sequence.
	/// </summary>
	public bool Reverse { get; set; } = false;

	/// <summary>
	/// Gets/Sets a value indicating whether the styled text will include the <see cref="Formatting.Underline(string)"/> escape sequence.
	/// </summary>
	public bool Underline { get; set; } = false;


	/// <summary>
	/// Adds styling to <paramref name="text"/> based on the values of the various properties, and returns the resulting string.
	/// </summary>
	/// <param name="text">The text to style. If null or empty string, an exception is thrown.</param>
	/// <returns></returns>
	/// <exception cref="ArgumentNullException"></exception>
	public string StyleText(string text)
	{
		if (string.IsNullOrEmpty(text))
		{ throw new ArgumentNullException(text); }

		StringBuilder sb = new();

		//Gradient
		if (GradientStart != null)
		{
			if (GradientMiddle != null)
			{ sb.Append(text.Gradient(GradientStart.Value, GradientMiddle.Value, GradientEnd!.Value)); }
			else
			{ sb.Append(text.Gradient(GradientStart.Value, GradientEnd!.Value)); }
		}
		else
		{ sb.Append(text); }

		//BackColor
		if (BackColor != null)
		{
			sb.Insert(0, EscapeCodes.GetBackColorSequence(BackColor.Value));
			sb.Append(EscapeCodes.ColorReset_Back);
		}

		//ForeColor
		if (ForeColor != null)
		{
			sb.Insert(0, EscapeCodes.GetForeColorSequence(ForeColor.Value));
			sb.Append(EscapeCodes.ColorReset_Fore);
		}

		//Reverse
		if (Reverse)
		{
			sb.Insert(0, EscapeCodes.Reverse);
			sb.Append(EscapeCodes.ReverseReset);
		}

		//Underline
		if (Underline)
		{
			sb.Insert(0, EscapeCodes.Underline);
			sb.Append(EscapeCodes.UnderlineReset);
		}

		return sb.ToString();
	}
}
