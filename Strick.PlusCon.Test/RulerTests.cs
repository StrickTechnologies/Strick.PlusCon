using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Strick.PlusCon.Models;

namespace Strick.PlusCon.Test;


[TestClass]
public class RulerTests
{
	[TestMethod]
	public void Basics()
	{
		var savedColors = Ruler.Colors;
		Ruler.Colors = null;
		int w = 1;

		string r = Ruler.Get(w);
		CheckGet(1, "-");
		CheckGet(2, "--");
		CheckGet(3, "---");
		CheckGet(4, "----");
		CheckGet(5, "----┼");
		CheckGet(6, "----┼-");
		CheckGet(7, "----┼--");
		CheckGet(8, "----┼---");
		CheckGet(9, "----┼----");
		CheckGet(10, "----┼----1");
		CheckGet(11, "----┼----1-");
		CheckGet(12, "----┼----1--");
		CheckGet(13, "----┼----1---");
		CheckGet(14, "----┼----1----");
		CheckGet(15, "----┼----1----┼");
		CheckGet(16, "----┼----1----┼-");
		CheckGet(17, "----┼----1----┼--");
		CheckGet(18, "----┼----1----┼---");
		CheckGet(19, "----┼----1----┼----");
		CheckGet(20, "----┼----1----┼----2");
		CheckGet(21, "----┼----1----┼----2-");

		//restore colors
		Ruler.Colors = savedColors;
	}

