## Console shortcuts
*Strick.PlusCon* includes several shortcuts or "wrappers" for commonly used Console methods. Documentation on the underlying console methods is beyond the scope of this document, but you can see the linked documentaion for information on how the methods work.


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

### Overloads

#### W and WL
There are a few different overloads of the `W` and `WL` methods that allow values to be output to the console with colors. ***Note:** The `WL` overloads are the same as the `W` overloads. The only difference is that `WL` appends the current line terminator to the output.*

Supply foreground and background colors (`System.Drawing.Color`) to display messages in the console using those colors.

```c#
WL("Hello World!", Color.Red);
WL("Hello World!", Color.Red, Color.White);
```

![Example - W/WL 1](https://raw.githubusercontent.com/StrickTechnologies/Strick.PlusCon/master/SampleImages/ex_wwl_1.png)

To hightlight only a portion of the text, enclose it in square brackets ([, ]).

```c#
WL("Hello [World]!", Color.Red);
WL("Hello [World]!", Color.Red, Color.White);
```

![Example - W/WL 2](https://raw.githubusercontent.com/StrickTechnologies/Strick.PlusCon/master/SampleImages/ex_wwl_2.png)

To show the brackets (this can be useful if a particular value might be an empty string).

```c#
WL("Hello [World]!", Color.Red, null, true);
WL("Hello [World]!", Color.Red, Color.White, true);
```

![Example - W/WL 3](https://raw.githubusercontent.com/StrickTechnologies/Strick.PlusCon/master/SampleImages/ex_wwl_3.png)

To specify the colors of the brackets

```c#
WL("Hello [World]!", Color.Red, null, Color.Red);
WL("Hello [World]!", Color.Red, Color.White, Color.Blue, Color.White);
```

![Example - W/WL 4](https://raw.githubusercontent.com/StrickTechnologies/Strick.PlusCon/master/SampleImages/ex_wwl_4.png)


#### CLS
Use the CLS method to clear the console screen. Pass background and/or foreground colors to set the console screen to those colors.
```c#
CLS(Color.LimeGreen, Color.Blue);
WL("Blue in Green");
```

![Example - CLS 1](https://raw.githubusercontent.com/StrickTechnologies/Strick.PlusCon/master/SampleImages/ex_cls_1.png)


Note that the colors set in CLS will remain in effect until a color reset sequence is sent to the console.
This would happen, for example, by using the `Colorize`, or `WL` methods (and others), as shown below.
To set the colors back to the desired colors, you can send a color escape sequence (without a reset sequence) to the console.

```c#
CLS(Color.LimeGreen, Color.Blue);
WL("Blue in Green");
//colors reset by the line below
WL("Kind Of Blue", Color.White, Color.Blue);
//back to default console colors here
WL("No longer blue");
//set to desired colors again
W(EscapeCodes.GetBackColorSequence(Color.LimeGreen) + EscapeCodes.GetForeColorSequence(Color.Blue));
WL("Blue once more");
WL("All Blues");
```

![Example - CLS 2](https://raw.githubusercontent.com/StrickTechnologies/Strick.PlusCon/master/SampleImages/ex_cls_2.png)
