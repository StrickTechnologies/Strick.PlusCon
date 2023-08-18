using System;
using System.Drawing;


namespace Strick.PlusCon.Models;


/// <summary>
/// Represents the size of the console window and buffer
/// <para><b>Note: Some/all of the features in this class may work only on Windows. It has not been tested on other platforms.</b></para>
/// </summary>
public class ConsoleSize
{
	/// <summary>
	/// Creates a new instance with the console's current window and buffer sizes stored 
	/// in the <see cref="WindowSize"/> and <see cref="BufferSize"/> properties.
	/// </summary>
	public ConsoleSize()
	{
		WindowSize = new(Console.WindowWidth, Console.WindowHeight);
		BufferSize = new(Console.BufferWidth, Console.BufferHeight);
	}

	/// <summary>
	/// <inheritdoc cref="ConsoleSize()"/>
	/// </summary>
	/// <param name="clearBufferOnSet"><inheritdoc cref="ClearBufferOnSet" path="/summary"/></param>
	public ConsoleSize(bool clearBufferOnSet) : this()
	{
		ClearBufferOnSet = clearBufferOnSet;
	}

	/// <summary>
	/// <inheritdoc cref="ConsoleSize()"/>
	/// Also sets the size of both the console window and buffer 
	/// to the values specified by the <paramref name="newWindowAndBufferSize"/> argument.
	/// </summary>
	/// <param name="newWindowAndBufferSize">The new size for the console window and buffer.</param>
	public ConsoleSize(Size newWindowAndBufferSize) : this(newWindowAndBufferSize, true) { }

	/// <summary>
	/// <inheritdoc cref="ConsoleSize(Size)"/>
	/// </summary>
	/// <param name="newWindowAndBufferSize"><inheritdoc cref="ConsoleSize(Size)" path="/param[@name='newWindowAndBufferSize']"/></param>
	/// <param name="clearBufferOnSet"><inheritdoc cref="ConsoleSize(bool)" path="/param[@name='clearBufferOnSet']"/></param>
	public ConsoleSize(Size newWindowAndBufferSize, bool clearBufferOnSet) : this(newWindowAndBufferSize, newWindowAndBufferSize, clearBufferOnSet) { }

	/// <summary>
	/// <inheritdoc cref="ConsoleSize()"/>
	/// Also sets the width and height of the console window to the values specified by
	/// the <paramref name="newWindowSize"/> argument, and the width and height of the
	/// console buffer to the values specified by the <paramref name="newBufferSize"/> argument.
	/// </summary>
	/// <param name="newWindowSize">The new size for the console window.</param>
	/// <param name="newBufferSize">The new size for the console buffer.</param>
	/// <param name="clearBufferOnSet"><inheritdoc cref="ConsoleSize(bool)" path="/param[@name='clearBufferOnSet']"/></param>
	public ConsoleSize(Size newWindowSize, Size newBufferSize, bool clearBufferOnSet) : this()
	{
		ClearBufferOnSet = clearBufferOnSet;
		Set(newWindowSize, newBufferSize, clearBufferOnSet);
	}


	/// <summary>
	/// The size of the console window
	/// </summary>
	public Size WindowSize { get; init; }

	/// <summary>
	/// The size of the console buffer
	/// </summary>
	public Size BufferSize { get; init; }

	/// <summary>
	/// Indicates whether or not to clear the console buffer when the console size is set from this object.
	/// </summary>
	public bool ClearBufferOnSet { get; set; } = false;


	/// <summary>
	/// Sets the the width and height of the console window to the values specified by the 
	/// <see cref="WindowSize"/> property of the <paramref name="size"/> parameter, and
	/// sets the the width and height of the console buffer to the values specified by the 
	/// <see cref="BufferSize"/> property of the <paramref name="size"/> parameter. 
	/// Clears the console buffer (or not) based on the <see cref="ConsoleSize.ClearBufferOnSet"/> 
	/// property of the <paramref name="size"/> argument.
	/// <para><b>Note: This only works on Windows. It has no effect on other platforms at this time.</b></para>
	/// </summary>
	public static void Set(ConsoleSize size)
	{
		if (OperatingSystem.IsWindows())
		{
			Console.SetWindowSize(size.WindowSize.Width, size.WindowSize.Height);
			Console.SetBufferSize(size.BufferSize.Width, size.BufferSize.Height);

			if (size.ClearBufferOnSet)
			{ ClearBuffer(); }
		}
	}


	/// <summary>
	/// Sets the the width and height of the console window and buffer to the values specified by the 
	/// <paramref name="windowAndBufferSize"/> argument.
	/// Also clears the console buffer.
	/// <para><b>Note: This only works on Windows. It has no effect on other platforms at this time.</b></para>
	/// </summary>
	public static void Set(Size windowAndBufferSize) => Set(windowAndBufferSize, true);

	/// <summary>
	/// Sets the the width and height of the console window and buffer to the values specified by the 
	/// <paramref name="windowAndBufferSize"/> argument.
	/// Also clears the console buffer.
	/// <para><b>Note: This only works on Windows. It has no effect on other platforms at this time.</b></para>
	/// </summary>
	public static void Set(Size windowAndBufferSize, bool clearBuffer) => Set(windowAndBufferSize, windowAndBufferSize, clearBuffer);

