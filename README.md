# Strick.PlusCon
Utilities that make working with console apps in .Net easier and more useful.


## Quick Start
Get the package from NuGet  
[NuGet](https://www.nuget.org/packages/Strick.PlusCon)  
![NuGet](https://img.shields.io/nuget/dt/Strick.PlusCon.svg) 
![NuGet](https://img.shields.io/nuget/v/Strick.PlusCon.svg)

.Net CLI: `dotnet add package Strick.PlusCon --version 1.1.0`  
Package Manager: `NuGet\Install-Package Strick.PlusCon -Version 1.1.0`

[See the full docs](https://stricktechnologies.github.io/Strick.PlusCon/)  


```c#
using static Strick.PlusCon.Helpers;

...

WL("Blue In Green", Color.Blue, Color.LimeGreen);
WL("Page [49] of [237]", Color.Lime);

TextStyle onTheBeach = new(Color.SandyBrown, Color.FromArgb(3, 240, 165), Color.FromArgb(145, 193, 255));
onTheBeach.Reverse = true;
WL(onTheBeach.StyleText(" Down On The Beach ".SpaceOut()));
WL(" Down On The Beach ".SpaceOut().Gradient(Color.SandyBrown, Color.FromArgb(3, 240, 165), Color.FromArgb(145, 193, 255)).Reverse());

Grid g = new();
g.Columns.Add("Qty", HorizontalAlignment.Right);
g.Columns.Add("Product");
g.Columns.Add("Price", HorizontalAlignment.Right);

g.AddRow(3, "Small Widet", 1.25M);
g.AddRow(1, "Medium Widet", 2.33M);
g.AddRow(2, "Large Widet", 3.49M);

g.Show();
```
![Example - Color Utilities 1](https://raw.githubusercontent.com/StrickTechnologies/Strick.PlusCon/master/SampleImages/ex_quickstart_1.png)


### Escape Sequences
Building blocks for working with color and other formatting escape sequences.

[See the Doc](https://stricktechnologies.github.io/Strick.PlusCon/escapeSequences.html) 


### Console shortcuts
*Strick.PlusCon* includes several shortcuts or "wrappers" for commonly used 
Console methods which provide functionality to display text in color, to highlight 
portions of a text value, prompt for user input, and more.

[See the Doc](https://stricktechnologies.github.io/Strick.PlusCon/consoleShortcuts.html)  


### Other Formatting for Console Output
*Strick.PlusCon* includes several `string` extension methods that can be used to 
format information for console output.

These methods work by wrapping the string in escape sequences to enable the 
specific console formatting.

[See the Doc](https://stricktechnologies.github.io/Strick.PlusCon/otherFormatting.html)  


### Other Utilities
Other utilities for enabling Virtual Terminal, to formatting text in various 
ways for better display.

[See the Doc](https://stricktechnologies.github.io/Strick.PlusCon/otherUtilities.html)  


### `TextStyle` and `StyledText` classes
These classes provide a number of ways to more easily apply combinations of styling 
such as foreground/background colors, gradients, and underlining to any text.

[See the Doc](https://stricktechnologies.github.io/Strick.PlusCon/textStyle.html)  


### Menus
Create a simple menu structure for your console apps to make navigation quick and easy.

[See the Doc](https://stricktechnologies.github.io/Strick.PlusCon/menu.html)  


### Grids
Grids provide a quick and easy method for displaying data in a tabular, *"rows and 
columns"*, format. Styling, including foreground/background colors, and horizontal 
alignment can be applied at the grid, row, column and cell level.

[See the Doc](https://stricktechnologies.github.io/Strick.PlusCon/grid.html)  


## Background and Inspiration
Some of the things in this utility (`W` and `WL` in particular) are things I've dragged around from project to project for years -- just generally copy/pasting into a new project whenever I finally tired of typing "Console.WriteLine" (intellisense notwithstanding) over and over. These things are generally used informally for basic testing, and to aid in creating and debugging unit tests.

I contemplated creating a library (copy/paste is evil when it comes to coding) at least a few times. But as it's something that's not used extensively very often, it hardly seemed worth it for a few one-line shortcuts (which also could've been made into snippets).

I was recently doing some things for a project where I needed to display many different values. Colors (in the form of `Console.ForegroundColor` and `BackgroundColor`) came in handy to highlight the values and make scanning for anomolies more effective and efficient. However, as the complexity increased, I wanted something more flexible -- a way to embed the color codes directly in the strings. This [answer](https://stackoverflow.com/a/60492990/1585667) on [Stack Overflow](https://stackoverflow.com) was a first step. I used the technique as the first incarnation of the W/WL overload.

As I searched for other ways to accomplish similar results, I came across this [great answer](https://stackoverflow.com/a/33206814/1585667) on [Stack Overflow](https://stackoverflow.com) (and its [Wikipedia page](https://en.wikipedia.org/wiki/ANSI_escape_code)).

I also found [Pastel](https://github.com/silkfire/Pastel), and I liked how it worked. It inspired me to go ahead and make this library.