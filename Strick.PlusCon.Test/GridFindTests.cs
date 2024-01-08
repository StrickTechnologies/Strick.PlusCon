using Strick.PlusCon.Models;


namespace Strick.PlusCon.Test;


[TestClass]
public class GridFindTests
{
	private static Grid grid = null!;

	#region INIT

	[ClassInitialize]
	public static void Init(TestContext context)
	{
		SearchGrid();
	}

	private static void SearchGrid()
	{
		grid = new Grid();
		grid.Columns.Add();
		grid.Columns.Add();
		grid.Columns.Add();

		grid.AddRow("row1 col1", "row1 col2", "row1 col3");
		grid.AddRow("row2 c1", "row2 c2", "row2 c3");
		grid.AddRow("r3 col1", "r3 col2", "r3 col3");
		grid.AddRow("r4 c1", "r4 c2", "r4 c3");
		grid.AddRow("r5 c1", "", null);
		GridTests.CheckGridState(grid, 3, 5);
	}

	#endregion INIT


	[TestMethod]
	public void ColumnFind()
	{
		GridColumn col1 = grid.Columns[0];
		GridColumn col2 = grid.Columns[1];
		GridColumn col3 = grid.Columns[2];

		CheckColumnFind(col1, "foo", SearchType.Contains);
		CheckColumnFind(col1, "foo", SearchType.StartsWith);
		CheckColumnFind(col1, "foo", SearchType.EndsWith);
		CheckColumnFind(col1, "foo", SearchType.Equals);
		CheckColumnFind(col1, "c1", SearchType.Contains, grid.Rows[1].Cells[0], grid.Rows[3].Cells[0], grid.Rows[4].Cells[0]);
		CheckColumnFind(col1, "c1", SearchType.EndsWith, grid.Rows[1].Cells[0], grid.Rows[3].Cells[0], grid.Rows[4].Cells[0]);
		CheckColumnFind(col1, "c1", SearchType.Equals);
		CheckColumnFind(col1, "c1", SearchType.StartsWith);
		CheckColumnFind(col1, "col1", SearchType.Contains, grid.Rows[0].Cells[0], grid.Rows[2].Cells[0]);
		CheckColumnFind(col1, "col1", SearchType.EndsWith, grid.Rows[0].Cells[0], grid.Rows[2].Cells[0]);
		CheckColumnFind(col1, "col1", SearchType.StartsWith);
		CheckColumnFind(col1, "col1", SearchType.Equals);
		CheckColumnFind(col1, "col", SearchType.Contains, grid.Rows[0].Cells[0], grid.Rows[2].Cells[0]);
		CheckColumnFind(col1, "col", SearchType.Equals);
		CheckColumnFind(col1, "col", SearchType.StartsWith);
		CheckColumnFind(col1, "col", SearchType.EndsWith);
		CheckColumnFind(col2, "r4 c2", SearchType.Contains, grid.Rows[3].Cells[1]);
		CheckColumnFind(col2, "r4 c2", SearchType.Contains, grid.Rows[3].Cells[1]);
		CheckColumnFind(col2, "r4 c2", SearchType.Equals, grid.Rows[3].Cells[1]);
		CheckColumnFind(col2, "r4 c2", SearchType.StartsWith, grid.Rows[3].Cells[1]);
		CheckColumnFind(col2, "r4 c2", SearchType.EndsWith, grid.Rows[3].Cells[1]);
		CheckColumnFind(col3, "col3", SearchType.Contains, grid.Rows[0].Cells[2], grid.Rows[2].Cells[2]);
		CheckColumnFind(col3, "col3", SearchType.EndsWith, grid.Rows[0].Cells[2], grid.Rows[2].Cells[2]);
		CheckColumnFind(col3, "col3", SearchType.StartsWith);
		CheckColumnFind(col3, "col3", SearchType.Equals);
		CheckColumnFind(col3, "row", SearchType.Contains, grid.Rows[0].Cells[2], grid.Rows[1].Cells[2]);
		CheckColumnFind(col3, "row", SearchType.StartsWith, grid.Rows[0].Cells[2], grid.Rows[1].Cells[2]);
		CheckColumnFind(col3, "row", SearchType.EndsWith);
		CheckColumnFind(col3, "row", SearchType.Equals);

		CheckColumnFind(col3, "", SearchType.Contains);
		CheckColumnFind(col2, "", SearchType.Contains, grid.Rows[4].Cells[1]);
		CheckColumnFind(col2, null, SearchType.Contains);
		CheckColumnFind(col3, null, SearchType.Contains, grid.Rows[4].Cells[2]);
		CheckColumnFind(col3, "", SearchType.StartsWith);
		CheckColumnFind(col2, "", SearchType.StartsWith, grid.Rows[4].Cells[1]);
		CheckColumnFind(col2, null, SearchType.StartsWith);
		CheckColumnFind(col3, null, SearchType.StartsWith, grid.Rows[4].Cells[2]);
		CheckColumnFind(col3, "", SearchType.EndsWith);
		CheckColumnFind(col2, "", SearchType.EndsWith, grid.Rows[4].Cells[1]);
		CheckColumnFind(col2, null, SearchType.EndsWith);
		CheckColumnFind(col3, null, SearchType.EndsWith, grid.Rows[4].Cells[2]);
		CheckColumnFind(col3, "", SearchType.Equals);
		CheckColumnFind(col2, "", SearchType.Equals, grid.Rows[4].Cells[1]);
		CheckColumnFind(col2, null, SearchType.Equals);
		CheckColumnFind(col3, null, SearchType.Equals, grid.Rows[4].Cells[2]);
	}

