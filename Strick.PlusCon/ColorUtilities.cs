using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace Strick.PlusCon;


/// <summary>
/// Methods for working with colors
/// </summary>
public static class ColorUtilities
{
	/// <summary>
	/// Returns a sequence consisting of <paramref name="steps"/> <see cref="Color"/> objects.
	/// <para>To create the individual colors, each component of the color (Red, Green, Blue, Alpha) is varied an equal amount for each step.</para>
	/// <para><b>Note:</b> Although the Alpha value is included in the calculations here, the console (at least on Windows) does not support alpha transparency.</para>
	/// </summary>
	/// <param name="start">The starting color of the gradient.</param>
	/// <param name="end">The ending color of the gradient.</param>
	/// <param name="steps">The number of steps in the gradient. An exception is thrown if <c>&lt; 1</c>.</param>
	/// <returns></returns>
	/// <exception cref="ArgumentOutOfRangeException"></exception>
	public static IEnumerable<Color> GetGradientColors(Color start, Color end, int steps)
	{
		if (steps < 1)
		{ throw new ArgumentOutOfRangeException(nameof(steps), "Must be > 0"); }

		if (steps == 1)
		{
			yield return start;
			yield break;
		}

		if (steps == 2)
		{
			yield return start;
			yield return end;
			yield break;
		}


		decimal stepA = GetGradientStep(start.A, end.A, steps);
		decimal stepR = GetGradientStep(start.R, end.R, steps);
		decimal stepG = GetGradientStep(start.G, end.G, steps);
		decimal stepB = GetGradientStep(start.B, end.B, steps);

		yield return start; //always start with "start"
		for (int i = 1; i < steps - 1; i++)
		{
			yield return Color.FromArgb
			(
				GetComponent(start.A, stepA, i),
				GetComponent(start.R, stepR, i),
				GetComponent(start.G, stepG, i),
				GetComponent(start.B, stepB, i)
			);
		}
		yield return end; //always end with "end"
	}

	private static int GetComponent(int startComponent, decimal componentStep, int currentStep) => startComponent + (int)Math.Round((componentStep * currentStep));

	private static decimal GetGradientStep(int colorComponentStart, int colorComponentEnd, int steps)
	{ return (colorComponentEnd - colorComponentStart) / (decimal)(steps - 1); }


	/// <summary>
	/// Returns a new color object which is created by increasing the "brightness" of <paramref name="baseColor"/>. 
	/// The components (<see cref="Color.R"/>, <see cref="Color.G"/>, <see cref="Color.B"/>) of the new color are calculated 
	/// by adding <paramref name="adjustment"/> to the value of each of color's components (Note: the Alpha component [<see cref="Color.A"/>] is NOT changed).
	/// </summary>
	/// <param name="baseColor"></param>
	/// <param name="adjustment">An integer value between 0 and 255.</param>
	/// <exception cref="ArgumentOutOfRangeException"></exception>
	public static Color Brighten(this Color baseColor, int adjustment)
	{
		if (adjustment == 0)
		{ return baseColor; }

		if (adjustment > 255 || adjustment < 0)
		{ throw new ArgumentOutOfRangeException(nameof(adjustment), "Allowable range is 0 - 255"); }

		return baseColor.AdjustBrightness(adjustment);
	}

	public static Color Darken(this Color baseColor, int adjustment)
	{
		if (adjustment == 0)
		{ return baseColor; }

		if (adjustment > 255 || adjustment < 0)
		{ throw new ArgumentOutOfRangeException(nameof(adjustment), "Allowable range is 0 - 255"); }

		return baseColor.AdjustBrightness(-adjustment);
	}

	public static Color AdjustBrightness(this Color baseColor, int adjustment)
	{ return Color.FromArgb(baseColor.A, GetAdjustedColorComponent(baseColor.R, adjustment), GetAdjustedColorComponent(baseColor.G, adjustment), GetAdjustedColorComponent(baseColor.B, adjustment)); }

	private static int GetAdjustedColorComponent(int component, int adjustment)
	{
		if (adjustment == 0)
		{ return component; }

		if (adjustment > 255 || adjustment < -255)
		{ throw new ArgumentOutOfRangeException(nameof(adjustment), "Allowable range is -255 - 255"); }

		if (adjustment < 0)
		{ return Math.Max(0, component + adjustment); }
		else
		{ return Math.Min(255, component + adjustment); }
	}

}