using System.Drawing;

using Strick.PlusCon.Models;

using static Strick.PlusCon.Helpers;


namespace Strick.PlusCon.Test.Models.Examples;


internal class DocSample
{
	public DocSample(string name, string header, Action action) : this(name, header, null, action) { }

	public DocSample(string name, string header, TextStyle? headerStyle, Action action)
	{
		Name = name;
		Action = action;

		if (headerStyle == null)
		{ headerStyle = new TextStyle(docHdFore, docHdBack); }
		Header = new StyledText(header, headerStyle);
	}


	private static readonly Color docHdFore = Color.White;
	private static readonly Color docHdBack = Color.Blue;


	public string Name { get; }

	public StyledText Header { get; }

	public Action Action { get; }

	public char Key { get; init; }


	public void Show(bool wait = false, bool clear = false)
	{
		if (clear)
		{
			WL(EscapeCodes.ResetAll);
			CLS();
		}

		Action();

		ShowHd();

		if (wait)
		{ RK(); }
	}

	private void ShowHd()
	{
		Console.SetCursorPosition(Console.WindowWidth - Header.Text.Length, 0);
		W(Header.TextStyled);
	}
}
