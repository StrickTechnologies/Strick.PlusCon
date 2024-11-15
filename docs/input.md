## `Input` class

Provides a collection of static methods used to collect strongly typed values from the keyboard. The specific types include numerics (e.g. `int`, `decimal`), dates, times, strings and chars.

It also has an easily extensible validation mechanism that allows entered values to be filtered against ranges (e.g. min/max values, min/max length for strings) and other conditions.

Each method has multiple overloads to keep things quick and easy. Use the overload that accepts only a `Prompt` argument when any value is acceptable. Or pass an `InputArguments` object when more advanced validation is needed.

Prompts can always be null or an empty string if no prompt is desired. The cursor will be positioned directly after the prompt while awaiting keyboard input.


### `InputArguments` class
This is an abstract base class. Several methods in the `Input` class accept an object derived from this base class.

#### `Prompt` (`string?`) property
A nullable string that represents the prompt to be displayed.

#### `PromptStyle` (`TextStyle?`) property
Styling that is applied to the prompt. If null, no styling is applied to the prompt.


### `Ch` method
Prompts for entry of a single `char`. Returns a `ConsoleKeyInfo` structure whose `KeyChar` property contains the key that was pressed.

#### `InputArgumentsCh` class
Represents arguments used by the `Ch` method to collect a `char` value from the keyboard. The arguments that contol how the input is collected and validated.

##### `Allowed` (`List<char>`) property.
The acceptable values - only values in the collection are accepted. If the collection is empty (the default), any value will be accepted.


### `Any` method
Prompts for input of any key. Returns a `ConsoleKeyInfo` structure whose `KeyChar` property contains the key that was pressed. Any key is accepted. 


### `YN` method
Prompts for input of a single char. The entered char must be one of Y, y, N, n.


### `Number<T>` method
Prompts for the entry of a numeric value of a specific type.

The type is specified by the type parameter `<T>`, which can be any `struct` type that implements the `INumber<T>` interface. This includes all numeric value types.

#### `InputArgumentsNumber<T>` class
Represents arguments used by the `Input.Number<T>()` method to collect a numeric value from the user.

The type parameter `T` must be any `struct` type that implements the `INumber<TSelf>` interface.

##### `Min` (`T?`) property
The minimum value that is acceptable. If null, there is no minimum value.
The minimum acceptable value. If null (the default), no minimum is checked. 

##### `Max` (`T?`) property
The maximum value that is acceptable. If null, there is no maximum value.
The maximum acceptable value. If null (the default), no maximum is checked.


### `Text` method
Prompts for the entry of a text value. Returns a `string` containing the entered text, or null if the user presses only the enter key (without entering anything).

#### `InputArgumentsText` class
Represents arguments used by the `Input.Text()` method to collect a `string` value from the user.

##### `MinLength` (`int`) property
If non-null, represents the minimum length for the entered string value. Must be zero or greater, otherwise an `ArgumentOutOfRangeException` is thrown. If null, the minimum length is not checked.

##### `MaxLength` (`int`) property
If non-null, represents the maximum length for the entered string value. Must be greater than zero, otherwise an `ArgumentOutOfRangeException` is thrown. If null, the maximum length is not checked.


### `Date` method
Prompts for the entry of a date value. Returns a `DateOnly?` structure containing the date value entered, or null if the enter key is pressed (without entering a date).

#### `InputArgumentsDate` class
Represents arguments used by the `Input.Date()` method to collect a date (`DateOnly`) value from the user.

##### `Min` (`DateOnly?`) property
The minimum time value that is acceptable. If null, there is no minimum value.

##### `Max` (`DateOnly?`) property
The maximum time value that is acceptable. If null, there is no maximum value.

##### `ParseFunction` (`InputEntryParseDelegate<DateOnly>`) property
A delegate that is used to parse the string entered by the user into a `DateOnly` object. The default is the `DateOnly.TryParse(string?, out DateOnly)` method. This can be set to a custom function to accomodate additional specific parsing requirements.


### `Time` method
Prompts for the entry of a time (time of day) value. Returns a `TimeOnly?` structure containing the time value entered, or null if the enter key is pressed (without entering a time).

#### `InputArgumentsTime` class
Represents arguments used by the `Input.Time()` method to collect a time of day (`TimeOnly`) value from the user.

##### `Min` (`TimeOnly?`) property
The minimum time value that is acceptable. If null, there is no minimum value.

##### `Max` (`TimeOnly?`) property
The maximum time value that is acceptable. If null, there is no maximum value.

##### `ParseFunction` (`InputEntryParseDelegate<TimeOnly>`) property
A delegate that is used to parse the string entered by the user into a `TimeOnly` object. The default is the `TimeOnly.TryParse(string?, out TimeOnly)` method. This can be set to a custom function to accomodate additional specific parsing requirements.


### `Select<T>` method
Presents a list of options and allows one to be selected. Returns the selected option, or null if escape is pressed. 

The options are of the type specified by the type parameter `<T>`. There are no constraints on the type, so any object can be used here. The value returned by the `ToString()` method is used to display each option.

#### `InputArgumentsSelect<T>` class

##### `Options` (`IReadOnlyList<T>`) property
The options that are presented to the user. The collection must be non-null and contain at least **two** elements.

##### `SelectNextKeys` (`List<char>`) property
A collection of char values that can be used to move to the next option when the user presses the key. Default vales are space (`ConsoleKey.Spacebar`), right arrow (`ConsoleKey.RightArrow`), and down arrow (`ConsoleKey.DownArrow`)

##### `SelectPreviousKeys` (`List<char>`) property
A collection of char values that can be used to move to the previous option when the user presses the key. Default values are left arrow (`ConsoleKey.LeftArrow`), and up arrow (`ConsoleKey.UpArrow`).

##### `SelectedOption` (`T?`) property
The option selected by the user.

##### `Wrap` (`bool`) property
Indicates whether or not to "wrap" the displayed option. If true, pressing any key in the `SelectNextKeys` collection when the last option is displayed will "wrap" to the first option, and pressing any key in the `SelectPreviousKeys` collection when the first option is displayed  will "wrap" to the last option. If false, these keypresses are ignored.

##### `SelectionOptionStyle` (TextStyle) property
A `TextStyle` object used to format the options when displayed for selection.


### `SelectYesNo` method
Shows Yes and No options, allowing one to be selected. Returns a boolean? indicating whether Yes (true) or No (false) was selected. If escape is pressed, null is returned.
