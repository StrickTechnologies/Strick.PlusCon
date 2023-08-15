## Ruler
The `Ruler` static class provides a way to create quick and easy 
"rulers" or "number lines" of any length. Use the `Get` method 
to create a Ruler.

```
WL(Ruler.Get(30));
WL("foo bar baz");
//Shows:
----?----1----?----2----?----3
foo bar baz
```

A ruler is made up of *"segments"* of ten characters. 
The first nine characters of each segment are specified by the `Segment` property. 
The tenth character of each segment is the *"tens" counter* (1 for 10, 2 for 20, etc.). 
The "tens" counter resets to zero at each hundred (100, 200).

The value of the `Segment` property can be changed to alter the appearance of Rulers. 
The segment can be set to any characters, but it must be exactly 9 characters in length 
(otherwise, an exception is thrown).

```
Ruler.Segment = "----*----";
WL(Ruler.Get(30));
WL("foo bar baz");
//Shows:
----*----1----*----2----*----3
foo bar baz
```

The `Colors` property allows colors to be applied to Rulers. 
A null value or an empty sequence is acceptable, and will result in 
rulers having no embeded color sequences. 
The colors default to a ten-color gradient from `Color.Gray` to `Color.White`.

See more about how the colors in the sequence are applied at 
[Other Formatting](otherFormatting.md#colorize).

The `Segment` and `Colors` can also be overridden using various overloads of 
the `Get` method. In these cases, the `Segment` or `Colors` apply only to that
particular call.
