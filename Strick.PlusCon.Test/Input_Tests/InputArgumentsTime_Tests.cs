using Strick.PlusCon.Models;


namespace Strick.PlusCon.Test.Input_Tests;


[TestClass]
public class InputArgumentsTime_Tests
{
	[TestMethod]
	public void x()
	{
		InputArgumentsTime args;
		args = new InputArgumentsTime();
		CheckArgs(args, null, null, null);
		args = new InputArgumentsTime("foo");
		CheckArgs(args, "foo", null, null);
		args = new InputArgumentsTime("foo", new(9, 0, 0), new(17, 0, 0));
		CheckArgs(args, "foo", new(9, 0, 0), new(17, 0, 0));
		args = new InputArgumentsTime(new(9, 0, 0), new(17, 0, 0));
		CheckArgs(args, null, new(9, 0, 0), new(17, 0, 0));
	}

	private void CheckArgs(InputArgumentsTime args, string? expectedPrompt, TimeOnly? expectedMin, TimeOnly? expectedMax)
	{
		Assert.IsNotNull(args);
		Assert.AreEqual(expectedPrompt, args.Prompt.Text);
		Assert.AreEqual(expectedMin, args.Min);
		Assert.AreEqual(expectedMax, args.Max);

		Range<TimeOnly> r = new Range<TimeOnly>(args.Min, args.Max);
		for (int i = 0; i <= 1439; i++)
		{
			var to = TimeOnly.FromTimeSpan(new TimeSpan(0, i, 0));
			Assert.AreEqual(r.InRange(to), args.Validate(to));
		}
	}
}
