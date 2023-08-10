using System;

using static Strick.PlusCon.Helpers;


namespace Strick.PlusCon;


/// <summary>
/// Shortcuts and helper methods for positioning the <see cref="Console"/>'s cursor and controlling its size/shape.
/// </summary>
public static class Cursor
{
	//https://learn.microsoft.com/en-us/windows/console/console-virtual-terminal-sequences#simple-cursor-positioning
	//https://msdn.microsoft.com/library/cc722862.aspx

	#region VERTICAL MOVEMENT

	/// <summary>
	/// Moves the cursor position to the top row of the <see cref="Console"/> buffer. 
	/// The cursor's horizontal position is NOT changed.
	/// </summary>
	public static void MoveTop() => Console.CursorTop = 0;

	/// <summary>
	/// Moves the cursor position to the bottom row of the <see cref="Console"/> buffer.
	/// The cursor's horizontal position is NOT changed.
	/// </summary>
	public static void MoveBottom() => Console.CursorTop = Console.BufferHeight - 1;


	/// <summary>
	/// Moves the cursor position vertically (up -- toward the top, or down -- toward the bottom) 
	/// within the <see cref="Console"/> buffer by the number of rows indicated 
	/// by the <paramref name="rows"/> argument. 
	/// <para>See the <paramref name="rows"/> parameter for specifics on how its value is interpreted. 
	/// Also see <see cref="MoveUp(int)"/> and <see cref="MoveDown(int)"/> for additional information.</para>
	/// The cursor's horizontal position is NOT changed.
	/// </summary>
	/// <param name="rows">An integer value indicating the number of rows to move the cursor:
	/// <list type="bullet">
	/// <item>If <paramref name="rows"/> is <b>positive</b>, the cursor is moved down (toward the bottom)</item>
	/// <item>If <paramref name="rows"/> is <b>negative</b>, the cursor is moved up (toward the top)</item>
	/// <item>If <paramref name="rows"/> is 0, the cursor's position is NOT changed</item>
	/// </list>
	/// </param>
	public static void MoveVertical(int rows)
	{
		if (rows < 0)
		{ MoveUp(Math.Abs(rows)); }

		if (rows > 0)
		{ MoveDown(rows); }
	}

	/// <summary>
	/// Moves the cursor position up (toward the top) within the <see cref="Console"/> buffer 
	/// by one row. 
	/// <para>If the cursor's current row is 0, the cursor's position is NOT changed.</para>
	/// The cursor's horizontal position is NOT changed.
	/// </summary>
	public static void MoveUp() => MoveUp(1);

	/// <summary>
	/// Moves the cursor position up (toward the top) within the <see cref="Console"/> buffer 
	/// by the number of rows indicated by the <paramref name="rows"/> argument. 
	/// <para>If <paramref name="rows"/> is &gt; the cursor's current row, it is moved to the top 
	/// of the buffer (row 0).</para>
	/// The cursor's horizontal position is NOT changed.
	/// </summary>
	/// <param name="rows">A positive value indicating the number of rows to move up. An exception is thrown if less than 0.</param>
	/// <exception cref="ArgumentOutOfRangeException"></exception>
	public static void MoveUp(int rows)
	{
		if (rows < 0)
		{ throw new ArgumentOutOfRangeException(nameof(rows), "Must be >= 0"); }

		if (rows == 0) //nothing to do here, but don't want an exception
		{ return; }

		if (Console.CursorTop == 0) //already at the top, nowhere else to go
		{ return; }

		if (rows > Console.CursorTop)
		{ MoveTop(); }
		else
		{ Console.CursorTop -= rows; }
	}

	/// <summary>
	/// Moves the cursor position down (toward the bottom) within the <see cref="Console"/> buffer 
	/// by one row. 
	/// <para>If the cursor's current row is the bottom row, the console is scrolled up by one row.</para>
	/// The cursor's horizontal position is NOT changed.
	/// </summary>
	public static void MoveDown() => MoveDown(1);

