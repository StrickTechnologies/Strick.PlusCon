using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static Strick.PlusCon.Test.Expectations;


namespace Strick.PlusCon.Test;


[TestClass]
public class EscapeCodeTests
{

	[TestMethod]
	public void Basics()
	{

		Assert.AreEqual(esc, EscapeCodes.Escape);

		Assert.AreEqual(ResetAll, EscapeCodes.ResetAll);
	}

	[TestMethod]
	public void colors()
	{
		string csFore = ColorSpace.fore.AsString();
		string csBack = ColorSpace.back.AsString();

		Assert.AreEqual(esc + "[{cs};2;{r};{g};{b}m", EscapeCodes.Color);
		Assert.AreEqual($"{esc}[{csFore};2;{{r}};{{g}};{{b}}m", EscapeCodes.Color_Fore);
		Assert.AreEqual($"{esc}[{csBack};2;{{r}};{{g}};{{b}}m", EscapeCodes.Color_Back);

		Assert.AreEqual(ForeColorRed, EscapeCodes.GetForeColorSequence(red));
		Assert.AreEqual(ForeColorGreen, EscapeCodes.GetForeColorSequence(green));
		Assert.AreEqual(ForeColorBlue, EscapeCodes.GetForeColorSequence(blue));
		Assert.AreEqual(ForeColorWhite, EscapeCodes.GetForeColorSequence(white));
		Assert.AreEqual(ForeColorBlack, EscapeCodes.GetForeColorSequence(black));
		Assert.AreEqual(BackColorRed, EscapeCodes.GetBackColorSequence(red));
		Assert.AreEqual(BackColorGreen, EscapeCodes.GetBackColorSequence(green));
		Assert.AreEqual(BackColorBlue, EscapeCodes.GetBackColorSequence(blue));
		Assert.AreEqual(BackColorWhite, EscapeCodes.GetBackColorSequence(white));
		Assert.AreEqual(BackColorBlack, EscapeCodes.GetBackColorSequence(black));

		Assert.AreEqual(ForeColorReset, EscapeCodes.ColorReset_Fore);
		Assert.AreEqual(BackColorReset, EscapeCodes.ColorReset_Back);
	}

	[TestMethod]
	public void underline()
	{
		Assert.AreEqual(Underline, EscapeCodes.Underline);
		Assert.AreEqual(UnderlineReset, EscapeCodes.UnderlineReset);
	}

	[TestMethod]
	public void reverse()
	{
		Assert.AreEqual(Reverse, EscapeCodes.Reverse);
		Assert.AreEqual(ReverseReset, EscapeCodes.ReverseReset);
	}
}
