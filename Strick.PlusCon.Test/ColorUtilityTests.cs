using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using static Strick.PlusCon.ColorUtilities;
using static Strick.PlusCon.Test.Expectations;


namespace Strick.PlusCon.Test;


[TestClass]
public class ColorUtilityTests
{
	[TestMethod]
	public void GetGradientColorsTests()
	{
		var colors = GetGradientColors(red, blue, 1).ToList();
		Assert.IsNotNull(colors);
		Assert.AreEqual(1, colors.Count);
		Assert.AreEqual(red, colors[0]);

		colors = GetGradientColors(red, blue, 2).ToList();
		Assert.IsNotNull(colors);
		Assert.AreEqual(2, colors.Count);
		Assert.AreEqual(red, colors[0]);
		Assert.AreEqual(blue, colors[1]);

		colors = GetGradientColors(red, blue, 3).ToList();
		Assert.IsNotNull(colors);
		Assert.AreEqual(3, colors.Count);
		Assert.AreEqual(red, colors[0]);
		Assert.AreEqual(blue, colors[2]);
		Assert.AreEqual(127, colors[1].R);
		Assert.AreEqual(128, colors[1].B);
		Assert.AreEqual(0, colors[1].G);

		colors = GetGradientColors(white, black, 3).ToList();
		Assert.IsNotNull(colors);
		Assert.AreEqual(3, colors.Count);
		Assert.AreEqual(white, colors[0]);
		Assert.AreEqual(black, colors[2]);
		Assert.AreEqual(127, colors[1].R);
		Assert.AreEqual(127, colors[1].B);
		Assert.AreEqual(127, colors[1].G);

		colors = GetGradientColors(black, white, 3).ToList();
		Assert.IsNotNull(colors);
		Assert.AreEqual(3, colors.Count);
		Assert.AreEqual(black, colors[0]);
		Assert.AreEqual(white, colors[2]);
		Assert.AreEqual(128, colors[1].R);
		Assert.AreEqual(128, colors[1].B);
		Assert.AreEqual(128, colors[1].G);

		colors = GetGradientColors(black, white, 256).ToList();
		Assert.IsNotNull(colors);
		Assert.AreEqual(256, colors.Count);
		Assert.AreEqual(black, colors[0]);
		Assert.AreEqual(white, colors[255]);
		for (int i = 0; i < colors.Count; i++)
		{
			Assert.AreEqual(i, colors[i].R);
			Assert.AreEqual(i, colors[i].B);
			Assert.AreEqual(i, colors[i].G);
		}
	}


	[TestMethod]
	public void AdjustBrightnessTests()
	{
		TestColor(red, red.A, 255, 0, 0);
		TestColor(red.AdjustBrightness(0), red.A, red.R, red.G, red.B);
		TestColor(red.AdjustBrightness(1), red.A, 255, 1, 1);
		TestColor(red.AdjustBrightness(10), red.A, 255, 10, 10);
		TestColor(red.AdjustBrightness(20), red.A, 255, 20, 20);

		TestColor(red, red.A, 255, 0, 0);
		TestColor(red.AdjustBrightness(0), red.A, red.R, red.G, red.B);
		TestColor(red.AdjustBrightness(-1), red.A, 254, 0, 0);
		TestColor(red.AdjustBrightness(-10), red.A, 245, 0, 0);
		TestColor(red.AdjustBrightness(-20), red.A, 235, 0, 0);

		Assert.ThrowsException<ArgumentOutOfRangeException>(() => { red.AdjustBrightness(-256); });
		Assert.ThrowsException<ArgumentOutOfRangeException>(() => { red.AdjustBrightness(256); });
	}

