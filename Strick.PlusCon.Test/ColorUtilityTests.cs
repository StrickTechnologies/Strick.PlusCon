using System;
using System.Collections.Generic;
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
}
