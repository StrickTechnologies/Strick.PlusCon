using System.Drawing;

using static Strick.PlusCon.Helpers;
using static Strick.PlusCon.Test.Expectations;


namespace Strick.PlusCon.Test;


[TestClass]
public class HelperTests
{
	[TestMethod]
	public void WandWL_NoValues()
	{
		foreach (string value in new[] { "test", "Test [22", "Test 22]" })
		{ testWandWL(value, value); }
	}

	[TestMethod]
	public void WandWL_Values()
	{
		string value = "test [foo]";
		string expected = "test {df}{db}{dl}{dbr}{dfr}{f}{b}foo{br}{fr}{df}{db}{dr}{dbr}{dfr}";
		/*
		placeholders:
			{df} - delimiter start foreground color
			{db} - delimiter start background color
			{dl}{dr} - left/right delimiter
			{dfr} - delimiter reset foreground color
			{dbr} - delimiter reset background color
			{f} - start foreground color
			{b} - start background color
			{br} - reset foreground color
			{fr} - reset background color
		 */

		testWandWLV(value, expected);

		value = "test [foo] [bar]";
		expected = "test {df}{db}{dl}{dbr}{dfr}{f}{b}foo{br}{fr}{df}{db}{dr}{dbr}{dfr} {df}{db}{dl}{dbr}{dfr}{f}{b}bar{br}{fr}{df}{db}{dr}{dbr}{dfr}";
		testWandWLV(value, expected);
	}


	private void testWandWL(string value, string expected)
	{
		var originalOut = Console.Out;
		using var sw = new StringWriter();
		Console.SetOut(sw);

		//no colors
		W(value);
		sw.Flush();
		string result = sw.ToString();
		Assert.AreEqual(expected, result);
		sw.Clear();

		WL(value);
		sw.Flush();
		result = sw.ToString();
		Assert.AreEqual(expected + "\r\n", result);
		sw.Clear();


		//fore color only
		W(value, red);
		sw.Flush();
		result = sw.ToString();
		Assert.AreEqual($"{ForeColorRed}{expected}{ForeColorReset}", result);
		sw.Clear();

		WL(value, red);
		sw.Flush();
		result = sw.ToString();
		Assert.AreEqual($"{ForeColorRed}{expected}{ForeColorReset}\r\n", result);
		sw.Clear();


		//fore+back colors
		W(value, red, blue);
		sw.Flush();
		result = sw.ToString();
		Assert.AreEqual($"{ForeColorRed}{BackColorBlue}{expected}{BackColorReset}{ForeColorReset}", result);
		sw.Clear();

		WL(value, red, blue);
		sw.Flush();
		result = sw.ToString();
		Assert.AreEqual($"{ForeColorRed}{BackColorBlue}{expected}{BackColorReset}{ForeColorReset}\r\n", result);
		sw.Clear();


		Console.SetOut(originalOut);
	}

