## Escape Sequences
An *escape sequence* is a series (or "sequence") of characters beginning with the 
escape character. Escape sequences are sent to the console to control various 
forms of styling.

## `EscapeCodes` class
The `EscapeCodes` static class contains a number of helpful properties and 
methods that allow you to do custom things with colors and other formatting.

### Properties

#### `Escape`
Returns a `char` representing the escape character. This can be useful in building 
your own esacpe sequences.

#### `Color`, `Color_Fore`, `Color_Back`
These properties return escape sequence "templates", which have placeholders that you 
can replace with color values to make a complete color escape sequence.

**Placeholders**  
`{cs}`. The color space. Replace with a value from the `ColorSpace` enum.  
`{r}`. The Red component of the color (a value between 0 and 255, inclusive).  
`{g}`. The Green component of the color (a value between 0 and 255, inclusive).  
`{b}`. The Blue component of the color (a value between 0 and 255, inclusive).

```
Color color = Color.Red;
string clrES = EscapeCodes.Color;
string ESRedFore = clrES.Replace("{cs}", ColorSpace.fore.ToString("D"))
	.Replace("{r}", color.R.ToString())
	.Replace("{g}", color.G.ToString())
	.Replace("{b}", color.B.ToString());
WL($"{ESRedFore}Red Hot{EscapeCodes.ColorReset_Fore}");
```


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