	[TestMethod]
	public void SegmentTests()
	{
		var savedColors = Ruler.Colors;
		Ruler.Colors = null;
		Assert.AreEqual("----┼----", Ruler.Segment);
		Ruler.Segment = "123456789";
		Assert.AreEqual("123456789", Ruler.Segment);
		CheckGet(1, "1");
		CheckGet(2, "12");
		CheckGet(3, "123");
		CheckGet(4, "1234");
		CheckGet(5, "12345");
		CheckGet(6, "123456");
		CheckGet(7, "1234567");
		CheckGet(8, "12345678");
		CheckGet(9, "123456789");
		CheckGet(10, "1234567891");
		CheckGet(11, "12345678911");
		CheckGet(12, "123456789112");
		CheckGet(13, "1234567891123");
		CheckGet(14, "12345678911234");
		CheckGet(15, "123456789112345");
		CheckGet(16, "1234567891123456");
		CheckGet(17, "12345678911234567");
		CheckGet(18, "123456789112345678");
		CheckGet(19, "1234567891123456789");
		CheckGet(20, "12345678911234567892");

		Assert.ThrowsException<ArgumentNullException>(() => Ruler.Segment = null!);
		Assert.ThrowsException<ArgumentException>(() => Ruler.Segment = "");
		Assert.ThrowsException<ArgumentException>(() => Ruler.Segment = "1");
		Assert.ThrowsException<ArgumentException>(() => Ruler.Segment = "12");
		Assert.ThrowsException<ArgumentException>(() => Ruler.Segment = "123");
		Assert.ThrowsException<ArgumentException>(() => Ruler.Segment = "1234");
		Assert.ThrowsException<ArgumentException>(() => Ruler.Segment = "12345");
		Assert.ThrowsException<ArgumentException>(() => Ruler.Segment = "123456");
		Assert.ThrowsException<ArgumentException>(() => Ruler.Segment = "1234567");
		Assert.ThrowsException<ArgumentException>(() => Ruler.Segment = "12345678");
		Assert.ThrowsException<ArgumentException>(() => Ruler.Segment = "1234567890");
		Assert.ThrowsException<ArgumentException>(() => Ruler.Segment = "12345678901");
		Assert.ThrowsException<ArgumentException>(() => Ruler.Segment = "123456789012");
		Assert.ThrowsException<ArgumentException>(() => Ruler.Segment = "1234567890123");
		Assert.ThrowsException<ArgumentException>(() => Ruler.Segment = "12345678901234");
		Assert.ThrowsException<ArgumentException>(() => Ruler.Segment = "123456789012345");

		string nullSeg = null!;
		Assert.ThrowsException<ArgumentNullException>(() => Ruler.Get(20, nullSeg));
		Assert.ThrowsException<ArgumentException>(() => Ruler.Get(20, ""));
		Assert.ThrowsException<ArgumentException>(() => Ruler.Get(20, "1"));
		Assert.ThrowsException<ArgumentException>(() => Ruler.Get(20, "12"));
		Assert.ThrowsException<ArgumentException>(() => Ruler.Get(20, "123"));
		Assert.ThrowsException<ArgumentException>(() => Ruler.Get(20, "1234"));
		Assert.ThrowsException<ArgumentException>(() => Ruler.Get(20, "12345"));
		Assert.ThrowsException<ArgumentException>(() => Ruler.Get(20, "123456"));
		Assert.ThrowsException<ArgumentException>(() => Ruler.Get(20, "1234567"));
		Assert.ThrowsException<ArgumentException>(() => Ruler.Get(20, "12345678"));
		Assert.ThrowsException<ArgumentException>(() => Ruler.Get(20, "1234567890"));
		Assert.ThrowsException<ArgumentException>(() => Ruler.Get(20, "12345678901"));
		Assert.ThrowsException<ArgumentException>(() => Ruler.Get(20, "123456789012"));
		Assert.ThrowsException<ArgumentException>(() => Ruler.Get(20, "1234567890123"));
		Assert.ThrowsException<ArgumentException>(() => Ruler.Get(20, "12345678901234"));
		Assert.ThrowsException<ArgumentException>(() => Ruler.Get(20, "123456789012345"));
		CheckGet(1, "123456789", "1");
		CheckGet(2, "123456789", "12");
		CheckGet(3, "123456789", "123");
		CheckGet(4, "123456789", "1234");
		CheckGet(5, "123456789", "12345");
		CheckGet(6, "123456789", "123456");
		CheckGet(7, "123456789", "1234567");
		CheckGet(8, "123456789", "12345678");
		CheckGet(9, "123456789", "123456789");
		CheckGet(10, "123456789", "1234567891");
		CheckGet(11, "123456789", "12345678911");
		CheckGet(12, "123456789", "123456789112");
		CheckGet(13, "123456789", "1234567891123");
		CheckGet(14, "123456789", "12345678911234");
		CheckGet(15, "123456789", "123456789112345");
		CheckGet(16, "123456789", "1234567891123456");
		CheckGet(17, "123456789", "12345678911234567");
		CheckGet(18, "123456789", "123456789112345678");
		CheckGet(19, "123456789", "1234567891123456789");
		CheckGet(20, "123456789", "12345678911234567892");

		Assert.ThrowsException<ArgumentNullException>(() => Ruler.Get(20, null, null!));
		Assert.ThrowsException<ArgumentNullException>(() => Ruler.Get(20, null, nullSeg));
		Assert.ThrowsException<ArgumentException>(() => Ruler.Get(20, null, ""));
		Assert.ThrowsException<ArgumentException>(() => Ruler.Get(20, null, "1"));
		Assert.ThrowsException<ArgumentException>(() => Ruler.Get(20, null, "12"));
		Assert.ThrowsException<ArgumentException>(() => Ruler.Get(20, null, "123"));
		Assert.ThrowsException<ArgumentException>(() => Ruler.Get(20, null, "1234"));
		Assert.ThrowsException<ArgumentException>(() => Ruler.Get(20, null, "12345"));
		Assert.ThrowsException<ArgumentException>(() => Ruler.Get(20, null, "123456"));
		Assert.ThrowsException<ArgumentException>(() => Ruler.Get(20, null, "1234567"));
		Assert.ThrowsException<ArgumentException>(() => Ruler.Get(20, null, "12345678"));
		Assert.ThrowsException<ArgumentException>(() => Ruler.Get(20, null, "1234567890"));
		Assert.ThrowsException<ArgumentException>(() => Ruler.Get(20, null, "12345678901"));
		Assert.ThrowsException<ArgumentException>(() => Ruler.Get(20, null, "123456789012"));
		Assert.ThrowsException<ArgumentException>(() => Ruler.Get(20, null, "1234567890123"));
		Assert.ThrowsException<ArgumentException>(() => Ruler.Get(20, null, "12345678901234"));
		Assert.ThrowsException<ArgumentException>(() => Ruler.Get(20, null, "123456789012345"));
		CheckGet(1, null, "123456789", "1");
		CheckGet(2, null, "123456789", "12");
		CheckGet(3, null, "123456789", "123");
		CheckGet(4, null, "123456789", "1234");
		CheckGet(5, null, "123456789", "12345");
		CheckGet(6, null, "123456789", "123456");
		CheckGet(7, null, "123456789", "1234567");
		CheckGet(8, null, "123456789", "12345678");
		CheckGet(9, null, "123456789", "123456789");
		CheckGet(10, null, "123456789", "1234567891");
		CheckGet(11, null, "123456789", "12345678911");
		CheckGet(12, null, "123456789", "123456789112");
		CheckGet(13, null, "123456789", "1234567891123");
		CheckGet(14, null, "123456789", "12345678911234");
		CheckGet(15, null, "123456789", "123456789112345");
		CheckGet(16, null, "123456789", "1234567891123456");
		CheckGet(17, null, "123456789", "12345678911234567");
		CheckGet(18, null, "123456789", "123456789112345678");
		CheckGet(19, null, "123456789", "1234567891123456789");
		CheckGet(20, null, "123456789", "12345678911234567892");

		//restore colors
		Ruler.Colors = savedColors;
	}

