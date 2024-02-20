using System.Drawing;

using Strick.PlusCon.Models;

using static Strick.PlusCon.Helpers;


namespace Strick.PlusCon.Test.Models.Examples;


internal static class GridExamples
{
	static GridExamples()
	{
		Samples = new List<DocSample>()
		{
			new DocSample("grid1", "Example - Grid (1)", Ex_Grid_1),
			new DocSample("grid2", "Example - Grid (2)", Ex_Grid_2),
			new DocSample("grid3", "Example - Grid (3)", Ex_Grid_3),
		};

	}

	internal static List<DocSample> Samples;

	internal static string MenuTitle => "Grids";


	internal static void Ex_Grid_1()
	{
		Grid g = new();
		g.Title = new("Grid".SpaceOut());
		g.Subtitle = new("Example 1");

		g.Columns.Add("C 1");
		g.Columns.Add("Column 2", HorizontalAlignment.Center);
		g.Columns.Add("Col 3", HorizontalAlignment.Right);

		g.AddRow("r1-c1", "row1-c2", "row1-column3");
		g.AddRow("row2-column1", "row2-col2", "r2-col3");
		g.AddRow("r3-col1", "row3-column2", "r3-c3");

		g.Footer = new($"Total Count {g.RowCount}");
		g.FooterAlignment = HorizontalAlignment.Left;

		g.AddSeparatorRow('-');

		g.Show();
		RK();
	}

	internal static void Ex_Grid_2()
	{
		Grid g = new();
		g.Title = new("Grid".SpaceOut(), new TextStyle(Color.Silver, Color.Gray, Color.Silver) { BackColor = Color.LimeGreen, Reverse = true });
		g.Subtitle = new("Example 2 (Styling)", Color.LimeGreen, Color.Gray);

		//set cell/content styling for entire grid
		g.CellStyle = new(Color.DodgerBlue, Color.Silver);
		g.CellContentStyle = new(Color.Blue, Color.Silver);
		g.ColumnHeaderContentStyle.Underline = false;
		g.ColumnHeaderCellStyle.Underline = false;

		GridColumn col = g.Columns.Add("C 1");
		col.Header.HorizontalAlignment = HorizontalAlignment.Center; //override column header alignment

		col = g.Columns.Add("Column 2", HorizontalAlignment.Center);
		//override styling for column
		col.CellStyle = new() { BackColor = Color.DarkGray };

		g.Columns.Add("Col 3", HorizontalAlignment.Right);

		g.AddRow("r1-c1", "row1-c2", "row1-column3");
		GridRow row = g.AddRow("row2-column1", "row2-column2", "r2-col3");
		//override styling for row
		row.CellStyle = new(Color.White, Color.SlateGray);
		row.ContentStyle = new(Color.White, Color.SlateGray);
		//override styling for specific cell
		GridCell cell = row.Cells[2];
		cell.ContentStyle = new(Color.Red, Color.White);
		cell.CellStyle = new(Color.White, Color.SlateGray);

		g.AddRow("r3-col1", "row3-col2", "r3-c3");

		g.Footer = new("Total Count 3", Color.White, Color.SkyBlue, Color.White);
		g.Footer.Style.Reverse = true;
		g.Show();
		RK();
	}

	internal static void Ex_Grid_3()
	{
		Color text = Color.White;
		Color background = Color.FromArgb(64, 64, 64);
		Grid grid = new Grid();
		grid.ColumnHeaderContentStyle.BackColor = Color.Green;
		grid.ColumnHeaderContentStyle.Underline = false;
		grid.ColumnHeaderCellStyle.Underline = false;
		grid.ColumnHeaderCellStyle.BackColor = Color.LightGreen;
		grid.CellContentStyle.BackColor = Color.Silver;
		grid.CellStyle.BackColor = Color.SkyBlue;

		GridColumn col = grid.AddColumn();
		col.CellLayout.PaddingLeft = 1;
		col.CellLayout.PaddingLeftChar = 'p';
		col.CellLayout.PaddingRight = 1;
		col.CellLayout.PaddingRightChar = 'p';
		col.Header.FillerChar = 'f';

		GridRow row = grid.AddRow(" row 1 ");
		row = grid.AddRow(" row 2 ");
		row.Cells[0].HorizontalAlignment = HorizontalAlignment.Center;
		row = grid.AddRow(" row 3 ");
		row.Cells[0].HorizontalAlignment = HorizontalAlignment.Right;
		grid.AddRow(""); //"empty" (zero-length) content
		grid.AddRow(" ");
		grid.AddRow(); //no content
		grid.AddRow(" row 7 - wider ");

		col.Header.Content = Ruler.GetH(col.ContentWidth, colors: null);
		col.Cells.SetFillerChar('f');

		CLS(background);
		W(EscapeCodes.ColorReset_Back);
		grid.Show();

		int legendLeft = grid.Width + 2;
		Console.SetCursorPosition(legendLeft, 0);
		W("Legend", new TextStyle(Color.DodgerBlue, background) { Underline = true });
		Console.SetCursorPosition(legendLeft, 1);
		W(" ", text, Color.Black);
		W(" Margin", text, background);
		Console.SetCursorPosition(legendLeft, 2);
		W("p Padding", text, background);
		Console.SetCursorPosition(legendLeft, 3);
		W("f Filler", text, background);
		Console.SetCursorPosition(legendLeft, 4);
		W(" ", text, grid.ColumnHeaderContentStyle.BackColor);
		W(" Header content", text, background);
		Console.SetCursorPosition(legendLeft, 5);
		W(" ", text, grid.ColumnHeaderCellStyle.BackColor);
		W(" Header non content", text, background);
		Console.SetCursorPosition(legendLeft, 6);
		W(" ", text, grid.CellContentStyle.BackColor);
		W(" Cell content", text, background);
		Console.SetCursorPosition(legendLeft, 7);
		W(" ", text, grid.CellStyle.BackColor);
		W(" Cell non content", text, background);
		RK();
	}
}
