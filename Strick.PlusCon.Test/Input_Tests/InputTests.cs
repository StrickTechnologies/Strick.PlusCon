using System.Drawing;

using Strick.PlusCon.Models;

using static Strick.PlusCon.Helpers;


namespace Strick.PlusCon.Test.Input_Tests;


[TestClass]
public class InputTests
{
	internal static Menu Menu()
	{
		Menu YNMenu = new Menu("Yes/No Input Tests");
		YNMenu.Add(new MenuOption("YN", 'Y', InputTests_YN));
		YNMenu.Add(new MenuOption("Select Yes/No", 'S', InputTests_SelectYesNo));

		Menu itMenu = new Menu("Input Tests");
		itMenu.Add(new MenuOption("Any", 'A', InputTests_Any));
		itMenu.Add(new MenuOption("Ch", 'C', InputTests_Ch));
		itMenu.Add(new MenuOption("Number", 'N', InputTests_Number));
		itMenu.Add(new MenuOption("Text", 'X', InputTestsText));
		itMenu.Add(new MenuOption("Date", 'D', InputTests_Date));
		itMenu.Add(new MenuOption("Time", 'T', InputTests_Time));
		itMenu.Add(new MenuOption("Select", 'S', InputTests_Select));
		itMenu.Add(new MenuOption("Yes/No Menu", 'Y', YNMenu));

		return itMenu;
	}


	internal static void InputTests_Any()
	{
		WL();
		Input.Any();
		WL();
		Input.Any(null);
		WL();
		Input.Any("");

		W("\r\nPress anything ");
		Input.Any("");
	}

	internal static void InputTests_Ch()
	{
		WL();
		var c = Input.Ch("Press any key ");
		WL();
		Input.Any($"You pressed '{c.KeyChar}'");

		InputArgumentsCh args = new("Press any of these ('a', 'b', 'c') ", ['a', 'b', 'c'])
		{
			PromptStyle = new(Color.DodgerBlue)
		};
		WL();
		c = Input.Ch(args);
		WL();
		Input.Any($"You pressed '{c.KeyChar}'");
	}


	internal static void InputTests_Number()
	{
		//CLS();
		//InputTests_Select();
		//RK();
		//return;

		//InputTests_Time();
		//InputTestsText();
		//RK();
		//return;

		byte? b = Input.Number<byte>("byte ");
		if (b == null)
		{ WL("null"); }
		else
		{ WL(b.Value.ToString()); }

		//byte? b6 = Input.Number<byte>("(6) byte ");
		//if (b6 == null)
		//{ WL("null"); }
		//else
		//{ WL(b6.Value.ToString()); }

		sbyte? sb = Input.Number<sbyte>("s-byte ");
		if (sb == null)
		{ WL("null"); }
		else
		{ WL(sb.Value.ToString()); }

		//sbyte? sb6 = Input.Number6<sbyte>("(6) s-byte ");
		//if (sb6 == null)
		//{ WL("null"); }
		//else
		//{ WL(sb6.Value.ToString()); }

		char? c = Input.Number<char>("char ");
		if (c == null)
		{ WL("null"); }
		else
		{ WL(c.Value.ToString()); }

		int? i = Input.Number<int>("int ");
		if (i == null)
		{ WL("null"); }
		else
		{ WL(i.Value.ToString()); }

		InputArgumentsNumber<decimal> argsDec = new("dec ")
		{
			PromptStyle = new TextStyle(Color.DodgerBlue)
		};
		decimal? d = Input.Number<decimal>(argsDec);
		if (d == null)
		{ WL("null"); }
		else
		{ WL(d.Value.ToString()); }

		RK();
	}

	internal static void InputTestsText()
	{
		InputArgumentsText args = new InputArgumentsText() { Prompt = "text ", MinLength = 3, MaxLength = 6 };
		string? txt = Input.Text(args);
		if (txt == null)
		{ Input.Any("null"); }
		else
		{ Input.Any(txt); }
	}


