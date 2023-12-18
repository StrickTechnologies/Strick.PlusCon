using System;
using System.Drawing;

using static Strick.PlusCon.Helpers;


namespace Strick.PlusCon.Models;


/// <summary>
/// A class that can be used to quickly and easily add 
/// styling to any text.
/// <para>See also <see cref="TextStyle"/></para>
/// </summary>
public class StyledText
{
	/// <summary>
	/// <para>Creates an instance and sets the <see cref="Text"/> property to the value of the <paramref name="text"/> argument. 
	/// If <paramref name="text"/> is null or empty string, an <see cref="ArgumentNullException"/> exception is thrown.</para>
	/// </summary>
	/// <param name="text">The text value to be styled. 
	/// An <see cref="ArgumentNullException"/> exception is thrown if <paramref name="text"/> is null or empty string.
	/// </param>
	/// <exception cref="ArgumentNullException"></exception>
	public StyledText(string text) : this(text, null) { }

	/// <summary>
	/// <inheritdoc cref="StyledText(string)"/>
	/// Also sets the <see cref="Style"/> property to the value of the <paramref name="style"/> argument.
	/// </summary>
	/// <param name="text"><inheritdoc cref="StyledText(string)" path="/param[@name='text']"/></param>
	/// <param name="style">A <see cref="TextStyle"/> object. The <see cref="Style"/> property is set 
	/// to the value of this argument. If null, the <see cref="Style"/> property will be set to a 
	/// new <see cref="TextStyle"/> object.</param>
	public StyledText(string text, TextStyle? style)
	{
		Text = text;

		if (style != null)
		{ Style = style; }
		else
		{ Style = new TextStyle(); }
	}

	/// <summary>
	/// <inheritdoc cref="StyledText(string)"/>
	/// The <see cref="TextStyle.ForeColor"/> property of the <see cref="Style"/> property 
	/// is set to the <paramref name="foreColor"/> argument.
	/// </summary>
	/// <param name="text"><inheritdoc cref="StyledText(string)" path="/param[@name='text']"/></param>
	/// <param name="foreColor"><inheritdoc cref="TextStyle(Color)" path="/param[@name='foreColor']"/></param>
	public StyledText(string text, Color foreColor) : this(text, new TextStyle(foreColor))
	{ }

	/// <summary>
	/// <inheritdoc cref="StyledText(string, Color)"/>
	/// And the <see cref="TextStyle.BackColor"/> property of the <see cref="Style"/> property 
	/// is set to the <paramref name="backColor"/> argument.
	/// </summary>
	/// <param name="text"><inheritdoc cref="StyledText(string)" path="/param[@name='text']"/></param>
	/// <param name="foreColor"><inheritdoc cref="TextStyle(Color)" path="/param[@name='foreColor']"/></param>
	/// <param name="backColor"><inheritdoc cref="TextStyle(Color, Color)" path="/param[@name='backColor']"/></param>
	public StyledText(string text, Color foreColor, Color backColor) : this(text, new TextStyle(foreColor, backColor))
	{ }

	/// <summary>
	/// <inheritdoc cref="StyledText(string)"/>
	/// The <see cref="TextStyle.GradientStart"/>, <see cref="TextStyle.GradientMiddle"/>, and <see cref="TextStyle.GradientEnd"/> 
	/// properties of the <see cref="Style"/> property are set to the corresponding arguments.
	/// </summary>
	/// <param name="text"><inheritdoc cref="StyledText(string)" path="/param[@name='text']"/></param>
	/// <param name="gradientStart"><inheritdoc cref="TextStyle(Color, Color?, Color)" path="/param[@name='gradientStart']"/></param>
	/// <param name="gradientMiddle"><inheritdoc cref="TextStyle(Color, Color?, Color)" path="/param[@name='gradientMiddle']"/></param>
	/// <param name="gradientEnd"><inheritdoc cref="TextStyle(Color, Color?, Color)" path="/param[@name='gradientEnd']"/></param>
	public StyledText(string text, Color gradientStart, Color? gradientMiddle, Color gradientEnd) : this(text)
	{
		if (gradientMiddle == null)
		{ Style.SetGradientColors(gradientStart, gradientEnd); }
		else
		{ Style.SetGradientColors(gradientStart, gradientMiddle.Value, gradientEnd); }
	}


	/// <summary>
	/// Backer field for the Text property.
	/// </summary>
	protected string txt = default!;

	/// <summary>
	/// The text to be styled. Throws an <see cref="ArgumentNullException"/> if set to a null or empty string.
	/// </summary>
	/// <exception cref="ArgumentNullException"></exception>
	public string Text
	{
		get => txt;
		set
		{
			if (string.IsNullOrEmpty(value))
			{ throw new ArgumentNullException(nameof(value)); }

			txt = value;
		}
	}


	/// <summary>
	/// The styles to apply to the text
	/// </summary>
	public TextStyle Style { get; set; }


	/// <summary>
	/// Adds styling to <paramref name="altText"/> based on the values of the various properties of the <see cref="Style"/> property, and returns the resulting string.
	/// </summary>
	/// <param name="altText">The text to be styled.</param>
	public string StyleText(string altText)
	{
		if (Style == null)
		{ return altText; }

		return Style.StyleText(altText);
	}

	/// <summary>
	/// Adds styling to <see cref="Text"/> based on the values of the various properties of the <see cref="Style"/> property, and returns the resulting string.
	/// </summary>
	public string TextStyled => StyleText(Text);


	//***initial implementation, not completed
	//***  maybe all/some for a later release
	//public Color? ForeColor
	//{
	//	get => Style != null ? Style.ForeColor : null;
	//	set => Style.ForeColor = value;
	//}

	//public Color? BackColor
	//{
	//	get => Style.BackColor;
	//	set => Style.BackColor = value;
	//}


	//public Color? GradientStart => Style.GradientStart;

	//public Color? GradientMiddle => Style.GradientMiddle;

	//public Color? GradientEnd => Style.GradientEnd;

	//public void SetGradientColors(Color start, Color end) => Style.SetGradientColors(start, end);

	//public void SetGradientColors(Color start, Color middle, Color end) => Style.SetGradientColors(start, middle, end);


	//public bool Reverse
	//{
	//	get => Style.Reverse;
	//	set => Style.Reverse = value;
	//}

	//public bool Underline
	//{
	//	get => Style.Underline;
	//	set => Style.Underline = value;
	//}

	//public bool IncludeNewline
	//{
	//	get => Style.IncludeNewline;
	//	set => Style.IncludeNewline = value;
	//}
}