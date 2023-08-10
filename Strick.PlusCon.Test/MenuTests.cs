using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualBasic.FileIO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Strick.PlusCon.Models;


namespace Strick.PlusCon.Test;


[TestClass]
public class MenuTests
{
	private static readonly StyledText defaultPrompt = new StyledText("Select an option ");
	private static readonly List<MenuOption> defaultOptions = new List<MenuOption>();
	private static readonly List<char> defaultExitKeys = new List<char>(new[] { '0', ' ', (char)ConsoleKey.Escape, (char)ConsoleKey.Enter, (char)ConsoleKey.Backspace });

	[TestMethod]
	public void Constructors()
	{
		var m = new Menu();
		TestMenuState(m, null, null, defaultPrompt, defaultOptions, defaultExitKeys);

		m = new Menu("foo");
		TestMenuState(m, new StyledText("foo"), null, defaultPrompt, defaultOptions, defaultExitKeys);

		m = new Menu("foo", "bar");
		TestMenuState(m, new StyledText("foo"), new StyledText("bar"), defaultPrompt, defaultOptions, defaultExitKeys);

		var opt = new List<MenuOption>();
		var opt2 = new List<MenuOption>();
		opt.Add(new MenuOption("test", '1', () => { }));
		opt2.Add(new MenuOption("test", '1', () => { }));
		m = new Menu(opt);
		TestMenuState(m, null, null, defaultPrompt, opt2, defaultExitKeys);

		m = new Menu("foo", opt);
		TestMenuState(m, new StyledText("foo"), null, defaultPrompt, opt2, defaultExitKeys);

		m = new Menu("foo", "bar", opt);
		TestMenuState(m, new StyledText("foo"), new StyledText("bar"), defaultPrompt, opt2, defaultExitKeys);
	}

	[TestMethod]
	public void Keys()
	{
		var m = new Menu();
		TestMenuState(m, null, null, defaultPrompt, defaultOptions, defaultExitKeys);

		m.ExitKeys.Add('z');
		var keys = new List<char>(defaultExitKeys);
		var keys2 = new List<char>(defaultExitKeys);
		keys.Add('z');
		keys2.Add('z');
		TestMenuState(m, null, null, defaultPrompt, defaultOptions, keys2);
		m.ExitKeys.Clear();
		keys2.Clear();
		TestMenuState(m, null, null, defaultPrompt, defaultOptions, keys2);

		m.ExitKeys.Clear();
		keys2.Clear();
		m.ExitKeys.Add('a');
		keys2.Add('a');
		TestMenuState(m, null, null, defaultPrompt, defaultOptions, keys2);
	}

	[TestMethod]
	public void WidthTests()
	{
		Menu m = new("title", "subtitle");
		Assert.IsNotNull(m);

		MenuSeperator fbb = new MenuSeperator("foo bar baz");
		MenuOption f = new MenuOption("foo", 'f', () => { });
		MenuOption b = new MenuOption("bar", 'b', () => { });
		MenuOption z = new MenuBackOption("baz", 'z');
		m.Add(fbb);
		m.Add(f);
		m.Add(b);
		m.Add(z);
		Assert.IsNotNull(m.Options);
		Assert.AreEqual(4, m.Options.Count);

		Assert.AreEqual(defaultPrompt.Text.Length, m.Width);

		m.Title.Text = new string(' ', 20);
		Assert.AreEqual(20, m.Width);

		m.Subtitle.Text = new string(' ', 21);
		Assert.AreEqual(21, m.Width);

		m.Title.Text = "title";
		m.Subtitle.Text = "subtitle";
		Assert.AreEqual(defaultPrompt.Text.Length, m.Width);

		m.Prompt.Text = "short";
		Assert.AreEqual(11, m.Width);

		f.Caption = "foo bar baz";
		Assert.AreEqual(14, m.Width);

		f.Caption = "foo";
		z.Caption = "foo bar baz";
		Assert.AreEqual(14, m.Width);
	}


	private static void TestMenuState(Menu m, StyledText? expectedTitle, StyledText? expectedSubtitle, StyledText? expectedPrompt, IEnumerable<MenuOption>? expectedOptions, IEnumerable<char>? expectedExitKeys)
	{
		Assert.IsNotNull(m);

		StyledTextTests.TestStyledTextEquality(m.Title, expectedTitle);
		StyledTextTests.TestStyledTextEquality(m.Subtitle, expectedSubtitle);
		StyledTextTests.TestStyledTextEquality(m.Prompt, expectedPrompt);

		TestMenuOptionsEquality(m.Options, expectedOptions);
		TestKeys(m.ExitKeys, expectedExitKeys);
	}

	internal static void TestMenuOptionsEquality(List<MenuOption>? options, IEnumerable<MenuOption>? expectedOptions)
	{
		if (!ReferenceEquals(options, expectedOptions))
		{
			Assert.IsNotNull(options);
			Assert.IsNotNull(expectedOptions);

			Assert.AreEqual(expectedOptions.Count(), options.Count);
			if (options.Count > 0)
			{
				foreach (MenuOption option in options)
				{ TestMenuOptionEquality(option, expectedOptions.ElementAt(options.IndexOf(option))); }
			}
		}
	}

	internal static void TestMenuOptionEquality(MenuOption? option, MenuOption? expectedOption)
	{
		if (!ReferenceEquals(option, expectedOption))
		{
			Assert.IsNotNull(option);
			Assert.IsNotNull(expectedOption);
			Assert.AreEqual(expectedOption.GetType(), option.GetType());
			Assert.AreEqual(expectedOption.Caption, option.Caption);
			TestKeys(option.Keys, expectedOption.Keys);
		}
	}

	internal static void TestKeys(IEnumerable<char>? keys, IEnumerable<char>? expectedKeys)
	{
		if (!ReferenceEquals(keys, expectedKeys))
		{
			Assert.IsNotNull(keys);
			Assert.IsNotNull(expectedKeys);
			Assert.AreEqual(expectedKeys.Count(), keys.Count());

			for (int i = 0; i < keys.Count(); i++)
			{ Assert.AreEqual(expectedKeys.ElementAt(i), keys.ElementAt(i)); }
		}
	}
}
