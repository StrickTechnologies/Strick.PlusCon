using System;
using System.Runtime.InteropServices;


namespace Strick.PlusCon;


/// <summary>
/// Console utility methods
/// </summary>
public static class ConsoleUtilities
{
	private static IntPtr GetStdOutHandle() => GetStdHandle(STD_OUTPUT_HANDLE);

	/// <summary>
	/// Enables the output console's screen buffer virtual terminal mode.
	/// </summary>
	/// <returns>A boolean indicating whether the operation was succesful or not. 
	/// If false is returned, the <see cref="LastError"/> property will contain the error message.</returns>
	public static bool EnableVirtualTerminal()
	{
		IntPtr handle = GetStdOutHandle();

		if (!GetConsoleMode(handle, out uint cMode))
		{
			LastError = "Unable to get output console mode";
			return false;
		}

		cMode |= ENABLE_VIRTUAL_TERMINAL_PROCESSING | DISABLE_NEWLINE_AUTO_RETURN;

		if (!SetConsoleMode(handle, cMode))
		{
			LastError = $"Unable to set output console mode, error code: {GetLastError()}";
			return false;
		}

		LastError = "";
		return true;
	}

	/// <summary>
	/// The last error that occured during the last operation. An empty string indicates the last operation completed successfully.
	/// </summary>
	public static string LastError { get; private set; } = "";


	#region REFERENCE

	//https://docs.microsoft.com/en-us/windows/console/getstdhandle
	//https://docs.microsoft.com/en-us/windows/console/setconsolemode

	//https://www.jerriepelser.com/blog/using-ansi-color-codes-in-net-console-apps/

	#endregion REFERENCE


	#region IMPORTS

	[DllImport("kernel32.dll")]
	private static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);

	[DllImport("kernel32.dll")]
	private static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);

	[DllImport("kernel32.dll", SetLastError = true)]
	private static extern IntPtr GetStdHandle(int nStdHandle);

	[DllImport("kernel32.dll")]
	private static extern uint GetLastError();

	#endregion IMPORTS


	#region CONSTANTS

	private const int STD_OUTPUT_HANDLE = -11;

	private const uint ENABLE_VIRTUAL_TERMINAL_PROCESSING = 0x0004;

	private const uint DISABLE_NEWLINE_AUTO_RETURN = 0x0008;

	#endregion CONSTANTS
}