	[TestMethod]
	public void RowFind()
	{
		GridRow row1 = grid.Rows[0];
		GridRow row5 = grid.Rows[4];

		CheckRowFind(row1, "foo", SearchType.Contains);
		CheckRowFind(row1, "foo", SearchType.StartsWith);
		CheckRowFind(row1, "foo", SearchType.EndsWith);
		CheckRowFind(row1, "foo", SearchType.Equals);
		CheckRowFind(row1, "col2", SearchType.Contains, row1.Cells[1]);
		CheckRowFind(row1, "col2", SearchType.EndsWith, row1.Cells[1]);
		CheckRowFind(row1, "col2", SearchType.Equals);
		CheckRowFind(row1, "col2", SearchType.StartsWith);
		
		CheckRowFind(row5, "", SearchType.Contains, row5.Cells[1]);
		CheckRowFind(row5, "", SearchType.StartsWith, row5.Cells[1]);
		CheckRowFind(row5, "", SearchType.EndsWith, row5.Cells[1]);
		CheckRowFind(row5, "", SearchType.Equals, row5.Cells[1]);
		CheckRowFind(row5, null, SearchType.Contains, row5.Cells[2]);
		CheckRowFind(row5, null, SearchType.StartsWith, row5.Cells[2]);
		CheckRowFind(row5, null, SearchType.EndsWith, row5.Cells[2]);
		CheckRowFind(row5, null, SearchType.Equals, row5.Cells[2]);
	}

