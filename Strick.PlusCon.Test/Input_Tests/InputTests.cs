using System.Drawing;

using Strick.PlusCon.Models;
using Strick.PlusCon.Test.Models;

using static Strick.PlusCon.Helpers;


namespace Strick.PlusCon.Test.Input_Tests;


[TestClass]
public class InputTests
{
	[TestMethod]
	public void SomeStuff()
	{
		DateOnly result;
		//Assert.IsTrue(DateOnly.TryParse("10", out result));
		Assert.IsTrue(DateOnly.TryParse("10.20", out result));
		Assert.AreEqual(new DateOnly(DateTime.Today.Year, 10, 20), result);
		Assert.IsTrue(DateOnly.TryParse("10-21", out result));
		Assert.AreEqual(new DateOnly(DateTime.Today.Year, 10, 21), result);
		Assert.IsTrue(DateOnly.TryParse("10/22", out result));
		Assert.AreEqual(new DateOnly(DateTime.Today.Year, 10, 22), result);
		Assert.IsTrue(DateOnly.TryParse("10 23", out result));
		Assert.AreEqual(new DateOnly(DateTime.Today.Year, 10, 23), result);
	}

	//[TestMethod]
	//does not work.
	public void Ch_Tests()
	{
		var originalIn = Console.In;

		using (StringReader sr = new("x"))
		{
			Console.SetIn(sr);
			ConsoleKeyInfo result = Input.Ch("");
			Assert.AreEqual('x', result.KeyChar);
		}

		//using (StringReader sr = new("asdfy"))
		//{
		//	Console.SetIn(sr);
		//	ConsoleKeyInfo result = Input.Ch(new InputArgumentsCh(['x','y','z']));
		//	Assert.AreEqual('y', result.KeyChar);
		//}

		Console.SetIn(originalIn);
	}

	[TestMethod]
	public void Number_Tests()
	{
		var originalIn = Console.In;

		using (StringReader sr = new("123"))
		{
			Console.SetIn(sr);
			int? result = Input.Number<int>("");
			Assert.AreEqual(123, result);
		}

		using (StringReader sr = new("123\r6\r4"))
		{
			Console.SetIn(sr);
			int? result = Input.Number(new InputArgumentsNumber<int>(1, 5));
			Assert.AreEqual(4, result);
		}

		Console.SetIn(originalIn);
	}

	[TestMethod]
	public void Text_Tests()
	{
		var originalIn = Console.In;

		using (StringReader sr = new("foo bar"))
		{
			Console.SetIn(sr);
			string? result = Input.Text("");
			Assert.AreEqual("foo bar", result);
		}

		using (StringReader sr = new("foo bar\rfoo"))
		{
			Console.SetIn(sr);
			string? result = Input.Text(new InputArgumentsText(3, 3));
			Assert.AreEqual("foo", result);
		}

		Console.SetIn(originalIn);
	}
}
