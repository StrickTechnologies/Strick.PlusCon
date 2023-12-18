## `TextStyle` and `StyledText` classes
The `TextStyle` and `StyledText` classes provide a number of ways to more easily apply combinations of styling to any text.

### `TextStyle` class 
Provides methods and properties to apply foreground and background colors, 
color gradients, reverse and underline styling to text. Set the various properties as desired, 
then use the `StyleText` method to apply the styling to any text.

```c#
TextStyle ts = new(Color.White, Color.DodgerBlue, Color.White);
//same style, different content
foreach (string s in new[] { "content 1", "level 2" })
{ WL(ts.StyleText(s)); }

ts.ForeColor = Color.Red;
ts.Underline = true;
ts.ClearGradient();
WL(ts.StyleText("Hello World!"));

ts.Underline = false;
ts.ForeColor = null;
ts.BackColor = Color.White;
ts.SetGradientColors(Color.Black, Color.White);
WL(ts.StyleText("***fade-out***"));

ts.SetGradientColors(Color.White, Color.Black);
WL(ts.StyleText("***fade-in!***"));

ts.BackColor = null;
ts.Reverse = true;
ts.SetGradientColors(Color.SandyBrown, Color.FromArgb(3, 240, 165), Color.FromArgb(145, 193, 255));
WL(ts.StyleText("-- ** down on the beach ** --"));

ts.Reverse = false;
ts.Underline = true;
W(ts.StyleText("-- ** down on the beach ** --"));
```

![Example - TextStyle 1](https://raw.githubusercontent.com/StrickTechnologies/Strick.PlusCon/master/SampleImages/ex_textStyle_1.png)

### `StyledText` class 
Combines text content with a `TextStyle` object. 
For flexibility, both the `Text` and `Style` properties are read/write, 
and a `StyleText` method allows the styling to be applied to any text. 
A number of constructor overloads allow for flexible instantiation.

```c#
StyledText st = new("Hello World!", Color.Blue);

//same content, different styling
foreach (Color c in ColorUtilities.GetGradientColors(Color.FromArgb(0, 255, 0), Color.FromArgb(0, 128, 0), 4))
{
	st.Style.BackColor = c;
	WL(st.TextStyled);
}
//same styling, alternate content
WL(st.StyleText("Blue in Green"));

//back to default
st.Style = new();
st.Text = "Default styling";
WL(st.TextStyled);

//different content, different styling
st.Style.BackColor = Color.DarkGray;
st.Style.ForeColor = Color.White;
st.Text = "(not) " + st.Text;
WL(st.TextStyled);

st = new StyledText("-- ** down on the beach ** --", Color.SandyBrown, Color.FromArgb(3, 240, 165), Color.FromArgb(145, 193, 255));
WL(st.TextStyled);
```

![Example - TextStyle 1](https://raw.githubusercontent.com/StrickTechnologies/Strick.PlusCon/master/SampleImages/ex_styledText_1.png)