	[TestMethod]
	public void GridFind()
	{
		CheckCellsFind(grid.Find("", SearchType.Contains), grid.Rows[4].Cells[1]);
		CheckCellsFind(grid.Find("", SearchType.StartsWith), grid.Rows[4].Cells[1]);
		CheckCellsFind(grid.Find("", SearchType.EndsWith), grid.Rows[4].Cells[1]);
		CheckCellsFind(grid.Find("", SearchType.Equals), grid.Rows[4].Cells[1]);
		CheckCellsFind(grid.Find(null, SearchType.Contains), grid.Rows[4].Cells[2]);
		CheckCellsFind(grid.Find(null, SearchType.StartsWith), grid.Rows[4].Cells[2]);
		CheckCellsFind(grid.Find(null, SearchType.EndsWith), grid.Rows[4].Cells[2]);
		CheckCellsFind(grid.Find(null, SearchType.Equals), grid.Rows[4].Cells[2]);

		CheckCellsFind(grid.Find("c1", SearchType.Contains), grid.Rows[1].Cells[0], grid.Rows[3].Cells[0], grid.Rows[4].Cells[0]);
		CheckCellsFind(grid.Find("c1", SearchType.EndsWith), grid.Rows[1].Cells[0], grid.Rows[3].Cells[0], grid.Rows[4].Cells[0]);
		CheckCellsFind(grid.Find("c1", SearchType.StartsWith));
		CheckCellsFind(grid.Find("c1", SearchType.Equals));

		CheckCellsFind(grid.Find("3", SearchType.Contains), grid.Rows[0].Cells[2], grid.Rows[1].Cells[2], grid.Rows[2].Cells[0], grid.Rows[2].Cells[1], grid.Rows[2].Cells[2], grid.Rows[3].Cells[2]);
	}


	private static void CheckRowFind(GridRow row, string? searchText, SearchType searchType, params GridCell[] expectedCells)
	{
		Assert.IsNotNull(row);
		Assert.IsNotNull(expectedCells);

		int expectedCount = expectedCells.Count();

		//CELLS
		CheckCellsFind(row.Find(searchText, searchType), expectedCells);

		if (expectedCount > 0)
		{ Assert.AreEqual(expectedCells[0], row.FindFirst(searchText, searchType)); }
		else
		{ Assert.AreEqual(null, row.FindFirst(searchText, searchType)); }

		//COLUMNS
		CheckColumnsFind(row.FindColumns(searchText, searchType), expectedCells.Select(cell => cell.Column).ToArray());

		if (expectedCount > 0)
		{ Assert.AreEqual(expectedCells[0].Column, row.FindFirstColumn(searchText, searchType)); }
		else
		{ Assert.AreEqual(null, row.FindFirstColumn(searchText, searchType)); }
	}

	private static void CheckColumnFind(GridColumn col, string? searchText, SearchType searchType, params GridCell[] expectedCells)
	{
		Assert.IsNotNull(col);
		Assert.IsNotNull(expectedCells);

		int expectedCount = expectedCells.Count();

		//CELLS
		CheckCellsFind(col.Find(searchText, searchType), expectedCells);

		if (expectedCount > 0)
		{ Assert.AreEqual(expectedCells[0], col.FindFirst(searchText, searchType)); }
		else
		{ Assert.AreEqual(null, col.FindFirst(searchText, searchType)); }

		//ROWS
		CheckRowsFind(col.FindRows(searchText, searchType), expectedCells.Select(cell => cell.Row).ToArray());

		if (expectedCount > 0)
		{ Assert.AreEqual(expectedCells[0].Row, col.FindFirstRow(searchText, searchType)); }
		else
		{ Assert.AreEqual(null, col.FindFirstRow(searchText, searchType)); }
	}

	private static void CheckCellsFind(IEnumerable<GridCell> cells, params GridCell[] expectedCells)
	{
		Assert.AreEqual(expectedCells.Count(), cells.Count());
		for (int i = 0; i < cells.Count(); i++)
		{
			Assert.AreEqual(expectedCells[i], cells.ElementAt(i));
		}
	}

	private static void CheckColumnsFind(IEnumerable<GridColumn> columns, params GridColumn[] expectedColumns)
	{
		Assert.AreEqual(expectedColumns.Count(), columns.Count());
		for (int i = 0; i < columns.Count(); i++)
		{
			Assert.AreEqual(expectedColumns[i], columns.ElementAt(i));
		}
	}

	private static void CheckRowsFind(IEnumerable<GridRow> rows, params GridRow[] expectedRows)
	{
		Assert.AreEqual(expectedRows.Count(), rows.Count());
		for (int i = 0; i < rows.Count(); i++)
		{
			Assert.AreEqual(expectedRows[i], rows.ElementAt(i));
		}
	}
}
