## Cursor Utilities

The `Cursor` static class has methods that allow you to move or position 
the cursor, and to control its shape, visibility and other appearance 
attributes.

### Move & position

#### MoveTop
Moves the cursor position to the top row of the console buffer. 

#### MoveBottom
Moves the cursor position to the bottom row of the console buffer.

#### MoveVertical
Moves the cursor position vertically (up -- toward the top, or down -- toward the bottom) 
within the <see cref="Console"/> buffer by the number of rows indicated 
by the "rows" argument.

The "rows parameter is an integer value indicating the number of rows to move the 
cursor, as follows:
* A **positive** value moves the cursor down (toward the bottom)
* A **negative** value moves the cursor up (toward the top)
* The cursor's position is NOT changed if the value is 0.

Also see [MoveUp](#moveup) and [MoveDown](#movedown) below for additional information.

#### MoveUp
Moves the cursor position up (toward the top) within the console buffer 
by the number of rows indicated by the "rows" argument 
(or by one for the overload that takes no argument). 

If "rows" is > the cursor's current row, it is moved to the top 
of the buffer (row 0).

#### MoveDown
Moves the cursor position down (toward the bottom) within the console buffer 
by the number of rows indicated by the "rows" argument 
(or by one for the overload that takes no argument). 

If the downward movement would result in the cursor position being below the 
bottom of the buffer, the console is scrolled up by the appropriate number of rows.

### Shape, visibility, appearance 

#### Show
Shows the cursor.

#### Show(bool)
Shows or Hides the cursor based on the boolean argument.

#### Hide
Hides the cursor.

#### Hide(bool)
Hides or Shows the cursor based on the boolean argument.

#### Blink
Starts blinking the cursor.

#### Blink(bool)
Starts or stops blinking the cursor based on the boolean argument.

#### Steady
Stops blinking the cursor.

#### Steady(bool)
Stops or starts blinking the cursor based on the boolean argument.

#### Shape
Sets the cursor to the shape specifed by the "shape" argument. 
The argument is a value from the `CursorShape` enum.
