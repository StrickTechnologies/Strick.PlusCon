using System.Drawing;

using Strick.PlusCon.Models;

using static Strick.PlusCon.Helpers;
using static Strick.PlusCon.Test.Expectations;


namespace Strick.PlusCon.Test;


[TestClass]
public class HelperTests
{

	[TestMethod]
	public void WandWL_NoValues()
	{
		string? ns = null;
		foreach (string? value in new[] { ns, "", "test", "Test [22", "Test 22]" })
		{ TestWandWL(value, value ?? ""); }
	}

	[TestMethod]
	public void WandWL_Values()
	{
		/*
		placeholders:
			{df}		delimiter start foreground color
			{db}		delimiter start background color
			{dl}{dr}	left/right delimiter
			{dfr}		delimiter reset foreground color
			{dbr}		delimiter reset background color
			{f}			start foreground color
			{b}			start background color
			{br}		reset background color
			{fr}		reset foreground color
		*/

		string? value = null;
		string expected = "";
		TestWandWLV(value, expected);

		value = "";
		TestWandWLV(value, expected);

		value = "test [foo]";
		expected = "test {df}{db}{dl}{dbr}{dfr}{f}{b}foo{br}{fr}{df}{db}{dr}{dbr}{dfr}";
		TestWandWLV(value, expected);

		value = "[foo] test";
		expected = "{df}{db}{dl}{dbr}{dfr}{f}{b}foo{br}{fr}{df}{db}{dr}{dbr}{dfr} test";
		TestWandWLV(value, expected);

		value = "test [foo] [bar]";
		expected = "test {df}{db}{dl}{dbr}{dfr}{f}{b}foo{br}{fr}{df}{db}{dr}{dbr}{dfr} {df}{db}{dl}{dbr}{dfr}{f}{b}bar{br}{fr}{df}{db}{dr}{dbr}{dfr}";
		TestWandWLV(value, expected);

		value = "[foo] test [bar]";
		expected = "{df}{db}{dl}{dbr}{dfr}{f}{b}foo{br}{fr}{df}{db}{dr}{dbr}{dfr} test {df}{db}{dl}{dbr}{dfr}{f}{b}bar{br}{fr}{df}{db}{dr}{dbr}{dfr}";
		TestWandWLV(value, expected);

		value = "[foo]";
		expected = "{df}{db}{dl}{dbr}{dfr}{f}{b}foo{br}{fr}{df}{db}{dr}{dbr}{dfr}";
		TestWandWLV(value, expected);

		value = "[]";
		expected = "{df}{db}{dl}{dbr}{dfr}{f}{b}{br}{fr}{df}{db}{dr}{dbr}{dfr}";
		TestWandWLV(value, expected);

		value = "[foo] [bar]";
		expected = "{df}{db}{dl}{dbr}{dfr}{f}{b}foo{br}{fr}{df}{db}{dr}{dbr}{dfr} {df}{db}{dl}{dbr}{dfr}{f}{b}bar{br}{fr}{df}{db}{dr}{dbr}{dfr}";
		TestWandWLV(value, expected);

		value = "[] [bar]";
		expected = "{df}{db}{dl}{dbr}{dfr}{f}{b}{br}{fr}{df}{db}{dr}{dbr}{dfr} {df}{db}{dl}{dbr}{dfr}{f}{b}bar{br}{fr}{df}{db}{dr}{dbr}{dfr}";
		TestWandWLV(value, expected);

		value = "[] []";
		expected = "{df}{db}{dl}{dbr}{dfr}{f}{b}{br}{fr}{df}{db}{dr}{dbr}{dfr} {df}{db}{dl}{dbr}{dfr}{f}{b}{br}{fr}{df}{db}{dr}{dbr}{dfr}";
		TestWandWLV(value, expected);
	}

	[TestMethod]
	public void WandWL_StyledText()
	{
		string? text = null;
		TextStyle style = new TextStyle();
		string expected;

		expected = "";
		TestWandWLStyledText(text, style, expected);

		text = "";
		TestWandWLStyledText(text, style, expected);

		text = " ";
		expected = " ";
		TestWandWLStyledText(text, style, expected);

		text = "foo";
		expected = "foo";
		TestWandWLStyledText(text, style, expected);

		style.ForeColor = Expectations.red;
		expected = $"{ForeColorRed}{text}{ForeColorReset}";
		TestWandWLStyledText(text, style, expected);

		style.BackColor = Expectations.white;
		expected = $"{ForeColorRed}{BackColorWhite}{text}{BackColorReset}{ForeColorReset}";
		TestWandWLStyledText(text, style, expected);

		style.Underline = true;
		expected = $"{Underline}{ForeColorRed}{BackColorWhite}{text}{BackColorReset}{ForeColorReset}{UnderlineReset}";
		TestWandWLStyledText(text, style, expected);

		style.Reverse = true;
		expected = $"{Underline}{Reverse}{ForeColorRed}{BackColorWhite}{text}{BackColorReset}{ForeColorReset}{ReverseReset}{UnderlineReset}";
		TestWandWLStyledText(text, style, expected);

		style.SetGradientColors(blue, green, white);
		style.BackColor = null;
		style.ForeColor = null;
		expected = $"{Underline}{Reverse}{ForeColorBlue}f{ForeColorReset}{ForeColorGreen}o{ForeColorReset}{ForeColorWhite}o{ForeColorReset}{ReverseReset}{UnderlineReset}";
		TestWandWLStyledText(text, style, expected);
	}