	[TestMethod]
	public void ColorTests()
	{
		var savedColors = Ruler.Colors;

		//default colors
		var colors = ColorUtilities.GetGradientColors(Color.Gray, Color.White, 10).ToList();
		Assert.IsNotNull(colors);
		Assert.IsNotNull(Ruler.Colors);
		Assert.AreEqual(colors.Count, Ruler.Colors.Count);
		for (int i = 0; i < colors.Count; i++)
		{ Assert.AreEqual(colors[i], Ruler.Colors[i]); }
		for (int i = 1; i < 120; i++)
		{ Assert.AreEqual(Ruler.Get(i, null, Ruler.Segment).Colorize(Ruler.Colors), Ruler.Get(i)); }

		Ruler.Colors = null;
		Assert.IsNull(Ruler.Colors);
		//other tests already cover no colors more extensively...
		CheckGet(1, "-");
		CheckGet(2, "--");
		CheckGet(3, "---");
		Ruler.Colors = new List<Color>();
		CheckGet(1, "-");
		CheckGet(2, "--");
		CheckGet(3, "---");

		Ruler.Colors = new List<Color>(new[] { Expectations.red, Expectations.white });
		Assert.AreEqual(2, Ruler.Colors.Count);
		Assert.AreEqual(Expectations.red, Ruler.Colors[0]);
		Assert.AreEqual(Expectations.white, Ruler.Colors[1]);
		//equivalent
		Assert.AreEqual($"{Expectations.ForeColorRed}-{Expectations.ForeColorReset}{Expectations.ForeColorWhite}-{Expectations.ForeColorReset}", Ruler.Get(2));
		Assert.AreEqual(Ruler.Get(2, null, Ruler.Segment).Colorize(Ruler.Colors), Ruler.Get(2));

		for (int i = 1; i < 120; i++)
		{ Assert.AreEqual(Ruler.Get(i, null, Ruler.Segment).Colorize(Ruler.Colors), Ruler.Get(i)); }

		Ruler.Colors = new List<Color>(new[] { Expectations.red, Expectations.white, Expectations.green, Expectations.blue, Expectations.black });
		Assert.AreEqual(5, Ruler.Colors.Count);
		Assert.AreEqual(Expectations.red, Ruler.Colors[0]);
		Assert.AreEqual(Expectations.white, Ruler.Colors[1]);
		Assert.AreEqual(Expectations.green, Ruler.Colors[2]);
		Assert.AreEqual(Expectations.blue, Ruler.Colors[3]);
		Assert.AreEqual(Expectations.black, Ruler.Colors[4]);
		for (int i = 1; i < 120; i++)
		{ Assert.AreEqual(Ruler.Get(i, null, Ruler.Segment).Colorize(Ruler.Colors), Ruler.Get(i)); }

		//restore colors
		Ruler.Colors = savedColors;
	}


	private void CheckGet(int width, string expectedResult)
	{
		string r = Ruler.Get(width);
		Assert.IsNotNull(r);
		Assert.AreEqual(width, r.Length);
		Assert.AreEqual(expectedResult, r);
	}

	private void CheckGet(int width, string segment, string expectedResult)
	{
		string r = Ruler.Get(width, segment);
		Assert.IsNotNull(r);
		Assert.AreEqual(width, r.Length);
		Assert.AreEqual(expectedResult, r);
	}

	private void CheckGet(int width, IEnumerable<Color>? colors, string segment, string expectedResult)
	{
		string r = Ruler.Get(width, colors, segment);
		Assert.IsNotNull(r);
		Assert.AreEqual(width, r.Length);
		Assert.AreEqual(expectedResult, r);
	}
}