	/// <summary>
	/// Sets the width and height of the console window to the values specified by
	/// the <paramref name="windowSize"/> argument, and the width and height of the
	/// console buffer to the values specified by the <paramref name="bufferSize"/> argument. 
	/// Also clears the console buffer.
	/// <para><b>Note: This only works on Windows. It has no effect on other platforms at this time.</b></para>
	/// </summary>
	public static void Set(Size windowSize, Size bufferSize) => Set(windowSize, bufferSize, true);

	/// <summary>
	/// Sets the width and height of the console window to the values specified by
	/// the <paramref name="windowSize"/> argument, and the width and height of the
	/// console buffer to the values specified by the <paramref name="bufferSize"/> argument. 
	/// Clears the console buffer (or not) based on the value of the 
	/// <paramref name="clearBuffer"/> argument.
	/// <para><b>Note: This only works on Windows. It has no effect on other platforms at this time.</b></para>
	/// </summary>
	public static void Set(Size windowSize, Size bufferSize, bool clearBuffer) => Set(windowSize.Width, windowSize.Height, bufferSize.Width, bufferSize.Height, clearBuffer);


	/// <summary>
	/// Sets the the width and height of the console window and buffer to the values specified by the 
	/// <paramref name="width"/> and <paramref name="height"/> arguments, respectively. 
	/// Also clears the console buffer.
	/// <para><b>Note: This only works on Windows. It has no effect on other platforms at this time.</b></para>
	/// </summary>
	public static void Set(int width, int height) => Set(width, height, true);

	/// <summary>
	/// Sets the the width and height of the console window and buffer to the values specified by the 
	/// <paramref name="width"/> and <paramref name="height"/> arguments, respectively. 
	/// Clears the console buffer (or not) based on the value of the <paramref name="clearBuffer"/> argument.
	/// <para><b>Note: This only works on Windows. It has no effect on other platforms at this time.</b></para>
	/// </summary>
	public static void Set(int width, int height, bool clearBuffer) => Set(width, height, width, height, clearBuffer);

	/// <summary>
	/// Sets the width and height of the console window to the values specified by
	/// the <paramref name="windowWidth"/> and <paramref name="windowHeight"/> arguments, 
	/// and the width and height of the console buffer to the values specified by the 
	/// <paramref name="bufferWidth"/> and <paramref name="bufferHeight"/> arguments. 
	/// Also clears the console buffer.
	/// <para><b>Note: This only works on Windows. It has no effect on other platforms at this time.</b></para>
	/// </summary>
	public static void Set(int windowWidth, int windowHeight, int bufferWidth, int bufferHeight) => Set(windowWidth, windowHeight, bufferWidth, bufferHeight, true);

	/// <summary>
	/// Sets the width and height of the console window to the values specified by
	/// the <paramref name="windowWidth"/> and <paramref name="windowHeight"/> arguments, 
	/// and the width and height of the console buffer to the values specified by the 
	/// <paramref name="bufferWidth"/> and <paramref name="bufferHeight"/> arguments. 
	/// Clears the console buffer (or not) based on the value of the <paramref name="clearBuffer"/> argument.
	/// <para><b>Note: This only works on Windows. It has no effect on other platforms at this time.</b></para>
	/// </summary>
	public static void Set(int windowWidth, int windowHeight, int bufferWidth, int bufferHeight, bool clearBuffer)
	{
		if (OperatingSystem.IsWindows())
		{
			Console.SetWindowSize(windowWidth, windowHeight);
			Console.SetBufferSize(bufferWidth, bufferHeight);

			if (clearBuffer)
			{ ClearBuffer(); }
		}
	}


	/// <summary>
	/// Sets (restores) the width and height of the console window to the values specified by the 
	/// <see cref="WindowSize"/> property, and sets the the width and height of the console buffer 
	/// to the values specified by the <see cref="BufferSize"/> property. 
	/// Clears the console buffer (or not) based on the value of the <see cref="ClearBufferOnSet"/> 
	/// property.
	/// <para><b>Note: This only works on Windows. It has no effect on other platforms at this time.</b></para>
	/// </summary>
	public void Restore() => ConsoleSize.Set(this);


	/// <summary>
	/// Clear the scrollback buffer.
	/// <para>This may or may not work on platforms other than Windows. It has not been tested on other platforms.</para>
	/// </summary>
	public static void ClearBuffer()
	{
		//Clear the scrollback buffer.
		//This avoids having two extraneous columns and one row on the console screen
		//(which cannot be written to). This appears to happen when the console is
		//set to a smaller size (haven't yet verified that is consistent and exclusive).

		//https://github.com/dotnet/runtime/issues/28355
		//https://en.wikipedia.org/wiki/ANSI_escape_code#CSI_(Control_Sequence_Introducer)_sequences
		//related: https://stackoverflow.com/questions/62841038/console-cannot-fill-bottom-and-right-area

		Console.Write(EscapeCodes.ClearConsoleBuffer);
	}
}
