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
	/// Creates an instance and sets the <see cref="Text"/> property to the value of the <paramref name="text"/> argument. 
	/// If <paramref name="text"/> is null or empty string, an <see cref="ArgumentNullException"/> exception is thrown.
	/// </summary>
	/// <param name="text"></param>
	public StyledText(string text)
	{
		Text = text;
		Style = new TextStyle();
	}

	/// <summary>
	/// <inheritdoc cref="StyledText(string)"/>
	/// <para>Also sets the <see cref="Style"/> property to the value of the <paramref name="style"/> argument.</para>
	/// </summary>
	/// <param name="text"></param>
	/// <param name="style"></param>
	public StyledText(string text, TextStyle style)
	{
		Text = text;
		Style = style;
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