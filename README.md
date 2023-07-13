# Strick.PlusCon
Utilities that make working with console apps in .Net easier and more useful.

## Contents
1. [Console shortcuts](#console-shortcuts)
1. [Other Formatting for Console Output](#other-formatting-for-console-output)
1. [Other Utilities](#other-utilities)
1. [`TextStyle` and `StyledText` classes](#textstyle-and-styledtext-classes)
1. [`Menu`, `MenuOption` classes](#menu-menuoption-classes)
1. [Grid](Docs/Grid.md)
1. [Background and Inspiration](#background-and-inspiration)


## Quick Start
Get the package from NuGet  
[NuGet](https://www.nuget.org/packages/Strick.PlusCon)  
![NuGet](https://img.shields.io/nuget/dt/Strick.PlusCon.svg) 
![NuGet](https://img.shields.io/nuget/v/Strick.PlusCon.svg)

.Net CLI: `dotnet add package Strick.PlusCon --version 1.0.0`  
Package Manager: `NuGet\Install-Package Strick.PlusCon -Version 1.0.0`

[See the full docs](https://stricktechnologies.github.io/Strick.PlusCon/)  

### Console shortcuts
*Strick.PlusCon* includes several shortcuts or "wrappers" for commonly used 
Console methods. 

Shortcut|Console Equivalent|Notes
-|-|-
W|[Write](https://learn.microsoft.com/en-us/dotnet/api/system.console.write?view=net-6.0)|Overloads provide functionality to display text in color, or to highlight portions of a text value.
WL|[WriteLine](https://learn.microsoft.com/en-us/dotnet/api/system.console.writeline?view=net-6.0)|Overloads provide functionality to display text in color, or to highlight portions of a text value.
RK|[ReadKey](https://learn.microsoft.com/en-us/dotnet/api/system.console.readkey?view=net-6.0)|Includes an optional prompt argument that will be displayed before waiting for user input.
RL|[ReadLine](https://learn.microsoft.com/en-us/dotnet/api/system.console.readline?view=net-6.0)|Includes an optional prompt argument that will be displayed before waiting for user input.
CLS|[Clear](https://learn.microsoft.com/en-us/dotnet/api/system.console.clear?view=net-6.0)|An overload provides the ability to clear the console window to specified foreground and background colors.

Include a `using static` directive in your file to make these shortcuts available without additional qualifying.

```c#
using static Strick.PlusCon.Helpers;
...
WL("Hello World!");
RK("Press any key to continue ");
```

#### CLS
Use the CLS method to clear the console screen. Pass background and/or foreground colors to set the console screen to those colors.
```c#
CLS(Color.LimeGreen, Color.Blue);
WL("Blue in Green");
```

![Example - CLS 1](https://raw.githubusercontent.com/StrickTechnologies/Strick.PlusCon/master/SampleImages/ex_cls_1.png)


## Other Formatting for Console Output
*Strick.PlusCon* includes several `string` extension methods that can be used to 
format information for console output.

These methods work by wrapping the string in escape sequences to enable the 
specific console formatting.


#### Colorize
The `Colorize` method wraps a string with foreground and/or background color 
escape sequences.

```c#
string colorized = "foo".Colorize(Color.Red);
WL(colorized);
WL(colorized.Colorize(null, Color.White)); //add background
WL("Hello World!".Colorize(Color.Red));
WL("Hello World!".Colorize(Color.Red, Color.White));
```

![Example - Colorize 1](https://raw.githubusercontent.com/StrickTechnologies/Strick.PlusCon/master/SampleImages/ex_colorize_1.png)

#### Underline
The `Underline` method wraps a string with underlining escape sequences.

```c#
string underlined = "foo".Underline();
WL(underlined);
WL("Hello World!".Underline());
```

![Example - Underline 1](https://raw.githubusercontent.com/StrickTechnologies/Strick.PlusCon/master/SampleImages/ex_underline_1.png)

#### Reverse
The `Reverse` method wraps a string with reverse text (foreground and background 
colors are swapped, or "reversed") escape sequences.

```c#
string reversed = "foo".Reverse();
WL(reversed);
WL("Hello World!".Reverse());
WL(reversed, Color.LimeGreen, Color.White);
WL(reversed, Color.White, Color.LimeGreen);
```

![Example - Reverse 1](https://raw.githubusercontent.com/StrickTechnologies/Strick.PlusCon/master/SampleImages/ex_reverse_1.png)

#### Gradient
The `Gradient` method inserts escape sequences into a string to vary the 
foreground color of each character in the string.

```c#
string grad = "foo-bar".Gradient(Color.White, Color.BlueViolet);
WL(grad);
WL("Hello-World!".Gradient(Color.Red, Color.White));
WL("--=gradients=--".Gradient(Color.White, Color.BlueViolet, Color.White));
WL("***fade-out***".Gradient(Color.White, Color.Black));
WL("***fade-in!***".Gradient(Color.Black, Color.White));
WL("-- ** down on the beach ** --".Gradient(Color.SandyBrown, Color.FromArgb(3, 240, 165), Color.FromArgb(145, 193, 255)));
```

![Example - Gradient 1](https://raw.githubusercontent.com/StrickTechnologies/Strick.PlusCon/master/SampleImages/ex_gradient_1.png)


#### Combining
The various formatting methods can be combined to create additional effects.

```c#
WL($"Hello World!".Underline(), Color.Red);
WL($"Hello {"cruel".Underline()} World!", Color.Red);
WL("***fade-out***".Gradient(Color.Black, Color.White).Colorize(null, Color.White));
WL("***fade-in!***".Gradient(Color.White, Color.Black).Colorize(null, Color.White));
WL("-- ** down on the beach ** --".Gradient(Color.SandyBrown, Color.FromArgb(3, 240, 165), Color.FromArgb(145, 193, 255)).Reverse());
WL("-- ** down on the beach ** --".Gradient(Color.SandyBrown, Color.FromArgb(3, 240, 165), Color.FromArgb(145, 193, 255)).Underline());
```

![Example - Combinations 1](https://raw.githubusercontent.com/StrickTechnologies/Strick.PlusCon/master/SampleImages/ex_combo_1.png)


## Other Utilities
### Virtual Terminal
If you see things that look like this:
```
?[38;2;255;0;0mHello world!?[39m
```
when you are expecting something with colors, you will need to enable virtual terminal mode. This will typically happen if you are running a console application from Windows Explorer, the Windows Command Prompt, or Windows PowerShell.

One solution is to run the application from [Windows Terminal](https://apps.microsoft.com/store/detail/windows-terminal/9N0DX20HK701) (a free download). Another is to add a call to `ConsoleUtilities.EnableVirtualTerminal` in your application ahead of where you want to use escape sequences to format console output.

## `TextStyle` and `StyledText` classes
The `TextStyle` and `StyledText` classes provide a number of ways to more easily apply combinations of styling to any text.

The `TextStyle` class provides methods and properties to apply foreground and background colors, 
color gradients, reverse and underline styling to text. Set the various properties as desired, 
then use the `StyleText` method to apply the styling to any text.

```c#
TextStyle ts = new(Color.White, Color.DodgerBlue, Color.White);
//same style, different content
foreach (string s in new[] { "content 1", "level 2" })
{ WL(ts.StyleText(s)); }

ts.ForeColor = Color.Red;
ts.Underline = true;
ts.ClearGradient();
WL(ts.StyleText("Hello World!"));

ts.Underline = false;
ts.ForeColor = null;
ts.BackColor = Color.White;
ts.SetGradientColors(Color.Black, Color.White);
WL(ts.StyleText("***fade-out***"));

ts.SetGradientColors(Color.White, Color.Black);
WL(ts.StyleText("***fade-in!***"));

ts.BackColor = null;
ts.Reverse = true;
ts.SetGradientColors(Color.SandyBrown, Color.FromArgb(3, 240, 165), Color.FromArgb(145, 193, 255));
WL(ts.StyleText("-- ** down on the beach ** --"));

ts.Reverse = false;
ts.Underline = true;
W(ts.StyleText("-- ** down on the beach ** --"));
```

![Example - TextStyle 1](https://raw.githubusercontent.com/StrickTechnologies/Strick.PlusCon/master/SampleImages/ex_textStyle_1.png)

The `StyledText` class combines text content with a `TextStyle` object. 
For flexibility, both the `Text` and `Style` properties are read/write, 
and a `StyleText` method allows the styling to be applied to any text.

```c#
StyledText st = new("Hello World!", new(Color.Blue));

//same content, different styling
foreach (Color c in ColorUtilities.GetGradientColors(Color.FromArgb(0, 255, 0), Color.FromArgb(0, 128, 0), 4))
{
	st.Style.BackColor = c;
	WL(st.TextStyled);
}
//same styling, alternate content
WL(st.StyleText("Blue in Green"));

//back to default
st.Style = new();
st.Text = "Default styling";
WL(st.TextStyled);

//different content, different styling
st.Style.BackColor = Color.DarkGray;
st.Style.ForeColor = Color.White;
st.Text = "(not) " + st.Text;
W(st.TextStyled);
```

![Example - TextStyle 1](https://raw.githubusercontent.com/StrickTechnologies/Strick.PlusCon/master/SampleImages/ex_styledText_1.png)


## `Menu`, `MenuOption` classes
The `Menu` and `MenuOption` classes provide a way to create a simple menu structure for a console app. 

A menu can have one or more `Options`, and optionally a `Title`, `Subtitle` and `Prompt`. 
The titles show at the top of the menu, above the options. 
The prompt shows beneath the options, and the cursor is located next to the prompt while awaiting user input.

There are three types of menu options:
1. **Invokable Options**. Either invoke a submenu or an Action (a method that takes no arguments and returns no value). Use the `MenuOption` class.
2. **Separator Options**. Shows as an option with NO key. Use the `MenuSeperator` class. 
Setting the `Caption` property to any single character (e.g. "-") will repeat that character for the width of the menu.
3. **Exit Menu Options**. Exits the menu when selected/invoked by the user. Use the `MenuBackOption` class.

The user selects, or invokes, an option by pressing the "key" (typically a letter or digit) shown next to the option.
Options can have multiple keys associated with them, but only the first key will be displayed on the menu.

A menu can have multiple `ExitKeys` associated with it. An "Exit Key" is a key that will close the menu.
The default `ExitKeys` for a menu are: *0 (the digit zero)*, *Space (" ")*, *Escape*, *Enter*, and *Backspace*. 
These can be overridden via the `ExitKeys` property.

If a prompt is not desired, set the `Prompt` property to null (as opposed to setting Prompt.Text).
```c#
Menu myMenu = new("Example Menu");
...
myMenu.Prompt = null; //remove the prompt

//these don't work
myMenu.Prompt.Text = null; //results in an exception
myMenu.Prompt.Text = ""; //results in an exception
```

### Key collisions
If an option in a menu's `Options` collection contains a key in its `Keys` collection that is also 
contained in the menu's `ExitKeys` collection, the menu option takes precedent.
If two (or more) options in the a menu's `Options` collection contain the same key in their `Keys` collection, 
the option with the lowest index within the `Options` collection will take precedent.

```c#
Menu subMenu = new("Example Submenu");
//lambda
subMenu.Add(new MenuOption("Option 1", '1', () =>
{
	CLS();
	WL("This is Example Submenu Option 1", Color.Red);
	RK("press a key to return to the menu...");
}));

//lambda
subMenu.Add(new MenuOption("Option 2", '2', () =>
{
	CLS();
	WL("This is Example Submenu Option 2", Color.LimeGreen);
	RK("press a key to return to the menu...");
}));

subMenu.Add(new MenuSeperator("-"));
subMenu.Add(new MenuBackOption("Return to Example Menu", 'X'));


Menu myMenu = new("Example Menu");
myMenu.Add(new MenuOption("Option 1", '1', ExampleMenuOption1));
myMenu.Add(new MenuOption("Option 2", '2', ExampleMenuOption2));
myMenu.Add(new MenuOption("Submenu", 'S', subMenu));

myMenu.Show();

private static void ExampleMenuOption1()
{
	CLS();
	WL("This is Example Menu Option 1", Color.Red);
	RK("press a key to return to the menu...");
}

private static void ExampleMenuOption2()
{
	CLS();
	WL("This is Example Menu Option 2", Color.LimeGreen);
	RK("press a key to return to the menu...");
}
```
![Example - Menu 1 (main menu)](https://raw.githubusercontent.com/StrickTechnologies/Strick.PlusCon/master/SampleImages/ex_menu_1-1.png)
![Example - Menu 1 (sub menu)](https://raw.githubusercontent.com/StrickTechnologies/Strick.PlusCon/master/SampleImages/ex_menu_1-2.png)

Colors for menu titles and menu options can easily be set. 
At the menu level, both titles (`Title`, `Subtitle`) and `Prompt` have `Style` properties.
The menu has an `OptionsStyle` property, which sets the style for ALL menu options.
Individual options can override the menu-level style via their `Style` property.

The `Style` properties are all [`TextStyle` objects](#textstyle-and-styledtext-classes).

```c#
Menu subMenu = new("Example Submenu", "-");
subMenu.Title!.Style.SetGradientColors(Color.Silver, Color.SlateGray, Color.Silver);
subMenu.Title!.Style.BackColor = Color.White;
subMenu.Title!.Style.Reverse = true;
subMenu.Subtitle!.Style = subMenu.Title.Style;
subMenu.OptionsStyle = new(Color.DodgerBlue);
subMenu.Prompt!.Style.ForeColor = Color.White;

//lambda
subMenu.Add(new MenuOption("Option 1", '1', () =>
{
	CLS();
	WL("This is Example Submenu Option 1", Color.Red);
	RK("press a key to return to the menu...");
}));

//lambda
subMenu.Add(new MenuOption("Option 2", '2', () =>
{
	CLS();
	WL("This is Example Submenu Option 2", Color.LimeGreen);
	RK("press a key to return to the menu...");
}));

subMenu.Add(new MenuSeperator("-"));
subMenu.Add(new MenuBackOption("Return to Example Menu", 'X'));
subMenu.Options[subMenu.Options.Count - 1].Style = new(Color.Silver);


Menu myMenu = new("Example Menu", " ");
myMenu.Title!.Style.ForeColor = Color.LimeGreen;
myMenu.OptionsStyle = new(Color.BlueViolet);
myMenu.Add(new MenuOption("Option 1", '1', ExampleMenuOption1));
myMenu.Add(new MenuOption("Option 2", '2', ExampleMenuOption2));
myMenu.Add(new MenuSeperator(""));
myMenu.Add(new MenuOption("Submenu", 'S', subMenu));
myMenu.Options[myMenu.Options.Count - 1].Style = new(Color.White);
myMenu.Add(new MenuSeperator(""));

myMenu.Show();

private static void ExampleMenuOption1()
{
	CLS();
	WL("This is Example Menu Option 1", Color.Red);
	RK("press a key to return to the menu...");
}

private static void ExampleMenuOption2()
{
	CLS();
	WL("This is Example Menu Option 2", Color.LimeGreen);
	RK("press a key to return to the menu...");
}
```
![Example - Menu 2 (main menu)](https://raw.githubusercontent.com/StrickTechnologies/Strick.PlusCon/master/SampleImages/ex_menu_2-1.png)
![Example - Menu 2 (sub menu)](https://raw.githubusercontent.com/StrickTechnologies/Strick.PlusCon/master/SampleImages/ex_menu_2-2.png)

## Grid

A `Grid` consists of rows and columns of data that is displayed in a tabular format. 
Each Column is sized automatically to fit its widest content. 
In addition to the rows and columns, grids can also have a `Title`, `Subtitle` and `Footer`. 
The titles are centered above the rows/columns, the footer is centered beneath the rows/columns. 

The appearance and layout of the grid's columns, rows, and cells can be customized through the 
use of properties available at the various levels. 
The styling for the titles and footer can also be customized. 

To create a grid, use the `Grid` class. Add columns and rows. 
Then use the `Show` method to display the grid.


## Background and Inspiration
Some of the things in this utility (`W` and `WL` in particular) are things I've dragged around from project to project for years -- just generally copy/pasting into a new project whenever I finally tired of typing "Console.WriteLine" (intellisense notwithstanding) over and over. These things are generally used informally for basic testing, and to aid in creating and debugging unit tests.

I contemplated creating a library (copy/paste is evil when it comes to coding) at least a few times. But as it's something that's not used extensively very often, it hardly seemed worth it for a few one-line shortcuts (which also could've been made into snippets).

I was recently doing some things for a project where I needed to display many different values. Colors (in the form of `Console.ForegroundColor` and `BackgroundColor`) came in handy to highlight the values and make scanning for anomolies more effective and efficient. However, as the complexity increased, I wanted something more flexible -- a way to embed the color codes directly in the strings. This [answer](https://stackoverflow.com/a/60492990/1585667) on [Stack Overflow](https://stackoverflow.com) was a first step. I used the technique as the first incarnation of the W/WL overload.

As I searched for other ways to accomplish similar results, I came across this [great answer](https://stackoverflow.com/a/33206814/1585667) on [Stack Overflow](https://stackoverflow.com) (and its [Wikipedia page](https://en.wikipedia.org/wiki/ANSI_escape_code)).

I also found [Pastel](https://github.com/silkfire/Pastel), and I liked how it worked. It inspired me to go ahead and make this library.