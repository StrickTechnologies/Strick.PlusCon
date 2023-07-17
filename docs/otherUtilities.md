## Other Utilities
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
This `string` extension method intersperses (inserts) a specified character between 
each of the characters of a string value and returns the resulting string. 

```c#
string nl = "----*----1";
WL(nl);
WL("A".Center(nl.Length));
WL("AB".Center(nl.Length));
WL("ABC".Center(nl.Length, '-'));
WL("ABCD".Center(nl.Length));
WL("ABCDEFGHIJ".Center(nl.Length));
WL("ABCDEFGHIJ-Longer".Center(nl.Length));

WL("Spaced out".SpaceOut());
WL("dashed".Intersperse('-'));
```
![Example - Formatting Utilities 1](https://raw.githubusercontent.com/StrickTechnologies/Strick.PlusCon/master/SampleImages/ex_formatutil_1.png)