	private void testWandWLV(string value, string expected)
	{
		var originalOut = Console.Out;
		using var sw = new StringWriter();
		Console.SetOut(sw);

		//no colors
		string exp = expected
			.Replace("{f}", "")
			.Replace("{fr}", "")
			.Replace("{b}", "")
			.Replace("{br}", "")
			.Replace("{dl}", "[")
			.Replace("{dr}", "]")
			.Replace("{df}", "")
			.Replace("{dfr}", "")
			.Replace("{db}", "")
			.Replace("{dbr}", "");

		W(value);
		sw.Flush();
		string result = sw.ToString();
		Assert.AreEqual(exp, result);
		sw.Clear();

		WL(value);
		sw.Flush();
		result = sw.ToString();
		Assert.AreEqual(exp + "\r\n", result);
		sw.Clear();


		//fore color only
		exp = expected
			.Replace("{f}", ForeColorRed)
			.Replace("{fr}", ForeColorReset)
			.Replace("{b}", "")
			.Replace("{br}", "")
			.Replace("{dl}", "")
			.Replace("{dr}", "")
			.Replace("{df}", "")
			.Replace("{dfr}", "")
			.Replace("{db}", "")
			.Replace("{dbr}", "");

		W(value, red);
		sw.Flush();
		result = sw.ToString();
		Assert.AreEqual(exp, result);
		sw.Clear();

		WL(value, red);
		sw.Flush();
		result = sw.ToString();
		Assert.AreEqual(exp + "\r\n", result);
		sw.Clear();


		//fore, back colors
		exp = expected
			.Replace("{f}", ForeColorRed)
			.Replace("{fr}", ForeColorReset)
			.Replace("{b}", BackColorBlue)
			.Replace("{br}", BackColorReset)
			.Replace("{dl}", "")
			.Replace("{dr}", "")
			.Replace("{df}", "")
			.Replace("{dfr}", "")
			.Replace("{db}", "")
			.Replace("{dbr}", "");

		W(value, red, blue);
		sw.Flush();
		result = sw.ToString();
		Assert.AreEqual(exp, result);
		sw.Clear();

		WL(value, red, blue);
		sw.Flush();
		result = sw.ToString();
		Assert.AreEqual(exp + "\r\n", result);
		sw.Clear();


		//fore color & delimiters
		exp = expected
			.Replace("{f}", ForeColorRed)
			.Replace("{fr}", ForeColorReset)
			.Replace("{b}", "")
			.Replace("{br}", "")
			.Replace("{dl}", "[")
			.Replace("{dr}", "]")
			.Replace("{df}", "")
			.Replace("{dfr}", "")
			.Replace("{db}", "")
			.Replace("{dbr}", "");

		W(value, red, null, true);
		sw.Flush();
		result = sw.ToString();
		Assert.AreEqual(exp, result);
		sw.Clear();

		WL(value, red, null, true);
		sw.Flush();
		result = sw.ToString();
		Assert.AreEqual(exp + "\r\n", result);
		sw.Clear();


		//fore, back colors & delimiters
		exp = expected
			.Replace("{f}", ForeColorRed)
			.Replace("{fr}", ForeColorReset)
			.Replace("{b}", BackColorBlue)
			.Replace("{br}", BackColorReset)
			.Replace("{dl}", "[")
			.Replace("{dr}", "]")
			.Replace("{df}", "")
			.Replace("{dfr}", "")
			.Replace("{db}", "")
			.Replace("{dbr}", "");

		W(value, red, blue, true);
		sw.Flush();
		result = sw.ToString();
		Assert.AreEqual(exp, result);
		sw.Clear();

		WL(value, red, blue, true);
		sw.Flush();
		result = sw.ToString();
		Assert.AreEqual(exp + "\r\n", result);
		sw.Clear();


		//fore color, delimiter fore color
		exp = expected
			.Replace("{f}", ForeColorRed)
			.Replace("{fr}", ForeColorReset)
			.Replace("{b}", "")
			.Replace("{br}", "")
			.Replace("{dl}", "[")
			.Replace("{dr}", "]")
			.Replace("{df}", ForeColorGreen)
			.Replace("{dfr}", ForeColorReset)
			.Replace("{db}", "")
			.Replace("{dbr}", "");

		W(value, red, null, green);
		sw.Flush();
		result = sw.ToString();
		Assert.AreEqual(exp, result);
		sw.Clear();

		WL(value, red, null, green);
		sw.Flush();
		result = sw.ToString();
		Assert.AreEqual(exp + "\r\n", result);
		sw.Clear();


		//fore, back colors &  delimiter fore color
		exp = expected
			.Replace("{f}", ForeColorRed)
			.Replace("{fr}", ForeColorReset)
			.Replace("{b}", BackColorBlue)
			.Replace("{br}", BackColorReset)
			.Replace("{dl}", "[")
			.Replace("{dr}", "]")
			.Replace("{df}", ForeColorGreen)
			.Replace("{dfr}", ForeColorReset)
			.Replace("{db}", "")
			.Replace("{dbr}", "");

		W(value, red, blue, green);
		sw.Flush();
		result = sw.ToString();
		Assert.AreEqual(exp, result);
		sw.Clear();

		WL(value, red, blue, green);
		sw.Flush();
		result = sw.ToString();
		Assert.AreEqual(exp + "\r\n", result);
		sw.Clear();


		//fore, back color & delimiter fore, back colors
		exp = expected
			.Replace("{f}", ForeColorRed)
			.Replace("{fr}", ForeColorReset)
			.Replace("{b}", BackColorBlue)
			.Replace("{br}", BackColorReset)
			.Replace("{dl}", "[")
			.Replace("{dr}", "]")
			.Replace("{df}", ForeColorGreen)
			.Replace("{dfr}", ForeColorReset)
			.Replace("{db}", BackColorWhite)
			.Replace("{dbr}", BackColorReset);

		W(value, red, blue, green, white);
		sw.Flush();
		result = sw.ToString();
		Assert.AreEqual(exp, result);
		sw.Clear();

		WL(value, red, blue, green, white);
		sw.Flush();
		result = sw.ToString();
		Assert.AreEqual(exp + "\r\n", result);
		sw.Clear();


		Console.SetOut(originalOut);
	}
}
