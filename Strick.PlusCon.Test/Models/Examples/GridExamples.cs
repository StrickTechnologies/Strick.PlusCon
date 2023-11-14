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
		g.Show();
		RK();
	}

	internal static void Ex_Grid_2()
	{
		Grid g = new();
		g.Title = new("Grid".SpaceOut(), new(Color.Silver, Color.Gray, Color.Silver) { BackColor = Color.LimeGreen, Reverse = true });
		g.Subtitle = new("Example 2 (Styling)", new(Color.LimeGreen, Color.Gray));

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

		g.Footer = new("Total Count 3");
		g.Show();
		RK();
	}
}
