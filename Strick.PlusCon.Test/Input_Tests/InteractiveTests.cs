using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static Strick.PlusCon.Helpers;
using Strick.PlusCon.Models;
using Strick.PlusCon.Test.Models;


namespace Strick.PlusCon.Test.Input_Tests;


internal class InteractiveTests
{
	internal static Menu Menu()
	{
		Menu YNMenu = new Menu("Yes/No Input Tests");
		YNMenu.Add(new MenuOption("YN", 'Y', InputTests_YN));
		YNMenu.Add(new MenuOption("Select Yes/No", 'S', InputTests_SelectYesNo));

		Menu SMenu = new Menu("Select");
		SMenu.Add(new MenuOption("Sunrise/Sunset", 'S', InputTests_SelectSun));
		SMenu.Add(new MenuOption("Int", 'I', InputTests_SelectInt));
		SMenu.Add(new MenuOption("Select Widget", 'W', InputTests_SelectWidget));

		Menu itMenu = new Menu("Input Tests");
		itMenu.Add(new MenuOption("Any", 'A', InputTests_Any));
		itMenu.Add(new MenuOption("Ch", 'C', InputTests_Ch));
		itMenu.Add(new MenuOption("Number", 'N', InputTests_Number));
		itMenu.Add(new MenuOption("Text", 'X', InputTestsText));
		itMenu.Add(new MenuOption("Date", 'D', InputTests_Date));
		itMenu.Add(new MenuOption("Time", 'T', InputTests_Time));
		itMenu.Add(new MenuOption("Select", 'S', SMenu));
		itMenu.Add(new MenuOption("Yes/No Menu", 'Y', YNMenu));

		return itMenu;
	}

	internal static void InputTests_Any()
	{
		TextStyle promptStyle = new TextStyle(Color.DodgerBlue);
		WL();
		Input.Any();
		WL();
		Input.Any(null, promptStyle);
		WL();
		Input.Any("", promptStyle);

		W("\r\nPress anything ", promptStyle);
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
		WL();

		InputArgumentsText args = new InputArgumentsText("Enter any text: ");
		string? txt = Input.Text(args);
		if (txt == null)
		{ Input.Any("null"); }
		else
		{ Input.Any(txt); }

		args = new InputArgumentsText("Enter text between 3 and 6 characters in length: ", 3, 6);
		txt = Input.Text(args);
		if (txt == null)
		{ Input.Any("null"); }
		else
		{ Input.Any(txt); }
	}


	internal static void InputTests_Date()
	{
		InputArgumentsDate args = new InputArgumentsDate("Enter a date (in this millennium up to today): ", new DateOnly(2000, 1, 1), DateOnly.FromDateTime(DateTime.Today));
		args.ParseFunction = ParseDateSample;

		WL();
		var dt = Input.Date(args);
		if (dt == null)
		{ Input.Any("null"); }
		else
		{ Input.Any(dt.Value.ToShortDateString()); }

		args = new InputArgumentsDate_Future("Enter any date in the future: ");
		args.ParseFunction = ParseDateSample;
		WL();
		dt = Input.Date(args);
		if (dt == null)
		{ Input.Any("null"); }
		else
		{ Input.Any(dt.Value.ToShortDateString()); }

		args = new InputArgumentsDate_Future_Weekend("Enter any Weekend date in the future: ");
		WL();
		dt = Input.Date(args);
		if (dt == null)
		{ Input.Any("null"); }
		else
		{ Input.Any(dt.Value.ToShortDateString() + $" {dt.Value.DayOfWeek.ToString()}"); }
	}

	internal class InputArgumentsDate_Future : InputArgumentsDate
	{
		public InputArgumentsDate_Future() : this(null) { }

		public InputArgumentsDate_Future(string? prompt) : base(prompt, getMin(), null) { }

		private static DateOnly getMin() => DateOnly.FromDateTime(DateTime.Today.AddDays(1));
	}

	internal class InputArgumentsDate_Future_Weekend : InputArgumentsDate_Future
	{
		public InputArgumentsDate_Future_Weekend() : this(null) { }

		public InputArgumentsDate_Future_Weekend(string? prompt) : base(prompt) { }


		internal override bool Validate(DateOnly value)
		{
			if (!base.Validate(value))
			{ return false; }

			return IsWeekend(value);
		}

		private bool IsWeekend(DateOnly value)
		{
			return value.DayOfWeek == DayOfWeek.Sunday || value.DayOfWeek == DayOfWeek.Saturday;
		}
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
		InputArgumentsTime args = new InputArgumentsTime("Enter a time: ", new TimeOnly(10, 0), new TimeOnly(13, 45));
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


	internal static void InputTests_SelectSun()
	{
		WL();

		List<string> choices = ["Sunrise", "Mid-day", "Sunset"];
		var args = new InputArgumentsSelect<string>("Which do you prefer? ", choices)
		{
			SelectionOptionStyle = new(Color.White, Color.DarkOrange),
			PromptStyle = new(Color.White)
		};

		string? sel = Input.Select(args);
		WL();
		if (sel != null)
		{ RK($"selected {sel}"); }
		else
		{ RK("escape"); }
	}

	internal static void InputTests_SelectInt()
	{
		WL();
		List<int?> choices = [1, 2, 3];
		var args = new InputArgumentsSelect<int?>("select one ", choices, 2) { Wrap = false };
		int? sel = Input.Select(args);
		WL();
		if (sel != null)
		{ RK($"selected {sel}"); }
		else
		{ RK("escape"); }
	}

	internal static void InputTests_SelectWidget()
	{
		WL();

		List<Widget> choices = WidgetRepository.AllWidgets().ToList();

		var args = new InputArgumentsSelect<Widget>("Choose a Widget: ", choices) { SelectionOptionStyle = new(Color.White, Color.Red) };
		Widget? sel = Input.Select(args);
		WL();
		if (sel != null)
		{ Input.Any($"selected {sel}"); }
		else
		{ Input.Any("escape"); }
	}


	internal static void InputTests_YN()
	{
		WL();
		var yn = Input.YN("Yes or No? ");
		WL();
		Input.Any(yn.YesNo());

		WL();
		yn = Input.YN("Again with style. Yes or No? ", new TextStyle(Color.DodgerBlue));
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

}
