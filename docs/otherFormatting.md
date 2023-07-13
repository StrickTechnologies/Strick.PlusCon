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

You can also wrap your colorized string with pre- and post-values. 
The pre- and post-values can also, optionally, be rendered with their own colors.

```c#
string wrapped = "foo".Colorize(Color.Red, null, "**[", "]**");
WL(wrapped);
wrapped = "foo".Colorize(Color.Red, null, "**[", "]**", Color.Lime, Color.White);
WL(wrapped);
wrapped = "cruel".Colorize(Color.Red, null, "*", "*", Color.Lime);
WL($"Hello {wrapped} World!");
```

![Example - Colorize 2](https://raw.githubusercontent.com/StrickTechnologies/Strick.PlusCon/master/SampleImages/ex_colorize_2.png)

#### Underline
The `Underline` method wraps a string with underlining escape sequences.

```c#
string underlined = "foo".Underline();
WL(underlined);
WL("Hello World!".Underline());
```

![Example - Underline 1](https://raw.githubusercontent.com/StrickTechnologies/Strick.PlusCon/master/SampleImages/ex_underline_1.png)

#### Reverse
The `Reverse` method wraps a string with reverse text (foreground and background colors are swapped, or "reversed") escape sequences.

```c#
string reversed = "foo".Reverse();
WL(reversed);
WL("Hello World!".Reverse());
WL(reversed, Color.LimeGreen, Color.White);
WL(reversed, Color.White, Color.LimeGreen);
```

![Example - Reverse 1](https://raw.githubusercontent.com/StrickTechnologies/Strick.PlusCon/master/SampleImages/ex_reverse_1.png)

#### Gradient
The `Gradient` method inserts escape sequences into a string to vary the foreground color of each character in the string.

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

Use the `ColorUtilities.GetGradientColors` method to get a sequence of colors for a gradient, 
which can be used in ways limited only by your imagination.

```c#
var colors = ColorUtilities.GetGradientColors(Color.SkyBlue, Color.Orange, Console.WindowHeight).ToList();
string spaces = new(' ', Console.WindowWidth);
foreach (var color in colors)
{ W(spaces, Color.White, color); }

Console.SetCursorPosition(0, 0);
W("Sunrise", Color.White, colors[0]);
```

![Example - Gradient 2](https://raw.githubusercontent.com/StrickTechnologies/Strick.PlusCon/master/SampleImages/ex_gradient_2.png)

```c#
int top = Console.WindowHeight / 2;
int bottom = Console.WindowHeight - top;
var colors = ColorUtilities.GetGradientColors(Color.FromArgb(145, 193, 255), Color.FromArgb(3, 240, 165), top).ToList();
colors.AddRange(ColorUtilities.GetGradientColors(Color.FromArgb(3, 240, 165), Color.SandyBrown, bottom));
string spaces = new(' ', Console.WindowWidth);
foreach (var color in colors)
{ W(spaces, Color.White, color); }

Console.SetCursorPosition(0, Console.WindowHeight - 2);
W("On the beach", Color.White, colors[^2]);
```

![Example - Gradient 3](https://raw.githubusercontent.com/StrickTechnologies/Strick.PlusCon/master/SampleImages/ex_gradient_3.png)

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

![Example - Other Notes 1](https://raw.githubusercontent.com/StrickTechnologies/Strick.PlusCon/master/SampleImages/ex_notes_1.png)

#### Other Color Utilities
These methods in the `ColorUtilities` static class work with the `System.Drawing.Color` structure.

`Brighten` `Color` extension method

`Darken` `Color` extension method

`AdjustBrightness` `Color` extension method
