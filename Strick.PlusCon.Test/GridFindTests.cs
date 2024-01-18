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

		CheckColumnFind(col1, new("foo", SearchType.Contains));
		CheckColumnFind(col1, new("foo", SearchType.StartsWith));
		CheckColumnFind(col1, new("foo", SearchType.EndsWith));
		CheckColumnFind(col1, new("foo", SearchType.Equals));
		CheckColumnFind(col1, new("c1", SearchType.Contains), grid.Rows[1].Cells[0], grid.Rows[3].Cells[0], grid.Rows[4].Cells[0]);
		CheckColumnFind(col1, new("c1", SearchType.EndsWith), grid.Rows[1].Cells[0], grid.Rows[3].Cells[0], grid.Rows[4].Cells[0]);
		CheckColumnFind(col1, new("c1", SearchType.Equals));
		CheckColumnFind(col1, new("c1", SearchType.StartsWith));
		CheckColumnFind(col1, new("col1", SearchType.Contains), grid.Rows[0].Cells[0], grid.Rows[2].Cells[0]);
		CheckColumnFind(col1, new("col1", SearchType.EndsWith), grid.Rows[0].Cells[0], grid.Rows[2].Cells[0]);
		CheckColumnFind(col1, new("col1", SearchType.StartsWith));
		CheckColumnFind(col1, new("col1", SearchType.Equals));
		CheckColumnFind(col1, new("col", SearchType.Contains), grid.Rows[0].Cells[0], grid.Rows[2].Cells[0]);
		CheckColumnFind(col1, new("col", SearchType.Equals));
		CheckColumnFind(col1, new("col", SearchType.StartsWith));
		CheckColumnFind(col1, new("col", SearchType.EndsWith));
		CheckColumnFind(col2, new("r4 c2", SearchType.Contains), grid.Rows[3].Cells[1]);
		CheckColumnFind(col2, new("r4 c2", SearchType.Contains), grid.Rows[3].Cells[1]);
		CheckColumnFind(col2, new("r4 c2", SearchType.Equals), grid.Rows[3].Cells[1]);
		CheckColumnFind(col2, new("r4 c2", SearchType.StartsWith), grid.Rows[3].Cells[1]);
		CheckColumnFind(col2, new("r4 c2", SearchType.EndsWith), grid.Rows[3].Cells[1]);
		CheckColumnFind(col3, new("col3", SearchType.Contains), grid.Rows[0].Cells[2], grid.Rows[2].Cells[2]);
		CheckColumnFind(col3, new("col3", SearchType.EndsWith), grid.Rows[0].Cells[2], grid.Rows[2].Cells[2]);
		CheckColumnFind(col3, new("col3", SearchType.StartsWith));
		CheckColumnFind(col3, new("col3", SearchType.Equals));
		CheckColumnFind(col3, new("row", SearchType.Contains), grid.Rows[0].Cells[2], grid.Rows[1].Cells[2]);
		CheckColumnFind(col3, new("row", SearchType.StartsWith), grid.Rows[0].Cells[2], grid.Rows[1].Cells[2]);
		CheckColumnFind(col3, new("row", SearchType.EndsWith));
		CheckColumnFind(col3, new("row", SearchType.Equals));

		CheckColumnFind(col3, new("", SearchType.Contains));
		CheckColumnFind(col2, new("", SearchType.Contains), grid.Rows[4].Cells[1]);
		CheckColumnFind(col2, new(null, SearchType.Contains));
		CheckColumnFind(col3, new(null, SearchType.Contains), grid.Rows[4].Cells[2]);
		CheckColumnFind(col3, new("", SearchType.StartsWith));
		CheckColumnFind(col2, new("", SearchType.StartsWith), grid.Rows[4].Cells[1]);
		CheckColumnFind(col2, new(null, SearchType.StartsWith));
		CheckColumnFind(col3, new(null, SearchType.StartsWith), grid.Rows[4].Cells[2]);
		CheckColumnFind(col3, new("", SearchType.EndsWith));
		CheckColumnFind(col2, new("", SearchType.EndsWith), grid.Rows[4].Cells[1]);
		CheckColumnFind(col2, new(null, SearchType.EndsWith));
		CheckColumnFind(col3, new(null, SearchType.EndsWith), grid.Rows[4].Cells[2]);
		CheckColumnFind(col3, new("", SearchType.Equals));
		CheckColumnFind(col2, new("", SearchType.Equals), grid.Rows[4].Cells[1]);
		CheckColumnFind(col2, new(null, SearchType.Equals));
		CheckColumnFind(col3, new(null, SearchType.Equals), grid.Rows[4].Cells[2]);

		CheckColumnFind(col1, new("col1", SearchType.Contains, StringComparison.Ordinal), grid.Rows[0].Cells[0], grid.Rows[2].Cells[0]);
		CheckColumnFind(col1, new("col1", SearchType.EndsWith, StringComparison.Ordinal), grid.Rows[0].Cells[0], grid.Rows[2].Cells[0]);
		CheckColumnFind(col1, new("col1", SearchType.StartsWith, StringComparison.Ordinal));
		CheckColumnFind(col1, new("col1", SearchType.Equals, StringComparison.Ordinal));
		CheckColumnFind(col1, new("Col1", SearchType.Contains, StringComparison.Ordinal));
		CheckColumnFind(col1, new("Col1", SearchType.EndsWith, StringComparison.Ordinal));
		CheckColumnFind(col1, new("Col1", SearchType.Contains, StringComparison.OrdinalIgnoreCase), grid.Rows[0].Cells[0], grid.Rows[2].Cells[0]);
		CheckColumnFind(col1, new("Col1", SearchType.EndsWith, StringComparison.OrdinalIgnoreCase), grid.Rows[0].Cells[0], grid.Rows[2].Cells[0]);

	}

	[TestMethod]
	public void RowFind()
	{
		GridRow row1 = grid.Rows[0];
		GridRow row5 = grid.Rows[4];

		CheckRowFind(row1, new("foo", SearchType.Contains));
		CheckRowFind(row1, new("foo", SearchType.StartsWith));
		CheckRowFind(row1, new("foo", SearchType.EndsWith));
		CheckRowFind(row1, new("foo", SearchType.Equals));
		CheckRowFind(row1, new("col2", SearchType.Contains), row1.Cells[1]);
		CheckRowFind(row1, new("col2", SearchType.EndsWith), row1.Cells[1]);
		CheckRowFind(row1, new("col2", SearchType.Equals));
		CheckRowFind(row1, new("col2", SearchType.StartsWith));

		CheckRowFind(row1, new("col2", SearchType.Contains, StringComparison.Ordinal), row1.Cells[1]);
		CheckRowFind(row1, new("col2", SearchType.EndsWith, StringComparison.Ordinal), row1.Cells[1]);
		CheckRowFind(row1, new("col2", SearchType.Equals, StringComparison.Ordinal));
		CheckRowFind(row1, new("col2", SearchType.StartsWith, StringComparison.Ordinal));
		CheckRowFind(row1, new("Col2", SearchType.Contains, StringComparison.Ordinal));
		CheckRowFind(row1, new("Col2", SearchType.EndsWith, StringComparison.Ordinal));
		CheckRowFind(row1, new("Col2", SearchType.Equals, StringComparison.Ordinal));
		CheckRowFind(row1, new("Col2", SearchType.StartsWith, StringComparison.Ordinal));
		CheckRowFind(row1, new("Col2", SearchType.Contains, StringComparison.OrdinalIgnoreCase), row1.Cells[1]);
		CheckRowFind(row1, new("Col2", SearchType.EndsWith, StringComparison.OrdinalIgnoreCase), row1.Cells[1]);
		CheckRowFind(row1, new("Col2", SearchType.Equals, StringComparison.OrdinalIgnoreCase));
		CheckRowFind(row1, new("Col2", SearchType.StartsWith, StringComparison.OrdinalIgnoreCase));
		
		CheckRowFind(row5, new("", SearchType.Contains), row5.Cells[1]);
		CheckRowFind(row5, new("", SearchType.StartsWith), row5.Cells[1]);
		CheckRowFind(row5, new("", SearchType.EndsWith), row5.Cells[1]);
		CheckRowFind(row5, new("", SearchType.Equals), row5.Cells[1]);
		CheckRowFind(row5, new(null, SearchType.Contains), row5.Cells[2]);
		CheckRowFind(row5, new(null, SearchType.StartsWith), row5.Cells[2]);
		CheckRowFind(row5, new(null, SearchType.EndsWith), row5.Cells[2]);
		CheckRowFind(row5, new(null, SearchType.Equals), row5.Cells[2]);
	}

	[TestMethod]
	public void GridFind()
	{
		CheckCellsFind(grid.Find(new GridSearchExpression("", SearchType.Contains)), grid.Rows[4].Cells[1]);
		CheckCellsFind(grid.Find(new GridSearchExpression("", SearchType.StartsWith)), grid.Rows[4].Cells[1]);
		CheckCellsFind(grid.Find(new GridSearchExpression("", SearchType.EndsWith)), grid.Rows[4].Cells[1]);
		CheckCellsFind(grid.Find(new GridSearchExpression("", SearchType.Equals)), grid.Rows[4].Cells[1]);
		CheckCellsFind(grid.Find(new GridSearchExpression(null, SearchType.Contains)), grid.Rows[4].Cells[2]);
		CheckCellsFind(grid.Find(new GridSearchExpression(null, SearchType.StartsWith)), grid.Rows[4].Cells[2]);
		CheckCellsFind(grid.Find(new GridSearchExpression(null, SearchType.EndsWith)), grid.Rows[4].Cells[2]);
		CheckCellsFind(grid.Find(new GridSearchExpression(null, SearchType.Equals)), grid.Rows[4].Cells[2]);

		CheckCellsFind(grid.Find(new GridSearchExpression("c1", SearchType.Contains)), grid.Rows[1].Cells[0], grid.Rows[3].Cells[0], grid.Rows[4].Cells[0]);
		CheckCellsFind(grid.Find(new GridSearchExpression("c1", SearchType.EndsWith)), grid.Rows[1].Cells[0], grid.Rows[3].Cells[0], grid.Rows[4].Cells[0]);
		CheckCellsFind(grid.Find(new GridSearchExpression("c1", SearchType.StartsWith)));
		CheckCellsFind(grid.Find(new GridSearchExpression("c1", SearchType.Equals)));

		CheckCellsFind(grid.Find(new GridSearchExpression("c1", SearchType.Contains, StringComparison.Ordinal)), grid.Rows[1].Cells[0], grid.Rows[3].Cells[0], grid.Rows[4].Cells[0]);
		CheckCellsFind(grid.Find(new GridSearchExpression("c1", SearchType.EndsWith, StringComparison.Ordinal)), grid.Rows[1].Cells[0], grid.Rows[3].Cells[0], grid.Rows[4].Cells[0]);
		CheckCellsFind(grid.Find(new GridSearchExpression("c1", SearchType.StartsWith, StringComparison.Ordinal)));
		CheckCellsFind(grid.Find(new GridSearchExpression("c1", SearchType.Equals, StringComparison.Ordinal)));
		CheckCellsFind(grid.Find(new GridSearchExpression("C1", SearchType.Contains, StringComparison.Ordinal)));
		CheckCellsFind(grid.Find(new GridSearchExpression("C1", SearchType.EndsWith, StringComparison.Ordinal)));
		CheckCellsFind(grid.Find(new GridSearchExpression("C1", SearchType.StartsWith, StringComparison.Ordinal)));
		CheckCellsFind(grid.Find(new GridSearchExpression("C1", SearchType.Equals, StringComparison.Ordinal)));
		CheckCellsFind(grid.Find(new GridSearchExpression("C1", SearchType.Contains, StringComparison.OrdinalIgnoreCase)), grid.Rows[1].Cells[0], grid.Rows[3].Cells[0], grid.Rows[4].Cells[0]);
		CheckCellsFind(grid.Find(new GridSearchExpression("C1", SearchType.EndsWith, StringComparison.OrdinalIgnoreCase)), grid.Rows[1].Cells[0], grid.Rows[3].Cells[0], grid.Rows[4].Cells[0]);
		CheckCellsFind(grid.Find(new GridSearchExpression("C1", SearchType.StartsWith, StringComparison.OrdinalIgnoreCase)));
		CheckCellsFind(grid.Find(new GridSearchExpression("C1", SearchType.Equals, StringComparison.OrdinalIgnoreCase)));

		CheckCellsFind(grid.Find(new GridSearchExpression("3", SearchType.Contains)), grid.Rows[0].Cells[2], grid.Rows[1].Cells[2], grid.Rows[2].Cells[0], grid.Rows[2].Cells[1], grid.Rows[2].Cells[2], grid.Rows[3].Cells[2]);
	}

	[TestMethod]
	public void GridSearchExpressionTests()
	{
		GridSearchExpression searchExpression = new();
		Assert.IsInstanceOfType(searchExpression, typeof(GridSearchExpression));
		Assert.IsNull(searchExpression.Text);
		Assert.AreEqual(SearchType.Equals, searchExpression.Type);
		Assert.AreEqual(StringComparison.OrdinalIgnoreCase, searchExpression.ComparisonType);
		searchExpression.Text = "foo";
		Assert.IsInstanceOfType(searchExpression, typeof(GridSearchExpression));
		Assert.AreEqual("foo",searchExpression.Text);
		Assert.AreEqual(SearchType.Equals, searchExpression.Type);
		Assert.AreEqual(StringComparison.OrdinalIgnoreCase, searchExpression.ComparisonType);
		searchExpression.Type = SearchType.Contains;
		Assert.IsInstanceOfType(searchExpression, typeof(GridSearchExpression));
		Assert.AreEqual("foo",searchExpression.Text);
		Assert.AreEqual(SearchType.Contains, searchExpression.Type);
		Assert.AreEqual(StringComparison.OrdinalIgnoreCase, searchExpression.ComparisonType);
		searchExpression.ComparisonType= StringComparison.Ordinal;
		Assert.IsInstanceOfType(searchExpression, typeof(GridSearchExpression));
		Assert.AreEqual("foo",searchExpression.Text);
		Assert.AreEqual(SearchType.Contains, searchExpression.Type);
		Assert.AreEqual(StringComparison.Ordinal, searchExpression.ComparisonType);

		searchExpression = new("bar", SearchType.StartsWith, StringComparison.CurrentCulture);
		Assert.IsInstanceOfType(searchExpression, typeof(GridSearchExpression));
		Assert.AreEqual("bar",searchExpression.Text);
		Assert.AreEqual(SearchType.StartsWith, searchExpression.Type);
		Assert.AreEqual(StringComparison.CurrentCulture, searchExpression.ComparisonType);
	}


	private static void CheckRowFind(GridRow row, GridSearchExpression searchExpression, params GridCell[] expectedCells)
	{
		Assert.IsNotNull(row);
		Assert.IsNotNull(expectedCells);

		int expectedCount = expectedCells.Count();

		//CELLS
		CheckCellsFind(row.Find(searchExpression), expectedCells);

		if (expectedCount > 0)
		{ Assert.AreEqual(expectedCells[0], row.FindFirst(searchExpression)); }
		else
		{ Assert.AreEqual(null, row.FindFirst(searchExpression)); }

		//COLUMNS
		CheckColumnsFind(row.FindColumns(searchExpression), expectedCells.Select(cell => cell.Column).ToArray());

		if (expectedCount > 0)
		{ Assert.AreEqual(expectedCells[0].Column, row.FindFirstColumn(searchExpression)); }
		else
		{ Assert.AreEqual(null, row.FindFirstColumn(searchExpression)); }
	}

	private static void CheckColumnFind(GridColumn col, GridSearchExpression searchExpression, params GridCell[] expectedCells)
	{
		Assert.IsNotNull(col);
		Assert.IsNotNull(expectedCells);

		int expectedCount = expectedCells.Count();

		//CELLS
		CheckCellsFind(col.Find(searchExpression), expectedCells);

		if (expectedCount > 0)
		{ Assert.AreEqual(expectedCells[0], col.FindFirst(searchExpression)); }
		else
		{ Assert.AreEqual(null, col.FindFirst(searchExpression)); }

		//ROWS
		CheckRowsFind(col.FindRows(searchExpression), expectedCells.Select(cell => cell.Row).ToArray());

		if (expectedCount > 0)
		{ Assert.AreEqual(expectedCells[0].Row, col.FindFirstRow(searchExpression)); }
		else
		{ Assert.AreEqual(null, col.FindFirstRow(searchExpression)); }
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
