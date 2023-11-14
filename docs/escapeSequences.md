## Escape Sequences
An *escape sequence* is a series (or "sequence") of characters beginning with the 
escape character. Escape sequences are sent to the console to control various 
forms of styling.

## `EscapeCodes` class
The `EscapeCodes` static class contains a number of properties and methods that 
allow you to do custom things with colors, other formatting, and cursor movement, 
shape & visibility.

### Properties

#### `Escape`
Returns a `char` representing the escape character. This can be useful in building 
your own esacpe sequences. It also better shows intent and is more readable in code
than `'\x1b'`.

#### `Color`, `Color_Fore`, `Color_Back`
These properties return escape sequence "templates", which have placeholders that you 
can replace with color values to make a complete color escape sequence.

**Placeholders**  
`{cs}`. The color space. Replace with a value from the `ColorSpace` enum.  
`{r}`. The Red component of the color (a value between 0 and 255, inclusive).  
`{g}`. The Green component of the color (a value between 0 and 255, inclusive).  
`{b}`. The Blue component of the color (a value between 0 and 255, inclusive).


#### `ColorReset_Fore`, `ColorReset_Back`
These properties return escape sequences to reset the console's foreground or 
background color.

#### Underline
Returns a `string` containing an escape sequence to start underlining.

#### UnderlineReset
Returns a `string` containing an escape sequence to reset underlining.

#### Reverse
Returns a `string` containing an escape sequence to start reverse text 
(the foreground and background colors are "reversed").

#### ReverseReset
Returns a `string` containing an escape sequence to reset reverse text.

#### ResetAll
Returns a `string` containing the escape sequence to reset all formatting

#### Cursor_Show, Cursor_Hide
These escape sequences control cursor visibility.

#### Cursor_Blink, Cursor_Steady
These escape sequences control cursor appearance -- either blinking or steady.

#### Cursor_Shape
This property returns an escape sequence "template", which has placeholders 
that you can replace with values to make a complete escape sequence to 
control the curor's shape.

**Placeholders**  
`{shape}`. The cursor shape. Replace with a value from the `CursorShape` enum.  


#### Down1Left1
An escape sequence that moves the cursor down one row and left one column 
when written to the console.

#### ClearConsoleBuffer
An escape sequence that clears the console buffer. 

### Methods
#### GetForeColorSequence(System.Drawing.Color)
Returns a string containing an escape sequence to begin a foreground color. 
The color is passed as an argument to the method in the form of a 
`System.Drawing.Color` structure.

#### GetBackColorSequence(System.Drawing.Color)
Returns a string containing an escape sequence to begin a background color. 
The color is passed as an argument to the method in the form of a 
`System.Drawing.Color` structure.


### Examples
```c#
Color hot = Color.Red;
Color grn = Color.LimeGreen;
string clrES = EscapeCodes.Color;
string ESHotFore = clrES.Replace("{cs}", ColorSpace.fore.ToString("D"))
	.Replace("{r}", hot.R.ToString())
	.Replace("{g}", hot.G.ToString())
	.Replace("{b}", hot.B.ToString());
string ESCoolFore = EscapeCodes.GetForeColorSequence(Color.DodgerBlue);
WL($"{ESHotFore}Red Hot\r\n{ESCoolFore}Cool To The Touch{EscapeCodes.ColorReset_Fore}\r\nBoring");

string ESGBack = clrES.Replace("{cs}", ColorSpace.back.ToString("D"))
	.Replace("{r}", grn.R.ToString())
	.Replace("{g}", grn.G.ToString())
	.Replace("{b}", grn.B.ToString());
ESCoolFore = EscapeCodes.GetForeColorSequence(Color.Blue);
string ESBlueBack = EscapeCodes.GetBackColorSequence(Color.Blue);
WL($"{ESCoolFore}{ESGBack}Blue In Green{EscapeCodes.ColorReset_Back}{EscapeCodes.ColorReset_Fore}");

WL($"{EscapeCodes.Reverse}Reverse{EscapeCodes.ReverseReset}\r\n{EscapeCodes.Underline}Underline{EscapeCodes.UnderlineReset}");
		
WL($"{EscapeCodes.Underline}{ESHotFore}{ESBlueBack}  Combo  {EscapeCodes.ColorReset_Back}{EscapeCodes.ColorReset_Fore}{EscapeCodes.UnderlineReset}");
```

![Example - Escape Sequences](https://raw.githubusercontent.com/StrickTechnologies/Strick.PlusCon/master/SampleImages/ex_esc_1.png)
