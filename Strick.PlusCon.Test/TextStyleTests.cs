using System.Drawing;

using Strick.PlusCon.Models;

using static Strick.PlusCon.Test.Expectations;


namespace Strick.PlusCon.Test;


[TestClass]
public class TextStyleTests
{
	private static readonly string foobar = "foobar";

	[TestMethod]
	public void Constructors()
	{
		TextStyle ts = default!;
		Assert.IsNull(ts);

		ts = new();
		TestTextStyleState(ts, null, null, null, null, null, false, false);
		TextStyle ts2 = new(ts);
		TestTextStyleState(ts2, null, null, null, null, null, false, false);
		Assert.IsFalse(ReferenceEquals(ts, ts2));

		ts = new(red);
		TestTextStyleState(ts, red, null, null, null, null, false, false);
		ts2 = new(ts);
		TestTextStyleState(ts2, red, null, null, null, null, false, false);
		Assert.IsFalse(ReferenceEquals(ts, ts2));
		ts.ForeColor = blue;
		TestTextStyleState(ts, blue, null, null, null, null, false, false);
		TestTextStyleState(ts2, red, null, null, null, null, false, false);

		ts = new(red, white);
		TestTextStyleState(ts, red, white, null, null, null, false, false);
		ts2 = new(ts);
		TestTextStyleState(ts2, red, white, null, null, null, false, false);
		Assert.IsFalse(ReferenceEquals(ts, ts2));

		ts = new(red, null, blue);
		TestTextStyleState(ts, null, null, red, null, blue, false, false);
		ts2 = new(ts);
		TestTextStyleState(ts2, null, null, red, null, blue, false, false);
		Assert.IsFalse(ReferenceEquals(ts, ts2));

		ts = new(red, white, blue);
		TestTextStyleState(ts, null, null, red, white, blue, false, false);
		ts2 = new(ts);
		TestTextStyleState(ts2, null, null, red, white, blue, false, false);
		Assert.IsFalse(ReferenceEquals(ts, ts2));

		ts2 = new(ts);
		TestTextStyleState(ts2, null, null, red, white, blue, false, false);
		Assert.IsFalse(ReferenceEquals(ts, ts2));

		ts = null!;
		ts2 = new(ts);
		TestTextStyleState(ts2, null, null, null, null, null, false, false);
		Assert.IsFalse(ReferenceEquals(ts, ts2));
	}

	[TestMethod]
	public void StyleTextTests()
	{
		TextStyle ts = new();
		TestTextStyleState(ts, null, null, null, null, null, false, false);

		ts.ForeColor = red;
		TestTextStyleState(ts, red, null, null, null, null, false, false);

		ts.BackColor = white;
		TestTextStyleState(ts, red, white, null, null, null, false, false);

		ts.ForeColor = null;
		TestTextStyleState(ts, null, white, null, null, null, false, false);

		ts.BackColor = null;
		TestTextStyleState(ts, null, null, null, null, null, false, false);

		ts.Underline = true;
		TestTextStyleState(ts, null, null, null, null, null, true, false);

		ts.Reverse = true;
		TestTextStyleState(ts, null, null, null, null, null, true, true);

		ts.ForeColor = red;
		TestTextStyleState(ts, red, null, null, null, null, true, true);

		ts.BackColor = white;
		TestTextStyleState(ts, red, white, null, null, null, true, true);

		ts = new TextStyle();
		ts.SetGradientColors(red, white);
		TestTextStyleState(ts, null, null, red, null, white, false, false);

		ts.SetGradientColors(red, white, blue);
		TestTextStyleState(ts, null, null, red, white, blue, false, false);

		ts.Reverse = true;
		TestTextStyleState(ts, null, null, red, white, blue, false, true);

		ts.ForeColor = green;
		TestTextStyleState(ts, green, null, red, white, blue, false, true);

		ts.ForeColor = null;
		ts.BackColor = green;
		TestTextStyleState(ts, null, green, red, white, blue, false, true);

		ts.ClearGradient();
		TestTextStyleState(ts, null, green, null, null, null, false, true);

		//...there are other combinations that could be tested
	}


