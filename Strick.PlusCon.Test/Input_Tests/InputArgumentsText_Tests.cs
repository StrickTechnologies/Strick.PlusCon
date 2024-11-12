using Strick.PlusCon.Models;


namespace Strick.PlusCon.Test.Input_Tests;


[TestClass]
public class InputArgumentsText_Tests
{
	[TestMethod]
	public void x()
	{
		InputArgumentsText args;

		args = new InputArgumentsText();
		CheckArgs(args, null, null, null);
		args = new InputArgumentsText("foo");
		CheckArgs(args, "foo", null, null);
		args.Prompt = "bar";
		CheckArgs(args, "bar", null, null);
		args.Prompt = null;
		CheckArgs(args, null, null, null);


		args = new InputArgumentsText(null, null);
		CheckArgs(args, null, null, null);
		args = new InputArgumentsText(5, null);
		CheckArgs(args, null, 5, null);
		args = new InputArgumentsText(5, 10);
		CheckArgs(args, null, 5, 10);
		args = new InputArgumentsText(null, 10);
		CheckArgs(args, null, null, 10);

		args = new InputArgumentsText("foo", null, null);
		CheckArgs(args, "foo", null, null);
		args = new InputArgumentsText("foo", 5, null);
		CheckArgs(args, "foo", 5, null);
		args = new InputArgumentsText("foo", 5, 10);
		CheckArgs(args, "foo", 5, 10);
		args = new InputArgumentsText("foo", null, 10);
		CheckArgs(args, "foo", null, 10);
	}

	[TestMethod]
	public void CtorEx()
	{
		Assert.ThrowsException<ArgumentOutOfRangeException>(() => new InputArgumentsText(-1, null));
		Assert.ThrowsException<ArgumentOutOfRangeException>(() => new InputArgumentsText(null, -1));
		Assert.ThrowsException<ArgumentOutOfRangeException>(() => new InputArgumentsText(null, 0));
	}


	private void CheckArgs(InputArgumentsText args, string? expectedPrompt, int? expectedMinLength, int? expectedMaxLength)
	{
		Assert.IsNotNull(args);
		Assert.AreEqual(expectedPrompt, args.Prompt);
		CheckRange(args, expectedMinLength, expectedMaxLength);
	}

	private void CheckRange(InputArgumentsText args, int? expectedMinLength, int? expectedMaxLength)
	{
		Assert.AreEqual(expectedMinLength, args.MinLength);
		Assert.AreEqual(expectedMaxLength, args.MaxLength);

		Range<int> r=new Range<int>(args.MinLength, args.MaxLength);

		int min = 0;
		int max = 5;

		if (args.MinLength != null && args.MaxLength != null)
		{
			min = Math.Max(0, args.MinLength.Value - 5);
			max = args.MaxLength.Value + 5;
		}
		else if (args.MinLength != null)
		{
			min = Math.Max(0, args.MinLength.Value - 5);
			max = args.MinLength.Value + 5;
		}
		else if (args.MaxLength != null)
		{
			min = Math.Max(0, args.MaxLength!.Value - 5);
			max = args.MaxLength.Value + 5;
		}
		for (int i = min; i <= max; i++)
		{
			//Console.WriteLine(i + " " + i.WithinRange(args.MinLength, args.MaxLength).ToString());
			Assert.AreEqual(r.InRange(i), args.Validate(new string('a', i)), $"Range validation {i}");
		}
	}
}