	/// <summary>
	/// Moves the cursor position down (toward the bottom) within the <see cref="Console"/> buffer 
	/// by the number of rows indicated by the <paramref name="rows"/> argument. 
	/// <para>If the downward movement would result in the cursor position being below the 
	/// bottom of the buffer, the console is scrolled up by the appropriate number of rows.</para>
	/// The cursor's horizontal position is NOT changed.
	/// </summary>
	public static void MoveDown(int rows)
	{
		if (rows < 0)
		{ throw new ArgumentOutOfRangeException(nameof(rows), "Must be > 0"); }

		if (rows == 0) //nothing to do here, but don't want an exception
		{ return; }


		//use newline char so screen scrolls when at the bottom
		W(new string('\n', rows));
	}

	#endregion VERTICAL MOVEMENT


	#region SIZE/SHAPE

	/// <summary>
	/// Shows the cursor
	/// </summary>
	public static void Show() => W(EscapeCodes.Cursor_Show);

	/// <summary>
	/// Shows or Hides the cursor based on the value of the <paramref name="show"/> argument.
	/// </summary>
	/// <param name="show">A boolean value indicating whether or not to show the cursor. 
	/// true=show, false=hide</param>
	public static void Show(bool show)
	{
		if (show)
		{ Show(); }
		else
		{ Hide(); }
	}


	/// <summary>
	/// Hides the cursor
	/// </summary>
	public static void Hide() => W(EscapeCodes.Cursor_Hide);

	/// <summary>
	/// Hides or Shows the cursor based on the value of the <paramref name="hide"/> argument.
	/// </summary>
	/// <param name="hide">A boolean value indicating whether or not to hide the cursor. 
	/// true=hide, false=show</param>
	public static void Hide(bool hide)
	{
		if (hide)
		{ Hide(); }
		else
		{ Show(); }
	}


	/// <summary>
	/// Starts blinking the cursor
	/// </summary>
	public static void Blink() => W(EscapeCodes.Cursor_Blink);

	/// <summary>
	/// Starts or stops blinking the cursor based on the value of the <paramref name="blink"/> argument.
	/// </summary>
	/// <param name="blink">A boolean value indicating whether or not to blink the cursor. 
	/// true=blink, false=do not blink (steady)</param>
	public static void Blink(bool blink)
	{
		if (blink)
		{ Blink(); }
		else
		{ Steady(); }
	}


	/// <summary>
	/// Stops blinking the cursor
	/// </summary>
	public static void Steady() => W(EscapeCodes.Cursor_Steady);

	/// <summary>
	/// Stops or starts blinking the cursor based on the value of the <paramref name="steady"/> argument.
	/// </summary>
	/// <param name="steady">A boolean value indicating whether or not to blink the cursor. 
	/// true=do not blink (steady), false=blink</param>
	public static void Steady(bool steady)
	{
		if (steady)
		{ Steady(); }
		else
		{ Blink(); }
	}


	/// <summary>
	/// Sets the cursor to the shape specifed by the <paramref name="shape"/> argument.
	/// </summary>
	public static void Shape(CursorShape shape)
	{
		if (!Enum.IsDefined(typeof(CursorShape), shape))
		{ throw new ArgumentException("Invalid cursor shape", nameof(shape)); }

		W(EscapeCodes.Cursor_Shape.Replace("{shape}", shape.AsString()));
	}


	//These are only supported in windows.
	//  visibility is handled with escape sequences (Show, Hide methods).
	//  skip Size for now. Might revisit later, but it's not really all that useful anyway.
	//public static void Size(int size) => Console.CursorSize = size;
	//public static bool Visible
	//{
	//	get=> Console.CursorVisible;
	//	set => Console.CursorVisible = value;
	//}

	#endregion SIZE/SHAPE
}

/// <summary>
/// Values used to create escape sequences for setting the cursor shape.
/// </summary>
public enum CursorShape
{
	/// <summary>
	/// Default cursor shape configured by the user
	/// </summary>
	Default = 0,


	/// <summary>
	/// Blinking block cursor shape
	/// </summary>
	Block_Blink = 1,

	/// <summary>
	/// Steady block cursor shape
	/// </summary>
	Block_Steady = 2,

	/// <summary>
	/// Blinking underline cursor shape
	/// </summary>
	Underline_Blink = 3,

	/// <summary>
	/// Steady underline cursor shape
	/// </summary>
	Underline_Steady = 4,

	/// <summary>
	/// Blinking bar cursor shape
	/// </summary>
	Bar_Blink = 5,

	/// <summary>
	/// Steady bar cursor shape
	/// </summary>
	Bar_Steady = 6
}