	private static void TestWandWL(string? value, string expected)
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
		if (string.IsNullOrEmpty(value))
		{ Assert.AreEqual(expected, result); }
		else
		{ Assert.AreEqual($"{ForeColorRed}{expected}{ForeColorReset}", result); }
		sw.Clear();

		WL(value, red);
		sw.Flush();
		result = sw.ToString();
		if (string.IsNullOrEmpty(value))
		{ Assert.AreEqual(expected + "\r\n", result); }
		else
		{ Assert.AreEqual($"{ForeColorRed}{expected}{ForeColorReset}\r\n", result); }
		sw.Clear();


		//fore+back colors
		W(value, red, blue);
		sw.Flush();
		result = sw.ToString();
		if (string.IsNullOrEmpty(value))
		{ Assert.AreEqual(expected, result); }
		else
		{ Assert.AreEqual($"{ForeColorRed}{BackColorBlue}{expected}{BackColorReset}{ForeColorReset}", result); }
		sw.Clear();

		WL(value, red, blue);
		sw.Flush();
		result = sw.ToString();
		if (string.IsNullOrEmpty(value))
		{ Assert.AreEqual(expected + "\r\n", result); }
		else
		{ Assert.AreEqual($"{ForeColorRed}{BackColorBlue}{expected}{BackColorReset}{ForeColorReset}\r\n", result); }
		sw.Clear();


		Console.SetOut(originalOut);
	}

	private static void TestWandWLV(string? value, string expected)
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


		Console.SetOut(originalOut);
		sw.Clear();
	}

	private static void TestWandWLStyledText(string? value, TextStyle style, string expected)
	{
		string expectedWL = expected + "\r\n";
		var originalOut = Console.Out;
		using var sw = new StringWriter();
		Console.SetOut(sw);

		if (string.IsNullOrEmpty(value))
		{
			StyledText st = null!;
			W(st);
			sw.Flush();
			Assert.AreEqual(expected, sw.ToString());
			sw.Clear();

			WL(st);
			sw.Flush();
			Assert.AreEqual(expectedWL, sw.ToString());
			sw.Clear();
		}
		else
		{
			StyledText st = new(value, style);
			W(st);
			sw.Flush();
			Assert.AreEqual(expected, sw.ToString());
			sw.Clear();

			WL(st);
			sw.Flush();
			Assert.AreEqual(expectedWL, sw.ToString());
			sw.Clear();
		}

		W(value, style);
		sw.Flush();
		Assert.AreEqual(expected, sw.ToString());
		sw.Clear();

		WL(value, style);
		sw.Flush();
		Assert.AreEqual(expectedWL, sw.ToString());
		sw.Clear();

		TextStyle tsN = null!;
		W(value, tsN);
		sw.Flush();
		Assert.AreEqual(value ?? "", sw.ToString());
		sw.Clear();

		WL(value, tsN);
		sw.Flush();
		Assert.AreEqual(value + "\r\n", sw.ToString());
		sw.Clear();

		Console.SetOut(originalOut);
	}


	[TestMethod]
	public void TestCls()
	{
		var originalOut = Console.Out;
		using var sw = new StringWriter();
		Console.SetOut(sw);
		CLS();
		sw.Flush();
		string result = sw.ToString();

		Assert.IsNotNull(result);
		Assert.IsTrue(string.IsNullOrEmpty(result));

		sw.Clear();
		CLS(Color.Red);
		sw.Flush();
		result = sw.ToString();
		Assert.IsNotNull(result);
		Assert.AreEqual(BackColorRed, result);

		sw.Clear();
		CLS(Color.Red, Color.Blue);
		sw.Flush();
		result = sw.ToString();
		Assert.IsNotNull(result);
		Assert.AreEqual(BackColorRed + ForeColorBlue, result);

		Console.SetOut(originalOut);
		sw.Clear();
	}
}
