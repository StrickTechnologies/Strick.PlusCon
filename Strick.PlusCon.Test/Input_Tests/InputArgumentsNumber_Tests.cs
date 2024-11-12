using System.Numerics;

using Strick.PlusCon.Models;


namespace Strick.PlusCon.Test.Input_Tests;


[TestClass]
public class InputArgumentsNumber_Tests
{
	[TestMethod]
	public void Byte()
	{
		InputArgumentsNumber<byte> args;

		args = new();
		CheckArgs(args, null, null, null, byte.MinValue, byte.MaxValue);
		args = new("foo");
		CheckArgs(args, "foo", null, null, byte.MinValue, byte.MaxValue);
		args.Prompt = "bar";
		CheckArgs(args, "bar", null, null, byte.MinValue, byte.MaxValue);
		args.Prompt = null;
		CheckArgs(args, null, null, null, byte.MinValue, byte.MaxValue);

		args = new(20, 50);
		CheckArgs(args, null, 20, 50, byte.MinValue, byte.MaxValue);
		args = new("foo", 20, 50);
		CheckArgs(args, "foo", 20, 50, byte.MinValue, byte.MaxValue);


		InputArgumentsNumber<sbyte> argsS;

		argsS = new();
		CheckArgs(argsS, null, null, null, sbyte.MinValue, sbyte.MaxValue);
		argsS = new("foo");
		CheckArgs(argsS, "foo", null, null, sbyte.MinValue, sbyte.MaxValue);

		argsS = new(-20, 30);
		CheckArgs(argsS, null, -20, 30, sbyte.MinValue, sbyte.MaxValue);
		argsS = new("foo", -20, 30);
		CheckArgs(argsS, "foo", -20, 30, sbyte.MinValue, sbyte.MaxValue);
	}

	[TestMethod]
	public void Short()
	{
		InputArgumentsNumber<short> args;

		args = new();
		CheckArgs(args, null, null, null, short.MinValue, short.MaxValue);
		args = new("foo");
		CheckArgs(args, "foo", null, null, short.MinValue, short.MaxValue);

		args = new(20, 50);
		CheckArgs(args, null, 20, 50, short.MinValue, short.MaxValue);
		args = new("foo", 20, 50);
		CheckArgs(args, "foo", 20, 50, short.MinValue, short.MaxValue);


		InputArgumentsNumber<ushort> argsU;

		argsU = new();
		CheckArgs(argsU, null, null, null, ushort.MinValue, ushort.MaxValue);
		argsU = new("foo");
		CheckArgs(argsU, "foo", null, null, ushort.MinValue, ushort.MaxValue);

		argsU = new(20, 50);
		CheckArgs(argsU, null, 20, 50, ushort.MinValue, ushort.MaxValue);
		argsU = new("foo", 20, 50);
		CheckArgs(argsU, "foo", 20, 50, ushort.MinValue, ushort.MaxValue);
	}

	[TestMethod]
	public void Int()
	{
		InputArgumentsNumber<int> args;

		args = new();
		CheckArgs(args, null, null, null, short.MinValue, short.MaxValue);
		args = new("foo");
		CheckArgs(args, "foo", null, null, short.MinValue, short.MaxValue);

		args = new(20, 50);
		CheckArgs(args, null, 20, 50, short.MinValue, short.MaxValue);
		args = new("foo", 20, 50);
		CheckArgs(args, "foo", 20, 50, short.MinValue, short.MaxValue);


		InputArgumentsNumber<uint> argsU;

		argsU = new();
		CheckArgs(argsU, null, null, null, ushort.MinValue, ushort.MaxValue);
		argsU = new("foo");
		CheckArgs(argsU, "foo", null, null, ushort.MinValue, ushort.MaxValue);

		argsU = new(20, 50);
		CheckArgs(argsU, null, 20, 50, ushort.MinValue, ushort.MaxValue);
		argsU = new("foo", 20, 50);
		CheckArgs(argsU, "foo", 20, 50, ushort.MinValue, ushort.MaxValue);
	}

	[TestMethod]
	public void Long()
	{
		InputArgumentsNumber<long> args;

		args = new();
		CheckArgs(args, null, null, null, short.MinValue, short.MaxValue);
		args = new("foo");
		CheckArgs(args, "foo", null, null, short.MinValue, short.MaxValue);

		args = new(20, 50);
		CheckArgs(args, null, 20, 50, short.MinValue, short.MaxValue);
		args = new("foo", 20, 50);
		CheckArgs(args, "foo", 20, 50, short.MinValue, short.MaxValue);


		InputArgumentsNumber<ulong> argsU;

		argsU = new();
		CheckArgs(argsU, null, null, null, ushort.MinValue, ushort.MaxValue);
		argsU = new("foo");
		CheckArgs(argsU, "foo", null, null, ushort.MinValue, ushort.MaxValue);

		argsU = new(20, 50);
		CheckArgs(argsU, null, 20, 50, ushort.MinValue, ushort.MaxValue);
		argsU = new("foo", 20, 50);
		CheckArgs(argsU, "foo", 20, 50, ushort.MinValue, ushort.MaxValue);
	}

	[TestMethod]
	public void Decimal()
	{
		InputArgumentsNumber<decimal> args;

		args = new();
		CheckArgs(args, null, null, null, short.MinValue, short.MaxValue);
		args = new("foo");
		CheckArgs(args, "foo", null, null, short.MinValue, short.MaxValue);

		args = new(20M, 50M);
		CheckArgs(args, null, 20M, 50M, short.MinValue, short.MaxValue);
		args = new("foo", 20M, 50M);
		CheckArgs(args, "foo", 20M, 50M, short.MinValue, short.MaxValue);
	}


	private void CheckArgs<T>(InputArgumentsNumber<T> args, string? expectedPrompt, T? expectedMin, T? expectedMax, T min, T max) where T : struct, INumber<T>, IComparable<T>
	{
		Assert.IsNotNull(args);
		Assert.AreEqual(expectedPrompt, args.Prompt);
		Assert.AreEqual(expectedMin, args.Min);
		Assert.AreEqual(expectedMax, args.Max);

		Range<T> r = new Range<T>(args.Min, args.Max);
		T i = min;
		do
		{
			Assert.AreEqual(r.InRange(i), args.Validate(i));
			if (i == max)
			{ break; }
			i++;
		}
		while (true);
	}
}
