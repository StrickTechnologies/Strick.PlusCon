# Strick.PlusCon
Utilities that make working with console apps in .Net easier and more useful.

## Console shortcuts
*Strick.PlusCon* includes several shortcuts for commonly used Console methods.


Shortcut|Console Equivalent|Notes
-|-|-
W|[Write](https://docs.microsoft.com/en-us/dotnet/api/system.console.write?view=net-6.0)|Overloads provide functionality to display text in color, or to highlight portions of a text value.<br>See the [Write](https://docs.microsoft.com/en-us/dotnet/api/system.console.write?view=net-6.0) doc for specifics on how that works.
WL|[WriteLine](https://docs.microsoft.com/en-us/dotnet/api/system.console.writeline?view=net-6.0)|Overloads provide functionality to display text in color, or to highlight portions of a text value.<br>See the [WriteLine](https://docs.microsoft.com/en-us/dotnet/api/system.console.writeline?view=net-6.0) doc for specifics on how that works.
RK|[ReadKey](https://docs.microsoft.com/en-us/dotnet/api/system.console.readkey?view=net-6.0)|Includes an optional prompt argument that will be displayed before waiting for user input.<br>See the [Readkey](https://docs.microsoft.com/en-us/dotnet/api/system.console.readkey?view=net-6.0) doc for specifics on how that works.
RL|[ReadLine](https://docs.microsoft.com/en-us/dotnet/api/system.console.readline?view=net-6.0)|Includes an optional prompt argument that will be displayed before waiting for user input.<br>See the [Readline](https://docs.microsoft.com/en-us/dotnet/api/system.console.readline?view=net-6.0) doc for specifics on how that works.

Include a `using static` directive in your file to make these shortcuts available without additional qualifying.

```c#
using static Strick.PlusCon.Helpers;
...
WL("Hello World!");
RK("Press any key to continue ");
```

### Overloads

#### W and WL
There are a few different overloads of the `W` and `WL` methods that allow values to be output to the console with colors. ***Note:** The `WL` overloads are the same as the `W` overloads. The only difference is that `WL` appends the current line terminator to the output.*

Supply foreground and background colors (`System.Drawing.Color`) to display messages in the console using those colors.

```c#
WL("Hello World!", Color.Red);
WL("Hello World!", Color.Red, Color.White);
```

![Sample 1](https://raw.githubusercontent.com/StrickTechnologies/Strick.PlusCon/master/SampleImages/Sample01.png)

To hightlight only a portion of the text, enclose it in brackets ([, ]).

```c#
WL("Hello [World]!", Color.Red);
WL("Hello [World]!", Color.Red, Color.White);
```

![Sample 2](https://raw.githubusercontent.com/StrickTechnologies/Strick.PlusCon/master/SampleImages/Sample02.png)

To show the brackets (this can be useful if a particular value might be an empty string).

```c#
WL("Hello [World]!", Color.Red, null, true);
WL("Hello [World]!", Color.Red, Color.White, true);
```

![Sample 3](https://raw.githubusercontent.com/StrickTechnologies/Strick.PlusCon/master/SampleImages/Sample03.png)

To specify the colors of the brackets

```c#
WL("Hello [World]!", Color.Red, null, Color.Red);
WL("Hello [World]!", Color.Red, Color.White, Color.Blue, Color.White);
```

![Sample 4](https://raw.githubusercontent.com/StrickTechnologies/Strick.PlusCon/master/SampleImages/Sample04.png)

## Other Formatting for Console Output
*Strick.PlusCon* includes several `string` extension methods that can be used to format information for console output.

These methods work by wrapping the string in escape sequences to enable the specific console formatting.


#### Colorize
The `Colorize` method wraps a string with foreground and/or background color escape sequences.

```c#
string colorized = "foo".Colorize(Color.Red);
WL(colorized);
WL(colorized.Colorize(null, Color.White)); //add background
WL("Hello World!".Colorize(Color.Red));
WL("Hello World!".Colorize(Color.Red, Color.White));
```

![Sample 5](https://raw.githubusercontent.com/StrickTechnologies/Strick.PlusCon/master/SampleImages/Sample05.png)

You can also wrap your colorized string with pre- and post-values. The pre- and post-values can also, optionally, be rendered with their own colors.

```c#
string wrapped = "foo".Colorize(Color.Red, null, "**[", "]**");
WL(wrapped);
wrapped = "foo".Colorize(Color.Red, null, "**[", "]**", Color.Lime, Color.White);
WL(wrapped);
wrapped = "cruel".Colorize(Color.Red, null, "*", "*", Color.Lime);
WL($"Hello {wrapped} World!");
```

![Sample 6](https://raw.githubusercontent.com/StrickTechnologies/Strick.PlusCon/master/SampleImages/Sample06.png)

#### Underline
The `Underline` method wraps a string with underlining escape sequences.

```c#
string underlined = "foo".Underline();
WL(underlined);
WL("Hello World!".Underline());
```

![Sample 7](https://raw.githubusercontent.com/StrickTechnologies/Strick.PlusCon/master/SampleImages/Sample07.png)

#### Reverse
The `Reverse` method wraps a string with reverse text (foreground and background colors are swapped, or "reversed") escape sequences.

```c#
string reversed = "foo".Reverse();
WL(reversed);
WL("Hello World!".Reverse());
```

![Sample 8](https://raw.githubusercontent.com/StrickTechnologies/Strick.PlusCon/master/SampleImages/Sample08.png)

#### Gradient
The `Gradient` method inserts escape sequences into a string to vary the foreground color of each character in the string.

```c#
string grad = "foo-bar".Gradient(Color.White, Color.BlueViolet);
WL(grad);
WL("Hello-World!".Gradient(Color.Red, Color.White));
WL("--=gradients=--".Gradient(Color.White, Color.BlueViolet, Color.White));
WL("***fade-out***".Gradient(Color.White, Color.Black));
WL("***fade-in!***".Gradient(Color.Black, Color.White));
WL("-- ** on the beach ** --".Gradient(Color.SandyBrown, Color.FromArgb(3, 240, 165), Color.FromArgb(145, 193, 255)));
```

![Sample 9](https://raw.githubusercontent.com/StrickTechnologies/Strick.PlusCon/master/SampleImages/Sample09.png)

#### Combining
The various formatting methods can be combined to create additional effects.

```c#
WL($"Hello World!".Underline(), Color.Red);
WL($"Hello {"cruel".Underline()} World!", Color.Red);
WL("***fade-out***".Gradient(Color.Black, Color.White).Colorize(null, Color.White));
WL("***fade-in!***".Gradient(Color.White, Color.Black).Colorize(null, Color.White));
WL("-- ** on the beach ** --".Gradient(Color.SandyBrown, Color.FromArgb(3, 240, 165), Color.FromArgb(145, 193, 255)).Reverse());
WL("-- ** on the beach ** --".Gradient(Color.SandyBrown, Color.FromArgb(3, 240, 165), Color.FromArgb(145, 193, 255)).Underline());
```

![Sample 10](https://raw.githubusercontent.com/StrickTechnologies/Strick.PlusCon/master/SampleImages/Sample10.png)

Note that nesting colors (either foreground or background) is NOT supported.  

```c#
Console.ForegroundColor = ConsoleColor.Red;
WL($"Hello {"cruel".Colorize(Color.Lime).Underline()} World!");
Console.ResetColor();
WL($"Hello {"cruel".Colorize(Color.Lime).Underline()} World!", Color.Red);
WL($"Hello [cruel] World!".Colorize(Color.Red), Color.Lime);
//You have to use something more like this...
WL($"{"Hello".Colorize(Color.Red)} {"cruel".Colorize(Color.Lime).Underline()} {"World!".Colorize(Color.Red)}");
```

![Sample 11](https://raw.githubusercontent.com/StrickTechnologies/Strick.PlusCon/master/SampleImages/Sample11.png)

## Other Utilities
### Virtual Terminal
If you see things that look like this:
```
?[38;2;255;0;0mHello world!?[39m
```
when you are expting something with colors, you will need to enable virtual terminal mode. This will typically happen if you are running a cosole application from Windows Explorer, the Windows Command Prompt, or Windows PowerShell.

One solution is to run the application from Windows Terminal. Another is to add a call to `ConsoleUtilities.EnableVirtualTerminal` in your application ahead of where you want to use escape sequences to format console output.

## Background and Inspiration
Some of the things in this utility (`W` and `WL` in particular) are things I've dragged around from project to project for years -- just generally copy/pasting into a new project whenever I finally tired of typing "Console.WriteLine" (intellisense notwithstanding) over and over. These things are generally used informally for basic testing, and to aid in creating and debugging unit tests.

I contemplated creating a library (copy/paste is evil when it comes to coding) at least a few times. But as it's something that's not used extensively very often, it hardly seemed worth it for a few one-line shortcuts (which also could've been made into snippets).

I was recently doing some things for a project where I needed to display many different values. Colors (in the form of `Console.ForegroundColor` and `BackgroundColor`) came in handy to highlight the values and make scanning for anomolies more effective and efficient. However, as the complexity increased, I wanted something more flexible -- a way to embed the color codes directly in the strings. This [answer](https://stackoverflow.com/a/60492990/1585667) on [Stack Overflow](https://stackoverflow.com) was a first step. I used the technique as the first incarnation of the W/WL overload.

As I searched for other ways to accomplish similar results, I came across this [great answer](https://stackoverflow.com/a/33206814/1585667) on [Stack Overflow](https://stackoverflow.com) (and its [Wikipedia page](https://en.wikipedia.org/wiki/ANSI_escape_code)).

I also found [Pastel](https://github.com/silkfire/Pastel), and I liked how it worked. It inspired me to go ahead and make this library.