	internal static void TestTextStyleState(TextStyle ts, Color? expectedFore, Color? expectedBack, Color? expectedGStart, Color? expectedGMid, Color? expectedGEnd, bool expectedUnder, bool expectedRev)
	{
		Assert.IsNotNull(ts);

		Assert.AreEqual(expectedFore, ts.ForeColor);
		Assert.AreEqual(expectedBack, ts.BackColor);

		Assert.AreEqual(expectedGStart, ts.GradientStart);
		Assert.AreEqual(expectedGMid, ts.GradientMiddle);
		Assert.AreEqual(expectedGEnd, ts.GradientEnd);

		Assert.AreEqual(expectedUnder, ts.Underline);
		Assert.AreEqual(expectedRev, ts.Reverse);

		TestTextStyle_StyleText(ts, expectedFore, expectedBack, expectedGStart, expectedGMid, expectedGEnd, expectedUnder, expectedRev);
	}

	internal static void TestTextStyle_StyleText(TextStyle ts, Color? expectedFore, Color? expectedBack, Color? expectedGStart, Color? expectedGMid, Color? expectedGEnd, bool expectedUnder, bool expectedRev)
	{
		string src;
		if (expectedGStart == null)
		{ src = foobar; }
		else if (expectedGMid == null)
		{ src = "12"; }
		else
		{ src = "123"; }

		string res = ts.StyleText(src);


		//underline
		if (expectedUnder)
		{
			Assert.IsTrue(res.StartsWith(Underline));
			Assert.IsTrue(res.EndsWith(UnderlineReset));
			res = res.Replace(Underline, "").Replace(UnderlineReset, "");
		}

		//reverse
		if (expectedRev)
		{
			Assert.IsTrue(res.StartsWith(Reverse));
			Assert.IsTrue(res.EndsWith(ReverseReset));
			res = res.Replace(Reverse, "").Replace(ReverseReset, "");
		}

		//fore
		if (expectedFore != null)
		{
			string s = expectedFore.Value.ForeColorString();

			Assert.IsTrue(res.StartsWith(s));
			Assert.IsTrue(res.EndsWith(ForeColorReset));
			res = res.Substring(s.Length, res.Length - s.Length - ForeColorReset.Length);
		}

		//back
		if (expectedBack != null)
		{
			string s = expectedBack.Value.BackColorString();

			Assert.IsTrue(res.StartsWith(s));
			Assert.IsTrue(res.EndsWith(BackColorReset));
			res = res.Substring(s.Length, res.Length - s.Length - BackColorReset.Length);
		}

		//gradient
		if (expectedGStart != null)
		{
			if (expectedGEnd == null)
			{ Assert.Fail("Only one expected gradient color is invalid"); }

			if (expectedGMid == null)
			{ FormattingTests.TestGResult(src, res, expectedGStart.Value, expectedGEnd.Value); }
			else
			{ FormattingTests.TestGResult(src, res, expectedGStart.Value, expectedGMid.Value, expectedGEnd.Value); }
		}
	}

	internal static void CheckTextStyleEquality(TextStyle? style, TextStyle? expectedStyle)
	{
		if (!ReferenceEquals(style, expectedStyle))
		{
			Assert.IsNotNull(style);
			Assert.IsNotNull(expectedStyle);
			Assert.AreEqual(expectedStyle.BackColor, style.BackColor);
			Assert.AreEqual(expectedStyle.ForeColor, style.ForeColor);
			Assert.AreEqual(expectedStyle.GradientStart, style.GradientStart);
			Assert.AreEqual(expectedStyle.GradientMiddle, style.GradientMiddle);
			Assert.AreEqual(expectedStyle.GradientEnd, style.GradientEnd);
			Assert.AreEqual(expectedStyle.Underline, style.Underline);
			Assert.AreEqual(expectedStyle.Reverse, style.Reverse);
		}
	}
}
