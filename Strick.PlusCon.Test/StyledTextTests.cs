﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static Strick.PlusCon.Test.Expectations;
using Strick.PlusCon.Models;

namespace Strick.PlusCon.Test;


[TestClass]
public class StyledTextTests
{
	[TestMethod]
	public void Constructors()
	{
		string text = default!;
		StyledText st = default!;
		Assert.IsNull(st);
		Assert.ThrowsException<NullReferenceException>(() => _ = st.Text);

		Assert.ThrowsException<ArgumentNullException>(() => new StyledText(null!));
		Assert.ThrowsException<ArgumentNullException>(() => new StyledText(""));
		Assert.ThrowsException<ArgumentNullException>(() => new StyledText(text));

		st = new(" ");
		Assert.IsNotNull(st);
		Assert.IsNotNull(st.Style);
		Assert.AreEqual(" ", st.Text);
		Assert.AreEqual(" ", st.TextStyled);
		Assert.AreEqual("foo", st.StyleText("foo"));

		st = new("foo");
		Assert.IsNotNull(st);
		Assert.IsNotNull(st.Style);
		Assert.AreEqual("foo", st.Text);
		Assert.AreEqual("foo", st.TextStyled);
		Assert.AreEqual("foobar", st.StyleText("foobar"));

		st = new("foo", null!);
		Assert.IsNotNull(st);
		Assert.IsNotNull(st.Style);
		Assert.AreEqual("foo", st.Text);
		Assert.AreEqual("foo", st.TextStyled);
		Assert.AreEqual("foobar", st.StyleText("foobar"));

		TextStyle style = null!;
		st = new("foo", style);
		Assert.IsNotNull(st);
		Assert.IsNotNull(st.Style);
		Assert.AreEqual("foo", st.Text);
		Assert.AreEqual("foo", st.TextStyled);
		Assert.AreEqual("foobar", st.StyleText("foobar"));

		style = new();
		st = new("foo", style);
		Assert.IsNotNull(st);
		Assert.IsNotNull(st.Style);
		Assert.AreEqual("foo", st.Text);
		Assert.AreEqual("foo", st.TextStyled);
		Assert.AreEqual("foobar", st.StyleText("foobar"));

		style.ForeColor = red;
		Assert.IsNotNull(st);
		Assert.IsNotNull(st.Style);
		Assert.AreEqual("foo", st.Text);
		FormattingTests.TestColorizedString(st.Text, st.TextStyled, red, null);
		FormattingTests.TestColorizedString("foobar", st.StyleText("foobar"), red, null);

		st = new StyledText("foo", blue);
		Assert.IsNotNull(st);
		Assert.IsNotNull(st.Style);
		Assert.AreEqual("foo", st.Text);
		Assert.AreEqual(blue, st.Style.ForeColor);
		FormattingTests.TestColorizedString(st.Text, st.TextStyled, blue, null);
		FormattingTests.TestColorizedString("foobar", st.StyleText("foobar"), blue, null);

		st = new StyledText("foo", blue, black);
		Assert.IsNotNull(st);
		Assert.IsNotNull(st.Style);
		Assert.AreEqual("foo", st.Text);
		Assert.AreEqual(blue, st.Style.ForeColor);
		Assert.AreEqual(black, st.Style.BackColor);
		FormattingTests.TestColorizedString(st.Text, st.TextStyled, blue, black);
		FormattingTests.TestColorizedString("foobar", st.StyleText("foobar"), blue, black);

		st = new StyledText("foo", green, blue, green);
		Assert.IsNotNull(st);
		Assert.IsNotNull(st.Style);
		Assert.AreEqual("foo", st.Text);
		Assert.AreEqual(green, st.Style.GradientStart);
		Assert.AreEqual(blue, st.Style.GradientMiddle);
		Assert.AreEqual(green, st.Style.GradientEnd);
		FormattingTests.TestGResult(st.Text, st.TextStyled, green, blue, green);
	}

	[TestMethod]
	public void Style()
	{
		//just some basic style tests here... More extensive style tests are done in TextStyleTests

		string text = default!;

		StyledText st = new("foo");
		Assert.IsNotNull(st);
		Assert.AreEqual("foo", st.Text);
		Assert.AreEqual("foo", st.TextStyled);
		Assert.AreEqual("foobar", st.StyleText("foobar"));

		Assert.ThrowsException<ArgumentNullException>(() => { st.Text = null!; });
		Assert.ThrowsException<ArgumentNullException>(() => { st.Text = ""; });
		Assert.ThrowsException<ArgumentNullException>(() => { st.Text = text; });

		Assert.AreEqual("foo", st.Text);
		Assert.AreEqual("foo", st.TextStyled);

		st.Style.ForeColor = red;
		FormattingTests.TestColorizedString(st.Text, st.TextStyled, red, null);
		FormattingTests.TestColorizedString("foobar", st.StyleText("foobar"), red, null);

		st.Style.BackColor = white;
		FormattingTests.TestColorizedString(st.Text, st.TextStyled, red, white);
		FormattingTests.TestColorizedString("foobar", st.StyleText("foobar"), red, white);

		st.Style = null!;
		Assert.IsNull(st.Style);
		Assert.AreEqual("foo", st.Text);
		Assert.AreEqual("foo", st.TextStyled);

		st.Style = new TextStyle();
		Assert.IsNotNull(st.Style);
		Assert.AreEqual("foo", st.Text);
		Assert.AreEqual("foo", st.TextStyled);
		Assert.AreEqual("foobar", st.StyleText("foobar"));

		st.Style.ForeColor = red;
		Assert.IsNotNull(st);
		Assert.IsNotNull(st.Style);
		Assert.AreEqual("foo", st.Text);
		FormattingTests.TestColorizedString(st.Text, st.TextStyled, red, null);
		FormattingTests.TestColorizedString("foobar", st.StyleText("foobar"), red, null);
	}


	internal static void TestStyledTextEquality(StyledText? styledText, StyledText? expectedStyledText)
	{
		if (!ReferenceEquals(styledText, expectedStyledText))
		{
			Assert.IsNotNull(styledText);
			Assert.IsNotNull(expectedStyledText);
			Assert.AreEqual(expectedStyledText.Text, styledText.Text);
			TextStyleTests.CheckTextStyleEquality(styledText.Style, expectedStyledText.Style);
		}
	}
}