	internal static void InputTests_Date()
	{
		InputArgumentsDate args = new InputArgumentsDate("Enter a date: ");
		args.Min = new DateOnly(2000, 1, 1);
		args.Max = DateOnly.FromDateTime(DateTime.Today);
		args.ParseFunction = ParseDateSample;

		WL();
		var dt = Input.Date(args);
		if (dt == null)
		{ Input.Any("null"); }
		else
		{ Input.Any(dt.Value.ToShortDateString()); }
	}

	internal static bool ParseDateSample(string date, out DateOnly result)
	{
		//Today
		if (date.Equals("t", StringComparison.OrdinalIgnoreCase))
		{
			result = DateOnly.FromDateTime(DateTime.Today);
			return true;
		}

		//One month from today
		if (date.Equals("1m", StringComparison.OrdinalIgnoreCase))
		{
			result = DateOnly.FromDateTime(DateTime.Today.AddMonths(1));
			return true;
		}

		//One month ago
		if (date.Equals("-1m", StringComparison.OrdinalIgnoreCase))
		{
			result = DateOnly.FromDateTime(DateTime.Today.AddMonths(-1));
			return true;
		}

		if (DateOnly.TryParse(date, out DateOnly dt))
		{
			result = dt;
			return true;
		}

		result = default;
		return false;
	}


	internal static void InputTests_Time()
	{
		InputArgumentsTime args = new InputArgumentsTime("Enter a time: ");
		args.Min = new TimeOnly(10, 0);
		args.Max = new TimeOnly(13, 45);
		args.ParseFunction = ParseTimeSample;

		WL();
		var t = Input.Time(args);
		if (t == null)
		{ Input.Any("null"); }
		else
		{ Input.Any(t.Value.ToLongTimeString()); }
	}

	internal static bool ParseTimeSample(string time, out TimeOnly result)
	{
		//Now
		if (time.Equals("n", StringComparison.OrdinalIgnoreCase))
		{
			result = TimeOnly.FromDateTime(DateTime.Now);
			return true;
		}

		TimeOnly tm;

		//assume it's a 1- or 2-digit hour value (e.g. "1" or "10" for 1:00am or 10:00am, "13" for 1:00pm, etc.)
		if (time.Length <= 2 && TimeOnly.TryParse(time + ":00", out tm))
		{
			result = tm;
			return true;
		}

		if (TimeOnly.TryParse(time, out tm))
		{
			result = tm;
			return true;
		}

		result = default;
		return false;
	}


	internal static void InputTests_Select()
	{
		WL();

		List<string> choices = ["foo", "bar", "baz", "foo bar"];
		var args = new InputArgumentsSelect<string>("select one ", choices, "bar") { SelectionOptionStyle = new(Color.White, Color.Red) };
		string? sel = Input.Select(args);
		WL();
		if (sel != null)
		{ WL($"selected {sel}"); }
		else
		{ WL("espace"); }

		List<int?> choices2 = [1, 2, 3];
		var args2 = new InputArgumentsSelect<int?>("select one ", choices2, 2) { Wrap = false };
		int? sel2 = Input.Select(args2);
		WL();
		if (sel2 != null)
		{ WL($"selected {sel2}"); }
		else
		{ WL("espace"); }

		RK();
	}


	internal static void InputTests_YN()
	{
		WL();
		var yn = Input.YN("Yes or No? ");
		WL();
		Input.Any(yn.YesNo());
	}

	internal static void InputTests_SelectYesNo()
	{
		WL();
		var yn = Input.SelectYesNo("Yes or No: ");
		WL();
		Input.Any(YNEsc(yn));

		WL();
		yn = Input.SelectYesNo("Yes or No 2: ", yn);
		WL();
		Input.Any(YNEsc(yn));

		WL();
		yn = Input.SelectYesNo("Yes or No 3: ", "no");
		WL();
		Input.Any(YNEsc(yn));
	}

	private static string YNEsc(bool? value) => value.HasValue ? value.YesNo() : "escape";


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
}
