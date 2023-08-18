## Console Size

The `ConsoleSize` class provides a quick and easy method 
for saving/setting/resetting the size of the console window 
and buffer. 

Here's the concept: *Save the console size 
by creating a new instance of the `ConsoleSize` class. 
Resize the console window to the desired size (using one of 
`ConsoleSize`'s constructor overloads, or one of the 
overloads of the `Set` method). Do whatever work needs to 
be done at that size. Use the `Restore` method to restore 
the console window and buffer back to their original sizes.*

The default (parameterless) constructor will save the size 
of the console window and buffer. Other overloads take various 
arguments to additionally set the size of the console window 
and buffer.

If you don't need to save and later restore the console size, 
there are several overloads of the static `Set` method that 
allow the console and buffer sizes to be set without saving the 
current size.

The `ClearBufferOnSet` property allows you to indicate whether or not 
to clear the console buffer when the console size is set or 
reset from a `ConsoleSize` object. Some constructor and `Set` 
method overloads include `clearBufferOnSet` parameters to 
allow the clearing of the buffer to be controlled at each 
level.

**Note:** Some or all of the functionality in this class
may be available only on the Windows platform.
