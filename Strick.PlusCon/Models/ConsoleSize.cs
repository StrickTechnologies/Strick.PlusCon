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
	/// Creates a new instance based on the console's current window and buffer sizes
	/// </summary>
	public ConsoleSize()
	{
		WindowSize = new(Console.WindowWidth, Console.WindowHeight);
		BufferSize = new(Console.BufferWidth, Console.BufferHeight);
	}

	/// <summary>
	/// Creates a new instance with the height and width of both the <see cref="WindowSize"/> and <see cref="BufferSize"/> 
	/// properties set to the values from the <paramref name="windowAndBufferSize"/> parameter.
	/// </summary>
	public ConsoleSize(Size windowAndBufferSize)
	{
		WindowSize = windowAndBufferSize;
		BufferSize = windowAndBufferSize;
	}

	/// <summary>
	/// Creates a new instance with the height and width of the <see cref="WindowSize"/> 
	/// property set to the values from the <paramref name="windowSize"/> parameter, and the 
	/// height and width of the <see cref="BufferSize"/> property set to the values from 
	/// the <paramref name="bufferSize"/> parameter.
	/// </summary>
	public ConsoleSize(Size windowSize, Size bufferSize)
	{
		WindowSize = windowSize;
		BufferSize = bufferSize;
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
	/// Sets the the height and width of the console window to the values specified by the 
	/// <see cref="WindowSize"/> property of the <paramref name="size"/> parameter, and
	/// sets the the height and width of the console buffer to the values specified by the 
	/// <see cref="BufferSize"/> property of the <paramref name="size"/> parameter.
	/// <para><b>Note: This only works on Windows. It has no effect on other platforms at this time.</b></para>
	/// </summary>
	public static void Set(ConsoleSize size)
	{
		if (OperatingSystem.IsWindows())
		{
			Console.SetWindowSize(size.WindowSize.Width, size.WindowSize.Height);
			Console.SetBufferSize(size.BufferSize.Width, size.BufferSize.Height);
		}
	}

	/// <summary>
	/// Sets the the height and width of the console window and buffer to the values specified by the 
	/// <paramref name="size"/> parameter.
	/// <para><b>Note: This only works on Windows. It has no effect on other platforms at this time.</b></para>
	/// </summary>
	public static void Set(Size size)
	{
		if (OperatingSystem.IsWindows())
		{
			Console.SetWindowSize(size.Width, size.Height);
			Console.SetBufferSize(size.Width, size.Height);
		}
	}

	/// <summary>
	/// Sets the the height and width of the console window to the values specified by the 
	/// <see cref="WindowSize"/> property, and
	/// sets the the height and width of the console buffer to the values specified by the 
	/// <see cref="BufferSize"/> property.
	/// <para><b>Note: This only works on Windows. It has no effect on other platforms at this time.</b></para>
	/// </summary>
	public void SetConsoleSize() => ConsoleSize.Set(this);
}
