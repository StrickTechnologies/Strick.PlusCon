## Console shortcuts
*Strick.PlusCon* includes several shortcuts or "wrappers" for commonly used 
Console methods. 


Shortcut|Console Equivalent|Notes
-|-|-
W|[Write](https://learn.microsoft.com/en-us/dotnet/api/system.console.write?view=net-6.0)|Overloads provide functionality to display text with various forms of styling.
WL|[WriteLine](https://learn.microsoft.com/en-us/dotnet/api/system.console.writeline?view=net-6.0)|Overloads provide functionality to display text with various forms of styling.
RK|[ReadKey](https://learn.microsoft.com/en-us/dotnet/api/system.console.readkey?view=net-6.0)|Includes an optional prompt argument that will be displayed before waiting for user input. Overloads provide functionality to display the prompt with various forms of styling.
RL|[ReadLine](https://learn.microsoft.com/en-us/dotnet/api/system.console.readline?view=net-6.0)|Includes an optional prompt argument that will be displayed before waiting for user input. Overloads provide functionality to display the prompt with various forms of styling.
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
There are a few different overloads of the `W` and `WL` methods that allow 
values to be output to the console with various forms of styling. 
As with `Console.Write` and `Console.WriteLine`, 
the `WL` methods are the same as the `W` methods. 
The only difference is that `WL` appends the current line terminator to the output.

All overloads will accept `null` or empty string values for the `message` parameter. 
For `W`, this generally results in no action being taken (nothing is sent 
to the console). For `WL`, this generally results in only the current line 
terminator being sent to the console. Refer to the internal documentation 
for each method for specific differences.

Supply foreground and background colors (`System.Drawing.Color`) to display 
messages in the console using those colors.

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

`W` and `WL` will also accept `TextStyle` and `StyledText` objects.

```c#
TextStyle style = new TextStyle(Color.Blue, Color.LimeGreen);
WL("Blue in Green", style);

style.BackColor = Color.MediumPurple;
style.ForeColor = Color.Blue;
WL(new StyledText("midnight blue", style));
```

![Example - W/WL 5](https://raw.githubusercontent.com/StrickTechnologies/Strick.PlusCon/master/SampleImages/ex_wwl_5.png)

#### RK and RL
The `RK` and `RL` methods are simple shortcuts, or wrappers, for the `Console.ReadKey` 
and `Console.ReadLine` methods, respectively.

There are a few overloads that allow you to display a prompt with or without 
styling. To render the prompt, each of these overloads makes use of the `W` method 
that takes the same arguments.

```c#
string anyPrompt = "Press any key ";
W(anyPrompt);
RK();
WL();

string namePrompt = "What's your name? ";
string? name;
W(namePrompt);
name = RL();
WL($"Hello {name}!");
name = RL(namePrompt);
WL($"Hello {name}!");

//colors
RK(anyPrompt, Color.DodgerBlue, Color.White);
WL();

ConsoleKeyInfo confirm;
confirm = RK("Are you sure [Y/N] ", Color.LimeGreen, Color.White);
WL();
confirm = RK("Are you really sure [Y/N] ", Color.LimeGreen, Color.White, Color.LimeGreen, Color.White);
WL();

TextStyle style = new TextStyle(Color.LimeGreen, null, Color.White);
RK(anyPrompt, style);
WL();
style.Reverse = true;
RK(new StyledText(anyPrompt, style));
```

![Example - RK/RL 1](https://raw.githubusercontent.com/StrickTechnologies/Strick.PlusCon/master/SampleImages/ex_rkrl_1.png)

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
