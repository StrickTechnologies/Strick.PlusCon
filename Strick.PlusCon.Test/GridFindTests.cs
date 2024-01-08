using Strick.PlusCon.Models;


namespace Strick.PlusCon.Test;


[TestClass]
public class GridFindTests
{
	[TestMethod]
	public void ColumnSearch()
	{
		Grid g = new Grid();
		g.Columns.Add();
		GridColumn col1 = g.Columns.Last();
		g.Columns.Add();
		GridColumn col2 = g.Columns.Last();
		g.Columns.Add();
		GridColumn col3 = g.Columns.Last();

		g.AddRow("row1 col1", "row1 col2", "row1 col3");
		g.AddRow("row2 c1", "row2 c2", "row2 c3");
		g.AddRow("r3 col1", "r3 col2", "r3 col3");
		g.AddRow("r4 c1", "r4 c2", "r4 c3");
		g.AddRow("r5 c1", "", null);
		GridTests.CheckGridState(g, 3, 5);

		CheckColumnFind(col1, "foo", SearchType.Contains, 0);
		CheckColumnFind(col1, "foo", SearchType.StartsWith, 0);
		CheckColumnFind(col1, "foo", SearchType.EndsWith, 0);
		CheckColumnFind(col1, "foo", SearchType.Equals, 0);
		CheckColumnFind(col1, "c1", SearchType.Contains, 3, g.Rows[1].Cells[0], g.Rows[3].Cells[0], g.Rows[4].Cells[0]);
		CheckColumnFind(col1, "c1", SearchType.EndsWith, 3, g.Rows[1].Cells[0], g.Rows[3].Cells[0], g.Rows[4].Cells[0]);
		CheckColumnFind(col1, "c1", SearchType.Equals, 0);
		CheckColumnFind(col1, "c1", SearchType.StartsWith, 0);
		CheckColumnFind(col1, "col1", SearchType.Contains, 2, g.Rows[0].Cells[0], g.Rows[2].Cells[0]);
		CheckColumnFind(col1, "col1", SearchType.EndsWith, 2, g.Rows[0].Cells[0], g.Rows[2].Cells[0]);
		CheckColumnFind(col1, "col1", SearchType.StartsWith, 0);
		CheckColumnFind(col1, "col1", SearchType.Equals, 0);
		CheckColumnFind(col1, "col", SearchType.Contains, 2, g.Rows[0].Cells[0], g.Rows[2].Cells[0]);
		CheckColumnFind(col1, "col", SearchType.Equals, 0);
		CheckColumnFind(col1, "col", SearchType.StartsWith, 0);
		CheckColumnFind(col1, "col", SearchType.EndsWith, 0);
		CheckColumnFind(col2, "r4 c2", SearchType.Contains, 1, g.Rows[3].Cells[1]);
		CheckColumnFind(col2, "r4 c2", SearchType.Contains, 1, g.Rows[3].Cells[1]);
		CheckColumnFind(col2, "r4 c2", SearchType.Equals, 1, g.Rows[3].Cells[1]);
		CheckColumnFind(col2, "r4 c2", SearchType.StartsWith, 1, g.Rows[3].Cells[1]);
		CheckColumnFind(col2, "r4 c2", SearchType.EndsWith, 1, g.Rows[3].Cells[1]);
		CheckColumnFind(col3, "col3", SearchType.Contains, 2, g.Rows[0].Cells[2], g.Rows[2].Cells[2]);
		CheckColumnFind(col3, "col3", SearchType.EndsWith, 2, g.Rows[0].Cells[2], g.Rows[2].Cells[2]);
		CheckColumnFind(col3, "col3", SearchType.StartsWith, 0);
		CheckColumnFind(col3, "col3", SearchType.Equals, 0);
		CheckColumnFind(col3, "row", SearchType.Contains, 2, g.Rows[0].Cells[2], g.Rows[1].Cells[2]);
		CheckColumnFind(col3, "row", SearchType.StartsWith, 2, g.Rows[0].Cells[2], g.Rows[1].Cells[2]);
		CheckColumnFind(col3, "row", SearchType.EndsWith, 0);
		CheckColumnFind(col3, "row", SearchType.Equals, 0);

		CheckColumnFind(col3, "", SearchType.Contains, 0);
		CheckColumnFind(col2, "", SearchType.Contains, 1, g.Rows[4].Cells[1]);
		CheckColumnFind(col2, null, SearchType.Contains, 0);
		CheckColumnFind(col3, null, SearchType.Contains, 1, g.Rows[4].Cells[2]);
		CheckColumnFind(col3, "", SearchType.StartsWith, 0);
		CheckColumnFind(col2, "", SearchType.StartsWith, 1, g.Rows[4].Cells[1]);
		CheckColumnFind(col2, null, SearchType.StartsWith, 0);
		CheckColumnFind(col3, null, SearchType.StartsWith, 1, g.Rows[4].Cells[2]);
		CheckColumnFind(col3, "", SearchType.EndsWith, 0);
		CheckColumnFind(col2, "", SearchType.EndsWith, 1, g.Rows[4].Cells[1]);
		CheckColumnFind(col2, null, SearchType.EndsWith, 0);
		CheckColumnFind(col3, null, SearchType.EndsWith, 1, g.Rows[4].Cells[2]);
		CheckColumnFind(col3, "", SearchType.Equals, 0);
		CheckColumnFind(col2, "", SearchType.Equals, 1, g.Rows[4].Cells[1]);
		CheckColumnFind(col2, null, SearchType.Equals, 0);
		CheckColumnFind(col3, null, SearchType.Equals, 1, g.Rows[4].Cells[2]);
	}


	private static void CheckColumnFind(GridColumn col, string? searchText, SearchType searchType, int expectedCount, params GridCell[] expectedCells)
	{
		Assert.AreEqual(expectedCount, expectedCells.Count());

		//CELLS
		var results = col.Find(searchText, searchType);
		Assert.IsInstanceOfType(results, typeof(IEnumerable<GridCell>));
		Assert.AreEqual(expectedCount, results.Count());
		for (int i = 0; i < results.Count(); i++)
		{
			Assert.AreEqual(expectedCells[i], results.ElementAt(i));
		}

		if (expectedCount > 0)
		//{ CheckFindCellResults(col.FindFirst(searchText, searchType), expectedCells[0]); }
		{ Assert.AreEqual(expectedCells[0], col.FindFirst(searchText, searchType)); }
		else
		//{ CheckFindCellResults(col.FindFirst(searchText, searchType), null); }
		{ Assert.AreEqual(null, col.FindFirst(searchText, searchType)); }

		//ROWS
		var rowResults = col.FindRows(searchText, searchType);
		Assert.IsInstanceOfType(rowResults, typeof(IEnumerable<GridRow>));
		Assert.AreEqual(expectedCount, rowResults.Count());
		for (int i = 0; i < rowResults.Count(); i++)
		{
			Assert.AreEqual(expectedCells[i].Row, rowResults.ElementAt(i));
		}

		if (expectedCount > 0)
		//{ CheckFindRowResults(col.FindFirstRow(searchText, searchType), expectedCells[0].Row); }
		{ Assert.AreEqual(expectedCells[0].Row, col.FindFirstRow(searchText, searchType)); }
		else
		{ Assert.AreEqual(null, col.FindFirstRow(searchText, searchType)); }
	}
}
