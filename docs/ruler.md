## Ruler
The `Ruler` static class provides a way to create quick and easy 
horizontal or vertical "rulers" or "number lines" of any length (horizontal)
or height (vertical). Use one of the `GetH` method overloads to create 
a horizontal Ruler. Use one of the `GetV` method overloads to create 
a vertical Ruler.

A ruler is made up of *"segments"* of ten characters. The first nine 
characters of each segment are specified by either the 
`HorizontalSegment` or `VerticalSegment` property. 
The tenth character of each segment is the *"tens" counter* (1 for 10, 2 for 20, etc.). 
The "tens" counter resets to zero at each hundred (100, 200).

The value of the `HorizontalSegment` and `VerticalSegment` properties can be 
changed to alter the appearance of Rulers. The segment can be set to any 
characters, but it must be exactly 9 characters in length (otherwise, an 
exception is thrown).

The `Colors` property allows colors to be applied to Rulers. 
A null value or an empty sequence is acceptable, and will result in 
rulers having no embeded color sequences. 
The colors default to a ten-color gradient from `Color.Gray` to `Color.White`.

See more about how the colors in the sequence are applied at 
[Other Formatting](otherFormatting.md#colorize).

The Segment and Colors can also be overridden using various overloads of 
the `GetH` and `GetV` methods. In these cases, the Segment or Colors apply only to that
particular call.

```c#
WL(Ruler.GetH(10));
WL("Default");

W(Ruler.GetH());
WL("console width");

WL(Ruler.GetH(30).Reverse());
WL("reverse color");

WL(Ruler.GetH(30, "----*----"));
WL("change characters");

WL(Ruler.GetH(30, new[] { Color.Red, Color.White }));
W("change colors");
```
![Example - Ruler - Horizontal 1](https://raw.githubusercontent.com/StrickTechnologies/Strick.PlusCon/master/SampleImages/ex_rulerH_1.png)

```c#
W(Ruler.GetV());
Console.SetCursorPosition(1, 0);
W("default".Vertical());

Console.SetCursorPosition(4, 0);
W(Ruler.GetV().Reverse());
Console.SetCursorPosition(5, 0);
W("reverse".Vertical());

Console.SetCursorPosition(8, 0);
W(Ruler.GetV(Console.WindowHeight, @"/\/\*/\/\"));
Console.SetCursorPosition(9, 0);
W("characters".Vertical());

Console.SetCursorPosition(12, 0);
W(Ruler.GetV(Console.WindowHeight, new[] { Color.Red, Color.White }));
Console.SetCursorPosition(13, 0);
W("colors".Vertical());
```
![Example - Ruler - Horizontal 1](https://raw.githubusercontent.com/StrickTechnologies/Strick.PlusCon/master/SampleImages/ex_rulerV_1.png)
