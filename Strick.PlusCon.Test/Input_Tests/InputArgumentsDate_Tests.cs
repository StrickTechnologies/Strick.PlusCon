using Strick.PlusCon.Models;


namespace Strick.PlusCon.Test.Input_Tests;


[TestClass]
public class InputArgumentsDate_Tests
{
	[TestMethod]
	public void x()
	{
		InputArgumentsDate args;
		DateOnly min = new(2024, 1, 1);
		DateOnly max = new(2030, 12, 31);

		args = new InputArgumentsDate();
		CheckArgs(args, null, null, null, min, max);
		args = new InputArgumentsDate("foo");
		CheckArgs(args, "foo", null, null, min, max);
		args = new InputArgumentsDate("foo", new(2024, 1, 1), new(2024, 6, 30));
		CheckArgs(args, "foo", new(2024, 1, 1), new(2024, 6, 30), min, max);

		args = new InputArgumentsDate(new(2024, 1, 1), new(2024, 6, 30));
		CheckArgs(args, null, new(2024, 1, 1), new(2024, 6, 30), min, max);
	}

	private void CheckArgs(InputArgumentsDate args, string? expectedPrompt, DateOnly? expectedMin, DateOnly? expectedMax, DateOnly min, DateOnly max)
	{
		Assert.IsNotNull(args);
		Assert.AreEqual(expectedPrompt, args.Prompt);
		Assert.AreEqual(expectedMin, args.Min);
		Assert.AreEqual(expectedMax, args.Max);

		Range<DateOnly> r = new Range<DateOnly>(args.Min, args.Max);
		for (int i = 0; i <= max.DayNumber - min.DayNumber; i++)
		{ Assert.AreEqual(r.InRange(min.AddDays(i)), args.Validate(min.AddDays(i))); }
	}
}
