using System.Diagnostics;
using System.Drawing;

using static Strick.PlusCon.Test.Expectations;


namespace Strick.PlusCon.Test;


[TestClass]
public class FormattingTests
{
	[TestMethod]
	public void ColorizeTests()
	{
		string test = "test";

		Assert.AreEqual(test, test.Colorize(null));

		TestColorizedString(test, test.Colorize(red), red, null);
		TestColorizedString(test, test.Colorize(green), green, null);
		TestColorizedString(test, test.Colorize(blue), blue, null);
		TestColorizedString(test, test.Colorize(white), white, null);

		TestColorizedString(test, test.Colorize(null, red), null, red);
		TestColorizedString(test, test.Colorize(null, green), null, green);
		TestColorizedString(test, test.Colorize(null, blue), null, blue);
		TestColorizedString(test, test.Colorize(null, white), null, white);

		TestColorizedString(test, test.Colorize(red, red), red, red);


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
	public void ColorizeTests2()
	{
		//tests the "string Colorize(string value, IEnumerable<Color> colors)" overload
		List<Color> colors = null!;
		string empty = "";
		string foo = "foo";
		string foobar = "foobar";

		Assert.AreEqual(empty, empty.Colorize(colors!));
		Assert.AreEqual(foo, foo.Colorize(colors!));
		Assert.AreEqual(foobar, foobar.Colorize(colors!));

		colors = new();
		Assert.AreEqual(empty, empty.Colorize(colors!));
		Assert.AreEqual(foo, foo.Colorize(colors!));
		Assert.AreEqual(foobar, foobar.Colorize(colors!));

		colors.Add(Expectations.red);
		Assert.AreEqual(empty, empty.Colorize(colors));
		Assert.AreEqual($"{ForeColorRed}{foo}{ForeColorReset}", foo.Colorize(colors));
		Assert.AreEqual($"{ForeColorRed}{foobar}{ForeColorReset}", foobar.Colorize(colors));

		colors.Add(Expectations.white);
		Assert.AreEqual(empty, empty.Colorize(colors));
		Assert.AreEqual($"{ForeColorRed}f{ForeColorReset}{ForeColorWhite}o{ForeColorReset}{ForeColorRed}o{ForeColorReset}", foo.Colorize(colors));
		Assert.AreEqual($"{ForeColorRed}f{ForeColorReset}{ForeColorWhite}o{ForeColorReset}{ForeColorRed}o{ForeColorReset}{ForeColorWhite}b{ForeColorReset}{ForeColorRed}a{ForeColorReset}{ForeColorWhite}r{ForeColorReset}", foobar.Colorize(colors));

		colors.Add(Expectations.blue);
		Assert.AreEqual(empty, empty.Colorize(colors));
		Assert.AreEqual($"{ForeColorRed}f{ForeColorReset}{ForeColorWhite}o{ForeColorReset}{ForeColorBlue}o{ForeColorReset}", foo.Colorize(colors));
		Assert.AreEqual($"{ForeColorRed}f{ForeColorReset}{ForeColorWhite}o{ForeColorReset}{ForeColorBlue}o{ForeColorReset}{ForeColorRed}b{ForeColorReset}{ForeColorWhite}a{ForeColorReset}{ForeColorBlue}r{ForeColorReset}", foobar.Colorize(colors));

		colors.Add(Expectations.green);
		Assert.AreEqual(empty, empty.Colorize(colors));
		Assert.AreEqual($"{ForeColorRed}f{ForeColorReset}{ForeColorWhite}o{ForeColorReset}{ForeColorBlue}o{ForeColorReset}", foo.Colorize(colors));
		Assert.AreEqual($"{ForeColorRed}f{ForeColorReset}{ForeColorWhite}o{ForeColorReset}{ForeColorBlue}o{ForeColorReset}{ForeColorGreen}b{ForeColorReset}{ForeColorRed}a{ForeColorReset}{ForeColorWhite}r{ForeColorReset}", foobar.Colorize(colors));
	}

	[TestMethod]
	public void GradientTests()
	{
		string val = "";
		string result = val.Gradient(red, blue);
		Assert.AreEqual($"", result);

		result = val.Gradient(red, white, blue);
		Assert.AreEqual($"", result);

		val = "1";
		result = val.Gradient(red, blue);
		TestGResult(val, result, red);

		val = "1";
		result = val.Gradient(red, white, blue);
		TestGResult(val, result, red);

		val = "12";
		result = val.Gradient(red, blue);
		TestGResult(val, result, red, blue);

		val = "12";
		result = val.Gradient(red, white, blue);
		TestGResult(val, result, red, blue);

		val = "123";
		result = val.Gradient(red, blue);
		TestGResult(val, result, red, Color.FromArgb(255, 127, 0, 128), blue);

		val = "123";
		result = val.Gradient(red, white, blue);
		TestGResult(val, result, red, white, blue);

		val = "1234";
		result = val.Gradient(red, white, blue);
		TestGResult(val, result, red, white, white, blue);

		val = "12345";
		result = val.Gradient(red, white, blue);
		TestGResult(val, result, red, white, white, Color.FromArgb(127, 127, 255), blue);

		val = "123456";
		result = val.Gradient(red, white, blue);
		TestGResult(val, result, red, Color.FromArgb(255, 128, 128), white, white, Color.FromArgb(127, 127, 255), blue);

		val = new string('x', 256);
		result = val.Gradient(black, white);
		List<Color> colors = new List<Color>();
		for (int i = 0; i < val.Length; i++)
		{ colors.Add(Color.FromArgb(i, i, i)); }
		TestGResult(val, result, colors.ToArray());
	}

	[TestMethod]
	public void UnderlineTests()
	{
		string test = "test";
		Assert.AreEqual(Underline + test + UnderlineReset, test.Underline());
	}

	[TestMethod]
	public void ReverseTests()
	{
		string test = "test";
		Assert.AreEqual(Reverse + test + ReverseReset, test.Reverse());
	}

	[TestMethod]
	public void CenterTests()
	{
		string src = default!;
		TestCenter(src);
		TestCenter(src, '-');

		src = "";
		TestCenter(src);
		TestCenter(src, '-');

		src = "1";
		TestCenter(src);
		TestCenter(src, '-');

		src = "12";
		TestCenter(src);
		TestCenter(src, '-');

		src = "123";
		TestCenter(src);
		TestCenter(src, '-');

		src = "1234567890";
		TestCenter(src);
		TestCenter(src, '-');
	}

	[TestMethod]
	public void IntersperseTests()
	{
		string? src = null;

		Assert.AreEqual(null, src!.Intersperse('-'));
		src = "";
		Assert.AreEqual("", src.Intersperse('-'));

		src = "foo";
		Assert.AreEqual("f-o-o", src.Intersperse('-'));
		Assert.AreEqual("f.o.o", src.Intersperse('.'));
		Assert.AreEqual("f o o", src.Intersperse(' '));
		Assert.AreEqual("f o o", src.SpaceOut());

		src = "bar";
		Assert.AreEqual("b-a-r", src.Intersperse('-'));

		src = "foo bar";
		Assert.AreEqual("f-o-o- -b-a-r", src.Intersperse('-'));
		Assert.AreEqual("f o o   b a r", src.Intersperse(' '));
		Assert.AreEqual("f o o   b a r", src.SpaceOut());
	}


	internal static void TestGResult(string source, string result, params Color[] expectedColors)
	{
		Assert.IsNotNull(source);
		Assert.IsNotNull(result);
		Assert.IsNotNull(expectedColors);

		string[] results = result.Split($"{ForeColorReset}", StringSplitOptions.RemoveEmptyEntries);
		Assert.AreEqual(results.Length, expectedColors.Length);

		for (int i = 0; i < results.Length; i++)
		{ TestColorizedString(source.Substring(i, 1), results[i] + ForeColorReset, expectedColors.ElementAt(i), null); }
	}

	internal static void TestColorizedString(string source, string result, Color? fore, Color? back)
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

	internal static void TestCenter(string source, char fillChar = ' ')
	{
		int sl = string.IsNullOrEmpty(source) ? 0 : source.Length;

		Assert.ThrowsException<ArgumentOutOfRangeException>(() => { source.Center(0); });
		Assert.ThrowsException<ArgumentOutOfRangeException>(() => { source.Center(0, fillChar); });

		for (int i = 1; i < sl; i++)
		{
			Assert.AreEqual(source, source.Center(i));
			Assert.AreEqual(source, source.Center(i, fillChar));
		}

		for (int i = sl + 1; i < Math.Max(sl * 3, 50); i++)
		{
			int l = (i - sl) / 2;
			int r = i - l - sl;
			Assert.AreEqual(new string(' ', l) + source + new string(' ', r), source.Center(i), $"src:'{source}', i:{i}");
			Assert.AreEqual(new string(fillChar, l) + source + new string(fillChar, r), source.Center(i, fillChar), $"src:'{source}', i:{i}");
			//Debug.WriteLine($"{i} '{source.Center(i, fillChar)}'");
		}
	}
}
