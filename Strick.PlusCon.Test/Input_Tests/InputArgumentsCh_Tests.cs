using Strick.PlusCon.Models;


namespace Strick.PlusCon.Test.Input_Tests;


[TestClass]
public class InputArgumentsCh_Tests
{
	[TestMethod]
	public void x()
	{
		InputArgumentsCh args = new InputArgumentsCh();
		CheckArgs(args, null);

		args = new InputArgumentsCh("foo");
		CheckArgs(args, "foo");
		args.Prompt = "bar";
		CheckArgs(args, "bar");
		args.Prompt = null;
		CheckArgs(args, null);

		args = new InputArgumentsCh(['a', 'b', 'c']);
		CheckArgs(args, null, ['a', 'b', 'c']);

		args = new InputArgumentsCh("foo", ['a', 'b', 'c']);
		CheckArgs(args, "foo", ['a', 'b', 'c']);
	}

	private void CheckArgs(InputArgumentsCh args, string? expectedPrompt, IEnumerable<char>? allowed = null)
	{
		Assert.IsNotNull(args);
		Assert.AreEqual(expectedPrompt, args.Prompt);
		Assert.IsNotNull(args.Allowed);

		if (allowed.HasAny())
		{ CheckAllowed(args, allowed); }
		else
		{ CheckAllowed(args); }
	}

	private void CheckAllowed(InputArgumentsCh args)
	{
		Assert.AreEqual(0, args.Allowed.Count);

		for (int c = 32; c < 128; c++)
		{ Assert.IsTrue(args.Validate((char)c)); }
	}

	private void CheckAllowed(InputArgumentsCh args, IEnumerable<char> allowed)
	{
		Assert.IsTrue(allowed.HasAny(), $"'{nameof(allowed)}' must have at least one value");
		Assert.AreEqual(allowed.Count(), args.Allowed.Count);

		for (int c = 32; c < 128; c++)
		{ Assert.AreEqual(allowed.Contains((char)c), args.Validate((char)c), $"char: {(char)c}"); }
	}
}