	[TestMethod]
	public void BrightenTests()
	{
		TestColor(red, red.A, 255, 0, 0);
		TestColor(red.Brighten(0), red.A, red.R, red.G, red.B);
		TestColor(red.Brighten(1), red.A, 255, 1, 1);
		TestColor(red.Brighten(10), red.A, 255, 10, 10);
		TestColor(red.Brighten(20), red.A, 255, 20, 20);

		TestColor(green, green.A, 0, 255, 0);
		TestColor(green.Brighten(0), green.A, green.R, green.G, green.B);
		TestColor(green.Brighten(1), green.A, 1, 255, 1);
		TestColor(green.Brighten(10), green.A, 10, 255, 10);
		TestColor(green.Brighten(20), green.A, 20, 255, 20);

		TestColor(blue, blue.A, 0, 0, 255);
		TestColor(blue.Brighten(0), blue.A, blue.R, blue.G, blue.B);
		TestColor(blue.Brighten(1), blue.A, 1, 1, 255);
		TestColor(blue.Brighten(10), blue.A, 10, 10, 255);
		TestColor(blue.Brighten(20), blue.A, 20, 20, 255);

		TestColor(black, black.A, 0, 0, 0);
		TestColor(black.Brighten(0), black.A, black.R, black.G, black.B);
		TestColor(black.Brighten(1), black.A, 1, 1, 1);
		TestColor(black.Brighten(10), black.A, 10, 10, 10);
		TestColor(black.Brighten(20), black.A, 20, 20, 20);

		TestColor(white, white.A, 255, 255, 255);
		TestColor(white.Brighten(0), white.A, white.R, white.G, white.B);
		TestColor(white.Brighten(1), white.A, white.R, white.G, white.B);
		TestColor(white.Brighten(10), white.A, white.R, white.G, white.B);
		TestColor(white.Brighten(20), white.A, white.R, white.G, white.B);

		Assert.ThrowsException<ArgumentOutOfRangeException>(() => { red.Brighten(-1); });
		Assert.ThrowsException<ArgumentOutOfRangeException>(() => { red.Brighten(-2); });
		Assert.ThrowsException<ArgumentOutOfRangeException>(() => { red.Brighten(-256); });
		Assert.ThrowsException<ArgumentOutOfRangeException>(() => { red.Brighten(256); });
	}

	[TestMethod]
	public void DarkenTests()
	{
		TestColor(red, red.A, 255, 0, 0);
		TestColor(red.Darken(0), red.A, red.R, red.G, red.B);
		TestColor(red.Darken(1), red.A, 254, 0, 0);
		TestColor(red.Darken(10), red.A, 245, 0, 0);
		TestColor(red.Darken(20), red.A, 235, 0, 0);

		TestColor(green, green.A, 0, 255, 0);
		TestColor(green.Darken(0), green.A, green.R, green.G, green.B);
		TestColor(green.Darken(1), green.A, 0, 254, 0);
		TestColor(green.Darken(10), green.A, 0, 245, 0);
		TestColor(green.Darken(20), green.A, 0, 235, 0);

		TestColor(blue, blue.A, 0, 0, 255);
		TestColor(blue.Darken(0), blue.A, blue.R, blue.G, blue.B);
		TestColor(blue.Darken(1), blue.A, 0, 0, 254);
		TestColor(blue.Darken(10), blue.A, 0, 0, 245);
		TestColor(blue.Darken(20), blue.A, 0, 0, 235);

		TestColor(black, black.A, 0, 0, 0);
		TestColor(black.Darken(0), black.A, black.R, black.G, black.B);
		TestColor(black.Darken(1), black.A, 0, 0, 0);
		TestColor(black.Darken(10), black.A, 0, 0, 0);
		TestColor(black.Darken(20), black.A, 0, 0, 0);

		TestColor(white, white.A, 255, 255, 255);
		TestColor(white.Darken(0), white.A, 255, 255, 255);
		TestColor(white.Darken(1), white.A, 254, 254, 254);
		TestColor(white.Darken(10), white.A, 245, 245, 245);
		TestColor(white.Darken(20), white.A, 235, 235, 235);

		Assert.ThrowsException<ArgumentOutOfRangeException>(() => { red.Darken(-1); });
		Assert.ThrowsException<ArgumentOutOfRangeException>(() => { red.Darken(-2); });
		Assert.ThrowsException<ArgumentOutOfRangeException>(() => { red.Darken(-256); });
		Assert.ThrowsException<ArgumentOutOfRangeException>(() => { red.Darken(256); });
	}

	private static void TestColor(Color color, int expectedA, int expectedR, int expectedG, int expectedB)
	{
		Assert.AreEqual(Color.FromArgb(expectedA, expectedR, expectedG, expectedB), color);
	}
}
