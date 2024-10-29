using System.Drawing;

using Strick.PlusCon.Models;

using static Strick.PlusCon.Helpers;


namespace Strick.PlusCon.Test.Input_Tests;


public class InputTests
{
	internal static Menu InputTestsMenu()
	{
		Menu itMenu = new Menu("Input Tests");
		itMenu.Add(new MenuOption("Any", 'A', InputTests_Any));
		itMenu.Add(new MenuOption("Select", 'S', InputTests_Select));
		itMenu.Add(new MenuOption("Number", 'N', InputTests_Number));
		itMenu.Add(new MenuOption("Text", 'X', InputTestsText));
		itMenu.Add(new MenuOption("Date", 'D', InputTests_Date));
		itMenu.Add(new MenuOption("Time", 'T', InputTests_Time));

		return itMenu;
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

		byte? b6 = Input.Number<byte>("(6) byte ");
		if (b6 == null)
		{ WL("null"); }
		else
		{ WL(b6.Value.ToString()); }

		sbyte? sb = Input.Number<sbyte>("s-byte ");
		if (sb == null)
		{ WL("null"); }
		else
		{ WL(sb.Value.ToString()); }

		sbyte? sb6 = Input.Number6<sbyte>("(6) s-byte ");
		if (sb6 == null)
		{ WL("null"); }
		else
		{ WL(sb6.Value.ToString()); }

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

		decimal? d = Input.Number<decimal>("dec ");
		if (d == null)
		{ WL("null"); }
		else
		{ WL(d.Value.ToString()); }

		RK();
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

	internal static void InputTestsText()
	{
		InputTextArguments args = new InputTextArguments() { Prompt = "text ", MinLength = 3, MaxLength = 6 };
		string? txt = Input.Text(args);
		if (txt == null)
		{ Input.Any("null"); }
		else
		{ Input.Any(txt); }
	}

	internal static void InputTests_Date()
	{
		InputDateArguments args = new InputDateArguments("date ");
		args.Min = new DateOnly(2000,1,1);
		args.Max = DateOnly.FromDateTime(DateTime.Today);

		var dt = Input.Date(args);
		if (dt == null)
		{ Input.Any("null"); }
		else
		{ Input.Any(dt.Value.ToShortDateString()); }
	}

	internal static void InputTests_Time()
	{
		InputTimeArguments args = new InputTimeArguments("time ");
		args.Min = new TimeOnly(10, 0);
		args.Max = new TimeOnly(13, 45);

		var t = Input.Time(args);
		if (t == null)
		{ Input.Any("null"); }
		else
		{ Input.Any(t.Value.ToLongTimeString()); }
	}

	internal static void InputTests_Select()
	{
		WL();

		List<string> choices = new() { "foo", "bar", "baz", "foo bar" };
		var args = new InputSelectArguments<string>("select one ", choices, "bar") { SelectionOptionStyle = new(Color.White, Color.Red) };
		string? sel = Input.Select(args);
		WL();
		WL($"selected {sel}");

		List<int?> choices2 = new() { 1, 2, 3 };
		var args2 = new InputSelectArguments<int?>("select one ", choices2, 2) { Wrap = false };
		int? sel2 = Input.Select(args2);
		WL();
		WL($"selected {sel2}");

		var yn = Input.SelectYesNo("Yes or No: ");
		WL();
		WL(yn.HasValue ? yn.YesNo() : "escape");

		yn = Input.SelectYesNo("Yes or No 2: ", yn);
		WL();
		WL(yn.HasValue ? (yn.Value ? "Yes" : "No") : "escape");

		RK();
	}
}
