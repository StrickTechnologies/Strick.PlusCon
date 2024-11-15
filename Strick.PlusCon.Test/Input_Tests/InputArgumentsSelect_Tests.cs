using Strick.PlusCon.Models;


namespace Strick.PlusCon.Test.Input_Tests;


[TestClass]
public class InputArgumentsSelect_Tests
{
	[TestMethod]
	public void Int()
	{
		Assert.ThrowsException<ArgumentNullException>(() => new InputArgumentsSelect<int?>(null!));
		Assert.ThrowsException<ArgumentException>(() => new InputArgumentsSelect<int?>([22]));

		InputArgumentsSelect<int?> args;

		args = new([22, 33, 44, 55]);
		CheckArgs(args, null, [22, 33, 44, 55], null);

		args = new("doubles", [22, 33, 44, 55]);
		CheckArgs(args, "doubles", [22, 33, 44, 55], null);
		args = new("doubles", [22, 33, 44, 55], 33);
		CheckArgs(args, "doubles", [22, 33, 44, 55], 33);

		args = new([22, 33, 44, 55], 44);
		CheckArgs(args, null, [22, 33, 44, 55], 44);

		args = new([22, 33, 44, 55]) { Wrap = false };
		CheckArgs(args, null, [22, 33, 44, 55], null);
	}

	[TestMethod]
	public void String()
	{
		Assert.ThrowsException<ArgumentNullException>(() => new InputArgumentsSelect<string>(null!));
		Assert.ThrowsException<ArgumentException>(() => new InputArgumentsSelect<string>(["foo"]));

		InputArgumentsSelect<string?> args;

		args = new(["down", "on", "the", "beach"]);
		CheckArgs(args, null, ["down", "on", "the", "beach"], null);

		args = new("chillin'", ["down", "on", "the", "beach"]);
		CheckArgs(args, "chillin'", ["down", "on", "the", "beach"], null);
		args = new("chillin'", ["down", "on", "the", "beach"], "on");
		CheckArgs(args, "chillin'", ["down", "on", "the", "beach"], "on");

		args = new(["down", "on", "the", "beach"]);
		CheckArgs(args, null, ["down", "on", "the", "beach"], "down");

		args = new(["down", "on", "the", "beach"], "the");
		CheckArgs(args, null, ["down", "on", "the", "beach"], "the");

		args = new(["down", "on", "the", "beach"]) { Wrap = false };
		CheckArgs(args, null, ["down", "on", "the", "beach"], "down");
	}

	private void CheckArgs<T>(InputArgumentsSelect<T> args, string? expectedPrompt, IEnumerable<T> expectedOptions, T expectedSelected)
	{
		Assert.IsNotNull(args);
		Assert.AreEqual(expectedPrompt, args.Prompt.Text);

		Assert.IsNotNull(args.Options);
		Assert.IsNotNull(expectedOptions);
		Assert.AreEqual(expectedOptions.Count(), args.Options.Count);
		Assert.IsTrue(args.Options.SequenceEqual(expectedOptions));

		if (expectedSelected != null)
		{ Assert.AreEqual(expectedSelected, args.SelectedOption); }
		else
		{ Assert.AreEqual(args.Options[0], args.SelectedOption); }

		args.SelectedOption = args.Options[0];
		for (int i = 0; i < args.Options.Count; i++)
		{
			Assert.AreEqual(args.Options[i], args.SelectedOption);
			args.SelectNext();
		}
		if (args.Wrap)
		{ Assert.AreEqual(args.Options[0], args.SelectedOption); }
		else
		{ Assert.AreEqual(args.Options[^1], args.SelectedOption); }

		args.SelectedOption = args.Options[^1];
		for (int i = args.Options.Count - 1; i >= 0; i--)
		{
			Assert.AreEqual(args.Options[i], args.SelectedOption);
			args.SelectPrevious();
		}
		if (args.Wrap)
		{ Assert.AreEqual(args.Options[^1], args.SelectedOption); }
		else
		{ Assert.AreEqual(args.Options[0], args.SelectedOption); }
	}
}
