using System.Numerics;

using Strick.PlusCon.Models;


namespace Strick.PlusCon.Test;


[TestClass]
public class RangeTests
{
	[TestMethod]
	public void Numbers()
	{
		Range<int> i;
		List<RangeTestCase<int>> cases =
		[
			.. GetNumberCases(-5, 0, 1, false),
			.. GetNumberCases(1, 5, 1, true),
			.. GetNumberCases(6, 99, 1, false),
		];

		i = new(1, 5);
		CheckRange(i, 1, 5);
		CheckInRange(i, cases);

		i = new(5, 1);
		CheckRange(i, 1, 5);
		CheckInRange(i, cases);


		Range<decimal> d;
		List<RangeTestCase<decimal>> casesD =
		[
			.. GetNumberCases(-5M, 0, 1, false),
			.. GetNumberCases(1M, 5, 1, true),
			.. GetNumberCases(6M, 99, 1, false),
		];

		d = new Range<decimal>(1, 5);
		CheckRange(d, 1.0M, 5.0M);
		CheckInRange(d, casesD);

		casesD.Clear();
		casesD.AddRange(GetNumberCases(-5, 0.9M, 0.1M, false));
		casesD.AddRange(GetNumberCases(1, 3.1M, 0.1M, true));
		casesD.AddRange(GetNumberCases(3.2M, 10, 0.1M, false));

		d = new Range<decimal>(1, 3.1M);
		CheckRange(d, 1.0M, 3.1M);
		CheckInRange(d, casesD);
	}

	[TestMethod]
	public void DO()
	{
		Range<DateOnly> d;
		List<RangeTestCase<DateOnly>> cases =
		[
			.. GetDateCases(GetDO(-5), GetDO(0), 1, false),
			.. GetDateCases(GetDO(1), GetDO(5), 1, true),
			.. GetDateCases(GetDO(6), GetDO(99), 1, false),
		];

		d = new(GetDO(1), GetDO(5));
		CheckRange(d, GetDO(1), GetDO(5));
		CheckInRange(d, cases);

		DateOnly bom = ToDO(BOM(DateTime.Today));
		DateOnly eom = ToDO(EOM(DateTime.Today));
		cases =
		[
			.. GetDateCases(bom.AddDays(-5), bom.AddDays(-1), 1, false),
			.. GetDateCases(bom, eom, 1, true),
			.. GetDateCases(eom.AddDays(1), eom.AddDays(22), 1, false),
		];
		d = new(bom, eom);
		CheckRange(d, bom, eom);
		CheckInRange(d, cases);
	}

	[TestMethod]
	public void TO()
	{
		Range<TimeOnly> t;

		TimeOnly bod = new TimeOnly(0);
		TimeOnly eod = new TimeOnly(23, 59, 59);
		TimeOnly bob = new TimeOnly(9, 0);
		TimeOnly eob = new TimeOnly(17, 0);

		List<RangeTestCase<TimeOnly>> cases =
		[
			.. GetTimeCases(bod, eod, 1, true)
		];

		t = new();
		CheckRange(t, null, null);
		CheckInRange(t, cases);

		cases = 
		[
			.. GetTimeCases(bod, bob.AddHours(-1), 1, false),
			.. GetTimeCases(bob, eob, 1, true),
			.. GetTimeCases(eob.AddHours(1), eod, 1, false),
		];
		t = new(bob, eob);
		CheckRange(t, bob, eob);
		CheckInRange(t, cases);
	}


	private void CheckRange<T>(Range<T> rnge, T? expectedMin, T? expectedMax) where T : struct, IComparable<T>
	{
		Assert.IsNotNull(rnge);
		Assert.AreEqual(expectedMin, rnge.Min);
		Assert.AreEqual(expectedMax, rnge.Max);
	}

	private void CheckInRange<T>(Range<T> rnge, IEnumerable<RangeTestCase<T>> testCases) where T : struct, IComparable<T>
	{
		foreach (RangeTestCase<T> testCase in testCases)
		{ CheckInRange(rnge, testCase); }
	}

	private void CheckInRange<T>(Range<T> rnge, RangeTestCase<T> testCase) where T : struct, IComparable<T>
	{
		Assert.IsNotNull(rnge);
		Assert.IsNotNull(testCase);
		Assert.AreEqual(testCase.ExpectedPass, rnge.InRange(testCase.Value), $"Test Case {testCase.Value}");
	}


	private DateOnly GetDO(int days)
	{
		return DateOnly.FromDateTime(DateTime.Today.AddDays(days));
	}

	private DateTime BOM(DateTime date) => new DateTime(date.Year, date.Month, 1);

	private DateTime EOM(DateTime date) => BOM(date.AddMonths(1)).AddDays(-1).AddSeconds(86399);

	private DateOnly ToDO(DateTime date) => DateOnly.FromDateTime(date);


	private IEnumerable<RangeTestCase<T>> GetNumberCases<T>(T min, T max, T increment, bool expectedPass) where T : struct, IComparable<T>, INumber<T>
	{
		for (T i = min; i <= max; i += increment)
		{ yield return new RangeTestCase<T>(i, expectedPass); }
	}

	private IEnumerable<RangeTestCase<DateOnly>> GetDateCases(DateOnly min, DateOnly max, int increment, bool expectedPass)
	{
		DateTime d = min.ToDateTime(new TimeOnly(0));
		for (int i = 0; i <= max.DayNumber - min.DayNumber; i += increment)
		{ yield return new RangeTestCase<DateOnly>(DateOnly.FromDateTime(d.AddDays(i)), expectedPass); }
	}

	private IEnumerable<RangeTestCase<TimeOnly>> GetTimeCases(TimeOnly min, TimeOnly max, int increment, bool expectedPass)
	{
		int hours = (int)(max - min).TotalHours;
		for (int i = 0; i <= hours; i += increment)
		{ yield return new RangeTestCase<TimeOnly>(new TimeOnly(min.Hour + i, 0), expectedPass); }
	}


	internal class RangeTestCase<T> where T : struct, IComparable<T>
	{
		public RangeTestCase(T value, bool expectedPass)
		{
			Value = value;
			ExpectedPass = expectedPass;
		}


		public T Value { get; }

		public bool ExpectedPass { get; }
	}
}
