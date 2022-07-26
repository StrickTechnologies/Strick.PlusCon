using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using static Strick.PlusCon.Test.Expectations;


namespace Strick.PlusCon.Test;

[TestClass]
public class FormattingTests
{
	[TestMethod]
	public void colorize()
	{
		string test = "test";

		Assert.AreEqual(test, test.Colorize(null));

		testColorizedString(test, test.Colorize(red), red, null);
		testColorizedString(test, test.Colorize(green), green, null);
		testColorizedString(test, test.Colorize(blue), blue, null);
		testColorizedString(test, test.Colorize(white), white, null);

		testColorizedString(test, test.Colorize(null, red), null, red);
		testColorizedString(test, test.Colorize(null, green), null, green);
		testColorizedString(test, test.Colorize(null, blue), null, blue);
		testColorizedString(test, test.Colorize(null, white), null, white);

		testColorizedString(test, test.Colorize(red, red), red, red);


		string dL = "[";
		string dR = "]";

		string result = test.Colorize(null, null, dL, dR);
		string exp = $"{dL}{test}{dR}";
		Assert.AreEqual(exp, result);

		result = test.Colorize(blue, null, dL, dR);
		exp = $"{dL}{ForeColorBlue}{test}{ForeColorReset}{dR}";
		Assert.AreEqual(exp, result);

		result = test.Colorize(null, white, dL, dR);
		exp = $"{dL}{BackColorWhite}{test}{BackColorReset}{dR}";
		Assert.AreEqual(exp, result);

		result = test.Colorize(blue, white, dL, dR);
		exp = $"{dL}{ForeColorBlue}{BackColorWhite}{test}{BackColorReset}{ForeColorReset}{dR}";
		Assert.AreEqual(exp, result);

		result = test.Colorize(null, null, dL, dR, green, null);
		exp = $"{ForeColorGreen}{dL}{ForeColorReset}{test}{ForeColorGreen}{dR}{ForeColorReset}";
		Assert.AreEqual(exp, result);

		result = test.Colorize(null, null, dL, dR, null, red);
		exp = $"{BackColorRed}{dL}{BackColorReset}{test}{BackColorRed}{dR}{BackColorReset}";
		Assert.AreEqual(exp, result);

		result = test.Colorize(null, null, dL, dR, green, red);
		exp = $"{ForeColorGreen}{BackColorRed}{dL}{BackColorReset}{ForeColorReset}{test}{ForeColorGreen}{BackColorRed}{dR}{BackColorReset}{ForeColorReset}";
		Assert.AreEqual(exp, result);

		result = test.Colorize(blue, null, dL, dR, green, null);
		exp = $"{ForeColorGreen}{dL}{ForeColorReset}{ForeColorBlue}{test}{ForeColorReset}{ForeColorGreen}{dR}{ForeColorReset}";
		Assert.AreEqual(exp, result);

		result = test.Colorize(blue, null, dL, dR, null, red);
		exp = $"{BackColorRed}{dL}{BackColorReset}{ForeColorBlue}{test}{ForeColorReset}{BackColorRed}{dR}{BackColorReset}";
		Assert.AreEqual(exp, result);

		result = test.Colorize(null, white, dL, dR, green);
		exp = $"{ForeColorGreen}{dL}{ForeColorReset}{BackColorWhite}{test}{BackColorReset}{ForeColorGreen}{dR}{ForeColorReset}";
		Assert.AreEqual(exp, result);

		result = test.Colorize(null, white, dL, dR, null, red);
		exp = $"{BackColorRed}{dL}{BackColorReset}{BackColorWhite}{test}{BackColorReset}{BackColorRed}{dR}{BackColorReset}";
		Assert.AreEqual(exp, result);

		result = test.Colorize(blue, white, dL, dR, green, red);
		exp = $"{ForeColorGreen}{BackColorRed}{dL}{BackColorReset}{ForeColorReset}{ForeColorBlue}{BackColorWhite}{test}{BackColorReset}{ForeColorReset}{ForeColorGreen}{BackColorRed}{dR}{BackColorReset}{ForeColorReset}";
		Assert.AreEqual(exp, result);

		//nested
		result = $"foo {test.Colorize(blue, white, dL, dR, green, red)} foo".Colorize(red);
		exp = $"{ForeColorRed}foo {ForeColorGreen}{BackColorRed}{dL}{BackColorReset}{ForeColorReset}{ForeColorBlue}{BackColorWhite}{test}{BackColorReset}{ForeColorReset}{ForeColorGreen}{BackColorRed}{dR}{BackColorReset}{ForeColorReset} foo{ForeColorReset}";
		//System.Diagnostics.Debug.WriteLine(exp);
		//System.Diagnostics.Debug.WriteLine(result);
		Assert.AreEqual(exp, result);
	}

	[TestMethod]
	public void gradient()
	{
		string val = "";
		string result = val.Gradient(red, blue);
		Assert.AreEqual($"", result);

		result = val.Gradient(red, white, blue);
		Assert.AreEqual($"", result);

		val = "1";
		result = val.Gradient(red, blue);
		testGResult(val, result, red);

		val = "1";
		result = val.Gradient(red, white, blue);
		testGResult(val, result, red);

		val = "12";
		result = val.Gradient(red, blue);
		testGResult(val, result, red, blue);

		val = "12";
		result = val.Gradient(red, white, blue);
		testGResult(val, result, red, blue);

		val = "123";
		result = val.Gradient(red, blue);
		testGResult(val, result, red, Color.FromArgb(255, 127, 0, 128), blue);

		val = "123";
		result = val.Gradient(red, white, blue);
		testGResult(val, result, red, white, blue);

		val = "1234";
		result = val.Gradient(red, white, blue);
		testGResult(val, result, red, white, white, blue);

		val = "12345";
		result = val.Gradient(red, white, blue);
		testGResult(val, result, red, white, white, Color.FromArgb(127, 127, 255), blue);

		val = "123456";
		result = val.Gradient(red, white, blue);
		testGResult(val, result, red, Color.FromArgb(255, 128, 128), white, white, Color.FromArgb(127, 127, 255), blue);

		val = new string('x', 256);
		result = val.Gradient(black, white);
		List<Color> colors = new List<Color>();
		for (int i = 0; i < val.Length; i++)
		{ colors.Add(Color.FromArgb(i, i, i)); }
		testGResult(val, result, colors.ToArray());
	}

	private void testGResult(string source, string result, params Color[] expectedColors)
	{
		Assert.IsNotNull(source);
		Assert.IsNotNull(result);
		Assert.IsNotNull(expectedColors);

		string[] results = result.Split($"{ForeColorReset}", StringSplitOptions.RemoveEmptyEntries);
		Assert.AreEqual(results.Length, expectedColors.Count());

		for (int i = 0; i < results.Length; i++)
		{ testColorizedString(source.Substring(i, 1), results[i] + ForeColorReset, expectedColors.ElementAt(i), null); }
	}

	private void testColorizedString(string source, string result, Color? fore, Color? back)
	{
		Assert.IsNotNull(source);
		Assert.IsNotNull(result);

		if (fore == null && back == null)
		{ Assert.AreEqual(source, result); }

		else if (fore != null && back != null)
		{ Assert.AreEqual($"{fore.Value.ForeColorString()}{back.Value.BackColorString()}{source}{BackColorReset}{ForeColorReset}", result); }

		else if (fore != null)
		{ Assert.AreEqual($"{fore.Value.ForeColorString()}{source}{ForeColorReset}", result); }

		else //if (back != null)
		{ Assert.AreEqual($"{back!.Value.BackColorString()}{source}{BackColorReset}", result); }
	}
}
