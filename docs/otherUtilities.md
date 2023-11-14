## Other Utilities
### About
The `About` static class has a couple of properties that provide information about 
the product.

#### `ProductName`
This property returns a string containing the name of the product.

#### `Version`
This property returns a `System.Version` object containing the 
version number of the product.

### Virtual Terminal
If you see console output that looks like this:
```
?[38;2;255;0;0mHello world!?[39m
```
when you are expecting something with colors, you will need to enable virtual terminal mode. 
This will typically happen if you are running a console application from Windows Explorer, 
the Windows Command Prompt, or Windows PowerShell.

One solution is to run the application from 
[Windows Terminal](https://apps.microsoft.com/store/detail/windows-terminal/9N0DX20HK701) 
(a free download). Another is to add a call to `ConsoleUtilities.EnableVirtualTerminal` in 
your application ahead of where you want to use escape sequences to format console output.

### Formatting
These methods in the `Formatting` static class help to format string values for display.

#### `Center`
This `string` extension method returns a string of characters of the 
specified "width" (length), with the passed value "Centered" (padded on the 
left and right with the specified character) within the string. 
If the passed value cannot be centered evenly, the extraneous character is on 
the right of the result.
If the passed value is null, or an empty string, a string consisting 
entirely of the specified fill character, exactly the length of the 
specified width is returned.
If the length of the passed value is greater than, or equal 
to the specified width, the passed value is returned unchanged.

#### `SpaceOut`
This `string` extension method intersperses (inserts) a space character (' ') 
between each of the characters of a string value and returns the resulting string. 


#### `Intersperse` 
This `string` extension method intersperses (inserts) a specified character or 
string between each of the characters of a string value and returns the 
resulting string. 

#### `Vertical`
This `string` extension method intersperses an `EscapeCodes.Down1Left1` sequence between 
each of the characters of a string value and returns the resulting string. 
When written to the console, the string will display vertically. 

```c#
int len = 10;
string ruler = Ruler.GetH(len);
WL();
WL(ruler);
WL("A".Center(len));
WL("AB".Center(len));
WL("ABC".Center(len, '-'));
WL("ABCD".Center(len));
WL("ABCDEFGHIJ".Center(len));
WL("ABCDEFGHIJ-Longer".Center(len));

WL("Spaced out".SpaceOut());
WL("dashed".Intersperse('-'));

Console.SetCursorPosition(25, 1);
W("Vertical".Vertical());
```
![Example - Formatting Utilities 1](https://raw.githubusercontent.com/StrickTechnologies/Strick.PlusCon/master/SampleImages/ex_formatutil_1.png)
