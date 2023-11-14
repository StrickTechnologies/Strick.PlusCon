using Strick.PlusCon.Models;


namespace Strick.PlusCon.Test.Models;


internal static class CursorUtil
{
	internal static Menu Menu
	{
		get
		{
			PCMenu testMenu = new PCMenu("Cursor Test Menu");
			testMenu.Options.Add(new("Show", '1', () => { Cursor.Show(); }));
			testMenu.Options.Add(new("Show - true", '2', () => { Cursor.Show(true); }));
			testMenu.Options.Add(new("Show - false (Hide)", '3', () => { Cursor.Show(false); }));

			testMenu.Options.Add(new MenuSeperator(""));
			testMenu.Options.Add(new("Hide", '4', () => { Cursor.Hide(); }));
			testMenu.Options.Add(new("Hide - true", '5', () => { Cursor.Hide(true); }));
			testMenu.Options.Add(new("Hide - false (Show)", '6', () => { Cursor.Hide(false); }));

			testMenu.Options.Add(new MenuSeperator(""));
			testMenu.Options.Add(new("Blink", '7', () => { Cursor.Blink(); }));
			testMenu.Options.Add(new("Blink - true", '8', () => { Cursor.Blink(true); }));
			testMenu.Options.Add(new("Blink - false (Steady)", '9', () => { Cursor.Blink(false); }));

			testMenu.Options.Add(new MenuSeperator(""));
			testMenu.Options.Add(new("Steady", 'A', () => { Cursor.Steady(); }));
			testMenu.Options.Add(new("Steady - true", 'B', () => { Cursor.Steady(true); }));
			testMenu.Options.Add(new("Steady - false (Blink)", 'C', () => { Cursor.Steady(false); }));


			PCMenu cursorMenu = new PCMenu("Cursor Menu");
			cursorMenu.Options.Add(new("Default", '0', () => { Cursor.Shape(CursorShape.Default); }));
			cursorMenu.Options.Add(new("Blinking Block", '1', () => { Cursor.Shape(CursorShape.Block_Blink); }));
			cursorMenu.Options.Add(new("Steady Block", '2', () => { Cursor.Shape(CursorShape.Block_Steady); }));
			cursorMenu.Options.Add(new("Blinking Underline", '3', () => { Cursor.Shape(CursorShape.Underline_Blink); }));
			cursorMenu.Options.Add(new("Steady Underline", '4', () => { Cursor.Shape(CursorShape.Underline_Steady); }));
			cursorMenu.Options.Add(new("Blinking Bar", '5', () => { Cursor.Shape(CursorShape.Bar_Blink); }));
			cursorMenu.Options.Add(new("Steady Bar", '6', () => { Cursor.Shape(CursorShape.Bar_Steady); }));
			cursorMenu.Options.Add(new("No-Blink (Steady)", 'N', () => { Cursor.Steady(); }));
			cursorMenu.Options.Add(new("Blink", 'B', () => { Cursor.Blink(); }));
			cursorMenu.Options.Add(new("Show", 'S', () => { Cursor.Show(); }));
			cursorMenu.Options.Add(new("Hide", 'H', () => { Cursor.Hide(); }));
			cursorMenu.Options.Add(new MenuSeperator(""));
			cursorMenu.Options.Add(new("Other Tests", 'T', testMenu));
			//cursorMenu.Options.Add(new("Size", 'Z', PromptCursorSize));

			return cursorMenu;
		}
	}
}
